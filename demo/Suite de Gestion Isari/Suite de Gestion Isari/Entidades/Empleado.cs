namespace Suite_de_Gestion_Isari.Entidades
{
    public class Empleado
    {
        public string ID_EMPLEADO { get; set; } = string.Empty;
        public string CEDULA { get; set; } = string.Empty;
        public string NOMBRE { get; set; } = string.Empty;
        public string EMAIL { get; set; } = string.Empty;
        public string CONTRASENA { get; set; } = string.Empty;
        public Boolean ESTADO { get; set; }
        public DateTime FECHA_CONTRATACION { get; set; }
        public string TELEFONO { get; set; } = string.Empty;
        public string ID_ROL { get; set; } = string.Empty;
        public string ID_PUESTO { get; set; } = string.Empty;
        public string Salario { get; set; } = string.Empty;
        public string NOMBRE_POSICION { get; set; } = string.Empty;

        // DESCRIPCI�N DEL ROL
        public string DESCRIPCION { get; set; } = string.Empty;

        // **NUEVA PROPIEDAD**: D�as de vacaciones disponibles
        public int DIAS_VACACIONES { get; set; }
    }
}
