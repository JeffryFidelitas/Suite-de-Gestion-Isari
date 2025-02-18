using Microsoft.AspNetCore.Mvc;
using Suite_de_Gestion_Isari.Entidades;
using Suite_de_Gestion_Isari.Models;

namespace Suite_de_Gestion_Isari.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly UsuarioModel _usuario;
        private readonly PuestoModel _puestoModel;

        public UsuarioController(IConfiguration configuration)
        {
            _usuario = new UsuarioModel(configuration);
            _puestoModel = new PuestoModel(configuration);
        }



        [HttpGet]
        public IActionResult Agregar_Empleado()

        {
            CargarDatosCompartidos();
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




        public IActionResult CambioContraseña()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var usuario = _usuario.ObtenerUsuarioPorID(id);

            if (usuario == null)
            {
                return RedirectToAction("ConsultarEmpledos");
            }

            CargarDatosCompartidos();


            return View(usuario);
        }

        [HttpPost]
        public IActionResult Editar(Empleado model)
        {
            if (ModelState.IsValid)
            {
                _usuario.ActualizarEmpleado(model);
                return RedirectToAction("ConsultarEmpleados");
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult ActualizarPerfil()
        {
            string usuarioID = HttpContext.Session.GetString("UsuarioID");
            if (string.IsNullOrEmpty(usuarioID))
            {
                return RedirectToAction("Login", "Login");
            }
            
            var usuario = _usuario.ObtenerUsuariologueado(int.Parse(usuarioID));

            

            return View(usuario);
        }


        [HttpPost]
        public IActionResult ActualizarPerfil(Empleado model)
        {
            if (ModelState.IsValid)
            {
                // Obtener el ID del usuario desde la sesión como string
                string usuarioID = HttpContext.Session.GetString("UsuarioID");

                if (!string.IsNullOrEmpty(usuarioID))
                {
                    model.ID_EMPLEADO = usuarioID;                   
                    _usuario.ActualizarUsuario(model);
                    ViewBag.Mensaje = "Perfil actualizado correctamente";
                }
                else
                {
                    ViewBag.Error = "No se ha encontrado un ID de usuario válido en la sesión.";
                }
            }
            else
            {
                ViewBag.Error = "Error al actualizar el perfil.";
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult MiPerfil()
        {
            string usuarioID = HttpContext.Session.GetString("UsuarioID");
            if (string.IsNullOrEmpty(usuarioID))
            {
                return RedirectToAction("Login", "Login");
            }

            var usuario = _usuario.ObtenerUsuarioPorID(int.Parse(usuarioID));

            return View(usuario);
        }

        public IActionResult SolicitarVacaciones()
        {
            return View();
        }

        public IActionResult RegistrarTardia()
        {
            return View();
        }


        private void CargarDatosCompartidos()
        {
            var listaPuestos = _puestoModel.ObtenerPuestos();
            ViewBag.Puestos = listaPuestos;

            var listarRoles = _usuario.ObtenerRoles();
            ViewBag.Roles = listarRoles;
        }

    }
}
