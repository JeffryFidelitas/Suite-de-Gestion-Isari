/*CREATE TABLE SuiteGestionIsari.dbo.CATEGORIA_PRODUCTOS (
	ID_CATEGORIA int IDENTITY(1,1) NOT NULL,
	DESCRIPCION nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK__CATEGORI__4BD51FA56F205036 PRIMARY KEY (ID_CATEGORIA)
);



CREATE TABLE SuiteGestionIsari.dbo.CLIENTES (
	ID_CLIENTE int IDENTITY(1,1) NOT NULL,
	NOMBRE nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	P_APELLIDO nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	S_APELLIDO nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CELULAR nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CORREO nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK__CLIENTES__23A341307D173CDB PRIMARY KEY (ID_CLIENTE)
);


CREATE TABLE SuiteGestionIsari.dbo.T_HORARIOS (
	ID_HORARIO int IDENTITY(1,1) NOT NULL,
	DIA_SEMANA nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	HORA_ENTRADA time NOT NULL,
	HORA_SALIDA time NOT NULL,
	CONSTRAINT PK__T_HORARI__CDBBBA34700360E8 PRIMARY KEY (ID_HORARIO)
);




CREATE TABLE SuiteGestionIsari.dbo.T_ROLES (
	ID_ROL int IDENTITY(1,1) NOT NULL,
	DESCRIPCION nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK__T_ROLES__203B0F685B2887EE PRIMARY KEY (ID_ROL)
);




CREATE TABLE SuiteGestionIsari.dbo.T_PRODUCTOS (
	ID_PRODUCTO int IDENTITY(1,1) NOT NULL,
	NOMBRE nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	DESCRIPCION nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PROVEEDOR nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PRECIO decimal(10,2) NOT NULL,
	CANTIDAD_DISPONIBLE int NOT NULL,
	ID_CATEGORIA int NOT NULL,
	CONSTRAINT PK__PRODUCTO__88BD03576E55F2C6 PRIMARY KEY (ID_PRODUCTO),
	CONSTRAINT FK__PRODUCTOS__ID_CA__6477ECF3 FOREIGN KEY (ID_CATEGORIA) REFERENCES SuiteGestionIsari.dbo.CATEGORIA_PRODUCTOS(ID_CATEGORIA)
);



CREATE TABLE SuiteGestionIsari.dbo.FACTURA (
	ID_FACTURA int IDENTITY(1,1) NOT NULL,
	FECHA_VENTA date NOT NULL,
	ID_CLIENTE int NOT NULL,
	ID_PRODUCTO int NOT NULL,
	CANTIDAD int NOT NULL,
	PRECIO decimal(10,2) NOT NULL,
	TOTAL decimal(10,2) NOT NULL,
	CONSTRAINT PK__FACTURA__4A921BED092F219C PRIMARY KEY (ID_FACTURA),
	CONSTRAINT FK__FACTURA__ID_CLIE__628FA481 FOREIGN KEY (ID_CLIENTE) REFERENCES SuiteGestionIsari.dbo.CLIENTES(ID_CLIENTE),
	CONSTRAINT FK__FACTURA__ID_PROD__6383C8BA FOREIGN KEY (ID_PRODUCTO) REFERENCES SuiteGestionIsari.dbo.T_PRODUCTOS(ID_PRODUCTO)
);



CREATE TABLE SuiteGestionIsari.dbo.SOLICITUD_VACACIONES (
	ID_SOLICITUD int IDENTITY(1,1) NOT NULL,
	ID_EMPLEADO int NOT NULL,
	FECHA_SOLICITUD date NOT NULL,
	FECHA_FINAL date NOT NULL,
	CANTIDAD_DIAS int NOT NULL,
	ESTADO nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK__SOLICITU__F090D584D002E257 PRIMARY KEY (ID_SOLICITUD)
);



CREATE TABLE SuiteGestionIsari.dbo.T_EMPLEADOS (
	ID_EMPLEADO bigint IDENTITY(1,1) NOT NULL,
	CEDULA nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	NOMBRE nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	EMAIL nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ESTADO bit NOT NULL,
	CONTRASENA nvarchar(255)NOT NULL,
	CONTRASENA_TEMPORAL BIT NOT NULL,
	FECHA_CONTRATACION date NOT NULL,
	TELEFONO nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ID_ROL int NULL,
	ID_PUESTO int NULL,
	VIGENCIA_CONTRASENA DATETIME,
	CONSTRAINT PK__T_EMPLEA__922CA2699C98B6D9 PRIMARY KEY (ID_EMPLEADO)
);


ALTER TABLE SuiteGestionIsari.dbo.T_EMPLEADOS ADD CONSTRAINT FK__T_EMPLEAD__ID_PU__5DCAEF64 FOREIGN KEY (ID_PUESTO) REFERENCES SuiteGestionIsari.dbo.T_POSICIONES(ID_PUESTO);
ALTER TABLE SuiteGestionIsari.dbo.T_EMPLEADOS ADD CONSTRAINT FK__T_EMPLEAD__ID_RO__5CD6CB2B FOREIGN KEY (ID_ROL) REFERENCES SuiteGestionIsari.dbo.T_ROLES(ID_ROL);




CREATE TABLE SuiteGestionIsari.dbo.T_ERRORES (
	ID_ERROR bigint IDENTITY(1,1) NOT NULL,
	ID_EMPLEADO int NOT NULL,
	DESCRIPCION nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	FECHA datetime NOT NULL,
	CONSTRAINT PK__T_ERRORE__9046136AC6A55B3F PRIMARY KEY (ID_ERROR)
);



CREATE TABLE SuiteGestionIsari.dbo.T_POSICIONES (
	ID_PUESTO int IDENTITY(1,1) NOT NULL,
	NOMBRE_POSICION nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	DESCRIPCION nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	SALARIO decimal(10,2) NOT NULL,	
	CONSTRAINT PK__T_POSICI__88D8DDB14593B656 PRIMARY KEY (ID_PUESTO)
);



CREATE TABLE SuiteGestionIsari.dbo.T_VACACIONES (
	ID_VACACION int IDENTITY(1,1) NOT NULL,
	ID_EMPLEADO int NOT NULL,
	FECHA_INICIO date NOT NULL,
	FECHA_FIN date NOT NULL,
	DIAS_TOTALES int NOT NULL,
	CONSTRAINT PK__T_VACACI__262D75615C90A401 PRIMARY KEY (ID_VACACION)
);



ALTER TABLE SuiteGestionIsari.dbo.SOLICITUD_VACACIONES ADD CONSTRAINT FK__SOLICITUD__ID_EM__619B8048 FOREIGN KEY (ID_EMPLEADO) REFERENCES SuiteGestionIsari.dbo.T_EMPLEADOS(ID_EMPLEADO);

ALTER TABLE SuiteGestionIsari.dbo.T_EMPLEADOS ADD CONSTRAINT FK__T_EMPLEAD__ID_PU__5DCAEF64 FOREIGN KEY (ID_PUESTO) REFERENCES SuiteGestionIsari.dbo.T_POSICIONES(ID_PUESTO);
ALTER TABLE SuiteGestionIsari.dbo.T_EMPLEADOS ADD CONSTRAINT FK__T_EMPLEAD__ID_RO__5CD6CB2B FOREIGN KEY (ID_ROL) REFERENCES SuiteGestionIsari.dbo.T_ROLES(ID_ROL);
ALTER TABLE SuiteGestionIsari.dbo.T_EMPLEADOS ADD CONSTRAINT FK__T_EMPLEAD__ID_VA__5EBF139D FOREIGN KEY (ID_VACACION) REFERENCES SuiteGestionIsari.dbo.T_VACACIONES(ID_VACACION);


ALTER TABLE SuiteGestionIsari.dbo.T_ERRORES ADD CONSTRAINT FK__T_ERRORES__ID_EM__5FB337D6 FOREIGN KEY (ID_EMPLEADO) REFERENCES SuiteGestionIsari.dbo.T_EMPLEADOS(ID_EMPLEADO);


ALTER TABLE SuiteGestionIsari.dbo.T_VACACIONES ADD CONSTRAINT FK__T_VACACIO__ID_EM__60A75C0F FOREIGN KEY (ID_EMPLEADO) REFERENCES SuiteGestionIsari.dbo.T_EMPLEADOS(ID_EMPLEADO);
------------------------------------------------------------------------------------------Codigo base---------------------------------------------------------------------------------------------

------------------------------------------------------------------------------------------Inicio SP---------------------------------------------------------------------------------------------


-- -------------------------- Agregar Puesto -------------------------
CREATE PROCEDURE AgregarPuesto
    @NOMBRE_POSICION NVARCHAR(255),
    @DESCRIPCION NVARCHAR(500) = NULL,
    @SALARIO DECIMAL(18,2) = 0
AS
BEGIN   
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM T_POSICIONES WHERE NOMBRE_POSICION = @NOMBRE_POSICION)
    BEGIN
        BEGIN TRANSACTION;
        INSERT INTO T_POSICIONES (NOMBRE_POSICION, DESCRIPCION, SALARIO)
        VALUES (@NOMBRE_POSICION, @DESCRIPCION, @SALARIO);
        COMMIT;
        RETURN 1;
    END 
END;

-- -------------------------- Obtener Puestos -------------------------
CREATE PROCEDURE ObtenerPuestos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_PUESTO, NOMBRE_POSICION, DESCRIPCION, SALARIO
    FROM T_POSICIONES;
END;

-- -------------------------- Consulta Puesto Individual -------------------------
CREATE PROCEDURE ConsultaPuesto
    @ID_PUESTO INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_PUESTO, NOMBRE_POSICION, DESCRIPCION, SALARIO
    FROM T_POSICIONES
    WHERE ID_PUESTO = @ID_PUESTO;
END;

-- -------------------------- Actualizar Puesto -------------------------
CREATE PROCEDURE ActualizarPuesto
    @ID_PUESTO INT,
    @NOMBRE_POSICION NVARCHAR(255),
    @DESCRIPCION NVARCHAR(500),
    @SALARIO DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM T_POSICIONES 
                   WHERE NOMBRE_POSICION = @NOMBRE_POSICION
                   AND ID_PUESTO != @ID_PUESTO)
    BEGIN
        BEGIN TRANSACTION;
        UPDATE T_POSICIONES
        SET 
            NOMBRE_POSICION = @NOMBRE_POSICION,
            DESCRIPCION = @DESCRIPCION,
            SALARIO = @SALARIO
        WHERE ID_PUESTO = @ID_PUESTO;
        COMMIT;
        RETURN 1;
    END
END;

-- -------------------------- Agregar Producto -------------------------
CREATE PROCEDURE AgregarProducto
    @NOMBRE NVARCHAR(255),
    @DESCRIPCION NVARCHAR(500) = NULL,
    @PROVEEDOR NVARCHAR(255),
    @PRECIO DECIMAL(18,2),
    @CANTIDAD_DISPONIBLE INT,
    @ID_CATEGORIA INT
AS
BEGIN   
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM T_PRODUCTOS WHERE NOMBRE = @NOMBRE)
    BEGIN
        BEGIN TRANSACTION;
        INSERT INTO T_PRODUCTOS (NOMBRE, DESCRIPCION, PROVEEDOR, PRECIO, CANTIDAD_DISPONIBLE, ID_CATEGORIA)
        VALUES (@NOMBRE, @DESCRIPCION, @PROVEEDOR, @PRECIO, @CANTIDAD_DISPONIBLE, @ID_CATEGORIA);
        COMMIT;
        RETURN 1;
    END 
END;

-- -------------------------- Obtener Productos -------------------------
CREATE PROCEDURE ObtenerProductos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_PRODUCTO, NOMBRE, DESCRIPCION, PROVEEDOR, PRECIO, CANTIDAD_DISPONIBLE, ID_CATEGORIA
    FROM T_PRODUCTOS;
END;

-- -------------------------- Consulta Producto Individual -------------------------
CREATE PROCEDURE ConsultaProducto
    @ID_PRODUCTO INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_PRODUCTO, NOMBRE, DESCRIPCION, PROVEEDOR, PRECIO, CANTIDAD_DISPONIBLE, ID_CATEGORIA
    FROM T_PRODUCTOS
    WHERE ID_PRODUCTO = @ID_PRODUCTO;
END;

-- -------------------------- Actualizar Producto -------------------------
CREATE PROCEDURE ActualizarProducto
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

    IF NOT EXISTS (SELECT 1 FROM T_PRODUCTOS 
                   WHERE NOMBRE = @NOMBRE
                   AND ID_PRODUCTO != @ID_PRODUCTO)
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
        RETURN 1;
    END
END;

-- -------------------------- Eliminar Producto -------------------------
CREATE PROCEDURE EliminarProducto
    @ID_PRODUCTO INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM T_PRODUCTOS WHERE ID_PRODUCTO = @ID_PRODUCTO)
    BEGIN
        BEGIN TRANSACTION;
        DELETE FROM T_PRODUCTOS WHERE ID_PRODUCTO = @ID_PRODUCTO;
        COMMIT;
        RETURN 1;
    END
END;

-- -------------------------- Agregar Categoría -------------------------
CREATE PROCEDURE AgregarCategoria
    @DESCRIPCION NVARCHAR(255)
AS
BEGIN   
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM CATEGORIA_PRODUCTOS WHERE DESCRIPCION = @DESCRIPCION)
    BEGIN
        BEGIN TRANSACTION;
        INSERT INTO CATEGORIA_PRODUCTOS (DESCRIPCION)
        VALUES (@DESCRIPCION);
        COMMIT;
        RETURN 1;
    END 
END;

-- -------------------------- Obtener Categorías -------------------------
CREATE PROCEDURE ObtenerCategorias
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_CATEGORIA, DESCRIPCION
    FROM CATEGORIA_PRODUCTOS;
END;

-- -------------------------- Consulta Categoría Individual -------------------------
CREATE PROCEDURE ConsultaCategoria
    @ID_CATEGORIA INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_CATEGORIA, DESCRIPCION
    FROM CATEGORIA_PRODUCTOS
    WHERE ID_CATEGORIA = @ID_CATEGORIA;
END;

-- -------------------------- Actualizar Categoría -------------------------
CREATE PROCEDURE ActualizarCategoria
    @ID_CATEGORIA INT,
    @DESCRIPCION NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM CATEGORIA_PRODUCTOS 
                   WHERE DESCRIPCION = @DESCRIPCION 
                   AND ID_CATEGORIA != @ID_CATEGORIA)
    BEGIN
        BEGIN TRANSACTION;
        UPDATE CATEGORIA_PRODUCTOS
        SET DESCRIPCION = @DESCRIPCION
        WHERE ID_CATEGORIA = @ID_CATEGORIA;
        COMMIT;
        RETURN 1;
    END
END;

-- ---------------------------- Eliminar Categoria -------------------------------
CREATE PROCEDURE EliminarCategoria
    @ID_CATEGORIA INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM CATEGORIA_PRODUCTOS WHERE ID_CATEGORIA = @ID_CATEGORIA)
    BEGIN
    	BEGIN TRANSACTION;
        DELETE FROM T_PRODUCTOS WHERE ID_CATEGORIA = @ID_CATEGORIA;
        DELETE FROM CATEGORIA_PRODUCTOS WHERE ID_CATEGORIA = @ID_CATEGORIA;
        COMMIT;
        RETURN 1;
    END
END;

----------------Crear Empleado------------------------------
CREATE PROCEDURE CrearEmpleado
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

---Consulta empleados----------
Create PROCEDURE [dbo].[ConsultarEmpleados]
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
		R.DESCRIPCION
	FROM 
            T_EMPLEADOS E

			INNER JOIN T_POSICIONES P
			ON E.ID_PUESTO =P.ID_PUESTO
			INNER JOIN T_ROLES R
			ON E.ID_ROL =R.ID_ROL
END


---------Selec de roles en el sistema---------

create procedure LeerRoles 

as
Begin

select ID_ROL,DESCRIPCION FROM T_ROLES

END



--------------Login------------

CREATE PROCEDURE IniciarSesion
    @EMAIL NVARCHAR(255),
    @CONTRASENA NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ID_EMPLEADO, NOMBRE, EMAIL, ESTADO,ID_ROL,CONTRASENA_TEMPORAL,VIGENCIA_CONTRASENA
    FROM T_EMPLEADOS
    WHERE Email = @EMAIL AND Contrasena = @CONTRASENA AND ESTADO=1;
END

----------ActualizarEmpleado------------
-- Procedimiento para actualizar datos del usuario
-- Procedimiento para actualizar datos del usuario
CREATE PROCEDURE ActualizarUsuario
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



--  Procedimiento para obtener datos del usuario
create PROCEDURE ObtenerUsuarioPorID
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
		P.ID_PUESTO
    FROM 
        T_EMPLEADOS E
    JOIN 
        T_POSICIONES P ON E.ID_PUESTO = P.ID_PUESTO
		inner join 
		T_ROLES R ON E.ID_ROL =R.ID_ROL

    WHERE 
        E.ID_EMPLEADO = @ID_EMPLEADO;
END;





------ActualizarEmpleado--------
Create PROCEDURE ActualizarEmpleado
    @ID_EMPLEADO INT,
    @NOMBRE NVARCHAR(100),
    @CEDULA NVARCHAR(20),
    @EMAIL NVARCHAR(100),
    @ID_ROL INT,
    @ID_PUESTO INT,
    @TELEFONO NVARCHAR(50)
AS
BEGIN
    UPDATE T_EMPLEADOS
    SET 
        NOMBRE = @NOMBRE,
        CEDULA = @CEDULA,
        EMAIL = @EMAIL,
        ID_ROL = @ID_ROL,
        ID_PUESTO = @ID_PUESTO,
        TELEFONO = @TELEFONO    
    WHERE ID_EMPLEADO = @ID_EMPLEADO;
END


----datos perfil logueado
CREATE PROCEDURE ObtenerUsuariologueado
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



--Metodo para recuperar contraseña----
create PROCEDURE [dbo].[ActualizarContrasenna]
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

--Metodo para validar usuario recuperar contraseña----
create PROCEDURE [dbo].[ValidarUsuario]
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
-----Actualizar días de vacaciones--------------

CREATE PROCEDURE ActualizarEstadoSolicitud
    @ID_SOLICITUD INT,
    @ESTADO VARCHAR(50)
AS
BEGIN
    UPDATE SuiteGestionIsari.dbo.SOLICITUD_VACACIONES
    SET ESTADO = @ESTADO
    WHERE ID_SOLICITUD = @ID_SOLICITUD;
END;


---------------tabla nueva productos
CREATE TABLE [dbo].[T_PRODUCTOS](
	[ID_PRODUCTO] [int] IDENTITY(1,1) NOT NULL,
	[NOMBRE] [nvarchar](255) NOT NULL,
	[DESCRIPCION] [nvarchar](255) NULL,
	[PROVEEDOR] [nvarchar](255) NULL,
	[PRECIO] [decimal](10, 2) NOT NULL,
	[CANTIDAD_DISPONIBLE] [int] NOT NULL,
	[ID_CATEGORIA] [int] NOT NULL,
	[CODIGO_PRODUCTO] [nvarchar](50) NULL,
 CONSTRAINT [PK__PRODUCTO__88BD03576E55F2C6] PRIMARY KEY CLUSTERED 
(
	[ID_PRODUCTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-----------tabla detalle factura
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


---tabla factura----------
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

----tabla venta temporal---
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
ALTER TABLE [dbo].[tFactura] ADD  CONSTRAINT [DF__tFactura__Fecha__1BC821DD]  DEFAULT (getdate()) FOR [Fecha]
GO
ALTER TABLE [dbo].[VentaTemporal] ADD  CONSTRAINT [DF__VentaTemp__Fecha__123EB7A3]  DEFAULT (getdate()) FOR [FechaCreacion]
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



-----sp actualizar venta temporal
/****** Object:  StoredProcedure [dbo].[ActualizarTotalVentaTemporal]    Script Date: 10/3/2025 23:15:38 ******/
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

/****** Object:  StoredProcedure [dbo].[AgregarVentaTemporal]    Script Date: 10/3/2025 23:15:38 ******/
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
/****** Object:  StoredProcedure [dbo].[ConssultarExistenciaProducto]    Script Date: 10/3/2025 23:15:38 ******/
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
/****** Object:  StoredProcedure [dbo].[ConsultaCategoria]    Script Date: 10/3/2025 23:15:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[ConsultaProducto]    Script Date: 10/3/2025 23:15:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultaProducto]
    @ID_PRODUCTO INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_PRODUCTO, NOMBRE, DESCRIPCION, PROVEEDOR, PRECIO, CANTIDAD_DISPONIBLE, ID_CATEGORIA
    FROM T_PRODUCTOS
    WHERE ID_PRODUCTO = @ID_PRODUCTO;
END;
GO



/****** Object:  StoredProcedure [dbo].[ConsultarFacturas]    Script Date: 10/3/2025 23:15:38 ******/
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

/****** Object:  StoredProcedure [dbo].[ObtenerDetalleVentaTemporal]    Script Date: 10/3/2025 23:15:38 ******/
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
/****** Object:  StoredProcedure [dbo].[ObtenerMontoTotalVentaTemporal]    Script Date: 10/3/2025 23:15:38 ******/
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


/****** Object:  StoredProcedure [dbo].[PagarVentaTemporal]    Script Date: 10/3/2025 23:15:38 ******/
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



