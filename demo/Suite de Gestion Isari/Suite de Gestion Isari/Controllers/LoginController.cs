﻿using Microsoft.AspNetCore.Mvc;
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

        public LoginController(LoginModel loginn)
        {
            _loginn = loginn;
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

        [HttpGet]
        public ActionResult OlvideContrasena()
        {
            return View();
        }


        [HttpPost]
        public IActionResult OlvideContrasena(Empleado model)
        {
            var respuesta = _login.OlvideContrasena(model.EMAIL);
            if (respuesta.Codigo == 0)
            {
                // Redirige al login si el correo fue enviado correctamente
                TempData["Mensaje"] = "Correo de recuperación enviado correctamente.";
                return RedirectToAction("Login", "Login");
            }
            else
            {
                // Si hay algún error, muestra el mensaje
                TempData["Mensaje"] = respuesta.Mensaje;
                return View(model);
            }
        }







    }
}
