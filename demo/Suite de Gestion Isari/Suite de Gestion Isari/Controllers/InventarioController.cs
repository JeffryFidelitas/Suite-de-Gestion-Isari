using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Suite_de_Gestion_Isari.Entidades;
using Suite_de_Gestion_Isari.Models;

namespace Suite_de_Gestion_Isari.Controllers;

public class InventarioController : Controller
{
    private readonly ProductoModel _productoModel;

    public InventarioController(IConfiguration configuration)
    {
        _productoModel = new ProductoModel(configuration);
    }

    public IActionResult Index()
    {
        return View();
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
        return View();
    }

    [HttpPost]
    public IActionResult Agregar(Productos model)
    {
        if (!ModelState.IsValid)
        {
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
            ViewBag.ErrorMessage = respuesta.Mensaje;
            return View(model);
        }
    }
}