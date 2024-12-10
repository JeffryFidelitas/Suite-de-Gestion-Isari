using Microsoft.AspNetCore.Mvc;

namespace Suite_de_Gestion_Isari.Controllers
{
    public class ReporteriaController : Controller
    {
        // Acción para mostrar el reporte de ventas diarias
        public IActionResult ReporteVentasDiarias()
        {
            return View();
        }

        // Acción para mostrar los productos más vendidos
        public IActionResult ProductosMasVendidos()
        {
            return View();
        }

        // Acción para mostrar el resumen de transacciones
        public IActionResult ResumenTransacciones()
        {
            return View();
        }

        // Acción para mostrar el reporte de inventario
        public IActionResult ReporteInventario()
        {
            return View();
        }

        // Acción para mostrar los productos próximos a vencer
        public IActionResult ProductosProximosAVencer()
        {
            return View();
        }

        // Acción para mostrar el reporte de ganancias y pérdidas
        public IActionResult ReporteGananciasYPérdidas()
        {
            return View();
        }

        // Acción para mostrar el rendimiento de los empleados
        public IActionResult ReporteRendimientoEmpleados()
        {
            return View();
        }
    }
}
