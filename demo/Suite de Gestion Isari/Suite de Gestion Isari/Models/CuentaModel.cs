using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Suite_de_Gestion_Isari.Entidades;

namespace Suite_de_Gestion_Isari.Models
{

    //NOTA: Esto son CUENTAS POR PAGAR (o cobrar), no tiene que ver con cuentas de usuario
    public class CuentaModel
    {
        private readonly IConfiguration _conf;

        public CuentaModel(IConfiguration conf)
        {
            _conf = conf;
            
        }

        [HttpPost]
        public Respuesta AgregarCuenta(Cuenta model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();

                var result = context.Execute("AgregarCuenta", new { model.INDIVIDUO, model.DESCRIPCION, model.DINERO, VENCIMIENTO = model.VENCIMIENTO.ToString("yyyy-MM-dd"), model.ESTADO });

                if (result > 0)
                {
                    respuesta.Codigo = 0;
                    respuesta.Mensaje = "Cuenta agregada exitosamente.";
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "Error agregando cuenta";
                }

                return respuesta;
            }
        }

        public List<Cuenta> ObtenerCuentas()
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                return context.Query<Cuenta>("ObtenerCuentas").ToList();
            }
        }

        public Cuenta ConsultarCuenta(long ID_CUENTA)
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                var cuenta = context.QueryFirstOrDefault<Cuenta>("ConsultaCuenta", new { ID_CUENTA });
                return cuenta ?? new Cuenta();
            }
        }

        public Respuesta CambiarEstado(long ID_CUENTA, int ESTADO)
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                var respuesta = new Respuesta();

                var result = context.Execute("CambiarEstado", new { ID_CUENTA, ESTADO });

                 if (result > 0)
                {
                    respuesta.Codigo = 0;
                    respuesta.Mensaje = "Estado de cuenta cambiado exitosamente.";
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "Error cambiado estado de cuenta";
                }

                return respuesta;
            }
        }
    }
}