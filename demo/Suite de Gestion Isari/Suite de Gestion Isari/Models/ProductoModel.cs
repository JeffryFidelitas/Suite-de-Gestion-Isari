using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Suite_de_Gestion_Isari.Entidades;

namespace Suite_de_Gestion_Isari.Models
{
    public class ProductoModel
    {
        private readonly IConfiguration _conf;

        public ProductoModel (IConfiguration conf)
        {
            _conf = conf;
        }

        [HttpPost]
        public Respuesta AgregarProducto(Productos model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Execute("AgregarProducto", new { model.NOMBRE, model.DESCRIPCION, model.PROVEEDOR, model.PRECIO, model.CANTIDAD_DISPONIBLE, model.ID_CATEGORIA});

                if (result > 0)
                {
                    respuesta.Codigo = 0;
                    respuesta.Mensaje = "Producto agregado exitosamente.";
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "Ya existe el producto, "+model.NOMBRE+". Vuelva a intentarlo";
                }
                return respuesta;
            }
        }

        public List<Productos> ObtenerProductos()
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                return context.Query<Productos>("ObtenerProductos").ToList();
            }
        }

        public Productos ConsultaProducto(long ID_PRODUCTO)
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                var producto = context.QueryFirstOrDefault<Productos>(
                    "ConsultaProducto",
                    new {ID_PRODUCTO}
                );
                return producto ?? new Productos();
            }
        }

        public bool ActualizarProducto(Productos model)
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                var filasAfectadas = context.Execute(
                    "ActualizarProducto",
                    new { model.ID_PRODUCTO, model.NOMBRE, model.DESCRIPCION, model.PROVEEDOR, model.PRECIO, model.CANTIDAD_DISPONIBLE, model.ID_CATEGORIA}
                );

                return filasAfectadas > 0;
            }
        }
    }
}