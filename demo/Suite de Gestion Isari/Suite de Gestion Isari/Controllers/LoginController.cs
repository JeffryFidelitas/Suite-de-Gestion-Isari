using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using System;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suite_de_Gestion_Isari.Models;
using Suite_de_Gestion_Isari.Entidades;

namespace Suite_de_Gestion_Isari.Controllers
{
    public class LoginController : Controller
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<object> _passwordHasher;
        private readonly LoginModel _login;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _passwordHasher = new PasswordHasher<object>();
            _login = new LoginModel(configuration);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult OlvideContrasena()
        {
            return View("OlvideContrasena");
        }

        [HttpPost]
        public IActionResult IniciarSesion(Empleado model)
        {
            if (model == null || string.IsNullOrEmpty(model.EMAIL) || string.IsNullOrEmpty(model.CONTRASENA))
            {
                ViewBag.Error = "Por favor ingrese el correo electrónico y la contraseña.";
                return View("Login"); 
            }

            
            var empleado = _login.IniciarSesion(model);

            if (!string.IsNullOrEmpty(empleado.EMAIL))
            {
                
                  HttpContext.Session.SetString("UsuarioID", empleado!.ID_EMPLEADO.ToString()); 
                  HttpContext.Session.SetString("UsuarioNombre", empleado.NOMBRE);
                  HttpContext.Session.SetString("IdRol", empleado.ID_ROL.ToString());


                return RedirectToAction("Index", "Home");
            }

            
            ViewBag.Error = "Correo electrónico o contraseña incorrectos.";
            return View("Login");
        }


        public IActionResult CerrarSesion()
        {
            
            HttpContext.Session.Clear();

            
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> OlvideContrasena(string correo)
        {
            if (string.IsNullOrEmpty(correo))
            {
                ViewBag.Error = "Ingrese un correo válido.";
                return View();
            }

            string userId = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id FROM Usuarios WHERE Email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", correo);

                await connection.OpenAsync();
                var result = await command.ExecuteScalarAsync();
                if (result != null)
                    userId = result.ToString();
            }

            if (userId == null)
            {
                ViewBag.Error = "No se encontró un usuario con ese correo.";
                return View();
            }

            string token = GenerarTokenSeguro();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string insertQuery = "INSERT INTO TokensRecuperacion (UsuarioId, Token, Expiracion, Usado) VALUES (@UsuarioId, @Token, DATEADD(HOUR, 1, GETDATE()), 0)";
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@UsuarioId", userId);
                command.Parameters.AddWithValue("@Token", token);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }

            string resetLink = Url.Action("RestablecerContrasena", "Login", new { token = token }, Request.Scheme);
            await EnviarCorreo(correo, resetLink);

            ViewBag.Mensaje = "Se ha enviado un enlace de recuperación a tu correo.";
            return View();
        }

        public IActionResult RestablecerContrasena(string token)
        {
            ViewBag.Token = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RestablecerContrasena(string token, string nuevaContrasena)
        {
            if (string.IsNullOrEmpty(nuevaContrasena))
            {
                ViewBag.Error = "Ingrese una nueva contraseña.";
                return View();
            }

            string userId = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT UsuarioId FROM TokensRecuperacion WHERE Token = @Token AND Expiracion > GETDATE() AND Usado = 0";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Token", token);

                await connection.OpenAsync();
                var result = await command.ExecuteScalarAsync();
                if (result != null)
                    userId = result.ToString();
            }

            if (userId == null)
            {
                ViewBag.Error = "El enlace ha expirado o es inválido.";
                return View();
            }

            string contraseñaHasheada = _passwordHasher.HashPassword(null, nuevaContrasena);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string updateQuery = "UPDATE Usuarios SET ContraseñaHash = @Password WHERE Id = @UsuarioId";
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@Password", contraseñaHasheada);
                command.Parameters.AddWithValue("@UsuarioId", userId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string deleteQuery = "UPDATE TokensRecuperacion SET Usado = 1 WHERE Token = @Token";
                SqlCommand command = new SqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@Token", token);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }

            TempData["SuccessMessage"] = "Tu contraseña ha sido actualizada.";
            return RedirectToAction("Login");
        }

        private async Task EnviarCorreo(string correo, string resetLink)
        {
            try
            {
                string smtpServer = _configuration["EmailSettings:SmtpServer"];
                int smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
                string emailSender = _configuration["EmailSettings:SenderEmail"];
                string emailPassword = _configuration["EmailSettings:SenderPassword"];

                using var smtpClient = new SmtpClient(smtpServer)
                {
                    Port = smtpPort,
                    Credentials = new NetworkCredential(emailSender, emailPassword),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(emailSender),
                    Subject = "Recuperación de Contraseña",
                    Body = $"Haz clic en el siguiente enlace para restablecer tu contraseña: <a href='{resetLink}'>Restablecer Contraseña</a>",
                    IsBodyHtml = true
                };
                mailMessage.To.Add(correo);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se pudo enviar el correo. Inténtelo más tarde.";
                Console.WriteLine($"Error al enviar correo: {ex.Message}");
            }
        }

        private string GenerarTokenSeguro()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenBytes = new byte[32];
                rng.GetBytes(tokenBytes);
                return Convert.ToBase64String(tokenBytes);
            }
        }
    }
}

