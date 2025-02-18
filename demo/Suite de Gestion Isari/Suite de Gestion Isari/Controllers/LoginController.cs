using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suite_de_Gestion_Isari.Models;
using Suite_de_Gestion_Isari.Entidades;


namespace Suite_de_Gestion_Isari.Controllers
{
    public class LoginController : Controller
    {

        private readonly LoginModel _login;

        public LoginController(IConfiguration configuration)
        {
            _login = new LoginModel(configuration);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult IniciarSesion(Empleado model)
        {
            if (model == null || string.IsNullOrEmpty(model.EMAIL) || string.IsNullOrEmpty(model.CONTRASENA))
            {
                ViewBag.Error = "Por favor ingrese el correo electrónico y la contraseña.";
                return View("Login"); 
            }

            
            var empleado = _login.IniciarSesion(model);

            if (!string.IsNullOrEmpty(empleado.EMAIL))
            {
                
                  HttpContext.Session.SetString("UsuarioID", empleado!.ID_EMPLEADO.ToString()); 
                  HttpContext.Session.SetString("UsuarioNombre", empleado.NOMBRE);
                  HttpContext.Session.SetString("IdRol", empleado.ID_ROL.ToString());


                return RedirectToAction("Index", "Home");
            }

            
            ViewBag.Error = "Correo electrónico o contraseña incorrectos.";
            return View("Login");
        }


        public IActionResult CerrarSesion()
        {
            
            HttpContext.Session.Clear();

            
            return RedirectToAction("Login", "Login");
        }


        public ActionResult OlvideContraseña()
        {
            return View();
        }

        
    }
}

