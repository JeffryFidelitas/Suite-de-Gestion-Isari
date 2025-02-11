using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Suite_de_Gestion_Isari.Entidades;

namespace Suite_de_Gestion_Isari.Models
{
    public class CategoriaModel
    {
        private readonly IConfiguration _conf;

        public CategoriaModel (IConfiguration conf)
        {
            _conf = conf;
        }

        [HttpPost]
        public Respuesta AgregarCategoria(Categorias model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var parametros = new DynamicParameters();
                parametros.Add("@DESCRIPCION", model.DESCRIPCION);
                parametros.Add("@Resultado", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                context.Execute("AgregarCategoria", parametros, commandType: CommandType.StoredProcedure);
                int result = parametros.Get<int>("@Resultado");

                if (result > 0)
                {
                    respuesta.Codigo = 0;
                    respuesta.Mensaje = "Categoria agregada exitosamente.";
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "Ya existe la categoria, "+model.DESCRIPCION+". Vuelva a intentarlo";
                }
                return respuesta;
            }
        }

        public List<Categorias> ObtenerCategorias()
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                return context.Query<Categorias>("ObtenerCategorias").ToList();
            }
        }

        public Categorias ConsultaCategoria(long ID_CATEGORIA)
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                var categoria = context.QueryFirstOrDefault<Categorias>(
                    "ConsultaCategoria",
                    new {ID_CATEGORIA}
                );
                return categoria ?? new Categorias();
            }
        }

        public bool ActualizarCategoria(Categorias model)
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                var filasAfectadas = context.Execute(
                    "ActualizarCategoria",
                    new { model.ID_CATEGORIA, model.DESCRIPCION }
                );

                return filasAfectadas > 0;
            }
        }
    }
}