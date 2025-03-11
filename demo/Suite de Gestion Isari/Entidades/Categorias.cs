
using System.ComponentModel.DataAnnotations;

namespace Suite_de_Gestion_Isari.Entidades
{
    public class Categorias
    {
        public int ID_CATEGORIA { get; set; }
        [Display(Name = "Nombre")]
        public string DESCRIPCION { get; set; } = string.Empty;
    }
}