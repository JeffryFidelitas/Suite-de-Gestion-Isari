using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Suite_de_Gestion_Isari.Entidades;

namespace Suite_de_Gestion_Isari.Models
{
    public class DevolucionModel
    {
        private readonly IConfiguration _conf;

        public DevolucionModel(IConfiguration conf)
        {
            _conf = conf;
            
        }

        [HttpPost]
        public Respuesta AgregarDevolucion(Devolucion model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();

                var result = context.Execute("AgregarDevolucion", new { model.ID_FACTURA, model.DESCRIPCION, model.DINERO });

                if (result > 0)
                {
                    respuesta.Codigo = 0;
                    respuesta.Mensaje = "Devolución procesada exitosamente.";
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "Factura no existe";
                }

                return respuesta;
            }
        }

        public List<Devolucion> ObtenerDevoluciones()
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                return context.Query<Devolucion>("ObtenerDevoluciones").ToList();
            }
        }
    }
}