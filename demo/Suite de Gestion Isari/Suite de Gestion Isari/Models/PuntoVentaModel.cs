namespace Suite_de_Gestion_Isari.Models;
using Suite_de_Gestion_Isari.Entidades;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using System.Data.Common;
using System.Net.Mail;
using System.Reflection;
using System.Reflection.Metadata;

public class PuntoVentaModel
    {
        private readonly IConfiguration _conf;
        private readonly IHostEnvironment _env;

        public PuntoVentaModel(IConfiguration conf, IHostEnvironment env)
        {
            _conf = conf;
            _env = env;
        }


        // Obtener producto por código de barras
        public Venta ObtenerProductoPorCodigoBarras(string codigoBarras)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                return context.QueryFirstOrDefault<Venta>(
                    "ConssultarExistenciaProducto",
                    new { CODIGO_PRODUCTO = codigoBarras },
                    commandType: CommandType.StoredProcedure
                ) ?? new Venta();
            }
        }

        public List<DetallePago> ObtenerHistorialPagos(long consecutivoFactura)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                return context.Query<DetallePago>("ObtenerHistorialPagos", new { ConsecutivoFactura = consecutivoFactura }, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        // Agregar venta temporal
        public bool AgregarVentaTemporal(Venta venta)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = context.Execute("AgregarVentaTemporal", new { venta.ID_PRODUCTO, venta.CODIGO_PRODUCTO, venta.Consecutivo, venta.cantidad, venta.NOMBRE, venta.Precio, venta.DESCRIPCION });
                return result > 0;
            }
        }

        // Obtener detalle de venta temporal
        public List<Venta> ObtenerDetalleVentaTemporal(int Consecutivo)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                return context.Query<Venta>("ObtenerDetalleVentaTemporal", new { Consecutivo }).ToList();
            }
        }

        // Obtener monto total venta temporal
        public decimal ObtenerMontoTotalVentaTemporal(int Consecutivo)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var montoTotal = context.QuerySingleOrDefault<decimal?>("ObtenerMontoTotalVentaTemporal", new { Consecutivo });
                return montoTotal ?? 0;
            }
        }

        // Registrar venta
        public bool Registrarventa(int ConsecutivoUsuario)
        {
            using (var connection = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ConsecutivoUsuario", ConsecutivoUsuario, DbType.Int32);
                parameters.Add("@Resultado", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("PagarVentaTemporal", parameters, commandType: CommandType.StoredProcedure);

                var resultado = parameters.Get<int>("@Resultado");
                return resultado == 1;
            }
        }

        // Verificar si hay productos en la venta
        public bool HayProductosEnVenta(int usuarioID)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var query = "SELECT COUNT(*) FROM VentaTemporal WHERE Consecutivo = @usuarioID";
                var cantidadProductos = context.ExecuteScalar<int>(query, new { usuarioID });
                return cantidadProductos > 0;
            }
        }

        // Consultar facturas
        public Respuesta ConsultarFacturas(long consecutivo)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Query<Venta>("ConsultarFacturas", new { Consecutivo = consecutivo }, commandType: CommandType.StoredProcedure).ToList();

                if (result.Any())
                {
                    respuesta.Codigo = 0;
                    respuesta.Contenido = result;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "No hay facturas en este momento";
                }

                return respuesta;
            }
        }

    
        // Consultar facturas
        public Respuesta ConsultarFacturasHistorico()
    {
        using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
        {
            var respuesta = new Respuesta();
            var result = context.Query<Venta>("ConsultarFacturasHistorico", new { }, commandType: CommandType.StoredProcedure).ToList();

            if (result.Any())
            {
                respuesta.Codigo = 0;
                respuesta.Contenido = result;
            }
            else
            {
                respuesta.Codigo = -1;
                respuesta.Mensaje = "No hay facturas en este momento";
            }

            return respuesta;
        }
    }

    // Consultar detalle de factura
    public Respuesta ConsultarDetalleFactura(long Consecutivo)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Query<Venta>("ConsultarDetalleFactura", new { Consecutivo });

                if (result.Any())
                {
                    respuesta.Codigo = 0;
                    respuesta.Contenido = result;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "No hay detalles para esa factura en este momento";
                }

                return respuesta;
            }
        }

        public bool EliminarArticuloTemporal(long ID_VentaTemporal)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {

                var result = context.Execute("EliminarArticuloTemporal", new { ID_VentaTemporal });


                return result > 0;
            }
        }


        public Respuesta ConsultarFacturasHoy (long consecutivo)
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Query<Venta>("ConsultarFacturasHoy", new { Consecutivo = consecutivo }, commandType: CommandType.StoredProcedure).ToList();

                if (result.Any())
                {
                    respuesta.Codigo = 0;
                    respuesta.Contenido = result;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "No hay facturas en este momento";
                }

                return respuesta;
            }

        }


        public Respuesta ConsultarFacturasHoyAdmin( )
        {
            using (var context = new SqlConnection(_conf.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var respuesta = new Respuesta();
                var result = context.Query<Venta>("ConsultarFacturasHoyAdmin", new { }, commandType: CommandType.StoredProcedure).ToList();

                if (result.Any())
                {
                    respuesta.Codigo = 0;
                    respuesta.Contenido = result;
                }
                else
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "No hay facturas en este momento";
                }

                return respuesta;
            }

        }
        
       

        public long ObtenerIdUltimaVenta(int usuarioID)
        {
            using (var connection = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var parametros = new DynamicParameters();
                parametros.Add("@UsuarioID", usuarioID);
                parametros.Add("@IdFactura", dbType: DbType.Int64, direction: ParameterDirection.Output);

                connection.Execute("ObtenerIdUltimaFactura", parametros, commandType: CommandType.StoredProcedure);

                return parametros.Get<long>("@IdFactura");
            }
        }


        public List<Venta> ObtenerDetallesFactura(long idFactura)
        {
            using (var connection = new SqlConnection(_conf.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var detalles = connection.Query<Venta>(
                    "DetalleEnvioFactura",
                    new { Consecutivo = idFactura },
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return detalles;
            }
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



        

        public Respuesta EnvioFactura(string email,string nombreCliente, string formapago, string cedulacliente, List<Venta> detallesFactura)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return new Respuesta
                {
                    Codigo = -1,
                    Mensaje = "El correo electrónico no puede estar vacío."
                };
            }

            try
            {
                // Leer y preparar la plantilla HTML
                var ruta = Path.Combine(_env.ContentRootPath, "EnvioFactura.html");
                if (!File.Exists(ruta))
                {
                    return new Respuesta
                    {
                        Codigo = -1,
                        Mensaje = "No se encontró la plantilla de factura."
                    };
                }

                var html = File.ReadAllText(ruta);

                // Obtener el primer elemento para datos generales de la factura
                var factura = detallesFactura.FirstOrDefault();
                if (factura == null)
                {
                    return new Respuesta
                    {
                        Codigo = -1,
                        Mensaje = "No se encontraron detalles de la factura."
                    };
                }
                html = html.Replace("@@Consecutivo", factura.Consecutivo.ToString());
                html = html.Replace("@@Nombre", nombreCliente);
                html = html.Replace("@@FormaPago", formapago);
                html = html.Replace("@@CedulaCliente", cedulacliente);
                html = html.Replace("@@Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
                html = html.Replace("@@Total", factura.TOTALFACTURA.ToString("C"));

                // Crear la tabla de detalle en HTML
                string detalleTabla = "<table><thead><tr><th>Producto</th><th>Cantidad</th><th>Precio Unitario</th><th>Precio Final</th></tr></thead><tbody>";
                foreach (var detalle in detallesFactura)
                {
                    detalleTabla += $"<tr><td>{detalle.Nombre}</td><td>{detalle.Unidades}</td><td>{detalle.Precio:C}</td><td>{detalle.Total:C}</td></tr>";
                }
                detalleTabla += "</tbody></table>";

                html = html.Replace("@@Detalle", detalleTabla);

                // Enviar el correo
                EnviarCorreo(email, "Detalle de tu Factura", html);

                return new Respuesta
                {
                    Codigo = 0,
                    Contenido = factura
                };
            }
            catch (Exception ex)
            {
                return new Respuesta
                {
                    Codigo = -1,
                    Mensaje = $"Error al enviar la factura: {ex.Message}"
                };
            }
        }

    }

