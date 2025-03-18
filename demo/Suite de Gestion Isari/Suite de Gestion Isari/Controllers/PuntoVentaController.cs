using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;
using Suite_de_Gestion_Isari.Entidades;
using System.Net.Http.Headers;
using Suite_de_Gestion_Isari.Models;
using System.Reflection;
using System.Text.Json;

namespace Suite_de_Gestion_Isari.Controllers
{
    public class PuntoVentaController : Controller
    {

        private readonly PuntoVentaModel _venta;
        private readonly PuntoVentaModel _productosService;
        private readonly DevolucionModel _devolucion;


        public PuntoVentaController(IConfiguration configuration)
        {
            _venta = new PuntoVentaModel(configuration);
            _productosService = new PuntoVentaModel(configuration);
            _devolucion = new DevolucionModel(configuration);
        }
       
        public ActionResult RegistroDevolucion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistroDevolucion(Devolucion devolucion)
        {
            //if (devolucion == null)
            var res = _devolucion.AgregarDevolucion(devolucion);
            if (res.Codigo > 0)
            {
                ViewBag.ErrorMessage = res.Mensaje;
                return View(); // TODO: ListarDevoluycionsesadas
            }
            else
            {
                ViewBag.ErrorMessage = res.Mensaje;
                return View(devolucion);
            }
        }

        public ActionResult ConsultaDevoluciones()
        {
            return View(_devolucion.ObtenerDevoluciones().ToList());
        }
        
        [HttpGet]
        public ActionResult RegistroVenta()
        {
            // Recupera el ID del usuario desde la sesión
            var usuarioID = int.Parse(HttpContext.Session.GetString("UsuarioID")!);

            
            var detallesVenta = _venta.ObtenerDetalleVentaTemporal(usuarioID);
            var cantidadArticulos = detallesVenta.Sum(d => d.cantidad);

            var montoTotal = _venta.ObtenerMontoTotalVentaTemporal(usuarioID);

            ViewBag.MontoTotal = montoTotal;
            ViewBag.MontoCantidadArticulos = cantidadArticulos;
           
            return View(detallesVenta);
        }

        [HttpPost]
        public IActionResult RegistroVenta(string codigoBarras)
        {
            // Obtener el producto por código de barras
            var producto = _productosService.ObtenerProductoPorCodigoBarras(codigoBarras);

            if (producto.ID_PRODUCTO != 0)
            {
                // Crear objeto para insertar en la tabla temporal
                var venta = new Venta
                {
                    Consecutivo = int.Parse(HttpContext.Session.GetString("UsuarioID")!),
                    ID_PRODUCTO = producto.ID_PRODUCTO,
                    cantidad = 1, // Por defecto agrega 
                    CODIGO_PRODUCTO=producto.CODIGO_PRODUCTO,                 
                    NOMBRE=producto.NOMBRE,
                    Precio=producto.Precio,
                    DESCRIPCION = producto.DESCRIPCION
                };

                // Agregar a la tabla temporal
                var resultado = _venta.AgregarVentaTemporal(venta);
                

                if (resultado)
                {
                    var detallesVenta = _venta.ObtenerDetalleVentaTemporal(venta.Consecutivo);
                    var montoTotal = _venta.ObtenerMontoTotalVentaTemporal(venta.Consecutivo);
                    var cantidadArticulos = detallesVenta.Sum(d => d.cantidad);
                    ViewBag.MontoTotal = montoTotal;
                    ViewBag.MontoCantidadArticulos = cantidadArticulos;
                    return PartialView("_DetalleVenta", detallesVenta); 
                }


            }
            else
            {

                ViewBag.MensajeError = "Producto no encontrado. Por favor, verifique el código de barras.";
                var usuarioID = int.Parse(HttpContext.Session.GetString("UsuarioID")!);
                var detallesVenta = _venta.ObtenerDetalleVentaTemporal(usuarioID);
                var cantidadArticulos = detallesVenta.Sum(d => d.cantidad);
                var montoTotal = _venta.ObtenerMontoTotalVentaTemporal(usuarioID);
                ViewBag.MontoTotal = montoTotal;
                ViewBag.MontoCantidadArticulos = cantidadArticulos;
                return PartialView("_DetalleVenta", detallesVenta); 

            }

            return Json(new { exito = false, mensaje = "Producto no encontrado o no se pudo agregar." });
        }

        [HttpPost]
        public IActionResult Registrarventa()
        {
            var usuarioID = int.Parse(HttpContext.Session.GetString("UsuarioID")!);

            // Verificar si hay productos en la venta temporal para el usuario
            var hayProductos = _venta.HayProductosEnVenta(usuarioID);
            if (!hayProductos)
            {
                TempData["ErrorMessage"] = "No hay productos en la venta. Agregue productos antes de finalizar.";
                return RedirectToAction("RegistroVenta");
            }

            // Registrar la venta
            var resultado = _venta.Registrarventa(usuarioID);
            if (resultado)
            {
                TempData["SuccessMessage"] = "Venta registrada exitosamente.";
                return RedirectToAction("RegistroVenta");
            }
            else
            {
                TempData["ErrorMessage"] = "No se pudo registrar la venta. Verifique el inventario y vuelva a intentarlo.";
                return RedirectToAction("RegistroVenta");
            }
        }

        [HttpGet]
        public IActionResult HistorialVentas()
        {
            var usuarioID = int.Parse(HttpContext.Session.GetString("UsuarioID")!);

            
            var respuesta = _venta.ConsultarFacturas(usuarioID);

           
            if (respuesta.Codigo == 0)
            {
                
                return View(respuesta.Contenido);
            }
            else
            {
               
                ViewBag.MensajeError = respuesta.Mensaje;
                return View(new List<Venta>());
            }
        }


        [HttpGet]
        public IActionResult ConsultarDetalleFactura(long consecutivo)
        {

            var respuesta = _venta.ConsultarDetalleFactura(consecutivo);


            if (respuesta.Codigo == 0)
            {
                
                return View(respuesta.Contenido);
            }
            else
            {
                
                ViewBag.MensajeError = respuesta.Mensaje;
                return View(new List<Venta>());
            }

        }

    }
}

    
