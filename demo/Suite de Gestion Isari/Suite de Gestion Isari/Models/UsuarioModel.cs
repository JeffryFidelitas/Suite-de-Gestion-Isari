using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Suite_de_Gestion_Isari.Entidades;
using System.Data;
using System.Reflection;
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
                        model.TELEFONO,
                        model.ESTADO

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



        //public Respuesta AgregarSolicitudVacaciones(SolicitudVacaciones model)
        //{
        //    try
        //    {
        //        using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
        //        {
        //            var respuesta = new Respuesta();

        //            var result = context.Execute(
        //                "CrearSolicitudVacaciones",
        //                new
        //                {
        //                    model.ID_EMPLEADO,
        //                    model.FECHA_INICIO,
        //                    model.FECHA_FIN,
        //                    model.DIAS_SOLICITADOS,
        //                    model.ESTADO,
        //                    model.FECHA_SOLICITUD,
        //                    model.MOTIVO
        //                },
        //                commandType: CommandType.StoredProcedure
        //            );


        //            if (result < 0)
        //            {
        //                respuesta.Codigo = 0;
        //                respuesta.Mensaje = "Solicitud de vacaciones registrada exitosamente.";
        //            }
        //            else
        //            {
        //                respuesta.Codigo = -1;
        //                respuesta.Mensaje = "No se pudo registrar la solicitud de vacaciones.";
        //            }

        //            return respuesta;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Respuesta
        //        {
        //            Codigo = -1,
        //            Mensaje = "Error al registrar la solicitud: " + ex.Message
        //        };
        //    }
        //}

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
                            model.MOTIVO,
                            model.DIAS_TOTALES
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

        //public List<SolicitudVacaciones> ObtenerSolicitudesVacaciones()
        //{
        //    try
        //    {
        //        using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
        //        {
        //            var solicitudes = context.Query<SolicitudVacaciones>(
        //                "SELECT * FROM SuiteGestionIsari.dbo.SOLICITUD_VACACIONES",
        //                commandType: CommandType.Text
        //            ).ToList();

        //            return solicitudes;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new List<SolicitudVacaciones>();
        //    }
        //}

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


        //public Respuesta ActualizarEstadoSolicitud(int idSolicitud, string estado)
        //{
        //    try
        //    {
        //        using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
        //        {
        //            var respuesta = new Respuesta();

        //            var result = context.Execute(
        //                "UPDATE SuiteGestionIsari.dbo.SOLICITUD_VACACIONES SET ESTADO = @ESTADO WHERE ID_SOLICITUD = @ID_SOLICITUD",
        //                new { ESTADO = estado, ID_SOLICITUD = idSolicitud },
        //                commandType: CommandType.Text
        //            );

        //            if (result > 0)
        //            {
        //                respuesta.Codigo = 0;
        //                respuesta.Mensaje = "Estado de la solicitud actualizado correctamente.";
        //            }
        //            else
        //            {
        //                respuesta.Codigo = -1;
        //                respuesta.Mensaje = "No se pudo actualizar el estado de la solicitud.";
        //            }

        //            return respuesta;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Respuesta
        //        {
        //            Codigo = -1,
        //            Mensaje = "Error al actualizar el estado: " + ex.Message
        //        };
        //    }
        //}

        public Respuesta ActualizarEstadoSolicitud(int idSolicitud, string estado)
        {
            try
            {
                using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
                {
                    var solicitud = context.QueryFirstOrDefault<SolicitudVacaciones>(
                        "SELECT ID_EMPLEADO, DIAS_SOLICITADOS, ESTADO FROM SOLICITUD_VACACIONES WHERE ID_SOLICITUD = @Id",
                        new { Id = idSolicitud });

                    if (solicitud == null)
                        return new Respuesta { Codigo = -1, Mensaje = "Solicitud no encontrada." };

                    if (solicitud.ESTADO != "Aceptada" && estado == "Aceptada")
                    {

                        context.Execute(
                            @"UPDATE SOLICITUD_VACACIONES
                      SET DIAS_TOTALES = DIAS_TOTALES - @Dias
                      WHERE ID_EMPLEADO = @IdEmpleado",
                            new
                            {
                                Dias = solicitud.DIAS_SOLICITADOS,
                                IdEmpleado = solicitud.ID_EMPLEADO
                            });
                    }

                    var result = context.Execute(
                        @"UPDATE SOLICITUD_VACACIONES
                  SET ESTADO = @Estado
                  WHERE ID_SOLICITUD = @IdSolicitud",
                        new
                        {
                            Estado = estado,
                            IdSolicitud = idSolicitud
                        });

                    if (result > 0)
                        return new Respuesta { Codigo = 0, Mensaje = "Estado actualizado correctamente." };

                    return new Respuesta { Codigo = -1, Mensaje = "No se pudo actualizar el estado." };
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

                    var result = context.QueryFirstOrDefault<string>(
                        "dbo.RegistrarHorario",
                        new
                        {
                            model.ID_EMPLEADO,
                            model.DIA_SEMANA,
                            model.HORA_ENTRADA,
                            model.HORA_SALIDA,
                            model.FECHA_ENTRADA
                        },
                        commandType: CommandType.StoredProcedure
                    );

                    if (result == "1")
                    {
                        return new Respuesta { Codigo = 0 };
                    }
                    else
                    {
                        return new Respuesta { Codigo = -1, Mensaje = result };
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

        public List<object> ObtenerListaEmpleados()
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                context.Open();
                var empleados = context.Query("SELECT ID_EMPLEADO, NOMBRE FROM SuiteGestionIsari.dbo.T_EMPLEADOS WHERE ESTADO = 1")
                                       .Select(e => new { id = e.ID_EMPLEADO, nombre = e.NOMBRE })
                                       .ToList<object>();

                return empleados;
            }
        }

        public bool ExisteEmpleado(long idEmpleado)
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                context.Open();
                var result = context.QueryFirstOrDefault<int>(
                    "SELECT COUNT(1) FROM SuiteGestionIsari.dbo.T_EMPLEADOS WHERE ID_EMPLEADO = @ID_EMPLEADO",
                    new { ID_EMPLEADO = idEmpleado }
                );

                return result > 0;
            }
        }

        public List<Horario> ObtenerHorarios()
        {
            try
            {
                using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
                {
                    var horarios = context.Query<Horario>(
                        @"SELECT 
            H.ID_HORARIO, 
            H.ID_EMPLEADO, 
            H.DIA_SEMANA, 
            H.HORA_ENTRADA, 
            H.HORA_SALIDA, 
            H.ESTADO, 
            H.FECHA_ENTRADA,
            E.NOMBRE AS NombreEmpleado
          FROM SuiteGestionIsari.dbo.T_HORARIOS H
          INNER JOIN SuiteGestionIsari.dbo.T_EMPLEADOS E 
            ON H.ID_EMPLEADO = E.ID_EMPLEADO",
                        commandType: CommandType.Text
                    ).ToList();

                    return horarios;
                }
            }
            catch (Exception)
            {
                return new List<Horario>();
            }
        }

        public bool CambiarContrasena(string usuarioID, string contrasenanueva)
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                
                var result = context.Execute("CambiarContrasena",
                    new { contrasenanueva, usuarioID },
                    commandType: System.Data.CommandType.StoredProcedure);

                return result > 0;
            }
        }


    }
}
