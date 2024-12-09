using Microsoft.AspNetCore.Mvc;

namespace Suite_de_Gestion_Isari.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Registro()
        {
            return View();
        }
        

        public IActionResult ConsultaUsuario()
        {
            return View();
        }


        public IActionResult EditarUsuario()
        {
            return View();
        }

        public IActionResult CambioContraseña()
        {
            return View();
        }
        public IActionResult ActualizarPerfil()
        {
            return View();
        }
        


    }
}
