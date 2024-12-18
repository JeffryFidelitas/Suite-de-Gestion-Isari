using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Suite_de_Gestion_Isari.Models;

namespace Suite_de_Gestion_Isari.Controllers;

public class InventarioController : Controller
{
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

    public IActionResult Crear()
    {
        return View();
    }
}