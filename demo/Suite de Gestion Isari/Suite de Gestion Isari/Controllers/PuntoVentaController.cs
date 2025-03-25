using Microsoft.AspNetCore.Mvc;
using Suite_de_Gestion_Isari.Entidades;
using Suite_de_Gestion_Isari.Models;

public class PuntoVentaController : Controller
{
        private readonly PuntoVentaModel _puntoVentaModel;
        private readonly DevolucionModel _devolucion;


        public PuntoVentaController(IConfiguration configuration)
        {
            _devolucion = new DevolucionModel(configuration);
            _puntoVentaModel = new PuntoVentaModel(configuration);
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

            
            var detallesVenta = _puntoVentaModel.ObtenerDetalleVentaTemporal(usuarioID);
            var cantidadArticulos = detallesVenta.Sum(d => d.cantidad);

            var montoTotal = _puntoVentaModel.ObtenerMontoTotalVentaTemporal(usuarioID);

            ViewBag.MontoTotal = montoTotal;
            ViewBag.MontoCantidadArticulos = cantidadArticulos;
           
            return View(detallesVenta);
        }

        [HttpPost]
        public IActionResult RegistroVenta(string codigoBarras)
        {
            // Obtener el producto por código de barras
            var producto = _puntoVentaModel.ObtenerProductoPorCodigoBarras(codigoBarras);

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
                var resultado = _puntoVentaModel.AgregarVentaTemporal(venta);
                

                if (resultado)
                {
                    var detallesVenta = _puntoVentaModel.ObtenerDetalleVentaTemporal(venta.Consecutivo);
                    var montoTotal = _puntoVentaModel.ObtenerMontoTotalVentaTemporal(venta.Consecutivo);
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
                var detallesVenta = _puntoVentaModel.ObtenerDetalleVentaTemporal(usuarioID);
                var cantidadArticulos = detallesVenta.Sum(d => d.cantidad);
                var montoTotal = _puntoVentaModel.ObtenerMontoTotalVentaTemporal(usuarioID);
                ViewBag.MontoTotal = montoTotal;
                ViewBag.MontoCantidadArticulos = cantidadArticulos;
                return PartialView("_DetalleVenta", detallesVenta); 

            }
        return View();
      }

    // Acción para consultar el historial de pagos
    public IActionResult ConsultarHistorialPagos(long consecutivoFactura)
    {
        try
        {
            // Consultamos el historial de pagos para la factura específica
            var pagos = _puntoVentaModel.ObtenerHistorialPagos(consecutivoFactura);

            if (pagos.Any())
            {
                return View(pagos);
            }
            else
            {
                ViewBag.MensajeError = "No se encontraron pagos para esta factura.";
                return View(new List<DetallePago>());
            }
        }
        catch (Exception ex)
        {
            ViewBag.MensajeError = $"Error al consultar historial de pagos: {ex.Message}";
            return View(new List<DetallePago>());
        }
    }

    // Resto de las acciones existentes...

    public IActionResult ConsultarDetalleFactura(long consecutivo)
    {
        var detalle = _puntoVentaModel.ConsultarDetalleFactura(consecutivo);
        return View(detalle.Contenido);
    }

    public IActionResult HistorialVentas()
    {
        if (HttpContext.Session.GetString("UsuarioID") == null)
            return View();
        var usuarioID = int.Parse(HttpContext.Session.GetString("UsuarioID")!);
        var results = _puntoVentaModel.ConsultarFacturas(usuarioID);
        return View(results.Contenido);
    }
}
