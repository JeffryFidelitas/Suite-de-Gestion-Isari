using System.ComponentModel.DataAnnotations;

namespace Suite_de_Gestion_Isari.Entidades
{
    public class Productos
    {
        public int ID_PRODUCTO { get; set; }
        [Display(Name = "Nombre")]
        public string NOMBRE { get; set; } = string.Empty;
        [Display(Name = "Descripcion")]
        public string DESCRIPCION { get; set; } = string.Empty;
        [Display(Name = "Proveedor")]
        public string PROVEEDOR { get; set; } = string.Empty;
        [Display(Name = "Precio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio tiene que ser mayor que 0.01")]
        public double PRECIO { get; set; }
        [Display(Name = "Cantidad Disponible")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad no puede ser negativa")]
        public int CANTIDAD_DISPONIBLE { get; set; }
        [Display(Name = "Categoria")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría valida")]
        public int ID_CATEGORIA { get; set; }
    }
}