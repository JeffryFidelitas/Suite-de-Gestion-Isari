using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Suite_de_Gestion_Isari.Entidades;
using System.Data;
using System.Net.Mail;
using System.Text;

namespace Suite_de_Gestion_Isari.Models
{
    public class LoginModel
    {

        private readonly IConfiguration _conf;
        private readonly IHostEnvironment _env;
        public LoginModel(IConfiguration conf, IHostEnvironment env)
        {
            _conf = conf;
            _env = env;

        }


       

        public Empleado IniciarSesion(Empleado model)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                
                var result = context.QueryFirstOrDefault<Empleado>(
                    "IniciarSesion",
                    new { model.EMAIL, model.CONTRASENA },
                    commandType: CommandType.StoredProcedure
                );

                
                return result ?? new Empleado();
            }
        }

        public Respuesta OlvideContrasena(string EMAIL)
        {
            if (string.IsNullOrWhiteSpace(EMAIL))
            {
                return new Respuesta
                {
                    Codigo = -1,
                    Mensaje = "El correo electrónico no puede estar vacío."
                };
            }

            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();

                var result = context.QueryFirstOrDefault<Empleado>("ValidarUsuario", new { EMAIL = EMAIL });

                if (result != null)
                {
                    var Codigo = GenerarCodigo();
                    var Contrasenna = Codigo;
                    var UsaClaveTemp = true;
                    var Vigencia = DateTime.Now.AddMinutes(10);

                    context.Execute("ActualizarContrasenna", new { result.ID_EMPLEADO, Contrasenna, UsaClaveTemp, Vigencia });

                    var ruta = Path.Combine(_env.ContentRootPath, "RecuperarAcceso.html");
                    var html = System.IO.File.ReadAllText(ruta);

                    html = html.Replace("@@Nombre", result.NOMBRE);
                    html = html.Replace("@@Contrasenna", Codigo);
                    html = html.Replace("@@Vencimiento", Vigencia.ToString("dd/MM/yyyy hh:mm tt"));

                    EnviarCorreo(result.EMAIL, "Recuperar Accesos Sistema", html);

                    respuesta.Codigo = 0;
                    respuesta.Contenido = result;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "Su información no se encontró en nuestro sistema";
                }

                return respuesta;
            }
        }
        private string GenerarCodigo()
        {
            int length = 8;
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ012456789";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }


        private void EnviarCorreo(string destino, string asunto, string contenido)
        {
            string cuenta = _conf.GetSection("Variables:CorreoEmail").Value!;
            string contrasenna = _conf.GetSection("Variables:ClaveEmail").Value!;

            MailMessage message = new MailMessage();
            message.From = new MailAddress(cuenta);
            message.To.Add(new MailAddress(destino));
            message.Subject = asunto;
            message.Body = contenido;
            message.Priority = MailPriority.Normal;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.office365.com", 587);
            client.Credentials = new System.Net.NetworkCredential(cuenta, contrasenna);
            client.EnableSsl = true;

            //Esto es para que no se intente enviar el correo si no hay una contraseña
            if (!string.IsNullOrEmpty(contrasenna))
            {
                client.Send(message);
            }
        }

    }
}

