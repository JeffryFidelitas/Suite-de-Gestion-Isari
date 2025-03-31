namespace Suite_de_Gestion_Isari.Entidades
{
    public class Venta
    {
        // VARIABLES DEL PRODUCTO
        public long ID_PRODUCTO { get; set; }
        public string NOMBRE { get; set; } = string.Empty;
        public string DESCRIPCION { get; set; } = string.Empty;
        public long CANTIDAD_DISPONIBLE { get; set; }
        public decimal Precio { get; set; }
        public string CODIGO_PRODUCTO { get; set; } = string.Empty;

        // VARIABLES VENDEDOR
        public int Consecutivo { get; set; }

        // VARIABLES VENTA TEMPORAL
        public int cantidad { get; set; }
        public int ID_VentaTemporal { get; set; }
        public decimal MontoTotal { get; set; }

        // VARIABLES FACTURA
        public int ConsecutivoFactura { get; set; }
        public int FacturaID { get; set; }
        public int Unidades { get; set; }
        public int ConsecutivoProducto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
        public int ConsecutivoUsuario { get; set; }

        public int TOTALFACTURA { get; set; }
        
    }

    public class DetallePago
    {
        public DateTime FechaPago { get; set; }
        public decimal MontoPagado { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
    }
}
