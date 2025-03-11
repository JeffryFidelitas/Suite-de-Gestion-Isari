using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Suite_de_Gestion_Isari.Entidades;

namespace Suite_de_Gestion_Isari.Models
{
    public class PuestoModel  
    {
        private readonly IConfiguration _conf;
        

       

        public PuestoModel(IConfiguration conf)
        {
            _conf = conf;
            
        }

      

        [HttpPost]
        public Respuesta AgregarPuesto(Puestos model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();

                var result = context.Execute("AgregarPuesto", new { model.NOMBRE_POSICION, model.Descripcion, model.Salario });

                if (result > 0)
                {
                    respuesta.Codigo = 0;
                    respuesta.Mensaje = "Puesto agregado exitosamente.";
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "Ya existe el puesto, "+model.NOMBRE_POSICION+". Vuelva a intentarlo";
                }

                return respuesta;
            }
        }

        public List<Puestos> ObtenerPuestos()
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                return context.Query<Puestos>("ObtenerPuestos").ToList();
            }
        }


        public Puestos ConsultaPuesto(long ID_PUESTO)
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                var puesto = context.QueryFirstOrDefault<Puestos>(
                    "ConsultaPuesto",
                    new { ID_PUESTO }
                );

                return puesto ?? new Puestos(); 
            }
        }

        public bool ActualizarPuesto(Puestos model)
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                var filasAfectadas = context.Execute(
                    "ActualizarPuesto",
                    new { model.ID_PUESTO, model.NOMBRE_POSICION, model.Descripcion, model.Salario }
                    
                );

                return filasAfectadas > 0;
            }
        }




    }
}
