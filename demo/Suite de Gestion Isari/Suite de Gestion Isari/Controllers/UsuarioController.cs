using Microsoft.AspNetCore.Mvc;
using Suite_de_Gestion_Isari.Entidades;
using Suite_de_Gestion_Isari.Models;

namespace Suite_de_Gestion_Isari.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly UsuarioModel _usuario;

        public UsuarioController(IConfiguration configuration)
        {
            _usuario = new UsuarioModel(configuration);
        }

        [HttpGet]
        public IActionResult Agregar_Empleado()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Agregar_Empleado(Empleado model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            var respuesta = _usuario.AgregarEmpleado(model);

            if (respuesta.Codigo == 0)
            {

                TempData["SuccessMessage"] = respuesta.Mensaje;
                return RedirectToAction("ConsultarEmpleados");
            }
            else
            {

                ViewBag.ErrorMessage = respuesta.Mensaje;
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ConsultarEmpleados()
        {
            var empleados = _usuario.ConsultarEmpleados();
            return View(empleados);
        }


        public IActionResult Editar()
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
        
        public IActionResult SolicitarVacaciones()
        {
            return View();
        }

        public IActionResult RegistrarTardia()
        {
            return View();
        }

    }
}
