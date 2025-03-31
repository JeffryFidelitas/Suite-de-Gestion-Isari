using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;
using Suite_de_Gestion_Isari.Entidades;
using System.Net.Http.Headers;
using Suite_de_Gestion_Isari.Models;
using System.Reflection;
using System.Text.Json;
using System;

namespace Suite_de_Gestion_Isari.Controllers
{
    public class PuntoVentaController : Controller
    {

        private readonly PuntoVentaModel _venta;
        private readonly PuntoVentaModel _productosService;


        public PuntoVentaController(IConfiguration configuration, IHostEnvironment environment)
        {
            _venta = new PuntoVentaModel(configuration, environment);
            _productosService = new PuntoVentaModel(configuration, environment);


        }

        public ActionResult RegistroDevolucion()
        {
            return View();
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

        //// Acción para consultar el historial de pagos
        //public IActionResult ConsultarHistorialPagos(long consecutivoFactura)
        //{
        //    try
        //    {
        //        // Consultamos el historial de pagos para la factura específica
        //        var pagos = _puntoVentaModel.ObtenerHistorialPagos(consecutivoFactura);

        //        if (pagos.Any())
        //        {
        //            return View(pagos);
        //        }
        //        else
        //        {
        //            ViewBag.MensajeError = "No se encontraron pagos para esta factura.";
        //            return View(new List<DetallePago>());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.MensajeError = $"Error al consultar historial de pagos: {ex.Message}";
        //        return View(new List<DetallePago>());
        //    }
        //}


        [HttpPost]
        public IActionResult Registrarventa( string? correoCliente = null, string? nombreCliente = null)
        {
            var usuarioID = int.Parse(HttpContext.Session.GetString("UsuarioID")!);
            var enviarFactura = !string.IsNullOrWhiteSpace(correoCliente) || !string.IsNullOrWhiteSpace(nombreCliente);


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
                // Si se solicita el envío de la factura
                if (enviarFactura)
                {
                    var idFactura = _venta.ObtenerIdUltimaVenta(usuarioID); // Método para obtener el ID de la última factura
                    var detallesFactura = _venta.ObtenerDetallesFactura(idFactura);

                    var resultadoEnvio = _venta.EnvioFactura(correoCliente, nombreCliente, detallesFactura);

                    if (resultadoEnvio.Codigo == 0)
                    {
                        TempData["SuccessMessage"] = "Venta registrada y factura enviada exitosamente.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"La venta se registró, pero hubo un problema al enviar la factura: {resultadoEnvio.Mensaje}";
                    }
                }
                else
                {
                    TempData["SuccessMessage"] = "Venta registrada exitosamente.";
                }

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

            public IActionResult EliminarArticulo(long ID_VentaTemporal)
        {
            try
            {
                var resultado = _venta.EliminarArticuloTemporal(ID_VentaTemporal);

                if (resultado)
                {
                    

                    TempData["SuccessMessage"] = "Artículo eliminado correctamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "No se pudo eliminar el artículo.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
            }

            
            return RedirectToAction("RegistroVenta");
        }

        
        [HttpGet]
        public IActionResult HistorialVentaDia()
        {
            var usuarioID = int.Parse(HttpContext.Session.GetString("UsuarioID")!);


            var respuesta = _venta.ConsultarFacturasHoy(usuarioID);


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
        public IActionResult HistorialVentaDiaAdmin()
        {
            


            var respuesta = _venta.ConsultarFacturasHoyAdmin();


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


    
