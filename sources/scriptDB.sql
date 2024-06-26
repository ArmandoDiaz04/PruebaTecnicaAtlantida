USE [master]
GO

CREATE DATABASE [PruebaTecnicaAtlantida]
 
ALTER DATABASE [PruebaTecnicaAtlantida] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PruebaTecnicaAtlantida].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TarjetaCreditoDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TarjetaCreditoDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET  MULTI_USER 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [PruebaTecnicaAtlantida] SET DELAYED_DURABILITY = DISABLED 
GO
USE [PruebaTecnicaAtlantida]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MovimientosTarjeta](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[IdTarjeta] [int] NOT NULL,
	[Descripcion] [text] NULL,
	[FechaMovimiento] [datetime] NULL,
	[Monto] [money] NULL,
	[TipoMovimiento] [char](1) NULL,
	[fechaCreacion] [datetime] NULL,
	[creadoPor] [varchar](50) NULL,
	[fechaModificacion] [datetime] NULL,
	[modificadoPor] [varchar](50) NULL,
	[status] [char](1) NULL,
 CONSTRAINT [PK_MovimientosTarjeta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TarjetasCredito](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[NombreTitular] [varchar](50) NULL,
	[NumeroTarjeta] [varchar](50) NULL,
	[MontoOtorgado] [money] NULL,
	[PorceInteres] [decimal](12, 2) NULL,
	[PorceInteresMinimo] [decimal](12, 2) NULL,
	[fechaCreacion] [datetime] NULL,
	[creadoPor] [varchar](50) NULL,
	[fechaModificacion] [datetime] NULL,
	[modificadoPor] [varchar](50) NULL,
	[status] [char](1) NULL,
 CONSTRAINT [PK_TarjetasCredito] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[MovimientosTarjeta] ON 

INSERT [dbo].[TarjetasCredito] ([id], [NombreTitular], [NumeroTarjeta], [MontoOtorgado], [PorceInteres], [PorceInteresMinimo], [fechaCreacion], [creadoPor], [fechaModificacion], [modificadoPor], [status]) VALUES (1, N'Armando Diaz', N'4550-0606-1509-0099', 2200.0000, CAST(25.00 AS Decimal(12, 2)), CAST(5.00 AS Decimal(12, 2)), CAST(N'2024-04-05 00:00:00.000' AS DateTime), N'cguerrero', NULL, NULL, N'1')
SET IDENTITY_INSERT [dbo].[TarjetasCredito] OFF
ALTER TABLE [dbo].[MovimientosTarjeta]  WITH CHECK ADD  CONSTRAINT [FK_MovimientosTarjeta_TarjetasCredito] FOREIGN KEY([IdTarjeta])
REFERENCES [dbo].[TarjetasCredito] ([id])
GO
ALTER TABLE [dbo].[MovimientosTarjeta] CHECK CONSTRAINT [FK_MovimientosTarjeta_TarjetasCredito]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ObtenerMovimientosTarjeta] 
	@IdTarjeta as int,
	@NumeroTarjeta varchar(50),
	@Tipo as int,
	@mes as int,
	@anio as int
AS
BEGIN

	SET NOCOUNT ON;
	
	IF @Tipo = 1
	BEGIN 
	select id, idTarjeta, Descripcion, FechaMovimiento, Monto,
		CASE TipoMovimiento
		WHEN 1 THEN 'Compra'
		WHEN 2 THEN 'Pago'
		ELSE 'Otro'
	END AS 'TipoMovimiento'
	from MovimientosTarjeta where IdTarjeta = @IdTarjeta and
	MONTH(FechaMovimiento) = @mes and YEAR(FechaMovimiento) = @anio and TipoMovimiento = 1
	ORDER BY FechaMovimiento desc
	END
	ELSE IF @Tipo = 2
	BEGIN
		select id, idTarjeta, Descripcion, FechaMovimiento, Monto, 
		CASE TipoMovimiento
		WHEN 1 THEN 'Compra'
		WHEN 2 THEN 'Pago'
		ELSE 'Otro'
	END AS 'TipoMovimiento'
	from MovimientosTarjeta where IdTarjeta = @IdTarjeta and
	MONTH(FechaMovimiento) = @mes and YEAR(FechaMovimiento) = @anio  and TipoMovimiento = 2
	ORDER BY FechaMovimiento desc
	END 
	ELSE
	BEGIN
		select id, idTarjeta, Descripcion, FechaMovimiento, Monto,
		CASE TipoMovimiento
		WHEN 1 THEN 'Compra'
		WHEN 2 THEN 'Pago'
		ELSE 'Otro'
	END AS 'TipoMovimientoNombre'
	from MovimientosTarjeta where IdTarjeta = @IdTarjeta and
	MONTH(FechaMovimiento) = @mes
	and YEAR(FechaMovimiento) = @anio 
	ORDER BY FechaMovimiento desc
	END
END

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ObtenerValoresTarjeta]
	@IdTarjeta int,
	@NumeroTarjeta varchar(50)
AS
BEGIN
	
	SET NOCOUNT ON;
--- Variables a utilizar
	DECLARE @SaldoTotal as money
	DECLARE @InteresBonificable as money
	DECLARE @CuotaMinima as money
	DECLARE @TotalContadoConInteres as money
	DECLARE @SaldoDisponible as money
	DECLARE @MontoOtorgado as money

--- Porcentajes de interes
	DECLARE @PorceInteres as decimal(12,2)
	DECLARE @PorcentajeSaldoMinimo as decimal(12,2)

--- Obtendremos el saldo actual
	select 
	@SaldoTotal = sum(Monto) 
	from MovimientosTarjeta where IdTarjeta = @IdTarjeta

	if @SaldoTotal = 0
	BEGIN
		SET @SaldoTotal = 0
		SET @CuotaMinima = 0
		SET @InteresBonificable = 0
		SET @TotalContadoConInteres = 0
	END

--- Una vez obtenido el saldo de las compras y pagos realizados en la tarjeta
--- Procederemos a calcular los demas valores
--- Antes llenaremos variables de la tarjeta como los porcentajes

	select @PorceInteres = PorceInteres, @PorcentajeSaldoMinimo = PorceInteresMinimo, @MontoOtorgado = MontoOtorgado
	from TarjetasCredito where Id = @IdTarjeta and NumeroTarjeta = @NumeroTarjeta

--- Obtenemos los demas valores
	SET @CuotaMinima = (@SaldoTotal * @PorcentajeSaldoMinimo) / 100
	SET @InteresBonificable = (@SaldoTotal * @PorceInteres) / 100
	SET @TotalContadoConInteres = @SaldoTotal + @InteresBonificable
	SET @SaldoDisponible = @MontoOtorgado - @SaldoTotal

--- Imprimimos los resultados
	select *,FORMAT(@SaldoTotal, '0.00') as SaldoTotal, 
	FORMAT(@CuotaMinima, '0.00') as CuotaMinima, FORMAT(@InteresBonificable, '0.00') as InteresBonificable,
	FORMAT(@TotalContadoConInteres, '0.00') as TotalContadoConInteres, FORMAT(@SaldoDisponible, '0.00') as Disponible
	from TarjetasCredito where Id = @IdTarjeta and NumeroTarjeta = @NumeroTarjeta

END

GO
USE [master]
GO
ALTER DATABASE PruebaTecnicaAtlantida SET  READ_WRITE 
GO
