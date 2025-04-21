using Dapper;
using Microsoft.Data.SqlClient;
using Suite_de_Gestion_Isari.Entidades;

namespace Suite_de_Gestion_Isari.Models
{
    public class DatosModel
    {
        private readonly IConfiguration _conf;

        public DatosModel(IConfiguration conf)
        {
            _conf = conf;

        }

        public List<Datos> ConsultarDatosVentasMensuales()
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                return context.Query<Datos>("ConsultarVentasMensuales").ToList();
            }
        }


        public List<Datos> ConsultarVentasPorProducto()
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                return context.Query<Datos>("ConsultarVentasPorProducto").ToList();
            }
        }



        public Datos DATOSCuentasPorCobrar()
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                return context.QuerySingleOrDefault<Datos>("DATOSCuentasPorCobrar");
            }
        }


        public  Datos  DATOSCuentasPorPagar()
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                return context.QuerySingleOrDefault<Datos>("DATOSCuentasPorPagar");
            }
        }


        public List<Datos> ConsultarVentasPorCategoria()
        {
            using (var context = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                return context.Query<Datos>("ConsultarVentasPorCategoria").ToList();
            }
        }
        


    }
}
