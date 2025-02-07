using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Suite_de_Gestion_Isari.Entidades;

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

    }
}
