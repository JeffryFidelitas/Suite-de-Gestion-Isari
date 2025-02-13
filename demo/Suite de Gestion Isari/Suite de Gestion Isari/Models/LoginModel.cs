using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Suite_de_Gestion_Isari.Entidades;
using System.Data;

namespace Suite_de_Gestion_Isari.Models
{
    public class LoginModel
    {

        private readonly IConfiguration _conf;

        public LoginModel(IConfiguration conf)
        {
            _conf = conf;

        }




        public Empleado IniciarSesion(Empleado model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                // Llama al procedimiento almacenado y obtén los datos del empleado
                var result = context.QueryFirstOrDefault<Empleado>(
                    "IniciarSesion",
                    new { model.EMAIL, model.CONTRASENA },
                    commandType: CommandType.StoredProcedure
                );

                // Si no se encuentra el empleado, devuelve un nuevo objeto Empleado vacío
                return result ?? new Empleado();
            }
        }


    }
}
