using Microsoft.AspNetCore.Mvc;

namespace Suite_de_Gestion_Isari.Controllers
{
    public class ContabilidadController : Controller
    {
        public IActionResult ReportesFinancieros()
        {
            return View();
        }

        public IActionResult RegistroCuentas()
        {
            return View();
        }

        public IActionResult ResumenDiario()
        {
            return View();
        }

        public IActionResult Caja()
        {
            return View();
        }
    }
}
