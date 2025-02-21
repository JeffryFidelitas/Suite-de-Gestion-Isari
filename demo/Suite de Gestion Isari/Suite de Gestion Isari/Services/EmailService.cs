using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Suite_de_Gestion_Isari.Services
{
    public class EmailService
    {
        public async Task<bool> EnviarCorreoRecuperacion(string destinatario, string nuevaContrasena)
        {
            try
            {
                var mensaje = new MimeMessage();
                mensaje.From.Add(new MailboxAddress("Soporte", "francinnizi69@gmail.com"));
                mensaje.To.Add(new MailboxAddress("", destinatario));
                mensaje.Subject = "Recuperación de Contraseña";

                mensaje.Body = new TextPart("plain")
                {
                    Text = $"Su nueva contraseña temporal es: {nuevaContrasena}\n\nPor favor, inicie sesión y cambie su contraseña."
                };

                using (var cliente = new SmtpClient())
                {
                    await cliente.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    await cliente.AuthenticateAsync("francinnizi69@gmail.com", "hzfrkrmfyosecrpv");
                    await cliente.SendAsync(mensaje);
                    await cliente.DisconnectAsync(true);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
