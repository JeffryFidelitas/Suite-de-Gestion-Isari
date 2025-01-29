using Microsoft.AspNetCore.Mvc;
using Suite_de_Gestion_Isari.Entidades;
using Suite_de_Gestion_Isari.Models;

namespace Suite_de_Gestion_Isari.Controllers
{
    public class PuestoController : Controller
    {
        private readonly PuestoModel _puestoModel;

        public PuestoController(IConfiguration configuration)
        {
            _puestoModel = new PuestoModel(configuration);
        }

        [HttpGet]
        public IActionResult Agregar()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Puestos model)
        {
            if (!ModelState.IsValid)
            {
                
                return View(model);
            }

            var respuesta = _puestoModel.AgregarPuesto(model);

            if (respuesta.Codigo == 0)
            {
                
                TempData["SuccessMessage"] = respuesta.Mensaje;
                return RedirectToAction("ConsultarPosiciones");
            }
            else
            {
                
                ViewBag.ErrorMessage = respuesta.Mensaje;
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ConsultarPosiciones()
        {
            
            var puestos = _puestoModel.ObtenerPuestos();

            
            return View(puestos);
        }



        [HttpGet]
        public IActionResult ActualizarPuesto (long id)
        {
            var puestos = _puestoModel.ConsultaPuesto(id);


            return View(puestos);


        }

      
        [HttpPost]
        public IActionResult ActualizarPuesto(Puestos model)
        {
            if (ModelState.IsValid)
            {
                var resultado = _puestoModel.ActualizarPuesto(model);

                if (resultado)
                {
                    return RedirectToAction("ConsultarPosiciones"); 
                }

                ViewBag.ErrorMessage = "No se puede actualizar el puesto, el nombre ya existe";
                return View(model);
                
            }

            return View("Detalle", model); 
        }
    }

}
