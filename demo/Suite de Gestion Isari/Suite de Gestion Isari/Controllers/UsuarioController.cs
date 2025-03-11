using Microsoft.AspNetCore.Mvc;
using Suite_de_Gestion_Isari.Models;
using Suite_de_Gestion_Isari.Entidades;

namespace Suite_de_Gestion_Isari.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioModel _usuarioModel;

        public UsuarioController(IConfiguration configuration)
        {
            _usuarioModel = new UsuarioModel(configuration);
        }

        public IActionResult Index()
        {
            var empleados = _usuarioModel.ConsultarEmpleados();
            return View(empleados);
        }

        public IActionResult Detalles(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var empleado = _usuarioModel.ObtenerUsuarioPorID(id);
            if (empleado == null)
                return NotFound();

            return View(empleado);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Empleado model)
        {
            if (ModelState.IsValid)
            {
                var respuesta = _usuarioModel.AgregarEmpleado(model);
                if (respuesta.Codigo == 0)
                    return RedirectToAction("Index");

                ModelState.AddModelError(string.Empty, respuesta.Mensaje);
            }

            return View(model);
        }

        public IActionResult Editar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var empleado = _usuarioModel.ObtenerUsuarioPorID(id);
            if (empleado == null)
                return NotFound();

            return View(empleado);
        }

        [HttpPost]
        public IActionResult Editar(Empleado model)
        {
            if (ModelState.IsValid)
            {
                _usuarioModel.ActualizarEmpleado(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult ObtenerUsuarioLogueado(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("El ID del usuario no puede estar vacío.");

            var usuario = _usuarioModel.ObtenerUsuarioLogueado(id);
            if (usuario == null)
                return NotFound();

            return View(usuario);
        }
    }
}
