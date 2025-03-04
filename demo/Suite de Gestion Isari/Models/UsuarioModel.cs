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

    }
}
