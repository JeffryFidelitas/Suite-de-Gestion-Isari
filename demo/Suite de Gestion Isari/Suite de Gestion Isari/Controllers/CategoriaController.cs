using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suite_de_Gestion_Isari.Entidades;
using Suite_de_Gestion_Isari.Models;

namespace Suite_de_Gestion_Isari.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly CategoriaModel _categoriaModel;

        public CategoriaController(IConfiguration configuration)
        {
            _categoriaModel = new CategoriaModel(configuration);
        }

        public IActionResult Index()
        {
            return View(_categoriaModel.ObtenerCategorias());
        }
        public IActionResult Editar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Categorias model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            var respuesta = _categoriaModel.AgregarCategoria(model);
            if (respuesta.Codigo == 0)
            {
                TempData["SuccessMessage"] = respuesta.Mensaje;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = respuesta.Mensaje;
                return View(model);
            }
                
        }

    }
}
