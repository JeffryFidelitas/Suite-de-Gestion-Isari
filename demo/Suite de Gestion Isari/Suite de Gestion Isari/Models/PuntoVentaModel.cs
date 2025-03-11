using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Suite_de_Gestion_Isari.Entidades;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace Suite_de_Gestion_Isari.Models
{
    public class PuntoVentaModel
    {


        private readonly IConfiguration _conf;

        public PuntoVentaModel(IConfiguration conf)
        {
            _conf = conf;
        }



        
        public Venta ObtenerProductoPorCodigoBarras(string codigoBarras)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                return context.QueryFirstOrDefault <Venta>(
                    "ConssultarExistenciaProducto",
                    new { CODIGO_PRODUCTO =codigoBarras },
                    commandType: CommandType.StoredProcedure
                ) ?? new Venta();
           
            }
        }


        public bool AgregarVentaTemporal(Venta venta)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {              
                
                var result = context.Execute("AgregarVentaTemporal", new { venta.ID_PRODUCTO, venta.CODIGO_PRODUCTO,venta.Consecutivo,venta.cantidad,venta.NOMBRE,venta.Precio,venta.DESCRIPCION});
               
                
                return result > 0;
            }
        }



        public List<Venta> ObtenerDetalleVentaTemporal(int Consecutivo)
        {
            
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                return context.Query<Venta>("ObtenerDetalleVentaTemporal", new { Consecutivo }).ToList();
              
            }
        }

        public decimal ObtenerMontoTotalVentaTemporal(int Consecutivo)
        {

            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var montoTotal= context.QuerySingleOrDefault<decimal?>("ObtenerMontoTotalVentaTemporal", new {Consecutivo});

                return montoTotal ?? 0;

            }
        }

        public bool Registrarventa(int ConsecutivoUsuario)
        {
            using (var connection = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ConsecutivoUsuario", ConsecutivoUsuario, DbType.Int32);
                parameters.Add("@Resultado", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("PagarVentaTemporal", parameters, commandType: CommandType.StoredProcedure);

                var resultado = parameters.Get<int>("@Resultado");
                return resultado == 1;
            }
        }


        public bool HayProductosEnVenta(int usuarioID)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var query = "SELECT COUNT(*) FROM VentaTemporal WHERE Consecutivo = @usuarioID";
                var cantidadProductos = context.ExecuteScalar<int>(query, new { usuarioID });
                return cantidadProductos > 0;
            }
        }


        public Respuesta ConsultarFacturas(long consecutivo)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Query<Venta>("ConsultarFacturas", new { Consecutivo = consecutivo }, commandType: CommandType.StoredProcedure).ToList();

                if (result.Any())
                {
                    respuesta.Codigo = 0;
                    respuesta.Contenido = result;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "No hay facturas en este momento";
                }

                return respuesta;
            }

        }

        public Respuesta ConsultarDetalleFactura(long Consecutivo)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Query<Venta>("ConsultarDetalleFactura", new { Consecutivo });

                if (result.Any())
                {
                    respuesta.Codigo = 0;
                    respuesta.Contenido = result;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "No hay detalles para esa factura en este momento";
                }

                return respuesta;
            }
        }



    }


}






