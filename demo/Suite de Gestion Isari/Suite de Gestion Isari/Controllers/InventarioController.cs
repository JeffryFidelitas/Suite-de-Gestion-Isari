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

    public IActionResult Eliminar()
    {
        return View();
    }

    public IActionResult Editar()
    {
        return View();
    }

    public IActionResult Crear()
    {
        return View();
    }
}