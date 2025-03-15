namespace Suite_de_Gestion_Isari.Entidades
{
    public class Horario
    {
        public int ID_HORARIO { get; set; }
        public long ID_EMPLEADO { get; set; }

        public string? DIA_SEMANA { get; set; }
        public TimeSpan HORA_ENTRADA { get; set; }
        public TimeSpan HORA_SALIDA { get; set; }

        public string? Estado { get; set; }
    }
}
