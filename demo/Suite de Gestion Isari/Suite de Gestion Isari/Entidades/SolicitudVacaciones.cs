namespace Suite_de_Gestion_Isari.Entidades
{
    public class SolicitudVacaciones
    {
        public int ID_SOLICITUD { get; set; }
        public int ID_EMPLEADO { get; set; }
        public DateTime FECHA_INICIO { get; set; }
        public DateTime FECHA_FIN { get; set; }
        public int DIAS_SOLICITADOS { get; set; }
        public string ESTADO { get; set; } = "Pendiente";
        public DateTime FECHA_SOLICITUD { get; set; } = DateTime.Now;
        public string MOTIVO { get; set; }
        public string Nombre { get; set; }

        public int DIAS_TOTALES { get; set; }
    }

}
