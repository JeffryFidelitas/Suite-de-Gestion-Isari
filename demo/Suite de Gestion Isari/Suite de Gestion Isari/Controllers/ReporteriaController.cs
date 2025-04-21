using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Suite_de_Gestion_Isari.Entidades;
using Suite_de_Gestion_Isari.Models;
//using System.Reflection.Metadata;

namespace Suite_de_Gestion_Isari.Controllers;

public class ReporteriaController : Controller
{
    private readonly ProductoModel _productoModel;
    private readonly CategoriaModel _categoriaModel;
    private readonly UsuarioModel _usuarioModel;
    private readonly UsuarioModel _empleadoModel;
    private readonly PuntoVentaModel _facturaModel;

    public ReporteriaController(IConfiguration configuration, ProductoModel producto, UsuarioModel empleado, PuntoVentaModel factura)
    {
        _productoModel = new ProductoModel(configuration);
        _categoriaModel = new CategoriaModel(configuration);
        _usuarioModel = new UsuarioModel(configuration);
        _empleadoModel = empleado;
        _facturaModel = factura;
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







    public IActionResult DescargarInventario(string formato)
    {
        var data = _productoModel.ObtenerProductos();

        if (formato == "excel")
            return ExportarExcel<Productos>(data, "ReporteInventario");
        else
            return ExportarPDF<Productos>(data, "ReporteInventario");
    }

    public IActionResult DescargarEmpleados(string formato)
    {
        var data = _empleadoModel.ConsultarEmpleados();

        if (formato == "excel")
            return ExportarExcel<Empleado>(data, "ReporteEmpleados");
        else
            return ExportarPDF<Empleado>(data, "ReporteEmpleados");
    }

    public IActionResult DescargarIngresos(string formato)
    {
        var respuesta = _facturaModel.ConsultarFacturas(0);
        var data = (List<Venta>)respuesta.Contenido;

        if (formato == "excel")
            return ExportarExcel(data, "ReporteIngresos");
        else
            return ExportarPDF(data, "ReporteIngresos");
    }


    private FileResult ExportarExcel<T>(List<T> data, string nombreArchivo)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Datos");
        worksheet.Cell(1, 1).InsertTable(data);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{nombreArchivo}.xlsx");
    }

    private FileResult ExportarPDF<T>(List<T> data, string nombreArchivo)
    {
        using var stream = new MemoryStream();
        var document = new Document();
        PdfWriter.GetInstance(document, stream);
        document.Open();

        var table = new PdfPTable(typeof(T).GetProperties().Length);
        foreach (var prop in typeof(T).GetProperties())
        {
            table.AddCell(new Phrase(prop.Name));
        }

        foreach (var item in data)
        {
            foreach (var prop in typeof(T).GetProperties())
            {
                var value = prop.GetValue(item)?.ToString() ?? "";
                table.AddCell(new Phrase(value));
            }
        }

        document.Add(table);
        document.Close();
        return File(stream.ToArray(), "application/pdf", $"{nombreArchivo}.pdf");
    }
}

