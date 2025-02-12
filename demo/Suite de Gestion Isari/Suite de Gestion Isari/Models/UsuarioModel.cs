using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Suite_de_Gestion_Isari.Entidades;
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

        var result = context.Execute("CrearEmpleado", new { model.CEDULA, model.NOMBRE, model.EMAIL, model.CONTRASENA, model.ID_ROL, model.ID_PUESTO, model.TELEFONO});

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

        public bool ActualizarPerfil(Empleado model, out string mensaje)
        {
            mensaje = "";

            // Validar que el correo y el teléfono no estén vacíos
            if (string.IsNullOrWhiteSpace(model.EMAIL) || string.IsNullOrWhiteSpace(model.TELEFONO))
            {
                mensaje = "El correo electrónico y el número de teléfono no pueden estar vacíos.";
                return false;
            }

            // Validar formato de correo electrónico
            if (!Regex.IsMatch(model.EMAIL, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                mensaje = "El correo electrónico no tiene un formato válido.";
                return false;
            }

            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                var filasAfectadas = context.Execute(
                    "ActualizarPerfil",
                    new { model.ID_EMPLEADO, model.EMAIL, model.TELEFONO }
                );

                if (filasAfectadas > 0)
                {
                    mensaje = "Perfil actualizado correctamente.";
                    return true;
                }
                else
                {
                    mensaje = "Error al actualizar el perfil. Intente nuevamente.";
                    return false;
                }
            }
        }
        public Empleado ObtenerPerfil(string idEmpleado, out string mensaje)
        {
            mensaje = "";

            try
            {
                using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
                {
                    var empleado = context.QueryFirstOrDefault<Empleado>(
                        "ObtenerPerfil",
                        new { ID_EMPLEADO = idEmpleado },
                        commandType: System.Data.CommandType.StoredProcedure
                    );

                    if (empleado == null)
                    {
                        mensaje = "No se encontró la información del perfil.";
                        return null;
                    }

                    return empleado;
                }
            }
            catch (SqlException)
            {
                mensaje = "Error al conectar con la base de datos. Intente nuevamente.";
                return null;
            }
            catch (Exception)
            {
                mensaje = "Ocurrió un error inesperado.";
                return null;
            }
        }

    }
}