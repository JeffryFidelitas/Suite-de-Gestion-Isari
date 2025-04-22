﻿USE [SuiteGestionIsari]
GO
/****** Object:  Table [dbo].[CATEGORIA_PRODUCTOS]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CATEGORIA_PRODUCTOS](
	[ID_CATEGORIA] [int] IDENTITY(1,1) NOT NULL,
	[DESCRIPCION] [nvarchar](255) NOT NULL,
    [ACTIVO] [int] NOT NULL,
 CONSTRAINT [PK__CATEGORI__4BD51FA56F205036] PRIMARY KEY CLUSTERED 
(
	[ID_CATEGORIA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CLIENTES]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CLIENTES](
	[ID_CLIENTE] [int] NOT NULL,
	[NOMBRE] [nvarchar](255) NOT NULL,
	[P_APELLIDO] [nvarchar](255) NULL,
	[S_APELLIDO] [nvarchar](255) NULL,
	[CELULAR] [nvarchar](20) NULL,
	[CORREO] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK__CLIENTES__23A341307D173CDB] PRIMARY KEY CLUSTERED 
(
	[ID_CLIENTE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PROVEEDORES]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PROVEEDORES](
	[ID_PROVEEDOR] [int] NOT NULL,
	[NOMBRE] [nvarchar](255) NOT NULL,
	[NUMERO] [nvarchar](20) NULL,
	[CORREO] [nvarchar](255) NULL,
 CONSTRAINT [PK__PROVEEDO__733DB4C4BFB80F88] PRIMARY KEY CLUSTERED 
(
	[ID_PROVEEDOR] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SOLICITUD_VACACIONES]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SOLICITUD_VACACIONES](
	[ID_SOLICITUD] [bigint] IDENTITY(1,1) NOT NULL,
	[ID_EMPLEADO] [bigint] NOT NULL,
	[FECHA_INICIO] [date] NOT NULL,
	[FECHA_FIN] [date] NOT NULL,
	[MOTIVO] [nvarchar](255) NOT NULL,
	[ESTADO] [nvarchar](50) NOT NULL,
	[DIAS_SOLICITADOS] [int] NOT NULL,
	[FECHA_SOLICITUD] [datetime] NOT NULL,
	[DIAS_TOTALES] [int] NULL,
 CONSTRAINT [PK_SOLICITUD_VACACIONES] PRIMARY KEY CLUSTERED 
(
	[ID_SOLICITUD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_CUENTAS]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_CUENTAS](
	[ID_CUENTA] [int] IDENTITY(1,1) NOT NULL,
	[INDIVIDUO] [nvarchar](50) NOT NULL,
	[DESCRIPCION] [nvarchar](50) NOT NULL,
	[DINERO] [decimal](10, 2) NOT NULL,
	[VENCIMIENTO] [date] NOT NULL,
	[ESTADO] [int] NOT NULL,
 CONSTRAINT [PK__T_CUENTAS__CDF4314123E6524] PRIMARY KEY CLUSTERED 
(
	[ID_CUENTA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_DEVOLUCIONES]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_DEVOLUCIONES](
	[ID_DEVOLUCION] [int] IDENTITY(1,1) NOT NULL,
	[ID_FACTURA] [int] NOT NULL,
	[DESCRIPCION] [nvarchar](50) NOT NULL,
	[DINERO] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK__T_DEVOLUCIONE__CDBAAE34710360E8] PRIMARY KEY CLUSTERED 
(
	[ID_DEVOLUCION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_EMPLEADOS]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_EMPLEADOS](
	[ID_EMPLEADO] [bigint] IDENTITY(1,1) NOT NULL,
	[CEDULA] [nvarchar](20) NOT NULL,
	[NOMBRE] [nvarchar](255) NOT NULL,
	[EMAIL] [nvarchar](255) NOT NULL,
	[ESTADO] [bit] NOT NULL,
	[CONTRASENA] [nvarchar](255) NOT NULL,
	[CONTRASENA_TEMPORAL] [bit] NOT NULL,
	[FECHA_CONTRATACION] [date] NOT NULL,
	[TELEFONO] [nvarchar](20) NULL,
	[ID_ROL] [int] NULL,
	[ID_PUESTO] [int] NULL,
	[VIGENCIA_CONTRASENA] [datetime] NULL,
 CONSTRAINT [PK__T_EMPLEA__922CA2699C98B6D9] PRIMARY KEY CLUSTERED 
(
	[ID_EMPLEADO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_ERRORES]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_ERRORES](
	[ID_ERROR] [bigint] NOT NULL,
	[ID_EMPLEADO] [bigint] NOT NULL,
	[DESCRIPCION] [nvarchar](255) NULL,
	[FECHA] [datetime] NOT NULL,
 CONSTRAINT [PK__T_ERRORE__9046136AC6A55B3F] PRIMARY KEY CLUSTERED 
(
	[ID_ERROR] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_HORARIOS]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_HORARIOS](
	[ID_HORARIO] [int] IDENTITY(1,1) NOT NULL,
	[ID_EMPLEADO] [bigint] NOT NULL,
	[DIA_SEMANA] [nvarchar](50) NOT NULL,
	[HORA_ENTRADA] [time](7) NOT NULL,
	[HORA_SALIDA] [time](7) NOT NULL,
	[ESTADO] [nvarchar](50) NULL,
	[FECHA_ENTRADA] [date] NULL,
 CONSTRAINT [PK_T_HORARIOS] PRIMARY KEY CLUSTERED 
(
	[ID_HORARIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_POSICIONES]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_POSICIONES](
	[ID_PUESTO] [int] IDENTITY(1,1) NOT NULL,
	[NOMBRE_POSICION] [nvarchar](255) NOT NULL,
	[DESCRIPCION] [nvarchar](255) NULL,
	[SALARIO] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK__T_POSICI__88D8DDB14593B656] PRIMARY KEY CLUSTERED 
(
	[ID_PUESTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_PRODUCTOS]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_PRODUCTOS](
	[ID_PRODUCTO] [int] IDENTITY(1,1) NOT NULL,
	[NOMBRE] [nvarchar](255) NOT NULL,
	[DESCRIPCION] [nvarchar](255) NULL,
	[PROVEEDOR] [nvarchar](255) NULL,
	[PRECIO] [decimal](10, 2) NOT NULL,
	[CANTIDAD_DISPONIBLE] [int] NOT NULL,
	[ID_CATEGORIA] [int] NOT NULL,
	[CODIGO_PRODUCTO] [nvarchar](50) NULL,
    [ACTIVO] [int] NOT NULL,
 CONSTRAINT [PK__PRODUCTO__88BD03576E55F2C6] PRIMARY KEY CLUSTERED 
(
	[ID_PRODUCTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_ROLES]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_ROLES](
	[ID_ROL] [int] IDENTITY(1,1) NOT NULL,
	[DESCRIPCION] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK__T_ROLES__203B0F685B2887EE] PRIMARY KEY CLUSTERED 
(
	[ID_ROL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_VACACIONES]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_VACACIONES](
	[ID_VACACION] [bigint] NOT NULL,
	[ID_EMPLEADO] [bigint] NOT NULL,
	[FECHA_INICIO] [date] NOT NULL,
	[FECHA_FIN] [date] NOT NULL,
	[DIAS_TOTALES] [int] NOT NULL,
 CONSTRAINT [PK__T_VACACI__262D75615C90A401] PRIMARY KEY CLUSTERED 
(
	[ID_VACACION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tDetalle]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tDetalle](
	[ConsecutivoDetalle] [int] IDENTITY(1,1) NOT NULL,
	[ConsecutivoFactura] [int] NOT NULL,
	[ConsecutivoProducto] [int] NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ConsecutivoDetalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tFactura]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tFactura](
	[ConsecutivoFactura] [int] IDENTITY(1,1) NOT NULL,
	[ConsecutivoUsuario] [int] NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[Fecha] [datetime] NOT NULL,
 CONSTRAINT [PK__tFactura__B290888BDB3F572B] PRIMARY KEY CLUSTERED 
(
	[ConsecutivoFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VentaTemporal]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VentaTemporal](
	[ID_VentaTemporal] [int] IDENTITY(1,1) NOT NULL,
	[Consecutivo] [int] NOT NULL,
	[CODIGO_PRODUCTO] [varchar](max) NOT NULL,
	[ID_PRODUCTO] [int] NOT NULL,
	[NOMBRE] [varchar](100) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Precio] [decimal](10, 2) NOT NULL,
	[FechaCreacion] [datetime] NULL,
	[MontoTotal] [decimal](18, 0) NULL,
	[DESCRIPCION] [nvarchar](max) NULL,
 CONSTRAINT [PK__VentaTem__107AE267385B5795] PRIMARY KEY CLUSTERED 
(
	[ID_VentaTemporal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[SOLICITUD_VACACIONES] ADD  DEFAULT ('Pendiente') FOR [ESTADO]
GO
ALTER TABLE [dbo].[SOLICITUD_VACACIONES] ADD  DEFAULT (getdate()) FOR [FECHA_SOLICITUD]
GO
ALTER TABLE [dbo].[tFactura] ADD  CONSTRAINT [DF__tFactura__Fecha__1BC821DD]  DEFAULT (getdate()) FOR [Fecha]
GO
ALTER TABLE [dbo].[VentaTemporal] ADD  CONSTRAINT [DF__VentaTemp__Fecha__123EB7A3]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[SOLICITUD_VACACIONES]  WITH CHECK ADD  CONSTRAINT [FK_SOLICITUD_VACACIONES_EMPLEADO] FOREIGN KEY([ID_EMPLEADO])
REFERENCES [dbo].[T_EMPLEADOS] ([ID_EMPLEADO])
GO
ALTER TABLE [dbo].[SOLICITUD_VACACIONES] CHECK CONSTRAINT [FK_SOLICITUD_VACACIONES_EMPLEADO]
GO
ALTER TABLE [dbo].[T_EMPLEADOS]  WITH CHECK ADD  CONSTRAINT [FK__T_EMPLEAD__ID_PU__5DCAEF64] FOREIGN KEY([ID_PUESTO])
REFERENCES [dbo].[T_POSICIONES] ([ID_PUESTO])
GO
ALTER TABLE [dbo].[T_EMPLEADOS] CHECK CONSTRAINT [FK__T_EMPLEAD__ID_PU__5DCAEF64]
GO
ALTER TABLE [dbo].[T_EMPLEADOS]  WITH CHECK ADD  CONSTRAINT [FK__T_EMPLEAD__ID_RO__5CD6CB2B] FOREIGN KEY([ID_ROL])
REFERENCES [dbo].[T_ROLES] ([ID_ROL])
GO
ALTER TABLE [dbo].[T_EMPLEADOS] CHECK CONSTRAINT [FK__T_EMPLEAD__ID_RO__5CD6CB2B]
GO
ALTER TABLE [dbo].[T_PRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK__PRODUCTOS__ID_CA__6477ECF3] FOREIGN KEY([ID_CATEGORIA])
REFERENCES [dbo].[CATEGORIA_PRODUCTOS] ([ID_CATEGORIA])
GO
ALTER TABLE [dbo].[T_PRODUCTOS] CHECK CONSTRAINT [FK__PRODUCTOS__ID_CA__6477ECF3]
GO
ALTER TABLE [dbo].[tDetalle]  WITH CHECK ADD  CONSTRAINT [FK__tDetalle__Consec__22751F6C] FOREIGN KEY([ConsecutivoFactura])
REFERENCES [dbo].[tFactura] ([ConsecutivoFactura])
GO
ALTER TABLE [dbo].[tDetalle] CHECK CONSTRAINT [FK__tDetalle__Consec__22751F6C]
GO
ALTER TABLE [dbo].[tDetalle]  WITH CHECK ADD FOREIGN KEY([ConsecutivoProducto])
REFERENCES [dbo].[T_PRODUCTOS] ([ID_PRODUCTO])
GO
/****** Object:  StoredProcedure [dbo].[ActualizarCategoria]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-------------------------- Actualizar Categoría -------------------------
CREATE PROCEDURE [dbo].[ActualizarCategoria]
    @ID_CATEGORIA INT,
    @DESCRIPCION NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1 
        FROM CATEGORIA_PRODUCTOS 
        WHERE DESCRIPCION = @DESCRIPCION 
        AND ID_CATEGORIA != @ID_CATEGORIA
        AND ACTIVO = 1
    )
    BEGIN
        IF EXISTS (
            SELECT 1
            FROM CATEGORIA_PRODUCTOS
            WHERE DESCRIPCION = @DESCRIPCION
            AND ID_CATEGORIA != @ID_CATEGORIA
            AND ACTIVO = 0
        )
        BEGIN
            BEGIN TRANSACTION;
            UPDATE CATEGORIA_PRODUCTOS
            SET DESCRIPCION = @DESCRIPCION,
                ACTIVO = 1
            WHERE ID_CATEGORIA = @ID_CATEGORIA;
            COMMIT;
            RETURN 1; -- Se actualizó y reactivó
        END
        ELSE
        BEGIN
            BEGIN TRANSACTION;
            UPDATE CATEGORIA_PRODUCTOS
            SET DESCRIPCION = @DESCRIPCION
            WHERE ID_CATEGORIA = @ID_CATEGORIA;
            COMMIT;
            RETURN 1; -- Se actualizó normalmente
        END
    END
    ELSE
    BEGIN
        RETURN 0;
    END
END;
GO

/****** Object:  StoredProcedure [dbo].[ActualizarContrasenna]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarContrasenna]
	@ID_EMPLEADO			bigint,
	@Contrasenna			varchar(255),
	@UsaClaveTemp			bit,
	@Vigencia				datetime
AS
BEGIN

	UPDATE dbo.T_EMPLEADOS
	   SET CONTRASENA = @Contrasenna,
		   CONTRASENA_TEMPORAL = @UsaClaveTemp,
		   VIGENCIA_CONTRASENA = @Vigencia
	 WHERE ID_EMPLEADO = @ID_EMPLEADO
	
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarEmpleado]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarEmpleado]
    @ID_EMPLEADO INT,
    @NOMBRE NVARCHAR(100),
    @CEDULA NVARCHAR(20),
    @EMAIL NVARCHAR(100),
    @ID_ROL INT,
    @ID_PUESTO INT,
    @TELEFONO NVARCHAR(50),
	@ESTADO INT
AS
BEGIN
    UPDATE T_EMPLEADOS
    SET 
        NOMBRE = @NOMBRE,
        CEDULA = @CEDULA,
        EMAIL = @EMAIL,
        ID_ROL = @ID_ROL,
        ID_PUESTO = @ID_PUESTO,
        TELEFONO = @TELEFONO,
		ESTADO = @ESTADO
    WHERE ID_EMPLEADO = @ID_EMPLEADO;
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarEstadoHorario]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[ActualizarEstadoHorario]
     @ID_HORARIO INT,
     @ESTADO NVARCHAR(50)
 AS
 BEGIN
     UPDATE SuiteGestionIsari.dbo.T_HORARIOS
     SET ESTADO = @ESTADO
     WHERE ID_HORARIO = @ID_HORARIO;
 END;
GO
/****** Object:  StoredProcedure [dbo].[ActualizarProducto]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ActualizarProducto]
    @ID_PRODUCTO INT,
    @NOMBRE NVARCHAR(255),
    @DESCRIPCION NVARCHAR(500),
    @PROVEEDOR NVARCHAR(500),
    @PRECIO DECIMAL(18,2),
    @CANTIDAD_DISPONIBLE INT,
    @ID_CATEGORIA INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1 
        FROM T_PRODUCTOS 
        WHERE NOMBRE = @NOMBRE 
        AND ID_PRODUCTO != @ID_PRODUCTO
        AND ACTIVO = 1
    )
    BEGIN
        IF EXISTS (
            SELECT 1 
            FROM T_PRODUCTOS 
            WHERE NOMBRE = @NOMBRE 
            AND ID_PRODUCTO != @ID_PRODUCTO
            AND ACTIVO = 0
        )
        BEGIN
            BEGIN TRANSACTION;
            UPDATE T_PRODUCTOS
            SET 
                NOMBRE = @NOMBRE,
                DESCRIPCION = @DESCRIPCION,
                PROVEEDOR = @PROVEEDOR,
                PRECIO = @PRECIO,
                CANTIDAD_DISPONIBLE = @CANTIDAD_DISPONIBLE,
                ID_CATEGORIA = @ID_CATEGORIA,
                ACTIVO = 1
            WHERE ID_PRODUCTO = @ID_PRODUCTO;
            COMMIT;
            RETURN 1; -- Reactivado
        END
        ELSE
        BEGIN
            BEGIN TRANSACTION;
            UPDATE T_PRODUCTOS
            SET 
                NOMBRE = @NOMBRE,
                DESCRIPCION = @DESCRIPCION,
                PROVEEDOR = @PROVEEDOR,
                PRECIO = @PRECIO,
                CANTIDAD_DISPONIBLE = @CANTIDAD_DISPONIBLE,
                ID_CATEGORIA = @ID_CATEGORIA
            WHERE ID_PRODUCTO = @ID_PRODUCTO;
            COMMIT;
            RETURN 1; -- Actualizado normalmente
        END
    END
    ELSE
    BEGIN
        RETURN 0; -- Ya existe un producto activo con ese nombre
    END
END;
GO

/****** Object:  StoredProcedure [dbo].[ActualizarPuesto]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------Actualizar Puesto-----------------------------------
CREATE PROCEDURE [dbo].[ActualizarPuesto]
    @ID_PUESTO INT,
    @NOMBRE_POSICION NVARCHAR(255),
    @DESCRIPCION NVARCHAR(500),
    @SALARIO DECIMAL(18,2)
    
AS
BEGIN
	
    IF NOT EXISTS (SELECT 1 FROM T_POSICIONES 
					WHERE (NOMBRE_POSICION = @NOMBRE_POSICION)
					 AND ID_PUESTO != @ID_PUESTO)
    BEGIN       
        UPDATE T_POSICIONES
        SET 
            NOMBRE_POSICION = @NOMBRE_POSICION,
            DESCRIPCION = @DESCRIPCION,
            SALARIO = @SALARIO 
			WHERE ID_PUESTO=@ID_PUESTO;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[ActualizarTotalVentaTemporal]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarTotalVentaTemporal]
    @Consecutivo INT
AS
BEGIN
    DECLARE @Total DECIMAL(18, 2);

    -- Calcular el total sumando cantidad * precio de los productos
    SELECT @Total = SUM(cantidad * Precio)
    FROM VentaTemporal
    WHERE Consecutivo = @Consecutivo;

    -- Actualizar el campo MontoTotal
    UPDATE VentaTemporal
    SET MontoTotal = @Total
    WHERE Consecutivo = @Consecutivo;
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarUsuario]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarUsuario]
    @NOMBRE VARCHAR(100),
    @EMAIL VARCHAR(100),
    @TELEFONO VARCHAR(20),
	@ID_EMPLEADO int
AS
BEGIN
    UPDATE T_EMPLEADOS
    SET 
        NOMBRE = @NOMBRE,
        EMAIL = @EMAIL,
        TELEFONO = @TELEFONO
    WHERE 
        ID_EMPLEADO = @ID_EMPLEADO;
END;
GO
/****** Object:  StoredProcedure [dbo].[AgregarCategoria]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- -------------------------- Agregar Categoría -------------------------
CREATE PROCEDURE [dbo].[AgregarCategoria]
    @DESCRIPCION NVARCHAR(255)
AS
BEGIN   
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1 
        FROM CATEGORIA_PRODUCTOS 
        WHERE DESCRIPCION = @DESCRIPCION AND ACTIVO = 1
    )
    BEGIN
        IF EXISTS (
            SELECT 1 
            FROM CATEGORIA_PRODUCTOS 
            WHERE DESCRIPCION = @DESCRIPCION AND ACTIVO = 0
        )
        BEGIN
            -- Si existe pero está inactiva, actualiza a activa
            BEGIN TRANSACTION;
            UPDATE CATEGORIA_PRODUCTOS
            SET ACTIVO = 1
            WHERE DESCRIPCION = @DESCRIPCION AND ACTIVO = 0;
            COMMIT;
            RETURN 1;
        END
        ELSE
        BEGIN
            -- Si no existe en absoluto, inserta una nueva
            BEGIN TRANSACTION;
            INSERT INTO CATEGORIA_PRODUCTOS (DESCRIPCION, ACTIVO)
            VALUES (@DESCRIPCION, 1);
            COMMIT;
            RETURN 1;
        END
    END
    ELSE
    BEGIN
        RETURN 0;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[AgregarCuenta]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- -------------------------- Agregar Cuenta -------------------------
 CREATE PROCEDURE [dbo].[AgregarCuenta]
     @INDIVIDUO NVARCHAR(50),
     @DESCRIPCION NVARCHAR(50),
     @DINERO DECIMAL(10,2),
     @VENCIMIENTO DATE,
     @ESTADO INT
 AS
 BEGIN   
     SET NOCOUNT ON;
     
     BEGIN TRANSACTION;
     INSERT INTO SuiteGestionIsari.dbo.T_CUENTAS (INDIVIDUO, DESCRIPCION, DINERO, VENCIMIENTO, ESTADO)
     VALUES (@INDIVIDUO, @DESCRIPCION, @DINERO, @VENCIMIENTO, @ESTADO);
     COMMIT;
     RETURN 1;
 END;
GO
/****** Object:  StoredProcedure [dbo].[AgregarDevolucion]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ------- Agregar Devolucion

CREATE PROCEDURE [dbo].[AgregarDevolucion]
    @ID_FACTURA INT,
    @DESCRIPCION NVARCHAR(50),
    @DINERO DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM tfactura WHERE ConsecutivoFactura = @ID_FACTURA)
	BEGIN
		BEGIN TRANSACTION;
	    INSERT INTO SuiteGestionIsari.dbo.T_DEVOLUCIONES (ID_FACTURA, DESCRIPCION, DINERO)
	    VALUES (@ID_FACTURA, @DESCRIPCION, @DINERO);
	    COMMIT;
	    RETURN 1;
	END;
	RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[AgregarProducto]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AgregarProducto]
    @CODIGO_PRODUCTO NVARCHAR(255),
    @NOMBRE NVARCHAR(255),
    @DESCRIPCION NVARCHAR(500) = NULL,
    @PROVEEDOR NVARCHAR(255),
    @PRECIO DECIMAL(18,2),
    @CANTIDAD_DISPONIBLE INT,
    @ID_CATEGORIA INT
AS
BEGIN   
    SET NOCOUNT ON;
    IF NOT EXISTS (
        SELECT 1 
        FROM T_PRODUCTOS 
        WHERE (NOMBRE = @NOMBRE OR CODIGO_PRODUCTO = @CODIGO_PRODUCTO) 
        AND ACTIVO = 1
    )
    BEGIN
        IF EXISTS (
            SELECT 1 
            FROM T_PRODUCTOS 
            WHERE (NOMBRE = @NOMBRE OR CODIGO_PRODUCTO = @CODIGO_PRODUCTO) 
            AND ACTIVO = 0
        )
        BEGIN
            BEGIN TRANSACTION;
            UPDATE T_PRODUCTOS
            SET 
                NOMBRE = @NOMBRE,
                DESCRIPCION = @DESCRIPCION,
                PROVEEDOR = @PROVEEDOR,
                PRECIO = @PRECIO,
                CANTIDAD_DISPONIBLE = @CANTIDAD_DISPONIBLE,
                ID_CATEGORIA = @ID_CATEGORIA,
                ACTIVO = 1
            WHERE (NOMBRE = @NOMBRE OR CODIGO_PRODUCTO = @CODIGO_PRODUCTO)
              AND ACTIVO = 0;
            COMMIT;
            RETURN 1; -- Producto reactivado
        END
        ELSE
        BEGIN
            BEGIN TRANSACTION;
            INSERT INTO T_PRODUCTOS (
                CODIGO_PRODUCTO, NOMBRE, DESCRIPCION, PROVEEDOR, 
                PRECIO, CANTIDAD_DISPONIBLE, ID_CATEGORIA, ACTIVO
            )
            VALUES (
                @CODIGO_PRODUCTO, @NOMBRE, @DESCRIPCION, @PROVEEDOR, 
                @PRECIO, @CANTIDAD_DISPONIBLE, @ID_CATEGORIA, 1
            );
            COMMIT;
            RETURN 1; -- Producto nuevo insertado
        END
    END
    ELSE
    BEGIN
        RETURN 0; -- Producto ya existe y está activo
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[AgregarPuesto]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AgregarPuesto]
    @NOMBRE_POSICION NVARCHAR(255),
    @Descripcion NVARCHAR(500) = NULL,
    @Salario DECIMAL(18,2)
AS
BEGIN   
    IF NOT EXISTS (SELECT 1 FROM T_POSICIONES WHERE NOMBRE_POSICION = @NOMBRE_POSICION)
    BEGIN
        
        INSERT INTO T_Posiciones (NOMBRE_POSICION, DESCRIPCION, SALARIO)
        VALUES (@NOMBRE_POSICION, @Descripcion, @Salario );      
    END 
END;
GO
/****** Object:  StoredProcedure [dbo].[AgregarVentaTemporal]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AgregarVentaTemporal]
	@ID_PRODUCTO	int,
	@CODIGO_PRODUCTO nvarchar(100),
	@Consecutivo int,--del usuario que esta realizando la venta
	@cantidad INT,
	@NOMBRE VARCHAR(100),
	@Precio decimal,
	@Descripcion VARCHAR(100)

AS

BEGIN

   DECLARE @Total DECIMAL(18, 2);
    -- Inserta directamente el producto en la tabla temporal de ventas
    INSERT INTO VentaTemporal (CODIGO_PRODUCTO, Consecutivo, ID_PRODUCTO, Cantidad,NOMBRE,Precio,DESCRIPCION)
            VALUES (@CODIGO_PRODUCTO, @Consecutivo, @ID_PRODUCTO, @cantidad,@NOMBRE,@Precio,@Descripcion);



   SELECT @Total = SUM(cantidad * Precio)
    FROM VentaTemporal
    WHERE Consecutivo = @Consecutivo;

    -- Actualizar el campo MontoTotal
    UPDATE VentaTemporal
    SET MontoTotal = @Total
    WHERE Consecutivo = @Consecutivo;
END;
GO
/****** Object:  StoredProcedure [dbo].[CambiarContrasena]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  CREATE PROCEDURE [dbo].[CambiarContrasena]
    @usuarioID INT,
    @contrasenanueva NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    -- Actualizar la contraseña del usuario
    UPDATE T_EMPLEADOS
    SET CONTRASENA = @contrasenanueva
    WHERE ID_EMPLEADO = @usuarioID;

    -- Comprobar si la operación fue exitosa
    IF @@ROWCOUNT = 0
    BEGIN
        
        RETURN -1;
    END

    -- Si todo fue exitoso, devolver un código de éxito
    RETURN 1;
END;
GO
/****** Object:  StoredProcedure [dbo].[CambiarEstado]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 -- -------------------------- Cambiar Estado ------------------------
 CREATE PROCEDURE [dbo].[CambiarEstado]
 	@ID_CUENTA INT,
 	@ESTADO INT
 AS
 BEGIN
 	SET NOCOUNT ON;
 	
 	UPDATE SuiteGestionIsari.dbo.T_CUENTAS
 	SET ESTADO = @ESTADO
 	WHERE ID_CUENTA = @ID_CUENTA
 END
GO
/****** Object:  StoredProcedure [dbo].[ConssultarExistenciaProducto]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ConssultarExistenciaProducto]
@CODIGO_PRODUCTO NVARCHAR(50)
AS
BEGIN
	SELECT ID_PRODUCTO,NOMBRE,DESCRIPCION,PRECIO,CANTIDAD_DISPONIBLE,CODIGO_PRODUCTO 
	FROM T_PRODUCTOS WHERE CODIGO_PRODUCTO = @CODIGO_PRODUCTO
END;
GO
/****** Object:  StoredProcedure [dbo].[ConsultaCategoria]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- -------------------------- Consulta Categoría Individual -------------------------
CREATE PROCEDURE [dbo].[ConsultaCategoria]
    @ID_CATEGORIA INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_CATEGORIA, DESCRIPCION, ACTIVO
    FROM CATEGORIA_PRODUCTOS
    WHERE ID_CATEGORIA = @ID_CATEGORIA;
END;
GO
/****** Object:  StoredProcedure [dbo].[ConsultaCuenta]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
 -- -------------------------- Consulta Cuenta Individual -------------------------
 CREATE PROCEDURE [dbo].[ConsultaCuenta]
     @ID_CUENTA INT
 AS
 BEGIN
     SET NOCOUNT ON;
     SELECT ID_CUENTA, INDIVIDUO, DESCRIPCION, DINERO, VENCIMIENTO, ESTADO
     FROM SuiteGestionIsari.dbo.T_CUENTAS
     WHERE ID_CUENTA = @ID_CUENTA;
 END;
 
GO
/****** Object:  StoredProcedure [dbo].[ConsultaProducto]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultaProducto]
    @ID_PRODUCTO INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_PRODUCTO, NOMBRE, DESCRIPCION, PROVEEDOR, PRECIO, CANTIDAD_DISPONIBLE, ID_CATEGORIA, ACTIVO
    FROM T_PRODUCTOS
    WHERE ID_PRODUCTO = @ID_PRODUCTO;
END;
GO
/****** Object:  StoredProcedure [dbo].[ConsultaPuesto]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[ConsultaPuesto]

@ID_PUESTO INT
AS
BEGIN

  
    SELECT ID_PUESTO,NOMBRE_POSICION, DESCRIPCION,SALARIO
    FROM T_POSICIONES where ID_PUESTO=@ID_PUESTO;
END;
GO
/****** Object:  StoredProcedure [dbo].[ConsultarDetalleFactura]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultarDetalleFactura]
	@Consecutivo BIGINT
AS
BEGIN
	
	SELECT	ConsecutivoFactura 'Consecutivo',
			ConsecutivoProducto,
			P.Nombre,
			D.Precio,
			D.Cantidad 'Unidades',
			D.Total
	FROM	tDetalle D
	INNER JOIN T_PRODUCTOS P ON D.ConsecutivoProducto = P.ID_PRODUCTO
	WHERE	ConsecutivoFactura = @Consecutivo

END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarEmpleados]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultarEmpleados]
AS
BEGIN

	SELECT
	    E.ID_EMPLEADO,
		NOMBRE,
	    E.CEDULA,
	    E.EMAIL,	
	    E.ID_ROL,
		E.ID_PUESTO,
        E.TELEFONO,
		P.NOMBRE_POSICION,
		R.DESCRIPCION,
		E.Estado
	FROM 
            T_EMPLEADOS E

			INNER JOIN T_POSICIONES P
			ON E.ID_PUESTO =P.ID_PUESTO
			INNER JOIN T_ROLES R
			ON E.ID_ROL =R.ID_ROL
END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarFacturas]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ConsultarFacturas]
	@Consecutivo BIGINT
AS
BEGIN
	
	SELECT	F.ConsecutivoFactura,
			U.Nombre,
			F.Total,
			F.Fecha
	FROM	tFactura F
	INNER JOIN T_EMPLEADOS U ON F.ConsecutivoUsuario = U.ID_EMPLEADO
	WHERE	ConsecutivoUsuario = @Consecutivo

END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarFacturasHoy]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[ConsultarFacturasHoy]
	@Consecutivo BIGINT
AS
BEGIN
	-- Obtener solo las facturas del día actual
	SELECT	F.ConsecutivoFactura,
			U.Nombre,
			F.Total,
			F.Fecha
	FROM	tFactura F
	INNER JOIN T_EMPLEADOS U ON F.ConsecutivoUsuario = U.ID_EMPLEADO
	WHERE	F.ConsecutivoUsuario = @Consecutivo
	AND		CONVERT(DATE, F.Fecha) = CONVERT(DATE, GETDATE())

END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarFacturasHoyAdmin]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultarFacturasHoyAdmin]
AS
BEGIN
	-- Obtener todas las facturas del día actual sin importar el vendedor
	SELECT	F.ConsecutivoFactura,
			U.Nombre,
			F.Total,
			F.Fecha
	FROM	tFactura F
	INNER JOIN T_EMPLEADOS U ON F.ConsecutivoUsuario = U.ID_EMPLEADO
	WHERE	CONVERT(DATE, F.Fecha) = CONVERT(DATE, GETDATE())

END
GO
/****** Object:  StoredProcedure [dbo].[CrearEmpleado]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CrearEmpleado]
	@CEDULA         varchar(20),
	@NOMBRE			varchar(255),
	@EMAIL			varchar(80),
	@CONTRASENA	varchar(255),
	@TELEFONO		varchar(255),
	@ID_ROL			int,
	@ID_PUESTO		int
AS
BEGIN

	DECLARE @ESTADO BIT = 1,			
			@CONTRASENA_TEMPORAL BIT = 0
	

	IF NOT EXISTS(SELECT 1 FROM dbo.T_EMPLEADOS
			      WHERE CEDULA = @CEDULA
					OR	EMAIL = @EMAIL)
	BEGIN
		INSERT INTO dbo.T_EMPLEADOS (CEDULA,NOMBRE,EMAIL,CONTRASENA,ESTADO,ID_ROL,ID_PUESTO,CONTRASENA_TEMPORAL,VIGENCIA_CONTRASENA,TELEFONO,FECHA_CONTRATACION)
		VALUES (@CEDULA,@NOMBRE,@EMAIL,@CONTRASENA,@ESTADO,@ID_ROL,@ID_PUESTO,@CONTRASENA_TEMPORAL,GETDATE(),@TELEFONO,GETDATE())
	END

END
GO
/****** Object:  StoredProcedure [dbo].[CrearSolicitudVacaciones]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[CrearSolicitudVacaciones]

    @ID_EMPLEADO BIGINT,

    @FECHA_INICIO DATE,

    @FECHA_FIN DATE,

    @DIAS_SOLICITADOS INT,

    @ESTADO VARCHAR(50),

    @FECHA_SOLICITUD DATE,

    @MOTIVO NVARCHAR(255),

    @DIAS_TOTALES INT

AS

BEGIN

    SET NOCOUNT ON;
 
    IF @DIAS_SOLICITADOS <= @DIAS_TOTALES

    BEGIN

        INSERT INTO SuiteGestionIsari.dbo.SOLICITUD_VACACIONES 

            (ID_EMPLEADO, FECHA_INICIO, FECHA_FIN, DIAS_SOLICITADOS, ESTADO, FECHA_SOLICITUD, MOTIVO, DIAS_TOTALES)

        VALUES 

            (@ID_EMPLEADO, @FECHA_INICIO, @FECHA_FIN, @DIAS_SOLICITADOS, @ESTADO, @FECHA_SOLICITUD, @MOTIVO, @DIAS_TOTALES);
 
        PRINT 'Solicitud almacenada correctamente';

    END

    ELSE

    BEGIN

        RAISERROR ('No tienes suficientes días acumulados.', 16, 1);

    END

END;


GO
/****** Object:  StoredProcedure [dbo].[DetalleEnvioFactura]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DetalleEnvioFactura]
    @Consecutivo BIGINT
AS
BEGIN
    -- Información de la factura
    SELECT 
        F.ConsecutivoFactura AS 'Consecutivo',
        D.ConsecutivoProducto,
        P.Nombre,
        D.Precio,
        D.Cantidad AS 'Unidades',
        D.Total,
        F.Total AS 'TOTALFACTURA' 
    FROM tDetalle D
    INNER JOIN T_PRODUCTOS P ON D.ConsecutivoProducto = P.ID_PRODUCTO
    INNER JOIN tFactura F ON D.ConsecutivoFactura = F.ConsecutivoFactura  -- Agregar INNER JOIN con tFactura
    WHERE D.ConsecutivoFactura = @Consecutivo
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarArticuloTemporal]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarArticuloTemporal]
    @ID_VentaTemporal INT
AS
BEGIN
    -- Verificar si el artículo existe en la tabla temporal
    IF EXISTS (SELECT 1 FROM VentaTemporal WHERE ID_VentaTemporal = @ID_VentaTemporal)
    BEGIN
        DELETE FROM VentaTemporal
        WHERE ID_VentaTemporal = @ID_VentaTemporal;

        PRINT 'Artículo eliminado correctamente.';
    END
    ELSE
    BEGIN
        PRINT 'El artículo no existe en la tabla temporal.';
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[EliminarCategoria]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EliminarCategoria]
    @ID_CATEGORIA INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM CATEGORIA_PRODUCTOS WHERE ID_CATEGORIA = @ID_CATEGORIA)
    BEGIN
        BEGIN TRANSACTION;
        UPDATE CATEGORIA_PRODUCTOS
        SET ACTIVO = 0
        WHERE ID_CATEGORIA = @ID_CATEGORIA;

        COMMIT;
        RETURN 1; -- Eliminación
    END
    ELSE
    BEGIN
        RETURN 0; -- No se encontró la categoría
    END
END;
GO

/****** Object:  StoredProcedure [dbo].[EliminarProducto]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- -------------------------- Eliminar Producto (lógica) -------------------------
CREATE PROCEDURE [dbo].[EliminarProducto]
    @ID_PRODUCTO INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM T_PRODUCTOS WHERE ID_PRODUCTO = @ID_PRODUCTO)
    BEGIN
        BEGIN TRANSACTION;
        UPDATE T_PRODUCTOS
        SET ACTIVO = 0
        WHERE ID_PRODUCTO = @ID_PRODUCTO;

        COMMIT;
        RETURN 1; -- Disque Eliminado
    END
    ELSE
    BEGIN
        RETURN 0; -- No se encontró el producto
    END
END;
GO

/****** Object:  StoredProcedure [dbo].[IniciarSesion]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IniciarSesion]
    @EMAIL NVARCHAR(255),
    @CONTRASENA NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ID_EMPLEADO, NOMBRE, EMAIL, ESTADO,ID_ROL,CONTRASENA_TEMPORAL,VIGENCIA_CONTRASENA
    FROM T_EMPLEADOS
    WHERE Email = @EMAIL AND Contrasena = @CONTRASENA AND ESTADO=1;
END
GO
/****** Object:  StoredProcedure [dbo].[LeerRoles]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[LeerRoles] 

as
Begin

select ID_ROL,DESCRIPCION FROM T_ROLES

END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerCategorias]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- -------------------------- Obtener Categorías -------------------------
CREATE PROCEDURE [dbo].[ObtenerCategorias]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_CATEGORIA, DESCRIPCION, ACTIVO
    FROM CATEGORIA_PRODUCTOS WHERE ACTIVO = 1;
END;
GO
/****** Object:  StoredProcedure [dbo].[ObtenerCuentas]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 -- -------------------------- Obtener Cuentas -------------------------
 CREATE PROCEDURE [dbo].[ObtenerCuentas]
 AS
 BEGIN
     SET NOCOUNT ON;
     SELECT ID_CUENTA, INDIVIDUO, DESCRIPCION, DINERO, VENCIMIENTO, ESTADO
     FROM SuiteGestionIsari.dbo.T_CUENTAS;
 END;
 
GO
/****** Object:  StoredProcedure [dbo].[ObtenerDetalleVentaTemporal]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerDetalleVentaTemporal]
	@Consecutivo int
	
AS

BEGIN
    Select ID_VentaTemporal,ID_PRODUCTO,Consecutivo,NOMBRE,Precio,cantidad, (cantidad*Precio) as total,MontoTotal,Descripcion
	from VentaTemporal where consecutivo= @Consecutivo;

END;
GO
/****** Object:  StoredProcedure [dbo].[ObtenerDevoluciones]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ------- Obtener Devoluciones

CREATE PROCEDURE [dbo].[ObtenerDevoluciones]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_DEVOLUCION, ID_FACTURA, DESCRIPCION, DINERO
    FROM T_DEVOLUCIONES;
END;
GO
/****** Object:  StoredProcedure [dbo].[ObtenerHistorialPagos]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  CREATE PROCEDURE [dbo].[ObtenerHistorialPagos]
AS
BEGIN
SET NOCOUNT ON;
SELECT
v.ConsecutivoFactura,
u.Nombre AS Nombre,
v.Fecha,
v.Total -- ID de la factura
-- Nombre del vendedor
-- Fecha de la venta
-- Monto total de la venta
FROM Ventas v
INNER JOIN Usuarios u ON v.ConsecutivoUsuario = u.Consecutivo
ORDER BY v.Fecha DESC;
END;

GO
/****** Object:  StoredProcedure [dbo].[ObtenerIdUltimaFactura]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ObtenerIdUltimaFactura]
    @UsuarioID INT,
    @IdFactura BIGINT OUTPUT
AS
BEGIN
    SELECT TOP 1 @IdFactura = ConsecutivoFactura
    FROM tFactura
    WHERE ConsecutivoUsuario = @UsuarioID
    ORDER BY Fecha DESC
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerMontoTotalVentaTemporal]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[ObtenerMontoTotalVentaTemporal]
    @Consecutivo INT
AS
BEGIN
    SELECT MontoTotal
    FROM VentaTemporal
    WHERE Consecutivo = @Consecutivo
    GROUP BY MontoTotal; -- Asegura que solo se obtiene un único valor
END;
GO
/****** Object:  StoredProcedure [dbo].[ObtenerProductos]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ObtenerProductos]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_PRODUCTO, NOMBRE, DESCRIPCION, PROVEEDOR, PRECIO, CANTIDAD_DISPONIBLE, ID_CATEGORIA, CODIGO_PRODUCTO, ACTIVO
    FROM T_PRODUCTOS WHERE ACTIVO = 1;
END;
GO
/****** Object:  StoredProcedure [dbo].[ObtenerPuestos]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerPuestos]
AS
BEGIN
    SELECT ID_PUESTO, NOMBRE_POSICION, DESCRIPCION, SALARIO
    FROM T_POSICIONES
    
END;
GO
/****** Object:  StoredProcedure [dbo].[ObtenerUsuariologueado]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerUsuariologueado]
    @ID_EMPLEADO INT
AS
BEGIN
    SELECT 
        E.ID_EMPLEADO, 
        E.NOMBRE, 
        E.EMAIL, 
        E.CEDULA, 
        E.TELEFONO, 
        E.FECHA_CONTRATACION,  
		P.NOMBRE_POSICION,
		P.SALARIO
    FROM 
        T_EMPLEADOS E

		INNER JOIN 
        T_POSICIONES P ON E.ID_PUESTO = P.ID_PUESTO
  
		inner join 
		T_ROLES R ON E.ID_ROL =R.ID_ROL
    WHERE 
        E.ID_EMPLEADO = @ID_EMPLEADO;
END;
GO
/****** Object:  StoredProcedure [dbo].[ObtenerUsuarioPorID]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerUsuarioPorID]
    @ID_EMPLEADO INT
AS
BEGIN
    SELECT 
        E.ID_EMPLEADO, 
        E.NOMBRE, 
        E.EMAIL, 
        E.CEDULA, 
        E.TELEFONO, 
        E.FECHA_CONTRATACION,  
		R.ID_ROL,
		P.ID_PUESTO,
		E.ESTADO
    FROM 
        T_EMPLEADOS E
    JOIN 
        T_POSICIONES P ON E.ID_PUESTO = P.ID_PUESTO
		inner join 
		T_ROLES R ON E.ID_ROL =R.ID_ROL

    WHERE 
        E.ID_EMPLEADO = @ID_EMPLEADO;
END;
GO
/****** Object:  StoredProcedure [dbo].[PagarVentaTemporal]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PagarVentaTemporal]
    @ConsecutivoUsuario BIGINT,
    @Resultado INT OUTPUT -- Variable de salida para indicar éxito (1) o error (0)
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Validar el stock agrupando por producto
        IF EXISTS (
            SELECT 1
            FROM (
                SELECT ID_PRODUCTO, SUM(Cantidad) AS TotalCantidad
                FROM VentaTemporal
                WHERE Consecutivo = @ConsecutivoUsuario
                GROUP BY ID_PRODUCTO
            ) AS VT
            INNER JOIN T_PRODUCTOS P ON VT.ID_PRODUCTO = P.ID_PRODUCTO
            WHERE VT.TotalCantidad > P.CANTIDAD_DISPONIBLE
        )
        BEGIN
            ROLLBACK TRANSACTION;
            SET @Resultado = 0; -- Operación fallida
            RETURN;
        END;

        -- Insertar en la tabla de factura (encabezado)
        INSERT INTO tFactura (ConsecutivoUsuario, Total, Fecha)
        SELECT Consecutivo, SUM(VT.Cantidad * P.Precio), GETDATE()
        FROM VentaTemporal VT
        INNER JOIN T_PRODUCTOS P ON VT.ID_PRODUCTO = P.ID_PRODUCTO
        WHERE VT.Consecutivo = @ConsecutivoUsuario
        GROUP BY VT.Consecutivo;

        -- Obtener el ID de la factura recién creada
        DECLARE @ID_FACTURA BIGINT = SCOPE_IDENTITY();

        -- Insertar en la tabla de detalles agrupando por producto
        INSERT INTO tDetalle (ConsecutivoFactura, ConsecutivoProducto, Precio, Cantidad, Total)
        SELECT 
            @ID_FACTURA, 
            VT.ID_PRODUCTO, 
            P.Precio, 
            SUM(VT.Cantidad) AS TotalCantidad, 
            SUM(VT.Cantidad * P.Precio) AS TotalPrecio
        FROM VentaTemporal VT
        INNER JOIN T_PRODUCTOS P ON VT.ID_PRODUCTO = P.ID_PRODUCTO
        WHERE VT.Consecutivo = @ConsecutivoUsuario
        GROUP BY VT.ID_PRODUCTO, P.Precio;

        -- Reducir el inventario agrupando por producto
        UPDATE P
        SET P.CANTIDAD_DISPONIBLE = P.CANTIDAD_DISPONIBLE - VT.TotalCantidad
        FROM T_PRODUCTOS P
        INNER JOIN (
            SELECT ID_PRODUCTO, SUM(Cantidad) AS TotalCantidad
            FROM VentaTemporal
            WHERE Consecutivo = @ConsecutivoUsuario
            GROUP BY ID_PRODUCTO
        ) AS VT ON P.ID_PRODUCTO = VT.ID_PRODUCTO;

        -- Limpiar la tabla temporal
        DELETE FROM VentaTemporal
        WHERE Consecutivo = @ConsecutivoUsuario;

        COMMIT TRANSACTION;
        SET @Resultado = 1; -- Operación exitosa
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0; -- Operación fallida
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[RecuperarClave]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RecuperarClave]
    @EMAIL NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NUEVA_CONTRASENA NVARCHAR(10);
    DECLARE @ID_EMPLEADO INT;

    -- Verificar si el correo existe en la base de datos
    SELECT @ID_EMPLEADO = ID_EMPLEADO 
    FROM SuiteGestionIsari.dbo.T_EMPLEADOS
    WHERE EMAIL = @EMAIL;

    IF @ID_EMPLEADO IS NOT NULL
    BEGIN
        -- Generar una nueva contraseña temporal aleatoria
        SET @NUEVA_CONTRASENA = LEFT(CONVERT(NVARCHAR(50), NEWID()), 10);

        -- Actualizar la contraseña en la base de datos
        UPDATE SuiteGestionIsari.dbo.T_EMPLEADOS
        SET CONTRASENA = @NUEVA_CONTRASENA,
            CONTRASENA_TEMPORAL = 1,
            VIGENCIA_CONTRASENA = DATEADD(DAY, 1, GETDATE())
        WHERE ID_EMPLEADO = @ID_EMPLEADO;

        -- Retornar la nueva contraseña generada
        SELECT 'Su nueva contraseña temporal es: ' + @NUEVA_CONTRASENA AS NUEVA_CONTRASENA;
    END
    ELSE
    BEGIN
        SELECT 'El correo ingresado no está registrado.' AS MENSAJE_ERROR;
    END;
END;
GO
/****** Object:  StoredProcedure [dbo].[RegistrarHorario]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarHorario]
    @ID_EMPLEADO BIGINT,
    @DIA_SEMANA NVARCHAR(50),
    @HORA_ENTRADA TIME,
    @HORA_SALIDA TIME,
    @FECHA_ENTRADA DATE
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM SuiteGestionIsari.dbo.T_EMPLEADOS WHERE ID_EMPLEADO = @ID_EMPLEADO)
    BEGIN
        RAISERROR('El empleado no existe.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        INSERT INTO SuiteGestionIsari.dbo.T_HORARIOS 
            (ID_EMPLEADO, DIA_SEMANA, HORA_ENTRADA, HORA_SALIDA, FECHA_ENTRADA)
        VALUES 
            (@ID_EMPLEADO, @DIA_SEMANA, @HORA_ENTRADA, @HORA_SALIDA, @FECHA_ENTRADA);

        SELECT 1 AS Success;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[ValidarUsuario]    Script Date: 13/4/2025 13:14:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ValidarUsuario]
	@EMAIL	varchar(80)
AS
BEGIN
	
	SELECT	U.ID_EMPLEADO,
			CEDULA,
			Nombre,
			EMAIL,
			ESTADO,
			U.ID_ROL,
			R.DESCRIPCION
	  FROM	dbo.T_EMPLEADOS U
	  INNER JOIN dbo.T_ROLES R ON U.ID_ROL = R.ID_ROL
	  WHERE	EMAIL = @EMAIL

END
GO





-----------CODIGOS DASBOARD

CREATE PROCEDURE [dbo].[ConsultarVentasMensuales]

AS
BEGIN

	SET LANGUAGE Spanish

	SELECT Mes, TotalMes, Año
FROM (
    SELECT 
        DATENAME(MONTH, DATEADD(MONTH, MONTH(Fecha), -1)) AS 'Mes',
        SUM(Total) AS 'TotalMes',
        MONTH(Fecha) AS 'NumeroMes',
        YEAR(Fecha) AS 'Año'
    FROM tFactura
    GROUP BY 
        DATENAME(MONTH, DATEADD(MONTH, MONTH(Fecha), -1)), 
        MONTH(Fecha), 
        YEAR(Fecha)
) X
ORDER BY X.Año, X.NumeroMes;

END





CREATE PROCEDURE ConsultarVentasPorProducto
AS
BEGIN
    SELECT TOP 5
        P.Nombre AS Producto,
        SUM(DF.Cantidad * DF.Precio) AS TotalVentas
    FROM 
        tDetalle DF
    INNER JOIN 
        T_Productos P ON DF.ConsecutivoProducto = P.ID_Producto
    GROUP BY 
        P.Nombre
    ORDER BY 
        TotalVentas DESC;
END;



CREATE PROCEDURE ConsultarVentasPorCategoria
AS
BEGIN
    SELECT 
        C.DESCRIPCION AS Categoria,
        SUM(DF.Cantidad * DF.Precio) AS TotalVentasCategoria
    FROM 
        tDetalle DF
    INNER JOIN 
        T_PRODUCTOS P ON DF.ConsecutivoProducto = P.ID_Producto
    INNER JOIN 
        CATEGORIA_PRODUCTOS C ON P.ID_Categoria = C.ID_Categoria
    GROUP BY 
        C.DESCRIPCION
    ORDER BY 
        TotalVentasCategoria DESC;
END;


CREATE PROCEDURE DATOSCuentasPorPagar
AS
BEGIN
    SELECT
        -- Total de cuentas por pagar (estado 0)
        (SELECT COUNT(ID_CUENTA) FROM T_CUENTAS WHERE Estado = 0) AS TotalPorPagar,
        
        -- Total de cuentas pagadas (estado 2)
        (SELECT COUNT(ID_CUENTA) FROM T_CUENTAS WHERE Estado = 2) AS TotalPagadas
END


-- Procedimiento para Cuentas por Cobrar
CREATE PROCEDURE DATOSCuentasPorCobrar
AS
BEGIN
    SELECT
        -- Total de cuentas por cobrar (estado 1)
        (SELECT COUNT(ID_CUENTA) FROM T_CUENTAS WHERE Estado = 1) AS TotalPorCobrar,
        
        -- Total de cuentas cobradas (estado 3)
        (SELECT COUNT(ID_CUENTA) FROM T_CUENTAS WHERE Estado = 3) AS TotalCobradas
END


--vacaciones por empleado
create PROCEDURE VacacionesPorEmpleado

  @ID_EMPLEADO INT
AS
BEGIN
    SELECT ID_SOLICITUD,FECHA_INICIO,FECHA_FIN,MOTIVO,ESTADO,DIAS_SOLICITADOS,FECHA_SOLICITUD FROM SOLICITUD_VACACIONES WHERE ID_EMPLEADO =@ID_EMPLEADO
        
END



---VACACIONES DE TODOS LOS EMPLEADOS
CREATE PROCEDURE VerSolicitudesVacaciones

AS
BEGIN
    SELECT E.NOMBRE,ID_SOLICITUD,FECHA_INICIO,FECHA_FIN,MOTIVO,SV.ESTADO,DIAS_SOLICITADOS,FECHA_SOLICITUD 
	FROM SOLICITUD_VACACIONES SV 
	INNER JOIN T_EMPLEADOS E ON SV.ID_EMPLEADO =E.ID_EMPLEADO
      
END


--ACTUALIZAR ESTE SP


ALTER PROCEDURE [dbo].[CrearSolicitudVacaciones]

    @ID_EMPLEADO BIGINT,

    @FECHA_INICIO DATE,

    @FECHA_FIN DATE,

    @DIAS_SOLICITADOS INT,

    @ESTADO VARCHAR(50),

    @FECHA_SOLICITUD DATEtime,

    @MOTIVO NVARCHAR(255),

    @DIAS_TOTALES INT

AS

BEGIN

    SET NOCOUNT ON;
 
    IF @DIAS_SOLICITADOS <= @DIAS_TOTALES

    BEGIN

        INSERT INTO SuiteGestionIsari.dbo.SOLICITUD_VACACIONES 

            (ID_EMPLEADO, FECHA_INICIO, FECHA_FIN, DIAS_SOLICITADOS, ESTADO, FECHA_SOLICITUD, MOTIVO, DIAS_TOTALES)

        VALUES 

            (@ID_EMPLEADO, @FECHA_INICIO, @FECHA_FIN, @DIAS_SOLICITADOS, @ESTADO, @FECHA_SOLICITUD, @MOTIVO, @DIAS_TOTALES);
 
        PRINT 'Solicitud almacenada correctamente';

    END

    ELSE

    BEGIN

        RAISERROR ('No tienes suficientes días acumulados.', 16, 1);

    END

END;

