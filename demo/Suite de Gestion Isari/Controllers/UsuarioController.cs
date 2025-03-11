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

        [HttpGet]
        public IActionResult SolicitarVacaciones()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult SolicitarVacaciones(SolicitudVacaciones model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
        
            var respuesta = _usuario.AgregarSolicitudVacaciones(model);
        
            if (respuesta.Codigo == 0)
            {
                TempData["SuccessMessage"] = respuesta.Mensaje;
                return RedirectToAction("VerSolicitudesVacaciones");
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


        public Respuesta AgregarHorario(Horario model)
        {
            try
            {
                using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
                {
                    context.Open();
        
                    var result = context.Execute(
                        "dbo.RegistrarHorario", 
                        new
                        {
                            model.ID_EMPLEADO,
                            model.DIA_SEMANA,
                            model.HORA_ENTRADA,
                            model.HORA_SALIDA
                        },
                        commandType: CommandType.StoredProcedure
                    );
        
                    if (result > 0)
                    {
                        return new Respuesta { Codigo = 0 };
                    }
                    else
                    {
                        return new Respuesta { Codigo = -1 };
                    }
                }
            }
            catch (Exception ex)
            {
                return new Respuesta
                {
                    Codigo = -1,
                    Mensaje = "Error al registrar la solicitud: " + ex.Message  
                };
            }
        }
        
        
        public Respuesta ActualizarEstadoHorario(int idHorario, string estado)
        {
            try
            {
                using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
                {
                    var respuesta = new Respuesta();
        
                    context.Open();
        
                    var result = context.Execute(
                        "dbo.ActualizarEstadoHorario",
                        new
                        {
                            ID_HORARIO = idHorario,
                            ESTADO = estado
                        },
                        commandType: CommandType.StoredProcedure
                    );
        
                    if (result > 0)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Mensaje = "Estado actualizado correctamente.";
                    }
                    else
                    {
                        respuesta.Codigo = -1;
                        respuesta.Mensaje = "No se pudo actualizar el estado.";
                    }
        
                    return respuesta;
                }
            }
            catch (Exception ex)
            {
                return new Respuesta
                {
                    Codigo = -1,
                    Mensaje = "Error al actualizar el estado: " + ex.Message
                };
            }
        }
        
        public List<Horario> ObtenerHorarios()
        {
            try
            {
                using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
                {
                    var horarios = context.Query<Horario>(
                        "SELECT * FROM SuiteGestionIsari.dbo.T_HORARIOS",
                        commandType: CommandType.Text
                    ).ToList();
        
                    return horarios;
                }
            }
            catch (Exception ex)
            {
                return new List<Horario>();
            }
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
