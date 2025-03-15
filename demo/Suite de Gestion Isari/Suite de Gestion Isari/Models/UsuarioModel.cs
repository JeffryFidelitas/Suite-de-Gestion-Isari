using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Suite_de_Gestion_Isari.Entidades;
using System.Data;
using System.Text.RegularExpressions;




namespace Suite_de_Gestion_Isari.Models
{
    public class UsuarioModel
    {


        private readonly IConfiguration _conf;

        public UsuarioModel(IConfiguration conf)
        {
            _conf = conf;

        }


        [HttpPost]
       public Respuesta AgregarEmpleado(Empleado model)
        {
        using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
        {
        var respuesta = new Respuesta();

        var result = context.Execute("CrearEmpleado", new { model.CEDULA, model.NOMBRE, model.EMAIL, model.CONTRASENA, model.TELEFONO, model.ID_ROL, model.ID_PUESTO });

        if (result > 0)
        {
            respuesta.Codigo = 0;
            respuesta.Mensaje = "Empleado agregado exitosamente.";
        }
        else
        {
            respuesta.Codigo = -1;
            respuesta.Mensaje = "Ya existe el empleado, " + model.NOMBRE + ". Vuelva a intentarlo";
        }

        return respuesta;
        }
        }

        public List<Empleado> ConsultarEmpleados()
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                return context.Query<Empleado>("ConsultarEmpleados").ToList();
            }
        }

        public Empleado ObtenerUsuarioPorID(int id)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                return context.QueryFirstOrDefault<Empleado>(
                    "ObtenerUsuarioPorID",
                    new { ID_EMPLEADO = id },
                    commandType: CommandType.StoredProcedure
                ) ?? new Empleado();
            }
        }

        public Empleado ObtenerUsuariologueado(int id)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                return context.QueryFirstOrDefault<Empleado>(
                    "ObtenerUsuariologueado",
                    new { ID_EMPLEADO = id },
                    commandType: CommandType.StoredProcedure
                ) ?? new Empleado();
            }
        }

        public void ActualizarUsuario(Empleado model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                context.Execute(
                    "ActualizarUsuario",
                    new { model.ID_EMPLEADO, model.NOMBRE, model.EMAIL, model.TELEFONO },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public void ActualizarEmpleado(Empleado model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                context.Execute(
                    "ActualizarEmpleado",
                    new
                    {
                        model.ID_EMPLEADO,
                        model.NOMBRE,
                        model.CEDULA,
                        model.EMAIL,
                        model.ID_ROL,
                        model.ID_PUESTO,
                        model.TELEFONO
                        
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
        }



        public List<Empleado> ObtenerRoles()
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                return context.Query<Empleado>("LeerRoles").ToList();
            }
        }

      

        public Respuesta AgregarSolicitudVacaciones(SolicitudVacaciones model)
        {
            try
            {
                using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
                {
                    var respuesta = new Respuesta();

                    var result = context.Execute(
                        "CrearSolicitudVacaciones",
                        new
                        {
                            model.ID_EMPLEADO,
                            model.FECHA_INICIO,
                            model.FECHA_FIN,
                            model.DIAS_SOLICITADOS,
                            model.ESTADO,
                            model.FECHA_SOLICITUD,
                            model.MOTIVO
                        },
                        commandType: CommandType.StoredProcedure
                    );


                    if (result < 0)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Mensaje = "Solicitud de vacaciones registrada exitosamente.";
                    }
                    else
                    {
                        respuesta.Codigo = -1;
                        respuesta.Mensaje = "No se pudo registrar la solicitud de vacaciones.";
                    }

                    return respuesta;
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
        public List<SolicitudVacaciones> ObtenerSolicitudesVacaciones()
        {
            try
            {
                using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
                {
                    var solicitudes = context.Query<SolicitudVacaciones>(
                        "SELECT * FROM SuiteGestionIsari.dbo.SOLICITUD_VACACIONES",
                        commandType: CommandType.Text
                    ).ToList();
        
                    return solicitudes;
                }
            }
            catch (Exception ex)
            {
                return new List<SolicitudVacaciones>();
            }
        }
        
        
        public Respuesta ActualizarEstadoSolicitud(int idSolicitud, string estado)
        {
            try
            {
                using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
                {
                    var respuesta = new Respuesta();
        
                    var result = context.Execute(
                        "UPDATE SuiteGestionIsari.dbo.SOLICITUD_VACACIONES SET ESTADO = @ESTADO WHERE ID_SOLICITUD = @ID_SOLICITUD",
                        new { ESTADO = estado, ID_SOLICITUD = idSolicitud },
                        commandType: CommandType.Text
                    );
        
                    if (result > 0)
                    {
                        respuesta.Codigo = 0;
                        respuesta.Mensaje = "Estado de la solicitud actualizado correctamente.";
                    }
                    else
                    {
                        respuesta.Codigo = -1;
                        respuesta.Mensaje = "No se pudo actualizar el estado de la solicitud.";
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
    }
}
