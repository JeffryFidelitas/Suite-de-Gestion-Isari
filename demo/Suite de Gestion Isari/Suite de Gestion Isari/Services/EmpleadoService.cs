using Microsoft.Data.SqlClient;
using System.Data;

namespace Suite_de_Gestion_Isari.Services
{
    public class EmpleadoService
    {
        private readonly IConfiguration _conf;

        public EmpleadoService(IConfiguration conf)
        {
            _conf = conf;

        }
        public async Task<string> RecuperarContrasena(string correo)
        {
            string nuevaContrasena = null;

            using (var connection = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("Recuperar_Contrasena", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", correo);

                    var result = await command.ExecuteScalarAsync();
                    if (result != null)
                    {
                        nuevaContrasena = result.ToString();
                    }
                }
            }

            return nuevaContrasena;
        }
    }
}
