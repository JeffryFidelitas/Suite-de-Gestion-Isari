using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suite_de_Gestion_Isari.Models;
using System.Reflection;

namespace Suite_de_Gestion_Isari.Controllers
{
    public class DatosController : Controller
    {

        private readonly DatosModel _Datos;

        public DatosController(IConfiguration configuration)
        {
            _Datos = new DatosModel(configuration);
            
        }

        // GET: DatosController
        public ActionResult ConsultarDatos()
        {
            
            return View();
        }



        [HttpPost]
        public ActionResult ConsultarDatosVentasMensuales()
        {
            var datos = _Datos.ConsultarDatosVentasMensuales();
            return Json(datos);
        }


        [HttpPost]
        public ActionResult ConsultarVentasPorProducto()
        {
            var datos = _Datos.ConsultarVentasPorProducto();
            return Json(datos);
        }

        [HttpPost]
        public ActionResult DATOSCuentasPorCobrar()
        {
            var datos = _Datos.DATOSCuentasPorCobrar();
            return Json(datos);
        }

        [HttpPost]
        public ActionResult DATOSCuentasPorPagar()
        {
            var datos = _Datos.DATOSCuentasPorPagar();


            return Json(datos);
        }

        [HttpPost]
        public ActionResult ConsultarVentasPorCategoria()
        {
            var datos = _Datos.ConsultarVentasPorCategoria();
            return Json(datos);
        }


        
    }
}
