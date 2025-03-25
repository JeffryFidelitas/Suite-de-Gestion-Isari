using Microsoft.AspNetCore.Mvc;
using Suite_de_Gestion_Isari.Entidades;
using Suite_de_Gestion_Isari.Models;

public class PuntoVentaController : Controller
{
    private readonly PuntoVentaModel _puntoVentaModel;

    public PuntoVentaController(PuntoVentaModel puntoVentaModel)
    {
        _puntoVentaModel = puntoVentaModel;
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
}
