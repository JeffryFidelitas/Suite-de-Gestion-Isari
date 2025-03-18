
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Suite_de_Gestion_Isari.Entidades
{
    public class Devolucion
    {
        public int ID_DEVOLUCION { get; set; }
        [Display(Name = "Número de Factura"), Required]
        public int ID_FACTURA { get; set; }
        [Display(Name = "Descripción"), Required]
        public string DESCRIPCION { get; set; }
        [Display(Name = "Cantidad de Dinero"), Required]
        public float DINERO { get; set; }
    }
}