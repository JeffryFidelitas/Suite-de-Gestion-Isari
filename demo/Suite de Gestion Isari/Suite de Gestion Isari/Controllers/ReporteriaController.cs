using Microsoft.AspNetCore.Mvc;
using Suite_de_Gestion_Isari.Entidades;
using Suite_de_Gestion_Isari.Models;

namespace Suite_de_Gestion_Isari.Controllers;

public class ReporteriaController : Controller
{
    private readonly ProductoModel _productoModel;
    private readonly CategoriaModel _categoriaModel;
    private readonly UsuarioModel _usuarioModel;

    public ReporteriaController(IConfiguration configuration)
    {
        _productoModel = new ProductoModel(configuration);
        _categoriaModel = new CategoriaModel(configuration);
        _usuarioModel = new UsuarioModel(configuration);
    }
    public IActionResult Empleados()
    {
        return View(_usuarioModel.ConsultarEmpleados().ToList());
    }

    public IActionResult Inventario()
    {
        return View(_productoModel.ObtenerProductos().ToList());
    }

    public IActionResult Index()
    {
        return View();
    }
}
