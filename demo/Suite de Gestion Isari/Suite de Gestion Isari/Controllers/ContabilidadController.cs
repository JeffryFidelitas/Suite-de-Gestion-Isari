using Microsoft.AspNetCore.Mvc;
using Suite_de_Gestion_Isari.Models;
using Suite_de_Gestion_Isari.Entidades;

namespace Suite_de_Gestion_Isari.Controllers
{
    public class ContabilidadController : Controller
    {
        private readonly CuentaModel _cuenta;
        private readonly PuntoVentaModel _puntoVentaModel;

        public ContabilidadController(IConfiguration configuration, IHostEnvironment environment)
        {
            _cuenta = new CuentaModel(configuration);
            _puntoVentaModel = new PuntoVentaModel(configuration, environment);
        }

        // Cuenta por pagar no cuenta de usuario
        public ActionResult RegistroCuenta() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistroCuenta(Cuenta cuenta)
        {
            if (!ModelState.IsValid)
                return View(cuenta);
            var res = _cuenta.AgregarCuenta(cuenta);
            if (res.Codigo > 0)
            {
                ViewBag.ErrorMessage = res.Mensaje;
                return View(cuenta);
            }
            else
            {
                return RedirectToAction("ConsultarCuentas");
            }
        }

        public ActionResult CambiarEstadoCuenta(int ID_CUENTA, int ESTADO)
        {
            _cuenta.CambiarEstado(ID_CUENTA, ESTADO);
            return RedirectToAction("ConsultarCuentas");
        }

        public ActionResult ConsultarCuentas()
        {
            return View(_cuenta.ObtenerCuentas());
        }

        public ActionResult ConsultarCuenta(int Id)
        {
            return View(_cuenta.ConsultarCuenta(Id));
        }


        public IActionResult ReportesFinancieros()
        {
            return View();
        }

        public IActionResult ResumenDiario()
        {
            Respuesta res = _puntoVentaModel.ConsultarFacturasHoyAdmin();
            List<Venta>? ventas = (List<Venta>?) res.Contenido;
            return View(ventas);
        }

        public IActionResult Caja()
        {
            return View();
        }
    }
}
