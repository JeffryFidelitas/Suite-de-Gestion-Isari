namespace Suite_de_Gestion_Isari.Entidades
{
    public class Datos
    {
        //GRAFICO VENTAS POR MES
        public string Mes { get; set; }
        public decimal TotalMes { get; set; }
        public int Año { get; set; }



        //GRAFICO VENTAS POR PRODUCTO
        public string Producto { get; set; }
        public decimal TotalVentas { get; set; }



        //GRAFICO VENTAS POR CATEGORIA
        public string Categoria { get; set; }
        public decimal TotalVentasCategoria { get; set; }


        //GRAFICO CUENTAS POR PAGAR Y COBRAR
        public int TotalPorCobrar { get; set; }
        public int TotalCobradas { get; set; }
        public int TotalPagadas { get; set; }
        public int TotalPorPagar { get; set; }



    }
}
