namespace Suite_de_Gestion_Isari.Entidades
{
    public class Productos
    {
        public int ID_PRODUCTO { get; set; }
        public string NOMBRE { get; set; } = string.Empty;
        public string DESCRIPCION { get; set; } = string.Empty;
        public string PROVEEDOR { get; set; } = string.Empty;
        public double PRECIO { get; set; }
        public int CANTIDAD_DISPONIBLE { get; set; }
        public int ID_CATEGORIA { get; set; }
    }
}