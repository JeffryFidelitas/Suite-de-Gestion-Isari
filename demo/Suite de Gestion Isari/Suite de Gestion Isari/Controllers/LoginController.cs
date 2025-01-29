using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Suite_de_Gestion_Isari.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginController
        public ActionResult Login()
        {
            return View();
        }

        // GET: LoginController/Details/5
        public ActionResult OlvideContraseña()
        {
            return View();
        }

        
    }
}
