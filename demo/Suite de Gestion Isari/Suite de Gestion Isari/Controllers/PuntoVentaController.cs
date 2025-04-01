using Microsoft.AspNetCore.Mvc;
using Suite_de_Gestion_Isari.Entidades;
using Suite_de_Gestion_Isari.Models;
using System.Reflection;
using System.Text.Json;
using System;

public class PuntoVentaController : Controller
{
        private readonly PuntoVentaModel _puntoVentaModel;
        private readonly DevolucionModel _devolucion;

        public PuntoVentaController(IConfiguration configuration, IHostEnvironment environment)
        {
            _devolucion = new DevolucionModel(configuration);
            _puntoVentaModel = new PuntoVentaModel(configuration, environment);
        }

        [HttpPost]
        public ActionResult RegistroDevolucion(Devolucion devolucion)
        {
            //if (devolucion == null)
            var res = _devolucion.AgregarDevolucion(devolucion);
            if (res.Codigo > 0)
            {
                ViewBag.ErrorMessage = res.Mensaje;
                return RedirectToAction("ConsultaDevoluciones");
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
            var hayProductos = _puntoVentaModel.HayProductosEnVenta(usuarioID);
            if (!hayProductos)
            {
                TempData["ErrorMessage"] = "No hay productos en la venta. Agregue productos antes de finalizar.";
                return RedirectToAction("RegistroVenta");
            }

            // Registrar la venta
            var resultado = _puntoVentaModel.Registrarventa(usuarioID);
            if (resultado)
            {
                // Si se solicita el envío de la factura
                if (enviarFactura)
                {
                    var idFactura = _puntoVentaModel.ObtenerIdUltimaVenta(usuarioID); // Método para obtener el ID de la última factura
                    var detallesFactura = _puntoVentaModel.ObtenerDetallesFactura(idFactura);

                    var resultadoEnvio = _puntoVentaModel.EnvioFactura(correoCliente, nombreCliente, detallesFactura);

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

            
            var respuesta = _puntoVentaModel.ConsultarFacturas(usuarioID);

           
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

            var respuesta = _puntoVentaModel.ConsultarDetalleFactura(consecutivo);


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
                var resultado = _puntoVentaModel.EliminarArticuloTemporal(ID_VentaTemporal);

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


            var respuesta = _puntoVentaModel.ConsultarFacturasHoy(usuarioID);


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
            


            var respuesta = _puntoVentaModel.ConsultarFacturasHoyAdmin();


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

