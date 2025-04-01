
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Suite_de_Gestion_Isari.Entidades
{
    public class Cuenta
    {
        public int ID_CUENTA { get; set; }
        [Display(Name = "Nombre de Individuo o Proveedor"), Required]
        public string INDIVIDUO { get; set; }
        [Display(Name = "Descripción"), Required]
        public string DESCRIPCION { get; set; }
        [Display(Name = "Cantidad de Dinero"), Required]
        public float DINERO { get; set; }
        [Display(Name = "Fecha de Vencimiento"), Required]
        public DateTime VENCIMIENTO { get; set; }
        [Display(Name = "Estado"), Required]
        public int ESTADO { get; set; }

    }
}