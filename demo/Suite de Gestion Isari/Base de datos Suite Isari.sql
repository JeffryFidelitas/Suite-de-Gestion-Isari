CREATE TABLE SuiteGestionIsari.dbo.CATEGORIA_PRODUCTOS (
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
    ID_EMPLEADO bigint NOT NULL,
    DIA_SEMANA nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    HORA_ENTRADA time NOT NULL,
    HORA_SALIDA time NOT NULL,
    ESTADO nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL, 
    CONSTRAINT PK_T_HORARIOS PRIMARY KEY (ID_HORARIO)
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
    ID_SOLICITUD BIGINT IDENTITY(1,1) NOT NULL,
    ID_EMPLEADO BIGINT NOT NULL,
    FECHA_INICIO DATE NOT NULL,
    FECHA_FIN DATE NOT NULL,
    DIAS_SOLICITADOS INT NOT NULL,
    ESTADO NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL DEFAULT 'Pendiente',
    FECHA_SOLICITUD DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_SOLICITUD_VACACIONES PRIMARY KEY (ID_SOLICITUD),
    CONSTRAINT FK_SOLICITUD_EMPLEADO FOREIGN KEY (ID_EMPLEADO) REFERENCES SuiteGestionIsari.dbo.T_EMPLEADOS(ID_EMPLEADO)
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

CREATE TABLE SuiteGestionIsari.dbo.T_DEVOLUCIONES (
	ID_DEVOLUCION int IDENTITY(1,1) NOT NULL,
	ID_FACTURA int NOT NULL,
	DESCRIPCION nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	DINERO decimal(10,2) NOT NULL,
	CONSTRAINT PK__T_DEVOLUCIONE__CDBAAE34710360E8 PRIMARY KEY (ID_DEVOLUCION)
);

CREATE TABLE SuiteGestionIsari.dbo.T_CUENTAS (
    ID_CUENTA int IDENTITY(1,1) NOT NULL,
    INDIVIDUO nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    DESCRIPCION nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    DINERO decimal(10,2) NOT NULL,
    VENCIMIENTO date NOT NULL,
    ESTADO int NOT NULL,
    CONSTRAINT PK__T_CUENTAS__CDF4314123E6524 PRIMARY KEY (ID_CUENTA)
)
-- Estados: 0 (Por pagar), 1 (Por Cobrar), 2 (Pagada), 3 (Cobrada)

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

    IF NOT EXISTS (SELECT 1 FROM CATEGORIAS 
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

-----------Crear Vacaciones------

CREATE PROCEDURE CrearSolicitudVacaciones
    @ID_EMPLEADO BIGINT,
    @FECHA_INICIO DATE,
    @FECHA_FIN DATE,
    @DIAS_SOLICITADOS INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @DIAS_ACUMULADOS INT;
    
    SELECT @DIAS_ACUMULADOS = DATEDIFF(DAY, FECHA_CONTRATACION, GETDATE()) / 30 
    FROM SuiteGestionIsari.dbo.T_EMPLEADOS 
    WHERE ID_EMPLEADO = @ID_EMPLEADO;

    IF @DIAS_ACUMULADOS IS NULL
    BEGIN
        RAISERROR ('El empleado no existe.', 16, 1);
        RETURN;
    END

    IF @DIAS_SOLICITADOS <= @DIAS_ACUMULADOS
    BEGIN
        INSERT INTO SuiteGestionIsari.dbo.SOLICITUD_VACACIONES 
            (ID_EMPLEADO, FECHA_INICIO, FECHA_FIN, DIAS_SOLICITADOS)
        VALUES 
            (@ID_EMPLEADO, @FECHA_INICIO, @FECHA_FIN, @DIAS_SOLICITADOS);

        PRINT 'Solicitud almacenada correctamente';
    END
    ELSE
    BEGIN
        RAISERROR ('No tienes suficientes días acumulados', 16, 1);
    END
END;

-------Registrar Horario------
CREATE PROCEDURE dbo.RegistrarHorario
    @ID_EMPLEADO BIGINT,
    @DIA_SEMANA NVARCHAR(50),
    @HORA_ENTRADA TIME,
    @HORA_SALIDA TIME
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
            (ID_EMPLEADO, DIA_SEMANA, HORA_ENTRADA, HORA_SALIDA)
        VALUES 
            (@ID_EMPLEADO, @DIA_SEMANA, @HORA_ENTRADA, @HORA_SALIDA);

        SELECT 1 AS Success; 
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS ErrorMessage;  -
    END CATCH
END;

---Actualizar Horario----

CREATE PROCEDURE dbo.ActualizarEstadoHorario
    @ID_HORARIO INT,
    @ESTADO NVARCHAR(50)
AS
BEGIN
    UPDATE SuiteGestionIsari.dbo.T_HORARIOS
    SET ESTADO = @ESTADO
    WHERE ID_HORARIO = @ID_HORARIO;
END;

-- ------- Obtener Devoluciones

CREATE PROCEDURE ObtenerDevoluciones
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_DEVOLUCION, ID_FACTURA, DESCRIPCION, DINERO
    FROM T_DEVOLUCIONES;
END;

-- ------- Agregar Devolucion

CREATE PROCEDURE AgregarDevolucion
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
----------------------cambio para mostrar el codigo----------------------------
ALTER PROCEDURE [dbo].[ObtenerProductos]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_PRODUCTO, NOMBRE, DESCRIPCION, PROVEEDOR, PRECIO, CANTIDAD_DISPONIBLE, ID_CATEGORIA,CODIGO_PRODUCTO
    FROM T_PRODUCTOS;
END;
------------------------------------------------------------------------------



CREATE PROCEDURE ObtenerIdUltimaFactura
    @UsuarioID INT,
    @IdFactura BIGINT OUTPUT
AS
BEGIN
    SELECT TOP 1 @IdFactura = ConsecutivoFactura
    FROM tFactura
    WHERE ConsecutivoUsuario = @UsuarioID
    ORDER BY Fecha DESC
END



---------------DetalleEnvioFacturaporcorreo-----------------------

Create PROCEDURE DetalleEnvioFactura
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


-------------modicion para agregar producto----------------

ALTER PROCEDURE [dbo].[AgregarProducto]
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

    -- Verificar si ya existe un producto con el mismo nombre o código
    IF NOT EXISTS (SELECT 1 FROM T_PRODUCTOS WHERE NOMBRE = @NOMBRE OR CODIGO_PRODUCTO = @CODIGO_PRODUCTO)
    BEGIN
        BEGIN TRANSACTION;
        -- Insertar nuevo producto
        INSERT INTO T_PRODUCTOS (CODIGO_PRODUCTO, NOMBRE, DESCRIPCION, PROVEEDOR, PRECIO, CANTIDAD_DISPONIBLE, ID_CATEGORIA)
        VALUES (@CODIGO_PRODUCTO, @NOMBRE, @DESCRIPCION, @PROVEEDOR, @PRECIO, @CANTIDAD_DISPONIBLE, @ID_CATEGORIA);
        COMMIT;
        RETURN 1;
    END
    ELSE
    BEGIN
        -- Si el nombre o el código ya existen, retornar 0
        RETURN 0;
    END
END;

------------------EliminarArticuloTemporal-------------------------
create PROCEDURE [dbo].[EliminarArticuloTemporal]
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

-- -------------------------- Agregar Cuenta -------------------------
CREATE PROCEDURE AgregarCuenta
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

-- -------------------------- Obtener Cuentas -------------------------
CREATE PROCEDURE ObtenerCuentas
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_CUENTA, INDIVIDUO, DESCRIPCION, DINERO, VENCIMIENTO, ESTADO
    FROM SuiteGestionIsari.dbo.T_CUENTAS;
END;

-- -------------------------- Consulta Cuenta Individual -------------------------
CREATE PROCEDURE ConsultaCuenta
    @ID_CUENTA INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_CUENTA, INDIVIDUO, DESCRIPCION, DINERO, VENCIMIENTO, ESTADO
    FROM SuiteGestionIsari.dbo.T_CUENTAS
    WHERE ID_CUENTA = @ID_CUENTA;
END;

-- -------------------------- Cambiar Estado ------------------------
CREATE PROCEDURE CambiarEstado
	@ID_CUENTA INT,
	@ESTADO INT
AS
BEGIN
	SET NOCOUNT ON;
	
	UPDATE SuiteGestionIsari.dbo.T_CUENTAS
	SET ESTADO = @ESTADO
	WHERE ID_CUENTA = @ID_CUENTA
END