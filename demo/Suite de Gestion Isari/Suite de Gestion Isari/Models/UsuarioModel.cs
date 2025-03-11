using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Suite_de_Gestion_Isari.Entidades;
using System.Data;

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
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                var respuesta = new Respuesta();

                var result = context.Execute(
                    "CrearEmpleado",
                    new { model.CEDULA, model.NOMBRE, model.EMAIL, model.CONTRASENA, model.TELEFONO, model.ID_ROL, model.ID_PUESTO },
                    commandType: CommandType.StoredProcedure
                );

                if (result > 0)
                {
                    respuesta.Codigo = 0;
                    respuesta.Mensaje = "Empleado agregado exitosamente.";
                    ActualizarDiasVacaciones(model.ID_EMPLEADO);
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = $"Ya existe el empleado {model.NOMBRE}. Vuelva a intentarlo.";
                }

                return respuesta;
            }
        }

        public void ActualizarDiasVacaciones(string idEmpleado)
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                context.Execute(
                    "ActualizarDiasVacaciones",
                    new { ID_EMPLEADO = idEmpleado },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public List<Empleado> ConsultarEmpleados()
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                return context.Query<Empleado>("ConsultarEmpleados", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public Empleado ObtenerUsuarioPorID(string id)
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                return context.QueryFirstOrDefault<Empleado>(
                    "ObtenerUsuarioPorID",
                    new { ID_EMPLEADO = id },
                    commandType: CommandType.StoredProcedure
                ) ?? new Empleado();
            }
        }

        public void ActualizarEmpleado(Empleado model)
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
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

                ActualizarDiasVacaciones(model.ID_EMPLEADO);
            }
        }

        public Empleado ObtenerUsuarioLogueado(string id)
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                return context.QueryFirstOrDefault<Empleado>(
                    "ObtenerUsuarioLogueado",
                    new { ID_EMPLEADO = id },
                    commandType: CommandType.StoredProcedure
                ) ?? new Empleado();
            }
        }
    }
}
