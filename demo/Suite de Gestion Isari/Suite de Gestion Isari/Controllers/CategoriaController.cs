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

        public IActionResult Index(string? search)
        {
            ViewBag.search = search;

            return View(_categoriaModel.ObtenerCategorias().Where(c => search == null || c.DESCRIPCION.Contains(search)).ToList());
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

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var categoria = _categoriaModel.ConsultaCategoria(id);
            if (categoria.ID_CATEGORIA > 0)
                return View(categoria);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Editar(Categorias model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var respuesta = _categoriaModel.ActualizarCategoria(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var categoria = _categoriaModel.ConsultaCategoria(id);
            if (categoria.ID_CATEGORIA > 0)
                return View(categoria);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EliminarConfirmar(int ID_CATEGORIA)
        {
            var respuesta = _categoriaModel.EliminarCategoria(ID_CATEGORIA);
            return RedirectToAction("Index");
        }
    }
}
