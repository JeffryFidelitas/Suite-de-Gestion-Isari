using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Suite_de_Gestion_Isari.Controllers
{
    public class PuntoVentaController : Controller
    {
        public ActionResult RegistroVenta()
        {
            return View();
        }

        public ActionResult HistorialVentas()
        {
            return View();
        }

        public ActionResult RegistroDevolucion()
        {
            return View();
        }
    }
}
