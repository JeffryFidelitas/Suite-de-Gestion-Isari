using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Suite_de_Gestion_Isari.Entidades;
using Suite_de_Gestion_Isari.Models;

namespace Suite_de_Gestion_Isari.Controllers;

public class InventarioController : Controller
{
    private readonly ProductoModel _productoModel;
    private readonly CategoriaModel _categoriaModel;

    public InventarioController(IConfiguration configuration)
    {
        _productoModel = new ProductoModel(configuration);
        _categoriaModel = new CategoriaModel(configuration);
    }

    public IActionResult Index()
    {
        return View(_productoModel.ObtenerProductos());
    }

    public IActionResult Eliminar(int id)
    {
        ViewBag.id = id;
        return View();
    }

    public IActionResult Editar(int id)
    {
        ViewBag.id = id;
        return View();
    }

    [HttpGet]
    public IActionResult Agregar()
    {
        ViewData["categorias"] = _categoriaModel.ObtenerCategorias();
        return View();
    }

    [HttpPost]
    public IActionResult Agregar(Productos model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["categorias"] = _categoriaModel.ObtenerCategorias();
            return View(model);
        }

        var respuesta = _productoModel.AgregarProducto(model);
        if (respuesta.Codigo == 0)
        {
            TempData["SuccessMessage"] = respuesta.Mensaje;
            return RedirectToAction("Index");
        }
        else
        {
            ViewData["categorias"] = _categoriaModel.ObtenerCategorias();
            ViewBag.ErrorMessage = respuesta.Mensaje;
            return View(model);
        }
    }
}