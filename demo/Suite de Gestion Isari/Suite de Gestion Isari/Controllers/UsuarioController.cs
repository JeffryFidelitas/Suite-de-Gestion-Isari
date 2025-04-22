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



        [HttpGet]
        public IActionResult CambioContraseña()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CambioContraseña(string nuevaContraseña, string confirmarNuevaContraseña)
        {
            if (nuevaContraseña != confirmarNuevaContraseña)
            {
                ViewBag.ErrorMessage = "Las contraseñas no coinciden. Inténtelo de nuevo.";
                return View();
            }

            string usuarioID = HttpContext.Session.GetString("UsuarioID");
            _usuario.CambiarContrasena(usuarioID, nuevaContraseña);

            TempData["SuccessMessage"] = "La contraseña se actualizó correctamente.";
            return RedirectToAction("CerrarSesion", "Login");
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

                    TempData["Mensaje"] = "Perfil actualizado correctamente"; // Usar TempData para persistir datos entre redirecciones
                    return RedirectToAction(nameof(ActualizarPerfil)); // Redirigir a la misma acción
                    //ViewBag.Mensaje = "Perfil actualizado correctamente";

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

        [HttpGet]
        public IActionResult SolicitarVacaciones()
        {
            return View();
        }


        public IActionResult SolicitarVacaciones(SolicitudVacaciones model)
        {
            string  usuario = HttpContext.Session.GetString("UsuarioID");

            int usuarioID = 0; // Valor por defecto en caso de error o si la sesión no existe
            if (!string.IsNullOrEmpty(usuario))
            {
                usuarioID = int.Parse(usuario); // Convertir el string a int
            }
            // Obtener el usuario (empleado) desde la base de datos
            var empleado = _usuario.ObtenerUsuarioPorID(usuarioID);

            if (empleado == null)
            {
                // Manejo de error si no se encuentra el empleado
                return NotFound();
            }

            var fechaContratacion = empleado.FECHA_CONTRATACION;
            var diasTrabajados = (DateTime.Now - fechaContratacion).Days;
            var diasDisponibles = diasTrabajados / 30; // Un día de vacaciones por cada 30 días trabajados

            // Crear el modelo de SolicitudVacaciones y asignar los días disponibles
            var modelo = new SolicitudVacaciones
            {
                ID_EMPLEADO = usuarioID,
                DIAS_SOLICITADOS = model.DIAS_SOLICITADOS,
                ESTADO = model.ESTADO,
                FECHA_FIN = model.FECHA_FIN,
                FECHA_INICIO = model.FECHA_INICIO,
                DIAS_TOTALES = diasDisponibles,
                MOTIVO = model.MOTIVO
            };

            var respuesta = _usuario.AgregarSolicitudVacaciones(modelo);

            if (respuesta.Codigo == 0)
            {
                TempData["SuccessMessage"] = respuesta.Mensaje;

                return RedirectToAction("VerSolicitudesVacacionesUsuario");
            }
            else
            {
                ViewBag.ErrorMessage = respuesta.Mensaje;
                return View(model);
            }
        }


        [HttpGet]
        public IActionResult VerSolicitudesVacaciones()
        {
            var solicitudes = _usuario.ObtenerSolicitudesVacaciones();
            var solicitudesOrdenadas = solicitudes.OrderByDescending(s => s.ESTADO == "Pendiente").ToList();
            return View(solicitudesOrdenadas); 
        }


        [HttpGet]
        public IActionResult VerSolicitudesVacacionesUsuario()
        {

            string usuario = HttpContext.Session.GetString("UsuarioID");

            int ID_EMPLEADO = 0; // Valor por defecto en caso de error o si la sesión no existe
            if (!string.IsNullOrEmpty(usuario))
            {
                ID_EMPLEADO = int.Parse(usuario); // Convertir el string a int
            }

            var solicitudes = _usuario.ObtenerSolicitudesVacacionesUsuario(ID_EMPLEADO);
            var solicitudesOrdenadas = solicitudes.OrderByDescending(s => s.ESTADO == "Pendiente").ToList();
            return View(solicitudesOrdenadas);
        }


        [HttpPost]
        public IActionResult CambiarEstadoSolicitud(int idSolicitud, string estado)
        {
            var respuesta = _usuario.ActualizarEstadoSolicitud(idSolicitud, estado);
        
            if (respuesta.Codigo == 0)
            {
                TempData["SuccessMessage"] = respuesta.Mensaje;
            }
            else
            {
                ViewBag.ErrorMessage = respuesta.Mensaje;
            }
        
            return RedirectToAction("VerSolicitudesVacaciones");
        }


         [HttpGet]
         public IActionResult RegistrarTardia()
         {
             return View();
         }
        
         [HttpPost]
         public IActionResult RegistrarTardia(Horario model)
         {
             if (!ModelState.IsValid)
             {
                 return View(model);
             }
        
             try
             {
                 var respuesta = _usuario.AgregarHorario(model);
        
                 if (respuesta.Codigo == 0)
                 {
                     TempData["SuccessMessage"] = "Horario registrado correctamente.";
                     return RedirectToAction("VerHorarios");
                 }
                 else
                 {
                     ViewBag.ErrorMessage = "Hubo un problema al registrar el horario: " + respuesta.Mensaje;
                     return View(model);
                 }
             }
             catch (Exception ex)
             {
                 ViewBag.ErrorMessage = "Error al procesar la solicitud: " + ex.Message;
                 return View(model);
             }
         }
        
        
        
         [HttpGet]
         public IActionResult ObtenerEmpleados()
         {
             var empleados = _usuario.ObtenerListaEmpleados();
             return Json(empleados);
         }
        
        [HttpPost]
        public IActionResult CambiarEstadoHorario(int idHorario, string estado)
        {
            try
            {
                var respuesta = _usuario.ActualizarEstadoHorario(idHorario, estado);
        
                if (respuesta.Codigo == 0)
                {
                    TempData["SuccessMessage"] = "Estado actualizado correctamente.";
                }
                else
                {
                    ViewBag.ErrorMessage = "No se pudo actualizar el estado.";
                }
        
                return RedirectToAction("VerHorarios");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al actualizar el estado: " + ex.Message;
                return RedirectToAction("VerHorarios");
            }
        }

        

        [HttpGet]
        public IActionResult VerHorarios()
        {
            var horarios = _usuario.ObtenerHorarios();
            return View(horarios);
        }

        [HttpGet]
        public IActionResult VerHorariosUsuario()
        {
            var horarios = _usuario.ObtenerHorarios();
            return View(horarios);
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
