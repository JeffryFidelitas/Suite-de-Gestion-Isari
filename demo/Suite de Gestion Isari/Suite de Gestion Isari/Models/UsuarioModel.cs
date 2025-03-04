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

                var result = context.Execute("CrearEmpleado", new
                {
                    model.CEDULA,
                    model.NOMBRE,
                    model.EMAIL,
                    model.CONTRASENA,
                    model.DESCRIPCION,
                    model.NOMBRE_POSICION,
                    model.TELEFONO
                });

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

        // NUEVO: Método para recuperar contraseña
        public bool EnviarRecuperacion(string correo, out string mensaje)
        {
            mensaje = "";
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                var usuario = context.QueryFirstOrDefault<Empleado>(
                    "SELECT * FROM Empleados WHERE EMAIL = @Correo",
                    new { Correo = correo }
                );

                if (usuario == null)
                {
                    mensaje = "No se encontró un usuario con ese correo.";
                    return false;
                }

                string nuevaContrasena = GenerarClaveTemporal();
                usuario.CONTRASENA_TEMPORAL = nuevaContrasena;
                usuario.VIGENCIA_CONTRASENA = DateTime.Now.AddMinutes(30);

                var filasAfectadas = context.Execute(
                    "UPDATE Empleados SET CONTRASENA_TEMPORAL = @ContrasenaTemporal, VIGENCIA_CONTRASENA = @Vigencia WHERE EMAIL = @Correo",
                    new { ContrasenaTemporal = nuevaContrasena, Vigencia = usuario.VIGENCIA_CONTRASENA, Correo = correo }
                );

                if (filasAfectadas > 0)
                {
                    mensaje = "Se ha enviado un correo con la nueva contraseña temporal.";
                    return true;
                }

                mensaje = "Error al generar la nueva contraseña.";
                return false;
            }
        }

        // NUEVO: Método para validar login con clave temporal
        public Empleado ValidarUsuario(string correo, string contrasena)
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                var usuario = context.QueryFirstOrDefault<Empleado>(
                    "SELECT * FROM Empleados WHERE EMAIL = @Correo",
                    new { Correo = correo }
                );

                if (usuario == null)
                    return null;

                // Si la contraseña temporal está vigente, permite el acceso y actualiza la contraseña principal
                if (!string.IsNullOrEmpty(usuario.CONTRASENA_TEMPORAL) && usuario.VIGENCIA_CONTRASENA > DateTime.Now)
                {
                    if (usuario.CONTRASENA_TEMPORAL == contrasena)
                    {
                        context.Execute(
                            "UPDATE Empleados SET CONTRASENA = @NuevaContrasena, CONTRASENA_TEMPORAL = NULL, VIGENCIA_CONTRASENA = NULL WHERE EMAIL = @Correo",
                            new { NuevaContrasena = usuario.CONTRASENA_TEMPORAL, Correo = correo }
                        );
                        return usuario;
                    }
                    return null;
                }

                return usuario.CONTRASENA == contrasena ? usuario : null;
            }
        }

        private string GenerarClaveTemporal()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8);
        }
    }
}