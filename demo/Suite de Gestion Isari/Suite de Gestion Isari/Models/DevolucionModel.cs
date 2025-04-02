using System.Data;
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

                var parametros = new DynamicParameters();
                parametros.Add("@ID_FACTURA", model.ID_FACTURA);
                parametros.Add("@DESCRIPCION", model.DESCRIPCION);
                parametros.Add("@DINERO", model.DINERO);
                parametros.Add("@Resultado", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                context.Execute("AgregarDevolucion", parametros, commandType: CommandType.StoredProcedure);
                int result = parametros.Get<int>("@Resultado");

                if (result > 0)
                {
                    respuesta.Codigo = 1;
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