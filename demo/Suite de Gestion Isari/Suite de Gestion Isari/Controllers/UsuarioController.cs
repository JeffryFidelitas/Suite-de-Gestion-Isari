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
        [HttpGet]
        public IActionResult ActualizarPerfil()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ActualizarPerfil(Empleado model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string mensaje;
            var resultado = _usuario.ActualizarPerfil(model, out mensaje);

            if (resultado)
            {
                TempData["SuccessMessage"] = mensaje;
                return RedirectToAction("ConsultarEmpleados");
            }
            else
            {
                ViewBag.ErrorMessage = mensaje;
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult MiPerfil()
        {
            // Simulando obtención del ID del empleado (usando un valor fijo)
            var idEmpleado = "1"; // Simulamos un ID de empleado (reemplazar por uno real si se desea)

            string mensaje;
            var empleado = _usuario.ObtenerPerfil(idEmpleado, out mensaje);

            if (empleado == null)
            {
                ViewBag.ErrorMessage = mensaje;
                return View();
            }

            return View(empleado);
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
