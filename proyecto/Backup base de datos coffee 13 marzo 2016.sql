USE [master]
GO
/****** Object:  Database [DBFinca]    Script Date: 13/03/2016 12:07:53 p. m. ******/
CREATE DATABASE [DBFinca]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBFinca', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\DBFinca.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DBFinca_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\DBFinca_log.ldf' , SIZE = 1088KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DBFinca] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBFinca].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBFinca] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBFinca] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBFinca] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBFinca] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBFinca] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBFinca] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBFinca] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBFinca] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBFinca] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBFinca] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBFinca] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBFinca] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBFinca] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBFinca] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBFinca] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DBFinca] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBFinca] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBFinca] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBFinca] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBFinca] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBFinca] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBFinca] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBFinca] SET RECOVERY FULL 
GO
ALTER DATABASE [DBFinca] SET  MULTI_USER 
GO
ALTER DATABASE [DBFinca] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBFinca] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBFinca] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBFinca] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DBFinca] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DBFinca', N'ON'
GO
USE [DBFinca]
GO
/****** Object:  User [DESKTOP-RDJ7PIN\Brian]    Script Date: 13/03/2016 12:07:53 p. m. ******/
CREATE USER [DESKTOP-RDJ7PIN\Brian] FOR LOGIN [DESKTOP-RDJ7PIN\Brian] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [DESKTOP-RDJ7PIN\Brian]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [DESKTOP-RDJ7PIN\Brian]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [DESKTOP-RDJ7PIN\Brian]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [DESKTOP-RDJ7PIN\Brian]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [DESKTOP-RDJ7PIN\Brian]
GO
ALTER ROLE [db_datareader] ADD MEMBER [DESKTOP-RDJ7PIN\Brian]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [DESKTOP-RDJ7PIN\Brian]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [DESKTOP-RDJ7PIN\Brian]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [DESKTOP-RDJ7PIN\Brian]
GO
/****** Object:  UserDefinedTableType [dbo].[DetalleCompra]    Script Date: 13/03/2016 12:07:53 p. m. ******/
CREATE TYPE [dbo].[DetalleCompra] AS TABLE(
	[Nombre] [varchar](45) NULL,
	[Cantidad] [varchar](10) NULL,
	[Precio] [varchar](10) NULL,
	[Subtotal] [varchar](20) NULL,
	[idInsumo] [varchar](20) NULL,
	[idCompra] [varchar](20) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[EstructuraSalarioPersonaPermanente]    Script Date: 13/03/2016 12:07:53 p. m. ******/
CREATE TYPE [dbo].[EstructuraSalarioPersonaPermanente] AS TABLE(
	[DocumentoPersona] [varchar](15) NULL,
	[Valor_a_Pagar] [money] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[InsumoLaborLote]    Script Date: 13/03/2016 12:07:53 p. m. ******/
CREATE TYPE [dbo].[InsumoLaborLote] AS TABLE(
	[idLabor_Lote] [int] NULL,
	[idInsumo] [int] NULL,
	[Cantidad] [int] NULL,
	[Precio] [money] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[MovimientoArboles]    Script Date: 13/03/2016 12:07:53 p. m. ******/
CREATE TYPE [dbo].[MovimientoArboles] AS TABLE(
	[idMovimiento] [varchar](10) NULL,
	[idArbol] [varchar](10) NULL,
	[NombreArbol] [varchar](45) NULL,
	[Cantidad] [varchar](10) NULL,
	[Fecha] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[RegistrarGasto]    Script Date: 13/03/2016 12:07:53 p. m. ******/
CREATE TYPE [dbo].[RegistrarGasto] AS TABLE(
	[idConcepto] [int] NULL,
	[Descripcion] [varchar](150) NULL,
	[Fecha] [date] NULL,
	[Valor] [money] NULL,
	[EstadoCuenta] [char](1) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[SalarioPeronaTemporal]    Script Date: 13/03/2016 12:07:54 p. m. ******/
CREATE TYPE [dbo].[SalarioPeronaTemporal] AS TABLE(
	[DocumentoPersona] [varchar](15) NULL,
	[idLabor_Lote] [int] NULL,
	[Cantidad] [int] NULL,
	[Valor] [money] NULL
)
GO
/****** Object:  Table [dbo].[AbonoCompra]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AbonoCompra](
	[idAbonoCompra] [int] IDENTITY(1,1) NOT NULL,
	[idCompra] [int] NOT NULL,
	[Valor] [money] NOT NULL,
	[Fecha] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idAbonoCompra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AbonoDeuda]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AbonoDeuda](
	[idAbonoDeuda] [int] IDENTITY(1,1) NOT NULL,
	[idDeudaPersona] [int] NOT NULL,
	[Valor] [money] NOT NULL,
	[Fecha] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idAbonoDeuda] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AbonoEgreso]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AbonoEgreso](
	[idAbonoEgreso] [int] IDENTITY(1,1) NOT NULL,
	[idEgreso] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Valor] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idAbonoEgreso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Arboles]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Arboles](
	[idArboles] [int] IDENTITY(1,1) NOT NULL,
	[idLote] [smallint] NOT NULL,
	[idTIpoArbol] [tinyint] NOT NULL,
	[Cantidad] [int] NOT NULL,
 CONSTRAINT [PK_Arboles] PRIMARY KEY CLUSTERED 
(
	[idArboles] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Compra]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Compra](
	[idCompra] [int] IDENTITY(1,1) NOT NULL,
	[NitProveedor] [varchar](20) NOT NULL,
	[Fecha] [date] NOT NULL,
	[NumeroFactura] [int] NULL,
	[EstadoCuenta] [char](1) NOT NULL CONSTRAINT [DF_Compra]  DEFAULT ('D'),
 CONSTRAINT [PK_Compra] PRIMARY KEY CLUSTERED 
(
	[idCompra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Compra_Insumo]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Compra_Insumo](
	[idCompraInsumo] [int] IDENTITY(1,1) NOT NULL,
	[idCompra] [int] NOT NULL,
	[idInsumo] [smallint] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Precio] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idCompraInsumo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Concepto]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Concepto](
	[idConcepto] [smallint] IDENTITY(1,1) NOT NULL,
	[NombreConcepto] [varchar](45) NOT NULL,
	[Descripcion] [varchar](45) NOT NULL,
	[EstadoConcepto] [char](1) NOT NULL CONSTRAINT [DF_Concepto]  DEFAULT ('A'),
 CONSTRAINT [PK_Concepto] PRIMARY KEY CLUSTERED 
(
	[idConcepto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CostoBeneficio]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CostoBeneficio](
	[idCostoBeneficio] [int] IDENTITY(1,1) NOT NULL,
	[idVenta] [int] NULL,
	[idCompra] [int] NULL,
	[Precio] [money] NULL,
	[EstadoCuenta] [char](1) NULL CONSTRAINT [DF_CostoBeneeficio]  DEFAULT ('D'),
 CONSTRAINT [PK_CostoBeneficio] PRIMARY KEY CLUSTERED 
(
	[idCostoBeneficio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Departamento]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Departamento](
	[idDepartamento] [tinyint] NOT NULL,
	[NombreDepartamento] [varchar](25) NOT NULL,
 CONSTRAINT [PK_Departamento] PRIMARY KEY CLUSTERED 
(
	[idDepartamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DeudaPersona]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DeudaPersona](
	[idDeudaPersona] [int] IDENTITY(1,1) NOT NULL,
	[DocumentoPersona] [varchar](15) NOT NULL,
	[Valor] [money] NOT NULL,
	[Fecha] [date] NOT NULL,
	[EstadoCuenta] [char](1) NOT NULL CONSTRAINT [DF_DeudaPersona]  DEFAULT ('D'),
	[Descripcion] [varchar](150) NULL,
 CONSTRAINT [PK_DeudaPersona] PRIMARY KEY CLUSTERED 
(
	[idDeudaPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Egreso]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Egreso](
	[idEgreso] [int] IDENTITY(1,1) NOT NULL,
	[idConcepto] [smallint] NOT NULL,
	[Descripcion] [varchar](150) NULL,
	[Fecha] [date] NOT NULL,
	[Valor] [money] NOT NULL,
	[EstadoCuenta] [char](1) NOT NULL CONSTRAINT [DF_Egreso]  DEFAULT ('D'),
 CONSTRAINT [PK_Egreso] PRIMARY KEY CLUSTERED 
(
	[idEgreso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Finca]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Finca](
	[idFinca] [tinyint] IDENTITY(1,1) NOT NULL,
	[NombreFinca] [varchar](25) NOT NULL,
	[Propietario] [varchar](45) NOT NULL,
	[idMunicipio] [smallint] NOT NULL,
	[Vereda] [varchar](50) NOT NULL,
	[Telefono] [varchar](10) NOT NULL,
	[Cuadras] [varchar](5) NOT NULL,
 CONSTRAINT [PK_Finca] PRIMARY KEY CLUSTERED 
(
	[idFinca] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Insumo]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Insumo](
	[idInsumo] [smallint] IDENTITY(1,1) NOT NULL,
	[idTipoInsumo] [tinyint] NULL,
	[NombreInsumo] [varchar](45) NOT NULL,
	[Descripcion] [varchar](150) NOT NULL,
	[CantidadExistente] [decimal](18, 0) NOT NULL CONSTRAINT [DF_Insumo]  DEFAULT ((0)),
	[Marca] [varchar](45) NOT NULL,
	[UnidadMedida] [varchar](45) NOT NULL,
	[EstadoInsumo] [char](1) NOT NULL CONSTRAINT [DF_InsumoDesac]  DEFAULT ('A'),
	[PrecioPromedio] [int] NULL,
 CONSTRAINT [PK_Insumo] PRIMARY KEY CLUSTERED 
(
	[idInsumo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Labor]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Labor](
	[idLabor] [smallint] IDENTITY(1,1) NOT NULL,
	[NombreLabor] [varchar](25) NOT NULL,
	[ModificaArboles] [bit] NOT NULL,
	[RequiereInsumo] [bit] NOT NULL,
	[Descripcion] [varchar](60) NULL,
	[EstadoLabor] [char](1) NOT NULL CONSTRAINT [DF_Labor]  DEFAULT ('A'),
 CONSTRAINT [PK_Labor] PRIMARY KEY CLUSTERED 
(
	[idLabor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Labor_Lote]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Labor_Lote](
	[idLabor_Lote] [int] IDENTITY(1,1) NOT NULL,
	[idLabor] [smallint] NOT NULL,
	[idLote] [smallint] NOT NULL,
	[Fecha] [date] NOT NULL,
 CONSTRAINT [PK_LaborLote] PRIMARY KEY CLUSTERED 
(
	[idLabor_Lote] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LaborLote_Insumo]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LaborLote_Insumo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idLabor_Lote] [int] NOT NULL,
	[idInsumo] [smallint] NOT NULL,
	[Cantidad] [decimal](18, 0) NOT NULL,
	[Precio] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Lote]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lote](
	[idLote] [smallint] IDENTITY(1,1) NOT NULL,
	[idFinca] [tinyint] NOT NULL,
	[NombreLote] [varchar](25) NOT NULL,
	[Observaciones] [varchar](100) NULL,
	[EstadoLote] [char](1) NOT NULL CONSTRAINT [DF_Lote]  DEFAULT ('A'),
	[Cuadras] [varchar](15) NULL,
 CONSTRAINT [PK_Lote] PRIMARY KEY CLUSTERED 
(
	[idLote] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MovimientoProduccion]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovimientoProduccion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idProduccion] [int] NOT NULL,
	[idVenta] [int] NOT NULL,
	[Cantidad] [decimal](18, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MovimientosArboles]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovimientosArboles](
	[idMovimientosArboles] [int] IDENTITY(1,1) NOT NULL,
	[idArboles] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Cantidad] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idMovimientosArboles] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Municipio]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Municipio](
	[idMunicipio] [smallint] NOT NULL,
	[idDepartamento] [tinyint] NOT NULL,
	[NombreMunicipio] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Municipio] PRIMARY KEY CLUSTERED 
(
	[idMunicipio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Persona]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Persona](
	[DocumentoPersona] [varchar](15) NOT NULL,
	[TipoDocumento] [varchar](2) NOT NULL,
	[TipoContratoPersona] [varchar](10) NOT NULL,
	[NombrePersona] [varchar](50) NOT NULL,
	[Genero] [char](1) NOT NULL,
	[Telefono] [varchar](10) NOT NULL,
	[FechaNacimineto] [date] NOT NULL,
	[EstadoPersona] [char](1) NOT NULL CONSTRAINT [DF_Persona]  DEFAULT ('A'),
 CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED 
(
	[DocumentoPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Produccion]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Produccion](
	[idProduccion] [int] IDENTITY(1,1) NOT NULL,
	[idLote] [smallint] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Cantidad] [decimal](18, 0) NULL,
	[EstadoProduccion] [char](2) NOT NULL CONSTRAINT [DF_Produccion]  DEFAULT ('NV'),
 CONSTRAINT [PK_Produccion] PRIMARY KEY CLUSTERED 
(
	[idProduccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Producto](
	[idProducto] [tinyint] IDENTITY(1,1) NOT NULL,
	[NombreProducto] [varchar](20) NOT NULL,
	[Descripcion] [varchar](70) NULL,
	[EstadoProducto] [char](1) NOT NULL CONSTRAINT [DF_Producto]  DEFAULT ('A'),
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Proveedor]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Proveedor](
	[Nit] [varchar](20) NOT NULL,
	[TipoDocumento] [varchar](20) NOT NULL,
	[NombreProveedor] [varchar](45) NOT NULL,
	[Telefono] [varchar](10) NOT NULL,
	[Direccion] [varchar](45) NOT NULL,
	[EstadoProveedor] [char](1) NOT NULL CONSTRAINT [DF_Proveedor]  DEFAULT ('A'),
 CONSTRAINT [PK_Proveedor] PRIMARY KEY CLUSTERED 
(
	[Nit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RegistroPago]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegistroPago](
	[idRegistroPago] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [date] NOT NULL,
 CONSTRAINT [PK_RegistroPago] PRIMARY KEY CLUSTERED 
(
	[idRegistroPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RegistroPagoSalario]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegistroPagoSalario](
	[idRegistroPagoSalario] [int] IDENTITY(1,1) NOT NULL,
	[idRegistroPago] [int] NOT NULL,
	[idSalarioPersonaTemporal] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idRegistroPagoSalario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalarioPersonaPermanente]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalarioPersonaPermanente](
	[idSalarioPersonaPermanente] [int] IDENTITY(1,1) NOT NULL,
	[DocumentoPersona] [varchar](15) NOT NULL,
	[Valor] [money] NOT NULL,
	[IdRegistroPago] [int] NOT NULL,
 CONSTRAINT [PK_SalarioPersonaPermanente] PRIMARY KEY CLUSTERED 
(
	[idSalarioPersonaPermanente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SalarioPersonaTemporal]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalarioPersonaTemporal](
	[idSalarioPersonaTemporal] [int] IDENTITY(1,1) NOT NULL,
	[DocumentoPersona] [varchar](15) NOT NULL,
	[idLabor_Lote] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Valor] [money] NOT NULL,
	[EstadoCuenta] [char](1) NOT NULL CONSTRAINT [DF_SalarioPersonaTemporal]  DEFAULT ('D'),
 CONSTRAINT [PK_SalarioPersonaTemporal] PRIMARY KEY CLUSTERED 
(
	[idSalarioPersonaTemporal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TipoArbol]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoArbol](
	[idTipoArbol] [tinyint] IDENTITY(1,1) NOT NULL,
	[NombreTipoArbol] [varchar](45) NOT NULL,
	[Descripcion] [varchar](45) NOT NULL,
	[EstadoTipoArbol] [char](1) NOT NULL CONSTRAINT [DF_TipoArbol]  DEFAULT ('A'),
	[TiempoProduccion] [tinyint] NOT NULL,
 CONSTRAINT [PK__TipoArbo__5040325FB285FADF] PRIMARY KEY CLUSTERED 
(
	[idTipoArbol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TipoInsumo]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoInsumo](
	[idTipoInsumo] [tinyint] IDENTITY(1,1) NOT NULL,
	[NombreTipoInsumo] [varchar](45) NOT NULL,
	[Descripcion] [varchar](45) NOT NULL,
	[EstadoTipoInsumo] [char](1) NOT NULL CONSTRAINT [DF_TipoInsumo]  DEFAULT ('A'),
 CONSTRAINT [PK_TipoInsumo] PRIMARY KEY CLUSTERED 
(
	[idTipoInsumo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[idUsuario] [smallint] IDENTITY(1,1) NOT NULL,
	[Nickname] [varchar](15) NOT NULL,
	[Rol] [varchar](15) NOT NULL,
	[Contrasena] [varchar](40) NOT NULL,
	[PreguntaSeguridad] [varchar](70) NOT NULL,
	[Respuesta] [varchar](30) NOT NULL,
	[EstadoUsuario] [char](1) NOT NULL CONSTRAINT [DF_Usuario]  DEFAULT ('A'),
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Venta]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta](
	[idVenta] [int] IDENTITY(1,1) NOT NULL,
	[idProducto] [tinyint] NOT NULL,
	[Fecha] [date] NOT NULL,
	[PrecioCarga] [money] NOT NULL,
	[CantidadCargas] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Venta] PRIMARY KEY CLUSTERED 
(
	[idVenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[AbonoCompra] ON 

INSERT [dbo].[AbonoCompra] ([idAbonoCompra], [idCompra], [Valor], [Fecha]) VALUES (1, 1, 2000.0000, CAST(N'2016-01-09' AS Date))
INSERT [dbo].[AbonoCompra] ([idAbonoCompra], [idCompra], [Valor], [Fecha]) VALUES (2, 1, 1000.0000, CAST(N'2016-01-11' AS Date))
INSERT [dbo].[AbonoCompra] ([idAbonoCompra], [idCompra], [Valor], [Fecha]) VALUES (3, 1, 1000.0000, CAST(N'2016-01-11' AS Date))
INSERT [dbo].[AbonoCompra] ([idAbonoCompra], [idCompra], [Valor], [Fecha]) VALUES (4, 2, 100.0000, CAST(N'2016-01-11' AS Date))
INSERT [dbo].[AbonoCompra] ([idAbonoCompra], [idCompra], [Valor], [Fecha]) VALUES (1002, 1012, 1000.0000, CAST(N'2016-02-04' AS Date))
INSERT [dbo].[AbonoCompra] ([idAbonoCompra], [idCompra], [Valor], [Fecha]) VALUES (1003, 1, 2000.0000, CAST(N'2016-02-04' AS Date))
INSERT [dbo].[AbonoCompra] ([idAbonoCompra], [idCompra], [Valor], [Fecha]) VALUES (2002, 1, 44000.0000, CAST(N'2016-02-05' AS Date))
INSERT [dbo].[AbonoCompra] ([idAbonoCompra], [idCompra], [Valor], [Fecha]) VALUES (2003, 2, 1000.0000, CAST(N'2016-02-10' AS Date))
INSERT [dbo].[AbonoCompra] ([idAbonoCompra], [idCompra], [Valor], [Fecha]) VALUES (2004, 2, 1000.0000, CAST(N'2016-02-10' AS Date))
INSERT [dbo].[AbonoCompra] ([idAbonoCompra], [idCompra], [Valor], [Fecha]) VALUES (2005, 3, 2000.0000, CAST(N'2016-03-11' AS Date))
SET IDENTITY_INSERT [dbo].[AbonoCompra] OFF
SET IDENTITY_INSERT [dbo].[AbonoDeuda] ON 

INSERT [dbo].[AbonoDeuda] ([idAbonoDeuda], [idDeudaPersona], [Valor], [Fecha]) VALUES (21, 9, 2000.0000, CAST(N'2016-02-27' AS Date))
INSERT [dbo].[AbonoDeuda] ([idAbonoDeuda], [idDeudaPersona], [Valor], [Fecha]) VALUES (22, 9, 2000.0000, CAST(N'2016-02-27' AS Date))
INSERT [dbo].[AbonoDeuda] ([idAbonoDeuda], [idDeudaPersona], [Valor], [Fecha]) VALUES (23, 10, 1000.0000, CAST(N'2016-02-27' AS Date))
INSERT [dbo].[AbonoDeuda] ([idAbonoDeuda], [idDeudaPersona], [Valor], [Fecha]) VALUES (24, 9, 1000.0000, CAST(N'2016-02-27' AS Date))
INSERT [dbo].[AbonoDeuda] ([idAbonoDeuda], [idDeudaPersona], [Valor], [Fecha]) VALUES (25, 9, 1000.0000, CAST(N'2016-02-28' AS Date))
INSERT [dbo].[AbonoDeuda] ([idAbonoDeuda], [idDeudaPersona], [Valor], [Fecha]) VALUES (26, 9, 2000.0000, CAST(N'2016-02-28' AS Date))
INSERT [dbo].[AbonoDeuda] ([idAbonoDeuda], [idDeudaPersona], [Valor], [Fecha]) VALUES (27, 9, 1000.0000, CAST(N'2016-02-28' AS Date))
INSERT [dbo].[AbonoDeuda] ([idAbonoDeuda], [idDeudaPersona], [Valor], [Fecha]) VALUES (28, 10, 3000.0000, CAST(N'2016-02-28' AS Date))
INSERT [dbo].[AbonoDeuda] ([idAbonoDeuda], [idDeudaPersona], [Valor], [Fecha]) VALUES (29, 9, 1000.0000, CAST(N'2016-03-01' AS Date))
INSERT [dbo].[AbonoDeuda] ([idAbonoDeuda], [idDeudaPersona], [Valor], [Fecha]) VALUES (30, 12, 2000.0000, CAST(N'2016-03-01' AS Date))
INSERT [dbo].[AbonoDeuda] ([idAbonoDeuda], [idDeudaPersona], [Valor], [Fecha]) VALUES (31, 12, 3000.0000, CAST(N'2016-03-11' AS Date))
SET IDENTITY_INSERT [dbo].[AbonoDeuda] OFF
SET IDENTITY_INSERT [dbo].[Arboles] ON 

INSERT [dbo].[Arboles] ([idArboles], [idLote], [idTIpoArbol], [Cantidad]) VALUES (1, 1, 1, 4900)
INSERT [dbo].[Arboles] ([idArboles], [idLote], [idTIpoArbol], [Cantidad]) VALUES (2, 2, 1, 12300)
INSERT [dbo].[Arboles] ([idArboles], [idLote], [idTIpoArbol], [Cantidad]) VALUES (3, 2, 2, 17700)
INSERT [dbo].[Arboles] ([idArboles], [idLote], [idTIpoArbol], [Cantidad]) VALUES (4, 2, 3, 4000)
SET IDENTITY_INSERT [dbo].[Arboles] OFF
SET IDENTITY_INSERT [dbo].[Compra] ON 

INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1, N'1785623', CAST(N'2016-01-08' AS Date), 30, N'P')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (2, N'1785623', CAST(N'2016-01-06' AS Date), 20, N'P')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (3, N'1785623', CAST(N'2016-01-07' AS Date), 290, N'D')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1002, N'1785623', CAST(N'2016-01-18' AS Date), 123, N'D')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1003, N'1785623', CAST(N'2016-01-13' AS Date), 587, N'D')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1004, N'1785623', CAST(N'2016-01-19' AS Date), 289, N'D')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1008, N'1785623', CAST(N'2016-01-11' AS Date), 209, N'D')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1009, N'1785623', CAST(N'2016-01-18' AS Date), 2009, N'D')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1010, N'1785623', CAST(N'2016-01-13' AS Date), 123, N'D')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1011, N'1785623', CAST(N'2016-01-15' AS Date), 654, N'D')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1012, N'1785623', CAST(N'2016-01-19' AS Date), 123, N'D')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1013, N'1785623', CAST(N'2016-02-04' AS Date), 234, N'D')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1014, N'1785623', CAST(N'2016-02-03' AS Date), 90, N'D')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1015, N'1785623', CAST(N'2016-02-09' AS Date), 1234567, N'D')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1016, N'123', CAST(N'2016-02-09' AS Date), 12345, N'D')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1017, N'1785623', CAST(N'2016-02-10' AS Date), 1234, N'D')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1018, N'123', CAST(N'2016-02-12' AS Date), 1234, N'D')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1021, N'1785623', CAST(N'2016-02-17' AS Date), 43556, N'D')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1022, N'123', CAST(N'2016-02-25' AS Date), 23487, N'D')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1023, N'1785623', CAST(N'2016-02-02' AS Date), 2343253, N'D')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1024, N'1785623', CAST(N'2016-02-18' AS Date), 4567, N'D')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1025, N'123', CAST(N'2016-02-18' AS Date), 34354, N'P')
INSERT [dbo].[Compra] ([idCompra], [NitProveedor], [Fecha], [NumeroFactura], [EstadoCuenta]) VALUES (1026, N'123', CAST(N'2016-03-10' AS Date), 123, N'D')
SET IDENTITY_INSERT [dbo].[Compra] OFF
SET IDENTITY_INSERT [dbo].[Compra_Insumo] ON 

INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (1, 1, 1, 10, 1000.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (2, 1, 1, 20, 2000.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (3, 2, 1, 2, 1000.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (4, 2, 1, 1, 100.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (5, 3, 3, 20, 10000.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (6, 3, 1, 1, 2000.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (1002, 1002, 1, 9, 1000.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (1003, 1003, 1, 2, 123.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (1004, 1004, 1, 1, 2000.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (1008, 1008, 1, 20, 1000.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (1009, 1009, 1, 20, 1100.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (1010, 1010, 1, 2, 1000.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (1011, 1010, 1, 5, 2000.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (1012, 1011, 1, 2, 1000.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (1013, 1011, 3, 2, 20000.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (1014, 1011, 1, 7, 4000.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (1015, 1012, 1, 2, 1000.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (1016, 1013, 1, 5, 545.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (1017, 1013, 3, 2, 2000.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (1018, 1014, 2, 12, 1000.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (1019, 1015, 1, 1234, 12.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (1020, 1016, 1, 123, 123.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (1021, 1017, 1, 4567, 123456.0000)
INSERT [dbo].[Compra_Insumo] ([idCompraInsumo], [idCompra], [idInsumo], [Cantidad], [Precio]) VALUES (1022, 1018, 1, 123, 123.0000)
SET IDENTITY_INSERT [dbo].[Compra_Insumo] OFF
SET IDENTITY_INSERT [dbo].[Concepto] ON 

INSERT [dbo].[Concepto] ([idConcepto], [NombreConcepto], [Descripcion], [EstadoConcepto]) VALUES (1, N'Fh', N'Prueba', N'A')
INSERT [dbo].[Concepto] ([idConcepto], [NombreConcepto], [Descripcion], [EstadoConcepto]) VALUES (2, N'Fhs', N'A', N'A')
INSERT [dbo].[Concepto] ([idConcepto], [NombreConcepto], [Descripcion], [EstadoConcepto]) VALUES (3, N'concepto4', N'prueba5', N'I')
INSERT [dbo].[Concepto] ([idConcepto], [NombreConcepto], [Descripcion], [EstadoConcepto]) VALUES (4, N'ADIOSCARRO', N'PRUEBA', N'I')
SET IDENTITY_INSERT [dbo].[Concepto] OFF
SET IDENTITY_INSERT [dbo].[CostoBeneficio] ON 

INSERT [dbo].[CostoBeneficio] ([idCostoBeneficio], [idVenta], [idCompra], [Precio], [EstadoCuenta]) VALUES (3, 3, 1021, 691340.0000, N'D')
INSERT [dbo].[CostoBeneficio] ([idCostoBeneficio], [idVenta], [idCompra], [Precio], [EstadoCuenta]) VALUES (4, 8, 1022, 2100000.0000, N'D')
INSERT [dbo].[CostoBeneficio] ([idCostoBeneficio], [idVenta], [idCompra], [Precio], [EstadoCuenta]) VALUES (5, 9, 1023, 31763.1600, N'D')
INSERT [dbo].[CostoBeneficio] ([idCostoBeneficio], [idVenta], [idCompra], [Precio], [EstadoCuenta]) VALUES (6, 10, 1024, 19686.3600, N'D')
INSERT [dbo].[CostoBeneficio] ([idCostoBeneficio], [idVenta], [idCompra], [Precio], [EstadoCuenta]) VALUES (7, 11, 1025, 24883.2000, N'D')
INSERT [dbo].[CostoBeneficio] ([idCostoBeneficio], [idVenta], [idCompra], [Precio], [EstadoCuenta]) VALUES (8, 11, 1025, 0.0000, N'D')
INSERT [dbo].[CostoBeneficio] ([idCostoBeneficio], [idVenta], [idCompra], [Precio], [EstadoCuenta]) VALUES (9, 12, 1026, 48000.0000, N'D')
SET IDENTITY_INSERT [dbo].[CostoBeneficio] OFF
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (1, N'AMAZONAS')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (2, N'ANTIOQUIA')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (3, N'ARAUCA')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (4, N'ATLÁNTICO')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (5, N'BOLÍVAR')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (6, N'BOYACÁ')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (7, N'CALDAS')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (8, N'CAQUETÁ')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (9, N'CASANARE')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (10, N'CAUCA')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (11, N'CESAR')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (12, N'CHOCÓ')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (13, N'CÓRDOBA')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (14, N'CUNDINAMARCA')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (15, N'GUAINÍA')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (16, N'GUAVIARE')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (17, N'HUILA')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (18, N'LA GUAJIRA')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (19, N'MAGDALENA')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (20, N'META')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (21, N'NARIÑO')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (22, N'NORTE DE SANTANDER')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (23, N'PUTUMAYO')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (24, N'QUINDÍO')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (25, N'RISARALDA')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (26, N'SAN ANDRÉS Y ROVIDENCIA')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (27, N'SANTANDER')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (28, N'SUCRE')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (29, N'TOLIMA')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (30, N'VALLE DEL CAUCA')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (31, N'VAUPÉS')
INSERT [dbo].[Departamento] ([idDepartamento], [NombreDepartamento]) VALUES (32, N'VICHADA')
SET IDENTITY_INSERT [dbo].[DeudaPersona] ON 

INSERT [dbo].[DeudaPersona] ([idDeudaPersona], [DocumentoPersona], [Valor], [Fecha], [EstadoCuenta], [Descripcion]) VALUES (9, N'103665432', 10000.0000, CAST(N'2016-02-20' AS Date), N'P', N'GFGFFGF')
INSERT [dbo].[DeudaPersona] ([idDeudaPersona], [DocumentoPersona], [Valor], [Fecha], [EstadoCuenta], [Descripcion]) VALUES (10, N'9090901', 20000.0000, CAST(N'2016-02-25' AS Date), N'D', N'POIOPP')
INSERT [dbo].[DeudaPersona] ([idDeudaPersona], [DocumentoPersona], [Valor], [Fecha], [EstadoCuenta], [Descripcion]) VALUES (11, N'103665432', 10000.0000, CAST(N'2016-02-27' AS Date), N'D', N'TTTTYTYTY')
INSERT [dbo].[DeudaPersona] ([idDeudaPersona], [DocumentoPersona], [Valor], [Fecha], [EstadoCuenta], [Descripcion]) VALUES (12, N'103665432', 5000.0000, CAST(N'2016-02-27' AS Date), N'P', N'CASAS')
INSERT [dbo].[DeudaPersona] ([idDeudaPersona], [DocumentoPersona], [Valor], [Fecha], [EstadoCuenta], [Descripcion]) VALUES (13, N'103665432', 3000.0000, CAST(N'2016-02-28' AS Date), N'D', N'PRUEBA8')
INSERT [dbo].[DeudaPersona] ([idDeudaPersona], [DocumentoPersona], [Valor], [Fecha], [EstadoCuenta], [Descripcion]) VALUES (14, N'103665432', 4000.0000, CAST(N'2016-02-28' AS Date), N'D', N'PAIDO')
INSERT [dbo].[DeudaPersona] ([idDeudaPersona], [DocumentoPersona], [Valor], [Fecha], [EstadoCuenta], [Descripcion]) VALUES (15, N'9090901', 5000.0000, CAST(N'2016-02-28' AS Date), N'D', N'JKLLKJJ')
SET IDENTITY_INSERT [dbo].[DeudaPersona] OFF
SET IDENTITY_INSERT [dbo].[Egreso] ON 

INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (1, 1, N'prueba', CAST(N'2015-12-10' AS Date), 4500.0000, N'P')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (2, 1, N'fg', CAST(N'2015-12-11' AS Date), 678.0000, N'P')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (3, 1, N'descarga', CAST(N'2015-12-03' AS Date), 3400.0000, N'P')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (4, 1, N'prueba', CAST(N'2015-12-10' AS Date), 80000.0000, N'P')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (5, 1, N'asdfghj', CAST(N'2016-02-05' AS Date), 1234.0000, N'P')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (6, 1, N'gfghfhgfg', CAST(N'2016-02-03' AS Date), 12345.0000, N'P')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (7, 2, N'fsdghjkj', CAST(N'2016-02-11' AS Date), 6567.0000, N'D')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (8, 1, N'dfghnm,', CAST(N'2016-02-04' AS Date), 45365.0000, N'P')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (9, 1, N'gfhjkl', CAST(N'2016-02-03' AS Date), 46578.0000, N'D')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (10, 1, N'dfghjk', CAST(N'2016-02-04' AS Date), 46578.0000, N'P')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (11, 2, N'vgbnm', CAST(N'2016-02-04' AS Date), 8976.0000, N'D')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (12, 2, N'dfhgjk', CAST(N'2016-02-10' AS Date), 35678.0000, N'D')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (13, 2, N'sdfghjk', CAST(N'2016-02-04' AS Date), 354678.0000, N'P')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (14, 2, N'dfghjk', CAST(N'2016-02-03' AS Date), 354687.0000, N'D')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (15, 2, N'dfghjk', CAST(N'2016-02-05' AS Date), 34567.0000, N'D')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (16, 2, N'dsfghkjl', CAST(N'2016-02-13' AS Date), 435678.0000, N'D')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (17, 2, N'dfghj,', CAST(N'2016-02-12' AS Date), 435678.0000, N'D')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (18, 2, N'dfghjkl', CAST(N'2016-02-04' AS Date), 35678.0000, N'D')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (19, 2, N'dfhgjkl', CAST(N'2016-02-13' AS Date), 35678.0000, N'D')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (20, 2, N'w45678', CAST(N'2016-02-05' AS Date), 43546787.0000, N'P')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (21, 2, N'dcvbn', CAST(N'2016-02-12' AS Date), 456.0000, N'D')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (22, 2, N'hgfgfgh', CAST(N'2016-02-05' AS Date), 22456.0000, N'D')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (23, 2, N'rfghjkm', CAST(N'2016-02-12' AS Date), 3456.0000, N'D')
INSERT [dbo].[Egreso] ([idEgreso], [idConcepto], [Descripcion], [Fecha], [Valor], [EstadoCuenta]) VALUES (24, 1, N'dfghj', CAST(N'2016-02-05' AS Date), 3456.0000, N'P')
SET IDENTITY_INSERT [dbo].[Egreso] OFF
SET IDENTITY_INSERT [dbo].[Finca] ON 

INSERT [dbo].[Finca] ([idFinca], [NombreFinca], [Propietario], [idMunicipio], [Vereda], [Telefono], [Cuadras]) VALUES (1, N'La Pava', N'Juan Diego Restrepo', 14, N'La ZocaRe', N'3216767', N'13458')
SET IDENTITY_INSERT [dbo].[Finca] OFF
SET IDENTITY_INSERT [dbo].[Insumo] ON 

INSERT [dbo].[Insumo] ([idInsumo], [idTipoInsumo], [NombreInsumo], [Descripcion], [CantidadExistente], [Marca], [UnidadMedida], [EstadoInsumo], [PrecioPromedio]) VALUES (1, 1, N'veneno', N'prueba', CAST(5908 AS Decimal(18, 0)), N'la yuca', N'litros', N'A', 92474)
INSERT [dbo].[Insumo] ([idInsumo], [idTipoInsumo], [NombreInsumo], [Descripcion], [CantidadExistente], [Marca], [UnidadMedida], [EstadoInsumo], [PrecioPromedio]) VALUES (2, 2, N'asdf', N'prueba', CAST(12 AS Decimal(18, 0)), N'prueba', N'litros', N'A', 1000)
INSERT [dbo].[Insumo] ([idInsumo], [idTipoInsumo], [NombreInsumo], [Descripcion], [CantidadExistente], [Marca], [UnidadMedida], [EstadoInsumo], [PrecioPromedio]) VALUES (3, 2, N'urea', N'vdk', CAST(1 AS Decimal(18, 0)), N'yara', N'kilos', N'A', 11000)
SET IDENTITY_INSERT [dbo].[Insumo] OFF
SET IDENTITY_INSERT [dbo].[Labor] ON 

INSERT [dbo].[Labor] ([idLabor], [NombreLabor], [ModificaArboles], [RequiereInsumo], [Descripcion], [EstadoLabor]) VALUES (1, N'Recoleccion', 0, 0, N'prueba', N'A')
INSERT [dbo].[Labor] ([idLabor], [NombreLabor], [ModificaArboles], [RequiereInsumo], [Descripcion], [EstadoLabor]) VALUES (2, N'fumigada', 1, 1, N'prueba', N'A')
INSERT [dbo].[Labor] ([idLabor], [NombreLabor], [ModificaArboles], [RequiereInsumo], [Descripcion], [EstadoLabor]) VALUES (3, N'abonar', 0, 1, N'vvhj', N'A')
INSERT [dbo].[Labor] ([idLabor], [NombreLabor], [ModificaArboles], [RequiereInsumo], [Descripcion], [EstadoLabor]) VALUES (4, N'Siembra', 1, 0, N'Siembra de un nuevo árbol', N'A')
INSERT [dbo].[Labor] ([idLabor], [NombreLabor], [ModificaArboles], [RequiereInsumo], [Descripcion], [EstadoLabor]) VALUES (5, N'Eliminación', 1, 0, N'Elimina un tipo de arbol', N'A')
SET IDENTITY_INSERT [dbo].[Labor] OFF
SET IDENTITY_INSERT [dbo].[Labor_Lote] ON 

INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (1, 1, 1, CAST(N'2016-02-08' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (2, 1, 1, CAST(N'2016-02-02' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (3, 1, 1, CAST(N'2016-02-10' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (4, 1, 1, CAST(N'2016-02-10' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (5, 1, 1, CAST(N'2016-02-11' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (6, 1, 2, CAST(N'2016-02-18' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (7, 1, 2, CAST(N'2016-02-18' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (8, 1, 1, CAST(N'2016-02-19' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (9, 1, 2, CAST(N'2016-02-18' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (10, 1, 1, CAST(N'0001-01-01' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (11, 1, 1, CAST(N'2016-02-18' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (12, 1, 1, CAST(N'2016-02-04' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (13, 2, 1, CAST(N'2016-02-26' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (14, 3, 2, CAST(N'2016-02-25' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (15, 2, 1, CAST(N'2016-02-03' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (16, 5, 2, CAST(N'2016-03-11' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (17, 4, 2, CAST(N'2016-03-11' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (18, 4, 2, CAST(N'2016-03-06' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (19, 4, 2, CAST(N'2016-03-13' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (20, 4, 2, CAST(N'2016-03-13' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (21, 4, 2, CAST(N'2016-03-13' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (22, 4, 2, CAST(N'2016-03-13' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (23, 4, 2, CAST(N'2016-03-13' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (24, 4, 2, CAST(N'2016-03-13' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (25, 4, 2, CAST(N'2016-03-13' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (26, 4, 2, CAST(N'2016-03-13' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (27, 5, 2, CAST(N'2016-03-13' AS Date))
INSERT [dbo].[Labor_Lote] ([idLabor_Lote], [idLabor], [idLote], [Fecha]) VALUES (28, 4, 2, CAST(N'2016-03-13' AS Date))
SET IDENTITY_INSERT [dbo].[Labor_Lote] OFF
SET IDENTITY_INSERT [dbo].[LaborLote_Insumo] ON 

INSERT [dbo].[LaborLote_Insumo] ([id], [idLabor_Lote], [idInsumo], [Cantidad], [Precio]) VALUES (1, 13, 1, CAST(100 AS Decimal(18, 0)), 92474.0000)
INSERT [dbo].[LaborLote_Insumo] ([id], [idLabor_Lote], [idInsumo], [Cantidad], [Precio]) VALUES (2, 14, 3, CAST(3 AS Decimal(18, 0)), 11000.0000)
INSERT [dbo].[LaborLote_Insumo] ([id], [idLabor_Lote], [idInsumo], [Cantidad], [Precio]) VALUES (3, 15, 1, CAST(90 AS Decimal(18, 0)), 92474.0000)
SET IDENTITY_INSERT [dbo].[LaborLote_Insumo] OFF
SET IDENTITY_INSERT [dbo].[Lote] ON 

INSERT [dbo].[Lote] ([idLote], [idFinca], [NombreLote], [Observaciones], [EstadoLote], [Cuadras]) VALUES (1, 1, N'lote 1', N'prueba', N'A', N'3500')
INSERT [dbo].[Lote] ([idLote], [idFinca], [NombreLote], [Observaciones], [EstadoLote], [Cuadras]) VALUES (2, 1, N'lote 2', N'prueba', N'A', N'23000')
SET IDENTITY_INSERT [dbo].[Lote] OFF
SET IDENTITY_INSERT [dbo].[MovimientoProduccion] ON 

INSERT [dbo].[MovimientoProduccion] ([id], [idProduccion], [idVenta], [Cantidad]) VALUES (433, 6, 3, CAST(2500 AS Decimal(18, 0)))
INSERT [dbo].[MovimientoProduccion] ([id], [idProduccion], [idVenta], [Cantidad]) VALUES (434, 6, 8, CAST(2625 AS Decimal(18, 0)))
INSERT [dbo].[MovimientoProduccion] ([id], [idProduccion], [idVenta], [Cantidad]) VALUES (435, 6, 9, CAST(875 AS Decimal(18, 0)))
INSERT [dbo].[MovimientoProduccion] ([id], [idProduccion], [idVenta], [Cantidad]) VALUES (436, 7, 9, CAST(2343 AS Decimal(18, 0)))
INSERT [dbo].[MovimientoProduccion] ([id], [idProduccion], [idVenta], [Cantidad]) VALUES (437, 7, 10, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[MovimientoProduccion] ([id], [idProduccion], [idVenta], [Cantidad]) VALUES (438, 8, 10, CAST(200 AS Decimal(18, 0)))
INSERT [dbo].[MovimientoProduccion] ([id], [idProduccion], [idVenta], [Cantidad]) VALUES (439, 9, 10, CAST(235 AS Decimal(18, 0)))
INSERT [dbo].[MovimientoProduccion] ([id], [idProduccion], [idVenta], [Cantidad]) VALUES (440, 9, 11, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[MovimientoProduccion] ([id], [idProduccion], [idVenta], [Cantidad]) VALUES (441, 10, 11, CAST(400 AS Decimal(18, 0)))
INSERT [dbo].[MovimientoProduccion] ([id], [idProduccion], [idVenta], [Cantidad]) VALUES (442, 11, 11, CAST(500 AS Decimal(18, 0)))
INSERT [dbo].[MovimientoProduccion] ([id], [idProduccion], [idVenta], [Cantidad]) VALUES (443, 11, 12, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[MovimientoProduccion] ([id], [idProduccion], [idVenta], [Cantidad]) VALUES (444, 12, 12, CAST(375 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[MovimientoProduccion] OFF
SET IDENTITY_INSERT [dbo].[MovimientosArboles] ON 

INSERT [dbo].[MovimientosArboles] ([idMovimientosArboles], [idArboles], [Fecha], [Cantidad]) VALUES (1, 1, CAST(N'2015-12-10' AS Date), 4500)
INSERT [dbo].[MovimientosArboles] ([idMovimientosArboles], [idArboles], [Fecha], [Cantidad]) VALUES (2, 1, CAST(N'2015-12-11' AS Date), 400)
INSERT [dbo].[MovimientosArboles] ([idMovimientosArboles], [idArboles], [Fecha], [Cantidad]) VALUES (3, 2, CAST(N'2015-01-27' AS Date), 3400)
INSERT [dbo].[MovimientosArboles] ([idMovimientosArboles], [idArboles], [Fecha], [Cantidad]) VALUES (4, 2, CAST(N'2015-02-26' AS Date), 8900)
INSERT [dbo].[MovimientosArboles] ([idMovimientosArboles], [idArboles], [Fecha], [Cantidad]) VALUES (5, 3, CAST(N'2015-05-04' AS Date), 9500)
INSERT [dbo].[MovimientosArboles] ([idMovimientosArboles], [idArboles], [Fecha], [Cantidad]) VALUES (6, 4, CAST(N'2015-06-17' AS Date), 4000)
INSERT [dbo].[MovimientosArboles] ([idMovimientosArboles], [idArboles], [Fecha], [Cantidad]) VALUES (7, 3, CAST(N'2016-03-11' AS Date), 1000)
INSERT [dbo].[MovimientosArboles] ([idMovimientosArboles], [idArboles], [Fecha], [Cantidad]) VALUES (8, 3, CAST(N'2016-03-13' AS Date), 2200)
INSERT [dbo].[MovimientosArboles] ([idMovimientosArboles], [idArboles], [Fecha], [Cantidad]) VALUES (9, 3, CAST(N'2016-03-13' AS Date), 5000)
SET IDENTITY_INSERT [dbo].[MovimientosArboles] OFF
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1, 1, N'EL ENCANTO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (2, 1, N'LA CHORRERA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (3, 1, N'LA PEDRERA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (4, 1, N'LA VICTORIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (5, 1, N'LETICIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (6, 1, N'MIRITI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (7, 1, N'PUERTO ALEGRIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (8, 1, N'PUERTO ARICA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (9, 1, N'PUERTO NARIÑO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (10, 1, N'PUERTO SANTANDER')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (11, 1, N'TURAPACA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (12, 2, N'ABEJORRAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (13, 2, N'ABRIAQUI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (14, 2, N'ALEJANDRIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (15, 2, N'AMAGA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (16, 2, N'AMALFI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (17, 2, N'ANDES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (18, 2, N'ANGELOPOLIS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (19, 2, N'ANGOSTURA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (20, 2, N'ANORI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (21, 2, N'ANTIOQUIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (22, 2, N'ANZA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (23, 2, N'APARTADO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (24, 2, N'ARBOLETES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (25, 2, N'ARGELIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (26, 2, N'ARMENIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (27, 2, N'BARBOSA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (28, 2, N'BELLO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (29, 2, N'BELMIRA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (30, 2, N'BETANIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (31, 2, N'BETULIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (32, 2, N'BOLIVAR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (33, 2, N'BRICEÑO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (34, 2, N'BURITICA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (35, 2, N'CACERES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (36, 2, N'CAICEDO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (37, 2, N'CALDAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (38, 2, N'CAMPAMENTO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (39, 2, N'CANASGORDAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (40, 2, N'CARACOLI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (41, 2, N'CARAMANTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (42, 2, N'CAREPA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (43, 2, N'CARMEN DE VIBORAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (44, 2, N'CAROLINA DEL PRINCIPE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (45, 2, N'CAUCASIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (46, 2, N'CHIGORODO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (47, 2, N'CISNEROS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (48, 2, N'COCORNA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (49, 2, N'CONCEPCION')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (50, 2, N'CONCORDIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (51, 2, N'COPACABANA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (52, 2, N'DABEIBA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (53, 2, N'DONMATIAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (54, 2, N'EBEJICO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (55, 2, N'EL BAGRE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (56, 2, N'EL PENOL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (57, 2, N'EL RETIRO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (58, 2, N'ENTRERRIOS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (59, 2, N'ENVIGADO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (60, 2, N'FREDONIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (61, 2, N'FRONTINO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (62, 2, N'GIRALDO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (63, 2, N'GIRARDOTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (64, 2, N'GOMEZ PLATA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (65, 2, N'GRANADA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (66, 2, N'GUADALUPE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (67, 2, N'GUARNE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (68, 2, N'GUATAQUE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (69, 2, N'HELICONIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (70, 2, N'HISPANIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (71, 2, N'ITAGUI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (72, 2, N'ITUANGO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (73, 2, N'JARDIN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (74, 2, N'JERICO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (75, 2, N'LA CEJA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (76, 2, N'LA ESTRELLA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (77, 2, N'LA PINTADA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (78, 2, N'LA UNION')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (79, 2, N'LIBORINA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (80, 2, N'MACEO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (81, 2, N'MARINILLA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (82, 2, N'MEDELLIN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (83, 2, N'MONTEBELLO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (84, 2, N'MURINDO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (85, 2, N'MUTATA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (86, 2, N'NARINO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (87, 2, N'NECHI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (88, 2, N'NECOCLI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (89, 2, N'OLAYA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (90, 2, N'PEQUE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (91, 2, N'PUEBLORRICO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (92, 2, N'PUERTO BERRIO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (93, 2, N'PUERTO NARE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (94, 2, N'PUERTO TRIUNFO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (95, 2, N'REMEDIOS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (96, 2, N'RIONEGRO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (97, 2, N'SABANALARGA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (98, 2, N'SABANETA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (99, 2, N'SALGAR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (100, 2, N'SAN ANDRES DE CUERQUIA')
GO
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (101, 2, N'SAN CARLOS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (102, 2, N'SAN FRANCISCO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (103, 2, N'SAN JERONIMO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (104, 2, N'SAN JOSE DE LA MONTAÑA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (105, 2, N'SAN JUAN DE URABA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (106, 2, N'SAN LUIS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (107, 2, N'SAN PEDRO DE LOS MILAGROS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (108, 2, N'SAN PEDRO DE URABA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (109, 2, N'SAN RAFAEL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (110, 2, N'SAN ROQUE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (111, 2, N'SAN VICENTE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (112, 2, N'SANTA BARBARA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (113, 2, N'SANTA ROSA DE OSOS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (114, 2, N'SANTO DOMINGO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (115, 2, N'SANTUARIO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (116, 2, N'SEGOVIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (117, 2, N'SONSON')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (118, 2, N'SOPETRAN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (119, 2, N'TAMESIS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (120, 2, N'TARAZA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (121, 2, N'TARSO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (122, 2, N'TITIRIBI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (123, 2, N'TOLEDO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (124, 2, N'TURBO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (125, 2, N'URAMITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (126, 2, N'URRAO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (127, 2, N'VALDIVIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (128, 2, N'VALPARAISO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (129, 2, N'VEGACHI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (130, 2, N'VENECIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (131, 2, N'VIGIA DEL FUERTE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (132, 2, N'YALI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (133, 2, N'YARUMAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (134, 2, N'YOLOMBO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (135, 2, N'YONDO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (136, 2, N'ZARAGOZA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (137, 3, N'ARAUCA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (138, 3, N'ARAUQUITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (139, 3, N'CRAVO NORTE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (140, 3, N'FORTUL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (141, 3, N'PUERTO RONDON')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (142, 3, N'SARAVENA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (143, 3, N'TAME')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (144, 4, N'BARANOA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (145, 4, N'BARRANQUILLA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (146, 4, N'CAMPO DE LA CRUZ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (147, 4, N'CANDELARIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (148, 4, N'GALAPA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (149, 4, N'JUAN DE ACOSTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (150, 4, N'LURUACO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (151, 4, N'MALAMBO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (152, 4, N'MANATI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (153, 4, N'PALMAR DE VARELA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (154, 4, N'PIOJO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (155, 4, N'POLO NUEVO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (156, 4, N'PONEDERA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (157, 4, N'PUERTO COLOMBIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (158, 4, N'REPELON')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (159, 4, N'SABANAGRANDE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (160, 4, N'SABANALARGA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (161, 4, N'SANTA LUCIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (162, 4, N'SANTO TOMAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (163, 4, N'SOLEDAD')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (164, 4, N'SUAN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (165, 4, N'TUBARA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (166, 4, N'USIACURI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (167, 5, N'ACHI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (168, 5, N'ALTOS DEL ROSARIO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (169, 5, N'ARENAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (170, 5, N'ARJONA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (171, 5, N'ARROYOHONDO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (172, 5, N'BARRANCO DE LOBA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (173, 5, N'BRAZUELO DE PAPAYAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (174, 5, N'CALAMAR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (175, 5, N'CANTAGALLO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (176, 5, N'CARTAGENA DE INDIAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (177, 5, N'CICUCO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (178, 5, N'CLEMENCIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (179, 5, N'CORDOBA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (180, 5, N'EL CARMEN DE BOLIVAR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (181, 5, N'EL GUAMO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (182, 5, N'EL PENION')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (183, 5, N'HATILLO DE LOBA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (184, 5, N'MAGANGUE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (185, 5, N'MAHATES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (186, 5, N'MARGARITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (187, 5, N'MARIA LA BAJA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (188, 5, N'MONTECRISTO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (189, 5, N'MORALES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (190, 5, N'MORALES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (191, 5, N'NOROSI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (192, 5, N'PINILLOS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (193, 5, N'REGIDOR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (194, 5, N'RIO VIEJO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (195, 5, N'SAN CRISTOBAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (196, 5, N'SAN ESTANISLAO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (197, 5, N'SAN FERNANDO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (198, 5, N'SAN JACINTO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (199, 5, N'SAN JACINTO DEL CAUCA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (200, 5, N'SAN JUAN DE NEPOMUCENO')
GO
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (201, 5, N'SAN MARTIN DE LOBA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (202, 5, N'SAN PABLO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (203, 5, N'SAN PABLO NORTE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (204, 5, N'SANTA CATALINA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (205, 5, N'SANTA CRUZ DE MOMPOX')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (206, 5, N'SANTA ROSA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (207, 5, N'SANTA ROSA DEL SUR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (208, 5, N'SIMITI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (209, 5, N'SOPLAVIENTO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (210, 5, N'TALAIGUA NUEVO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (211, 5, N'TUQUISIO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (212, 5, N'TURBACO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (213, 5, N'TURBANA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (214, 5, N'VILLANUEVA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (215, 5, N'ZAMBRANO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (216, 6, N'AQUITANIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (217, 6, N'ARCABUCO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (218, 6, N'BELÉN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (219, 6, N'BERBEO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (220, 6, N'BETÉITIVA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (221, 6, N'BOAVITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (222, 6, N'BOYACÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (223, 6, N'BRICEÑO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (224, 6, N'BUENAVISTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (225, 6, N'BUSBANZÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (226, 6, N'CALDAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (227, 6, N'CAMPO HERMOSO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (228, 6, N'CERINZA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (229, 6, N'CHINAVITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (230, 6, N'CHIQUINQUIRÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (231, 6, N'CHÍQUIZA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (232, 6, N'CHISCAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (233, 6, N'CHITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (234, 6, N'CHITARAQUE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (235, 6, N'CHIVATÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (236, 6, N'CIÉNEGA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (237, 6, N'CÓMBITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (238, 6, N'COPER')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (239, 6, N'CORRALES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (240, 6, N'COVARACHÍA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (241, 6, N'CUBARA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (242, 6, N'CUCAITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (243, 6, N'CUITIVA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (244, 6, N'DUITAMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (245, 6, N'EL COCUY')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (246, 6, N'EL ESPINO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (247, 6, N'FIRAVITOBA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (248, 6, N'FLORESTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (249, 6, N'GACHANTIVÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (250, 6, N'GÁMEZA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (251, 6, N'GARAGOA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (252, 6, N'GUACAMAYAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (253, 6, N'GÜICÁN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (254, 6, N'IZA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (255, 6, N'JENESANO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (256, 6, N'JERICÓ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (257, 6, N'LA UVITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (258, 6, N'LA VICTORIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (259, 6, N'LABRANZA GRANDE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (260, 6, N'MACANAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (261, 6, N'MARIPÍ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (262, 6, N'MIRAFLORES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (263, 6, N'MONGUA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (264, 6, N'MONGUÍ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (265, 6, N'MONIQUIRÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (266, 6, N'MOTAVITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (267, 6, N'MUZO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (268, 6, N'NOBSA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (269, 6, N'NUEVO COLÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (270, 6, N'OICATÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (271, 6, N'OTANCHE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (272, 6, N'PACHAVITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (273, 6, N'PÁEZ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (274, 6, N'PAIPA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (275, 6, N'PAJARITO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (276, 6, N'PANQUEBA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (277, 6, N'PAUNA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (278, 6, N'PAYA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (279, 6, N'PAZ DE RÍO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (280, 6, N'PESCA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (281, 6, N'PISBA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (282, 6, N'PUERTO BOYACA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (283, 6, N'QUÍPAMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (284, 6, N'RAMIRIQUÍ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (285, 6, N'RÁQUIRA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (286, 6, N'RONDÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (287, 6, N'SABOYÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (288, 6, N'SÁCHICA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (289, 6, N'SAMACÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (290, 6, N'SAN EDUARDO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (291, 6, N'SAN JOSÉ DE PARE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (292, 6, N'SAN LUÍS DE GACENO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (293, 6, N'SAN MATEO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (294, 6, N'SAN MIGUEL DE SEMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (295, 6, N'SAN PABLO DE BORBUR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (296, 6, N'SANTA MARÍA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (297, 6, N'SANTA ROSA DE VITERBO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (298, 6, N'SANTA SOFÍA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (299, 6, N'SANTANA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (300, 6, N'SATIVANORTE')
GO
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (301, 6, N'SATIVASUR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (302, 6, N'SIACHOQUE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (303, 6, N'SOATÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (304, 6, N'SOCHA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (305, 6, N'SOCOTÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (306, 6, N'SOGAMOSO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (307, 6, N'SORA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (308, 6, N'SORACÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (309, 6, N'SOTAQUIRÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (310, 6, N'SUSACÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (311, 6, N'SUTARMACHÁN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (312, 6, N'TASCO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (313, 6, N'TIBANÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (314, 6, N'TIBASOSA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (315, 6, N'TINJACÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (316, 6, N'TIPACOQUE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (317, 6, N'TOCA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (318, 6, N'TOGÜÍ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (319, 6, N'TÓPAGA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (320, 6, N'TOTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (321, 6, N'TUNJA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (322, 6, N'TUNUNGUÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (323, 6, N'TURMEQUÉ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (324, 6, N'TUTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (325, 6, N'TUTAZÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (326, 6, N'UMBITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (327, 6, N'VENTA QUEMADA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (328, 6, N'VILLA DE LEYVA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (329, 6, N'VIRACACHÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (330, 6, N'ZETAQUIRA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (331, 7, N'AGUADAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (332, 7, N'ANSERMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (333, 7, N'ARANZAZU')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (334, 7, N'BELALCAZAR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (335, 7, N'CHINCHINÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (336, 7, N'FILADELFIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (337, 7, N'LA DORADA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (338, 7, N'LA MERCED')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (339, 7, N'MANIZALES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (340, 7, N'MANZANARES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (341, 7, N'MARMATO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (342, 7, N'MARQUETALIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (343, 7, N'MARULANDA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (344, 7, N'NEIRA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (345, 7, N'NORCASIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (346, 7, N'PACORA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (347, 7, N'PALESTINA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (348, 7, N'PENSILVANIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (349, 7, N'RIOSUCIO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (350, 7, N'RISARALDA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (351, 7, N'SALAMINA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (352, 7, N'SAMANA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (353, 7, N'SAN JOSE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (354, 7, N'SUPÍA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (355, 7, N'VICTORIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (356, 7, N'VILLAMARÍA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (357, 7, N'VITERBO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (358, 8, N'ALBANIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (359, 8, N'BELÉN ANDAQUIES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (360, 8, N'CARTAGENA DEL CHAIRA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (361, 8, N'CURILLO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (362, 8, N'EL DONCELLO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (363, 8, N'EL PAUJIL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (364, 8, N'FLORENCIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (365, 8, N'LA MONTAÑITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (366, 8, N'MILÁN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (367, 8, N'MORELIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (368, 8, N'PUERTO RICO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (369, 8, N'SAN  VICENTE DEL CAGUAN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (370, 8, N'SAN JOSÉ DE FRAGUA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (371, 8, N'SOLANO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (372, 8, N'SOLITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (373, 8, N'VALPARAÍSO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (374, 9, N'AGUAZUL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (375, 9, N'CHAMEZA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (376, 9, N'HATO COROZAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (377, 9, N'LA SALINA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (378, 9, N'MANÍ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (379, 9, N'MONTERREY')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (380, 9, N'NUNCHIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (381, 9, N'OROCUE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (382, 9, N'PAZ DE ARIPORO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (383, 9, N'PORE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (384, 9, N'RECETOR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (385, 9, N'SABANA LARGA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (386, 9, N'SACAMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (387, 9, N'SAN LUIS DE PALENQUE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (388, 9, N'TAMARA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (389, 9, N'TAURAMENA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (390, 9, N'TRINIDAD')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (391, 9, N'VILLANUEVA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (392, 9, N'YOPAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (393, 10, N'ALMAGUER')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (394, 10, N'ARGELIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (395, 10, N'BALBOA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (396, 10, N'BOLÍVAR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (397, 10, N'BUENOS AIRES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (398, 10, N'CAJIBIO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (399, 10, N'CALDONO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (400, 10, N'CALOTO')
GO
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (401, 10, N'CORINTO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (402, 10, N'EL TAMBO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (403, 10, N'FLORENCIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (404, 10, N'GUAPI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (405, 10, N'INZA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (406, 10, N'JAMBALÓ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (407, 10, N'LA SIERRA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (408, 10, N'LA VEGA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (409, 10, N'LÓPEZ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (410, 10, N'MERCADERES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (411, 10, N'MIRANDA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (412, 10, N'MORALES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (413, 10, N'PADILLA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (414, 10, N'PÁEZ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (415, 10, N'PATIA (EL BORDO)')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (416, 10, N'PIAMONTE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (417, 10, N'PIENDAMO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (418, 10, N'POPAYÁN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (419, 10, N'PUERTO TEJADA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (420, 10, N'PURACE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (421, 10, N'ROSAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (422, 10, N'SAN SEBASTIÁN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (423, 10, N'SANTA ROSA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (424, 10, N'SANTANDER DE QUILICHAO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (425, 10, N'SILVIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (426, 10, N'SOTARA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (427, 10, N'SUÁREZ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (428, 10, N'SUCRE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (429, 10, N'TIMBÍO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (430, 10, N'TIMBIQUÍ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (431, 10, N'TORIBIO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (432, 10, N'TOTORO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (433, 10, N'VILLA RICA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (434, 11, N'AGUACHICA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (435, 11, N'AGUSTÍN CODAZZI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (436, 11, N'ASTREA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (437, 11, N'BECERRIL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (438, 11, N'BOSCONIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (439, 11, N'CHIMICHAGUA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (440, 11, N'CHIRIGUANÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (441, 11, N'CURUMANÍ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (442, 11, N'EL COPEY')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (443, 11, N'EL PASO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (444, 11, N'GAMARRA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (445, 11, N'GONZÁLEZ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (446, 11, N'LA GLORIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (447, 11, N'LA JAGUA IBIRICO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (448, 11, N'MANAURE BALCÓN DEL CESAR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (449, 11, N'PAILITAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (450, 11, N'PELAYA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (451, 11, N'PUEBLO BELLO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (452, 11, N'RÍO DE ORO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (453, 11, N'ROBLES (LA PAZ)')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (454, 11, N'SAN ALBERTO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (455, 11, N'SAN DIEGO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (456, 11, N'SAN MARTÍN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (457, 11, N'TAMALAMEQUE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (458, 11, N'VALLEDUPAR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (459, 12, N'ACANDI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (460, 12, N'ALTO BAUDO (PIE DE PATO)')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (461, 12, N'ATRATO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (462, 12, N'BAGADO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (463, 12, N'BAHIA SOLANO (MUTIS)')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (464, 12, N'BAJO BAUDO (PIZARRO)')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (465, 12, N'BOJAYA (BELLAVISTA)')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (466, 12, N'CANTON DE SAN PABLO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (467, 12, N'CARMEN DEL DARIEN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (468, 12, N'CERTEGUI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (469, 12, N'CONDOTO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (470, 12, N'EL CARMEN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (471, 12, N'ISTMINA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (472, 12, N'JURADO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (473, 12, N'LITORAL DEL SAN JUAN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (474, 12, N'LLORO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (475, 12, N'MEDIO ATRATO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (476, 12, N'MEDIO BAUDO (BOCA DE PEPE)')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (477, 12, N'MEDIO SAN JUAN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (478, 12, N'NOVITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (479, 12, N'NUQUI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (480, 12, N'QUIBDO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (481, 12, N'RIO IRO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (482, 12, N'RIO QUITO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (483, 12, N'RIOSUCIO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (484, 12, N'SAN JOSE DEL PALMAR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (485, 12, N'SIPI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (486, 12, N'TADO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (487, 12, N'UNGUIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (488, 12, N'UNIÓN PANAMERICANA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (489, 13, N'AYAPEL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (490, 13, N'BUENAVISTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (491, 13, N'CANALETE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (492, 13, N'CERETÉ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (493, 13, N'CHIMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (494, 13, N'CHINÚ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (495, 13, N'CIENAGA DE ORO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (496, 13, N'COTORRA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (497, 13, N'LA APARTADA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (498, 13, N'LORICA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (499, 13, N'LOS CÓRDOBAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (500, 13, N'MOMIL')
GO
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (501, 13, N'MONTELÍBANO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (502, 13, N'MONTERÍA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (503, 13, N'MOÑITOS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (504, 13, N'PLANETA RICA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (505, 13, N'PUEBLO NUEVO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (506, 13, N'PUERTO ESCONDIDO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (507, 13, N'PUERTO LIBERTADOR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (508, 13, N'PURÍSIMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (509, 13, N'SAHAGÚN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (510, 13, N'SAN ANDRÉS SOTAVENTO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (511, 13, N'SAN ANTERO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (512, 13, N'SAN BERNARDO VIENTO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (513, 13, N'SAN CARLOS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (514, 13, N'SAN PELAYO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (515, 13, N'TIERRALTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (516, 13, N'VALENCIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (517, 14, N'AGUA DE DIOS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (518, 14, N'ALBAN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (519, 14, N'ANAPOIMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (520, 14, N'ANOLAIMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (521, 14, N'ARBELAEZ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (522, 14, N'BELTRÁN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (523, 14, N'BITUIMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (524, 14, N'BOGOTÁ DC')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (525, 14, N'BOJACÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (526, 14, N'CABRERA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (527, 14, N'CACHIPAY')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (528, 14, N'CAJICÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (529, 14, N'CAPARRAPÍ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (530, 14, N'CAQUEZA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (531, 14, N'CARMEN DE CARUPA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (532, 14, N'CHAGUANÍ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (533, 14, N'CHIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (534, 14, N'CHIPAQUE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (535, 14, N'CHOACHÍ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (536, 14, N'CHOCONTÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (537, 14, N'COGUA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (538, 14, N'COTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (539, 14, N'CUCUNUBÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (540, 14, N'EL COLEGIO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (541, 14, N'EL PEÑÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (542, 14, N'EL ROSAL1')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (543, 14, N'FACATATIVA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (544, 14, N'FÓMEQUE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (545, 14, N'FOSCA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (546, 14, N'FUNZA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (547, 14, N'FÚQUENE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (548, 14, N'FUSAGASUGA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (549, 14, N'GACHALÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (550, 14, N'GACHANCIPÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (551, 14, N'GACHETA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (552, 14, N'GAMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (553, 14, N'GIRARDOT')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (554, 14, N'GRANADA2')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (555, 14, N'GUACHETÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (556, 14, N'GUADUAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (557, 14, N'GUASCA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (558, 14, N'GUATAQUÍ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (559, 14, N'GUATAVITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (560, 14, N'GUAYABAL DE SIQUIMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (561, 14, N'GUAYABETAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (562, 14, N'GUTIÉRREZ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (563, 14, N'JERUSALÉN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (564, 14, N'JUNÍN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (565, 14, N'LA CALERA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (566, 14, N'LA MESA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (567, 14, N'LA PALMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (568, 14, N'LA PEÑA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (569, 14, N'LA VEGA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (570, 14, N'LENGUAZAQUE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (571, 14, N'MACHETÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (572, 14, N'MADRID')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (573, 14, N'MANTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (574, 14, N'MEDINA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (575, 14, N'MOSQUERA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (576, 14, N'NARIÑO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (577, 14, N'NEMOCÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (578, 14, N'NILO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (579, 14, N'NIMAIMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (580, 14, N'NOCAIMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (581, 14, N'OSPINA PÉREZ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (582, 14, N'PACHO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (583, 14, N'PAIME')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (584, 14, N'PANDI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (585, 14, N'PARATEBUENO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (586, 14, N'PASCA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (587, 14, N'PUERTO SALGAR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (588, 14, N'PULÍ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (589, 14, N'QUEBRADANEGRA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (590, 14, N'QUETAME')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (591, 14, N'QUIPILE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (592, 14, N'RAFAEL REYES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (593, 14, N'RICAURTE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (594, 14, N'SAN  ANTONIO DEL  TEQUENDAMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (595, 14, N'SAN BERNARDO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (596, 14, N'SAN CAYETANO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (597, 14, N'SAN FRANCISCO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (598, 14, N'SAN JUAN DE RIOSECO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (599, 14, N'SASAIMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (600, 14, N'SESQUILÉ')
GO
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (601, 14, N'SIBATÉ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (602, 14, N'SILVANIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (603, 14, N'SIMIJACA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (604, 14, N'SOACHA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (605, 14, N'SOPO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (606, 14, N'SUBACHOQUE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (607, 14, N'SUESCA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (608, 14, N'SUPATÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (609, 14, N'SUSA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (610, 14, N'SUTATAUSA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (611, 14, N'TABIO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (612, 14, N'TAUSA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (613, 14, N'TENA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (614, 14, N'TENJO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (615, 14, N'TIBACUY')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (616, 14, N'TIBIRITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (617, 14, N'TOCAIMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (618, 14, N'TOCANCIPÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (619, 14, N'TOPAIPÍ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (620, 14, N'UBALÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (621, 14, N'UBAQUE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (622, 14, N'UBATÉ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (623, 14, N'UNE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (624, 14, N'UTICA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (625, 14, N'VERGARA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (626, 14, N'VIANI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (627, 14, N'VILLA GOMEZ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (628, 14, N'VILLA PINZÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (629, 14, N'VILLETA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (630, 14, N'VIOTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (631, 14, N'YACOPÍ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (632, 14, N'ZIPACÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (633, 14, N'ZIPAQUIRÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (634, 15, N'BARRANCO MINAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (635, 15, N'CACAHUAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (636, 15, N'INÍRIDA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (637, 15, N'LA GUADALUPE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (638, 15, N'MAPIRIPANA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (639, 15, N'MORICHAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (640, 15, N'PANA PANA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (641, 15, N'PUERTO COLOMBIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (642, 15, N'SAN FELIPE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (643, 16, N'CALAMAR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (644, 16, N'EL RETORNO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (645, 16, N'MIRAFLOREZ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (646, 16, N'SAN JOSÉ DEL GUAVIARE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (647, 17, N'ACEVEDO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (648, 17, N'AGRADO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (649, 17, N'AIPE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (650, 17, N'ALGECIRAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (651, 17, N'ALTAMIRA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (652, 17, N'BARAYA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (653, 17, N'CAMPO ALEGRE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (654, 17, N'COLOMBIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (655, 17, N'ELIAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (656, 17, N'GARZÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (657, 17, N'GIGANTE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (658, 17, N'GUADALUPE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (659, 17, N'HOBO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (660, 17, N'IQUIRA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (661, 17, N'ISNOS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (662, 17, N'LA ARGENTINA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (663, 17, N'LA PLATA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (664, 17, N'NATAGA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (665, 17, N'NEIVA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (666, 17, N'OPORAPA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (667, 17, N'PAICOL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (668, 17, N'PALERMO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (669, 17, N'PALESTINA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (670, 17, N'PITAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (671, 17, N'PITALITO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (672, 17, N'RIVERA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (673, 17, N'SALADO BLANCO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (674, 17, N'SAN AGUSTÍN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (675, 17, N'SANTA MARIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (676, 17, N'SUAZA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (677, 17, N'TARQUI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (678, 17, N'TELLO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (679, 17, N'TERUEL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (680, 17, N'TESALIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (681, 17, N'TIMANA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (682, 17, N'VILLAVIEJA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (683, 17, N'YAGUARA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (684, 18, N'ALBANIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (685, 18, N'BARRANCAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (686, 18, N'DIBULLA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (687, 18, N'DISTRACCIÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (688, 18, N'EL MOLINO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (689, 18, N'FONSECA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (690, 18, N'HATO NUEVO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (691, 18, N'LA JAGUA DEL PILAR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (692, 18, N'MAICAO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (693, 18, N'MANAURE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (694, 18, N'RIOHACHA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (695, 18, N'SAN JUAN DEL CESAR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (696, 18, N'URIBIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (697, 18, N'URUMITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (698, 18, N'VILLANUEVA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (699, 19, N'ALGARROBO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (700, 19, N'ARACATACA')
GO
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (701, 19, N'ARIGUANI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (702, 19, N'CERRO SAN ANTONIO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (703, 19, N'CHIVOLO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (704, 19, N'CIENAGA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (705, 19, N'CONCORDIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (706, 19, N'EL BANCO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (707, 19, N'EL PIÑON')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (708, 19, N'EL RETEN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (709, 19, N'FUNDACION')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (710, 19, N'GUAMAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (711, 19, N'NUEVA GRANADA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (712, 19, N'PEDRAZA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (713, 19, N'PIJIÑO DEL CARMEN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (714, 19, N'PIVIJAY')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (715, 19, N'PLATO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (716, 19, N'PUEBLO VIEJO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (717, 19, N'REMOLINO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (718, 19, N'SABANAS DE SAN ANGEL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (719, 19, N'SALAMINA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (720, 19, N'SAN SEBASTIAN DE BUENAVISTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (721, 19, N'SAN ZENON')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (722, 19, N'SANTA ANA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (723, 19, N'SANTA BARBARA DE PINTO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (724, 19, N'SANTA MARTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (725, 19, N'SITIONUEVO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (726, 19, N'TENERIFE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (727, 19, N'ZAPAYAN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (728, 19, N'ZONA BANANERA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (729, 20, N'ACACIAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (730, 20, N'BARRANCA DE UPIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (731, 20, N'CABUYARO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (732, 20, N'CASTILLA LA NUEVA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (733, 20, N'CUBARRAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (734, 20, N'CUMARAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (735, 20, N'EL CALVARIO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (736, 20, N'EL CASTILLO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (737, 20, N'EL DORADO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (738, 20, N'FUENTE DE ORO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (739, 20, N'GRANADA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (740, 20, N'GUAMAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (741, 20, N'LA MACARENA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (742, 20, N'LA URIBE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (743, 20, N'LEJANÍAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (744, 20, N'MAPIRIPÁN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (745, 20, N'MESETAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (746, 20, N'PUERTO CONCORDIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (747, 20, N'PUERTO GAITÁN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (748, 20, N'PUERTO LLERAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (749, 20, N'PUERTO LÓPEZ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (750, 20, N'PUERTO RICO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (751, 20, N'RESTREPO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (752, 20, N'SAN  JUAN DE ARAMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (753, 20, N'SAN CARLOS GUAROA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (754, 20, N'SAN JUANITO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (755, 20, N'SAN MARTÍN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (756, 20, N'VILLAVICENCIO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (757, 20, N'VISTA HERMOSA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (758, 21, N'ALBAN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (759, 21, N'ALDAÑA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (760, 21, N'ANCUYA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (761, 21, N'ARBOLEDA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (762, 21, N'BARBACOAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (763, 21, N'BELEN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (764, 21, N'BUESACO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (765, 21, N'CHACHAGUI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (766, 21, N'COLON (GENOVA)')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (767, 21, N'CONSACA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (768, 21, N'CONTADERO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (769, 21, N'CORDOBA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (770, 21, N'CUASPUD')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (771, 21, N'CUMBAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (772, 21, N'CUMBITARA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (773, 21, N'EL CHARCO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (774, 21, N'EL PEÑOL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (775, 21, N'EL ROSARIO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (776, 21, N'EL TABLÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (777, 21, N'EL TAMBO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (778, 21, N'FUNES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (779, 21, N'GUACHUCAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (780, 21, N'GUAITARILLA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (781, 21, N'GUALMATAN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (782, 21, N'ILES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (783, 21, N'IMUES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (784, 21, N'IPIALES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (785, 21, N'LA CRUZ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (786, 21, N'LA FLORIDA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (787, 21, N'LA LLANADA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (788, 21, N'LA TOLA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (789, 21, N'LA UNION')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (790, 21, N'LEIVA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (791, 21, N'LINARES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (792, 21, N'LOS ANDES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (793, 21, N'MAGUI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (794, 21, N'MALLAMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (795, 21, N'MOSQUEZA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (796, 21, N'NARIÑO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (797, 21, N'OLAYA HERRERA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (798, 21, N'OSPINA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (799, 21, N'PASTO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (800, 21, N'PIZARRO')
GO
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (801, 21, N'POLICARPA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (802, 21, N'POTOSI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (803, 21, N'PROVIDENCIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (804, 21, N'PUERRES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (805, 21, N'PUPIALES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (806, 21, N'RICAURTE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (807, 21, N'ROBERTO PAYAN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (808, 21, N'SAMANIEGO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (809, 21, N'SAN BERNARDO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (810, 21, N'SAN LORENZO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (811, 21, N'SAN PABLO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (812, 21, N'SAN PEDRO DE CARTAGO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (813, 21, N'SANDONA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (814, 21, N'SANTA BARBARA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (815, 21, N'SANTACRUZ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (816, 21, N'SAPUYES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (817, 21, N'TAMINANGO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (818, 21, N'TANGUA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (819, 21, N'TUMACO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (820, 21, N'TUQUERRES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (821, 21, N'YACUANQUER')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (822, 22, N'ABREGO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (823, 22, N'ARBOLEDAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (824, 22, N'BOCHALEMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (825, 22, N'BUCARASICA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (826, 22, N'CÁCHIRA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (827, 22, N'CÁCOTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (828, 22, N'CHINÁCOTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (829, 22, N'CHITAGÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (830, 22, N'CONVENCIÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (831, 22, N'CÚCUTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (832, 22, N'CUCUTILLA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (833, 22, N'DURANIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (834, 22, N'EL CARMEN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (835, 22, N'EL TARRA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (836, 22, N'EL ZULIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (837, 22, N'GRAMALOTE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (838, 22, N'HACARI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (839, 22, N'HERRÁN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (840, 22, N'LA ESPERANZA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (841, 22, N'LA PLAYA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (842, 22, N'LABATECA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (843, 22, N'LOS PATIOS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (844, 22, N'LOURDES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (845, 22, N'MUTISCUA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (846, 22, N'OCAÑA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (847, 22, N'PAMPLONA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (848, 22, N'PAMPLONITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (849, 22, N'PUERTO SANTANDER')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (850, 22, N'RAGONVALIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (851, 22, N'SALAZAR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (852, 22, N'SAN CALIXTO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (853, 22, N'SAN CAYETANO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (854, 22, N'SANTIAGO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (855, 22, N'SARDINATA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (856, 22, N'SILOS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (857, 22, N'TEORAMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (858, 22, N'TIBÚ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (859, 22, N'TOLEDO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (860, 22, N'VILLA CARO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (861, 22, N'VILLA DEL ROSARIO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (862, 23, N'COLÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (863, 23, N'MOCOA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (864, 23, N'ORITO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (865, 23, N'PUERTO ASÍS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (866, 23, N'PUERTO CAYCEDO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (867, 23, N'PUERTO GUZMÁN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (868, 23, N'PUERTO LEGUÍZAMO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (869, 23, N'SAN FRANCISCO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (870, 23, N'SAN MIGUEL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (871, 23, N'SANTIAGO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (872, 23, N'SIBUNDOY')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (873, 23, N'VALLE DEL GUAMUEZ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (874, 23, N'VILLAGARZÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (875, 24, N'ARMENIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (876, 24, N'BUENAVISTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (877, 24, N'CALARCÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (878, 24, N'CIRCASIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (879, 24, N'CÓRDOBA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (880, 24, N'FILANDIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (881, 24, N'GÉNOVA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (882, 24, N'LA TEBAIDA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (883, 24, N'MONTENEGRO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (884, 24, N'PIJAO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (885, 24, N'QUIMBAYA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (886, 24, N'SALENTO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (887, 25, N'APIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (888, 25, N'BALBOA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (889, 25, N'BELÉN DE UMBRÍA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (890, 25, N'DOS QUEBRADAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (891, 25, N'GUATICA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (892, 25, N'LA CELIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (893, 25, N'LA VIRGINIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (894, 25, N'MARSELLA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (895, 25, N'MISTRATO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (896, 25, N'PEREIRA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (897, 25, N'PUEBLO RICO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (898, 25, N'QUINCHÍA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (899, 25, N'SANTA ROSA DE CABAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (900, 25, N'SANTUARIO')
GO
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (901, 26, N'PROVIDENCIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (902, 26, N'SAN ANDRES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (903, 26, N'SANTA CATALINA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (904, 27, N'AGUADA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (905, 27, N'ALBANIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (906, 27, N'ARATOCA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (907, 27, N'BARBOSA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (908, 27, N'BARICHARA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (909, 27, N'BARRANCABERMEJA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (910, 27, N'BETULIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (911, 27, N'BOLÍVAR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (912, 27, N'BUCARAMANGA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (913, 27, N'CABRERA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (914, 27, N'CALIFORNIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (915, 27, N'CAPITANEJO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (916, 27, N'CARCASI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (917, 27, N'CEPITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (918, 27, N'CERRITO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (919, 27, N'CHARALÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (920, 27, N'CHARTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (921, 27, N'CHIMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (922, 27, N'CHIPATÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (923, 27, N'CIMITARRA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (924, 27, N'CONCEPCIÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (925, 27, N'CONFINES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (926, 27, N'CONTRATACIÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (927, 27, N'COROMORO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (928, 27, N'CURITÍ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (929, 27, N'EL CARMEN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (930, 27, N'EL GUACAMAYO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (931, 27, N'EL PEÑÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (932, 27, N'EL PLAYÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (933, 27, N'ENCINO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (934, 27, N'ENCISO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (935, 27, N'FLORIÁN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (936, 27, N'FLORIDABLANCA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (937, 27, N'GALÁN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (938, 27, N'GAMBITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (939, 27, N'GIRÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (940, 27, N'GUACA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (941, 27, N'GUADALUPE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (942, 27, N'GUAPOTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (943, 27, N'GUAVATÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (944, 27, N'GUEPSA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (945, 27, N'HATO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (946, 27, N'JESÚS MARIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (947, 27, N'JORDÁN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (948, 27, N'LA BELLEZA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (949, 27, N'LA PAZ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (950, 27, N'LANDAZURI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (951, 27, N'LEBRIJA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (952, 27, N'LOS SANTOS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (953, 27, N'MACARAVITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (954, 27, N'MÁLAGA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (955, 27, N'MATANZA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (956, 27, N'MOGOTES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (957, 27, N'MOLAGAVITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (958, 27, N'OCAMONTE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (959, 27, N'OIBA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (960, 27, N'ONZAGA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (961, 27, N'PALMAR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (962, 27, N'PALMAS DEL SOCORRO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (963, 27, N'PÁRAMO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (964, 27, N'PIEDECUESTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (965, 27, N'PINCHOTE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (966, 27, N'PUENTE NACIONAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (967, 27, N'PUERTO PARRA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (968, 27, N'PUERTO WILCHES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (969, 27, N'RIONEGRO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (970, 27, N'SABANA DE TORRES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (971, 27, N'SAN ANDRÉS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (972, 27, N'SAN BENITO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (973, 27, N'SAN GIL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (974, 27, N'SAN JOAQUÍN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (975, 27, N'SAN JOSÉ DE MIRANDA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (976, 27, N'SAN MIGUEL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (977, 27, N'SAN VICENTE DE CHUCURÍ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (978, 27, N'SANTA BÁRBARA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (979, 27, N'SANTA HELENA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (980, 27, N'SIMACOTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (981, 27, N'SOCORRO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (982, 27, N'SUAITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (983, 27, N'SUCRE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (984, 27, N'SURATA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (985, 27, N'TONA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (986, 27, N'VALLE SAN JOSÉ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (987, 27, N'VÉLEZ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (988, 27, N'VETAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (989, 27, N'VILLANUEVA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (990, 27, N'ZAPATOCA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (991, 28, N'BUENAVISTA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (992, 28, N'CAIMITO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (993, 28, N'CHALÁN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (994, 28, N'COLOSO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (995, 28, N'COROZAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (996, 28, N'EL ROBLE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (997, 28, N'GALERAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (998, 28, N'GUARANDA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (999, 28, N'LA UNIÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1000, 28, N'LOS PALMITOS')
GO
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1001, 28, N'MAJAGUAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1002, 28, N'MORROA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1003, 28, N'OVEJAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1004, 28, N'PALMITO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1005, 28, N'SAMPUES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1006, 28, N'SAN BENITO ABAD')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1007, 28, N'SAN JUAN DE BETULIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1008, 28, N'SAN MARCOS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1009, 28, N'SAN ONOFRE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1010, 28, N'SAN PEDRO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1011, 28, N'SINCÉ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1012, 28, N'SINCELEJO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1013, 28, N'SUCRE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1014, 28, N'TOLÚ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1015, 28, N'TOLUVIEJO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1016, 29, N'ALPUJARRA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1017, 29, N'ALVARADO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1018, 29, N'AMBALEMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1019, 29, N'ANZOATEGUI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1020, 29, N'ARMERO (GUAYABAL)')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1021, 29, N'ATACO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1022, 29, N'CAJAMARCA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1023, 29, N'CARMEN DE APICALÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1024, 29, N'CASABIANCA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1025, 29, N'CHAPARRAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1026, 29, N'COELLO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1027, 29, N'COYAIMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1028, 29, N'CUNDAY')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1029, 29, N'DOLORES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1030, 29, N'ESPINAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1031, 29, N'FALÁN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1032, 29, N'FLANDES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1033, 29, N'FRESNO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1034, 29, N'GUAMO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1035, 29, N'HERVEO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1036, 29, N'HONDA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1037, 29, N'IBAGUÉ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1038, 29, N'ICONONZO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1039, 29, N'LÉRIDA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1040, 29, N'LÍBANO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1041, 29, N'MARIQUITA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1042, 29, N'MELGAR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1043, 29, N'MURILLO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1044, 29, N'NATAGAIMA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1045, 29, N'ORTEGA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1046, 29, N'PALOCABILDO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1047, 29, N'PIEDRAS PLANADAS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1048, 29, N'PRADO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1049, 29, N'PURIFICACIÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1050, 29, N'RIOBLANCO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1051, 29, N'RONCESVALLES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1052, 29, N'ROVIRA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1053, 29, N'SALDAÑA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1054, 29, N'SAN ANTONIO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1055, 29, N'SAN LUIS')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1056, 29, N'SANTA ISABEL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1057, 29, N'SUÁREZ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1058, 29, N'VALLE DE SAN JUAN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1059, 29, N'VENADILLO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1060, 29, N'VILLAHERMOSA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1061, 29, N'VILLARRICA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1062, 30, N'ALCALÁ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1063, 30, N'ANDALUCÍA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1064, 30, N'ANSERMA NUEVO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1065, 30, N'ARGELIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1066, 30, N'BOLÍVAR')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1067, 30, N'BUENAVENTURA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1068, 30, N'BUGA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1069, 30, N'BUGALAGRANDE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1070, 30, N'CAICEDONIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1071, 30, N'CALI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1072, 30, N'CALIMA (DARIEN)')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1073, 30, N'CANDELARIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1074, 30, N'CARTAGO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1075, 30, N'DAGUA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1076, 30, N'EL AGUILA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1077, 30, N'EL CAIRO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1078, 30, N'EL CERRITO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1079, 30, N'EL DOVIO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1080, 30, N'FLORIDA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1081, 30, N'GINEBRA GUACARI')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1082, 30, N'JAMUNDÍ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1083, 30, N'LA CUMBRE')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1084, 30, N'LA UNIÓN')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1085, 30, N'LA VICTORIA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1086, 30, N'OBANDO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1087, 30, N'PALMIRA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1088, 30, N'PRADERA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1089, 30, N'RESTREPO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1090, 30, N'RIO FRÍO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1091, 30, N'ROLDANILLO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1092, 30, N'SAN PEDRO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1093, 30, N'SEVILLA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1094, 30, N'TORO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1095, 30, N'TRUJILLO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1096, 30, N'TULÚA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1097, 30, N'ULLOA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1098, 30, N'VERSALLES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1099, 30, N'VIJES')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1100, 30, N'YOTOCO')
GO
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1101, 30, N'YUMBO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1102, 30, N'ZARZAL')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1103, 31, N'CARURÚ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1104, 31, N'MITÚ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1105, 31, N'PACOA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1106, 31, N'PAPUNAUA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1107, 31, N'TARAIRA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1108, 31, N'YAVARATÉ')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1109, 32, N'CUMARIBO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1110, 32, N'LA PRIMAVERA')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1111, 32, N'PUERTO CARREÑO')
INSERT [dbo].[Municipio] ([idMunicipio], [idDepartamento], [NombreMunicipio]) VALUES (1112, 32, N'SANTA ROSALIA')
INSERT [dbo].[Persona] ([DocumentoPersona], [TipoDocumento], [TipoContratoPersona], [NombrePersona], [Genero], [Telefono], [FechaNacimineto], [EstadoPersona]) VALUES (N'1', N'CC', N'Temporal', N'SNIEDER URREGO', N'M', N'2341234', CAST(N'2016-01-01' AS Date), N'A')
INSERT [dbo].[Persona] ([DocumentoPersona], [TipoDocumento], [TipoContratoPersona], [NombrePersona], [Genero], [Telefono], [FechaNacimineto], [EstadoPersona]) VALUES (N'1036612756', N'CC', N'Temporal', N'andres vargas', N'M', N'3671145', CAST(N'2015-12-16' AS Date), N'I')
INSERT [dbo].[Persona] ([DocumentoPersona], [TipoDocumento], [TipoContratoPersona], [NombrePersona], [Genero], [Telefono], [FechaNacimineto], [EstadoPersona]) VALUES (N'103665432', N'CC', N'Temporal', N'ANDRES BENITEZ GONZALEZ', N'M', N'234098', CAST(N'2015-12-09' AS Date), N'A')
INSERT [dbo].[Persona] ([DocumentoPersona], [TipoDocumento], [TipoContratoPersona], [NombrePersona], [Genero], [Telefono], [FechaNacimineto], [EstadoPersona]) VALUES (N'45234321', N'CC', N'Temporal', N'ABDRES', N'M', N'453212', CAST(N'2015-12-29' AS Date), N'A')
INSERT [dbo].[Persona] ([DocumentoPersona], [TipoDocumento], [TipoContratoPersona], [NombrePersona], [Genero], [Telefono], [FechaNacimineto], [EstadoPersona]) VALUES (N'9090901', N'CC', N'Permanente', N'Carlos Lopez', N'M', N'231', CAST(N'1991-01-01' AS Date), N'A')
SET IDENTITY_INSERT [dbo].[Produccion] ON 

INSERT [dbo].[Produccion] ([idProduccion], [idLote], [Fecha], [Cantidad], [EstadoProduccion]) VALUES (6, 1, CAST(N'2016-02-11' AS Date), CAST(6000 AS Decimal(18, 0)), N'VC')
INSERT [dbo].[Produccion] ([idProduccion], [idLote], [Fecha], [Cantidad], [EstadoProduccion]) VALUES (7, 2, CAST(N'2016-02-18' AS Date), CAST(2343 AS Decimal(18, 0)), N'VC')
INSERT [dbo].[Produccion] ([idProduccion], [idLote], [Fecha], [Cantidad], [EstadoProduccion]) VALUES (8, 2, CAST(N'2016-02-18' AS Date), CAST(200 AS Decimal(18, 0)), N'VC')
INSERT [dbo].[Produccion] ([idProduccion], [idLote], [Fecha], [Cantidad], [EstadoProduccion]) VALUES (9, 1, CAST(N'2016-02-19' AS Date), CAST(235 AS Decimal(18, 0)), N'VC')
INSERT [dbo].[Produccion] ([idProduccion], [idLote], [Fecha], [Cantidad], [EstadoProduccion]) VALUES (10, 2, CAST(N'2016-02-18' AS Date), CAST(400 AS Decimal(18, 0)), N'VC')
INSERT [dbo].[Produccion] ([idProduccion], [idLote], [Fecha], [Cantidad], [EstadoProduccion]) VALUES (11, 1, CAST(N'2016-02-18' AS Date), CAST(500 AS Decimal(18, 0)), N'VC')
INSERT [dbo].[Produccion] ([idProduccion], [idLote], [Fecha], [Cantidad], [EstadoProduccion]) VALUES (12, 1, CAST(N'2016-02-04' AS Date), CAST(433 AS Decimal(18, 0)), N'NV')
SET IDENTITY_INSERT [dbo].[Produccion] OFF
SET IDENTITY_INSERT [dbo].[Producto] ON 

INSERT [dbo].[Producto] ([idProducto], [NombreProducto], [Descripcion], [EstadoProducto]) VALUES (1, N'Producto', N'Prueba', N'A')
INSERT [dbo].[Producto] ([idProducto], [NombreProducto], [Descripcion], [EstadoProducto]) VALUES (2, N'andres2', N'asdf', N'A')
INSERT [dbo].[Producto] ([idProducto], [NombreProducto], [Descripcion], [EstadoProducto]) VALUES (3, N'cafe seco', N'pruebaasdfasdf', N'A')
INSERT [dbo].[Producto] ([idProducto], [NombreProducto], [Descripcion], [EstadoProducto]) VALUES (4, N'CAFE MOTA', N'CAFE PRUEBA', N'A')
SET IDENTITY_INSERT [dbo].[Producto] OFF
INSERT [dbo].[Proveedor] ([Nit], [TipoDocumento], [NombreProveedor], [Telefono], [Direccion], [EstadoProveedor]) VALUES (N'123', N'CC', N'Andres', N'1234567', N'Asdfghjk', N'A')
INSERT [dbo].[Proveedor] ([Nit], [TipoDocumento], [NombreProveedor], [Telefono], [Direccion], [EstadoProveedor]) VALUES (N'123456', N'NIT', N'juan s', N'32456', N'cll 5B 7 este 45', N'I')
INSERT [dbo].[Proveedor] ([Nit], [TipoDocumento], [NombreProveedor], [Telefono], [Direccion], [EstadoProveedor]) VALUES (N'1785623', N'NIT', N'fabrica ', N'234567', N'cll 5 b 7 este 57', N'A')
SET IDENTITY_INSERT [dbo].[RegistroPago] ON 

INSERT [dbo].[RegistroPago] ([idRegistroPago], [Fecha]) VALUES (1, CAST(N'2016-03-11' AS Date))
INSERT [dbo].[RegistroPago] ([idRegistroPago], [Fecha]) VALUES (2, CAST(N'2016-03-11' AS Date))
SET IDENTITY_INSERT [dbo].[RegistroPago] OFF
SET IDENTITY_INSERT [dbo].[RegistroPagoSalario] ON 

INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (2, 1, 1)
INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (3, 1, 2)
INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (4, 1, 3)
INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (5, 1, 4)
INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (6, 1, 5)
INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (7, 1, 6)
INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (8, 1, 7)
INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (9, 1, 8)
INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (10, 1, 9)
INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (11, 1, 10)
INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (12, 1, 11)
INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (13, 1, 12)
INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (14, 1, 13)
INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (15, 1, 14)
INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (16, 1, 15)
INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (17, 1, 16)
INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (18, 1, 17)
INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (19, 1, 18)
INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (20, 1, 19)
INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (21, 1, 20)
INSERT [dbo].[RegistroPagoSalario] ([idRegistroPagoSalario], [idRegistroPago], [idSalarioPersonaTemporal]) VALUES (22, 1, 21)
SET IDENTITY_INSERT [dbo].[RegistroPagoSalario] OFF
SET IDENTITY_INSERT [dbo].[SalarioPersonaPermanente] ON 

INSERT [dbo].[SalarioPersonaPermanente] ([idSalarioPersonaPermanente], [DocumentoPersona], [Valor], [IdRegistroPago]) VALUES (1, N'9090901', 20000.0000, 2)
SET IDENTITY_INSERT [dbo].[SalarioPersonaPermanente] OFF
SET IDENTITY_INSERT [dbo].[SalarioPersonaTemporal] ON 

INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (1, N'103665432', 1, 1500, 300.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (2, N'9090901', 1, 2000, 300.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (3, N'103665432', 2, 500, 120.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (4, N'9090901', 3, 123, 2323.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (5, N'103665432', 4, 145, 12345.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (6, N'103665432', 5, 3500, 1000.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (7, N'9090901', 5, 3000, 1000.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (8, N'103665432', 6, 2343, 2000.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (9, N'9090901', 7, 200, 1000.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (10, N'103665432', 8, 235, 1000.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (11, N'103665432', 9, 400, 10000.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (12, N'103665432', 10, 500, 20000.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (13, N'103665432', 11, 500, 20000.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (14, N'9090901', 12, 233, 1888.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (15, N'103665432', 12, 200, 1990.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (16, N'103665432', 13, 34, 100.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (17, N'103665432', 14, 34, 100.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (18, N'9090901', 15, 30, 1000.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (19, N'103665432', 15, 60, 1000.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (20, N'1', 16, 1000, 12.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (21, N'1', 17, 1000, 12.0000, N'P')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (22, N'1', 26, 1212, 123.0000, N'D')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (23, N'103665432', 26, 1200, 123.0000, N'D')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (24, N'1', 27, 412, 100.0000, N'D')
INSERT [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal], [DocumentoPersona], [idLabor_Lote], [Cantidad], [Valor], [EstadoCuenta]) VALUES (25, N'1', 28, 5000, 12.0000, N'D')
SET IDENTITY_INSERT [dbo].[SalarioPersonaTemporal] OFF
SET IDENTITY_INSERT [dbo].[TipoArbol] ON 

INSERT [dbo].[TipoArbol] ([idTipoArbol], [NombreTipoArbol], [Descripcion], [EstadoTipoArbol], [TiempoProduccion]) VALUES (1, N'zoca', N'prueba', N'A', 18)
INSERT [dbo].[TipoArbol] ([idTipoArbol], [NombreTipoArbol], [Descripcion], [EstadoTipoArbol], [TiempoProduccion]) VALUES (2, N'Almacigo', N'arbol pequeño', N'A', 36)
INSERT [dbo].[TipoArbol] ([idTipoArbol], [NombreTipoArbol], [Descripcion], [EstadoTipoArbol], [TiempoProduccion]) VALUES (3, N'rezoca', N'se corta a 10 cm', N'A', 18)
INSERT [dbo].[TipoArbol] ([idTipoArbol], [NombreTipoArbol], [Descripcion], [EstadoTipoArbol], [TiempoProduccion]) VALUES (4, N'ZOCA CALAVERA', N'PRUEBA', N'A', 14)
INSERT [dbo].[TipoArbol] ([idTipoArbol], [NombreTipoArbol], [Descripcion], [EstadoTipoArbol], [TiempoProduccion]) VALUES (5, N'ZOCA RECORTE', N'PRUEBA', N'A', 25)
INSERT [dbo].[TipoArbol] ([idTipoArbol], [NombreTipoArbol], [Descripcion], [EstadoTipoArbol], [TiempoProduccion]) VALUES (6, N'Zoca Dos', N'Prueba', N'A', 18)
SET IDENTITY_INSERT [dbo].[TipoArbol] OFF
SET IDENTITY_INSERT [dbo].[TipoInsumo] ON 

INSERT [dbo].[TipoInsumo] ([idTipoInsumo], [NombreTipoInsumo], [Descripcion], [EstadoTipoInsumo]) VALUES (1, N'Insumo Uno', N'Prueba', N'A')
INSERT [dbo].[TipoInsumo] ([idTipoInsumo], [NombreTipoInsumo], [Descripcion], [EstadoTipoInsumo]) VALUES (2, N'abonos', N'prueba', N'A')
INSERT [dbo].[TipoInsumo] ([idTipoInsumo], [NombreTipoInsumo], [Descripcion], [EstadoTipoInsumo]) VALUES (3, N'Prueba', N'Esto es una prueba', N'A')
INSERT [dbo].[TipoInsumo] ([idTipoInsumo], [NombreTipoInsumo], [Descripcion], [EstadoTipoInsumo]) VALUES (4, N'prueba', N'base', N'A')
SET IDENTITY_INSERT [dbo].[TipoInsumo] OFF
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([idUsuario], [Nickname], [Rol], [Contrasena], [PreguntaSeguridad], [Respuesta], [EstadoUsuario]) VALUES (1, N'admin', N'Empleado', N'MQAyADMA', N'¿Cuál es el nombre de su primer mascota?', N'123', N'A')
INSERT [dbo].[Usuario] ([idUsuario], [Nickname], [Rol], [Contrasena], [PreguntaSeguridad], [Respuesta], [EstadoUsuario]) VALUES (2, N'carlos', N'Administrador', N'MQAyADMA', N'¿Cuál es el nombre de su primer mascota?', N'123', N'A')
SET IDENTITY_INSERT [dbo].[Usuario] OFF
SET IDENTITY_INSERT [dbo].[Venta] ON 

INSERT [dbo].[Venta] ([idVenta], [idProducto], [Fecha], [PrecioCarga], [CantidadCargas]) VALUES (3, 4, CAST(N'2016-02-17' AS Date), 600000.0000, CAST(20.00 AS Decimal(18, 2)))
INSERT [dbo].[Venta] ([idVenta], [idProducto], [Fecha], [PrecioCarga], [CantidadCargas]) VALUES (8, 4, CAST(N'2016-02-25' AS Date), 1000.0000, CAST(21.00 AS Decimal(18, 2)))
INSERT [dbo].[Venta] ([idVenta], [idProducto], [Fecha], [PrecioCarga], [CantidadCargas]) VALUES (9, 4, CAST(N'2016-02-02' AS Date), 1000000.0000, CAST(26.00 AS Decimal(18, 2)))
INSERT [dbo].[Venta] ([idVenta], [idProducto], [Fecha], [PrecioCarga], [CantidadCargas]) VALUES (10, 4, CAST(N'2016-02-18' AS Date), 10000.0000, CAST(3.00 AS Decimal(18, 2)))
INSERT [dbo].[Venta] ([idVenta], [idProducto], [Fecha], [PrecioCarga], [CantidadCargas]) VALUES (11, 4, CAST(N'2016-02-18' AS Date), 2345.0000, CAST(7.20 AS Decimal(18, 2)))
INSERT [dbo].[Venta] ([idVenta], [idProducto], [Fecha], [PrecioCarga], [CantidadCargas]) VALUES (12, 1, CAST(N'2016-03-10' AS Date), 600000.0000, CAST(3.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Venta] OFF
ALTER TABLE [dbo].[AbonoCompra]  WITH CHECK ADD  CONSTRAINT [FK_AbonoCompra_Compra] FOREIGN KEY([idCompra])
REFERENCES [dbo].[Compra] ([idCompra])
GO
ALTER TABLE [dbo].[AbonoCompra] CHECK CONSTRAINT [FK_AbonoCompra_Compra]
GO
ALTER TABLE [dbo].[AbonoDeuda]  WITH CHECK ADD  CONSTRAINT [FK_AbonoDeuda_DeudaPersona] FOREIGN KEY([idDeudaPersona])
REFERENCES [dbo].[DeudaPersona] ([idDeudaPersona])
GO
ALTER TABLE [dbo].[AbonoDeuda] CHECK CONSTRAINT [FK_AbonoDeuda_DeudaPersona]
GO
ALTER TABLE [dbo].[AbonoEgreso]  WITH CHECK ADD  CONSTRAINT [FK_AbonoEgreso_Egreso] FOREIGN KEY([idEgreso])
REFERENCES [dbo].[Egreso] ([idEgreso])
GO
ALTER TABLE [dbo].[AbonoEgreso] CHECK CONSTRAINT [FK_AbonoEgreso_Egreso]
GO
ALTER TABLE [dbo].[Arboles]  WITH CHECK ADD  CONSTRAINT [FK_Arboles_Lote] FOREIGN KEY([idLote])
REFERENCES [dbo].[Lote] ([idLote])
GO
ALTER TABLE [dbo].[Arboles] CHECK CONSTRAINT [FK_Arboles_Lote]
GO
ALTER TABLE [dbo].[Arboles]  WITH CHECK ADD  CONSTRAINT [FK_Arboles_TipoArbol] FOREIGN KEY([idTIpoArbol])
REFERENCES [dbo].[TipoArbol] ([idTipoArbol])
GO
ALTER TABLE [dbo].[Arboles] CHECK CONSTRAINT [FK_Arboles_TipoArbol]
GO
ALTER TABLE [dbo].[Compra]  WITH CHECK ADD  CONSTRAINT [FK_Compra_Proveedor] FOREIGN KEY([NitProveedor])
REFERENCES [dbo].[Proveedor] ([Nit])
GO
ALTER TABLE [dbo].[Compra] CHECK CONSTRAINT [FK_Compra_Proveedor]
GO
ALTER TABLE [dbo].[Compra_Insumo]  WITH CHECK ADD  CONSTRAINT [FK_AbonoCompra_Insumo] FOREIGN KEY([idInsumo])
REFERENCES [dbo].[Insumo] ([idInsumo])
GO
ALTER TABLE [dbo].[Compra_Insumo] CHECK CONSTRAINT [FK_AbonoCompra_Insumo]
GO
ALTER TABLE [dbo].[Compra_Insumo]  WITH CHECK ADD  CONSTRAINT [FK_CompraInsumo_Compra] FOREIGN KEY([idCompra])
REFERENCES [dbo].[Compra] ([idCompra])
GO
ALTER TABLE [dbo].[Compra_Insumo] CHECK CONSTRAINT [FK_CompraInsumo_Compra]
GO
ALTER TABLE [dbo].[CostoBeneficio]  WITH CHECK ADD  CONSTRAINT [FK_CostoBeneficio_Compra] FOREIGN KEY([idCompra])
REFERENCES [dbo].[Compra] ([idCompra])
GO
ALTER TABLE [dbo].[CostoBeneficio] CHECK CONSTRAINT [FK_CostoBeneficio_Compra]
GO
ALTER TABLE [dbo].[CostoBeneficio]  WITH CHECK ADD  CONSTRAINT [FK_CostoBeneficio_Venta] FOREIGN KEY([idVenta])
REFERENCES [dbo].[Venta] ([idVenta])
GO
ALTER TABLE [dbo].[CostoBeneficio] CHECK CONSTRAINT [FK_CostoBeneficio_Venta]
GO
ALTER TABLE [dbo].[DeudaPersona]  WITH CHECK ADD  CONSTRAINT [FK_DeudaPersona_Persona] FOREIGN KEY([DocumentoPersona])
REFERENCES [dbo].[Persona] ([DocumentoPersona])
GO
ALTER TABLE [dbo].[DeudaPersona] CHECK CONSTRAINT [FK_DeudaPersona_Persona]
GO
ALTER TABLE [dbo].[Egreso]  WITH CHECK ADD  CONSTRAINT [FK_Egreso_Concepto] FOREIGN KEY([idConcepto])
REFERENCES [dbo].[Concepto] ([idConcepto])
GO
ALTER TABLE [dbo].[Egreso] CHECK CONSTRAINT [FK_Egreso_Concepto]
GO
ALTER TABLE [dbo].[Finca]  WITH CHECK ADD  CONSTRAINT [FK_Finca_Municipio] FOREIGN KEY([idMunicipio])
REFERENCES [dbo].[Municipio] ([idMunicipio])
GO
ALTER TABLE [dbo].[Finca] CHECK CONSTRAINT [FK_Finca_Municipio]
GO
ALTER TABLE [dbo].[Insumo]  WITH CHECK ADD  CONSTRAINT [FK_Insumo_TipoInsumo] FOREIGN KEY([idTipoInsumo])
REFERENCES [dbo].[TipoInsumo] ([idTipoInsumo])
GO
ALTER TABLE [dbo].[Insumo] CHECK CONSTRAINT [FK_Insumo_TipoInsumo]
GO
ALTER TABLE [dbo].[Labor_Lote]  WITH CHECK ADD  CONSTRAINT [FK_LaborLote_Labor] FOREIGN KEY([idLabor])
REFERENCES [dbo].[Labor] ([idLabor])
GO
ALTER TABLE [dbo].[Labor_Lote] CHECK CONSTRAINT [FK_LaborLote_Labor]
GO
ALTER TABLE [dbo].[Labor_Lote]  WITH CHECK ADD  CONSTRAINT [FK_LaborLote_Lote] FOREIGN KEY([idLote])
REFERENCES [dbo].[Lote] ([idLote])
GO
ALTER TABLE [dbo].[Labor_Lote] CHECK CONSTRAINT [FK_LaborLote_Lote]
GO
ALTER TABLE [dbo].[LaborLote_Insumo]  WITH CHECK ADD  CONSTRAINT [FK_LaborLoteInsumo_Insumo] FOREIGN KEY([idInsumo])
REFERENCES [dbo].[Insumo] ([idInsumo])
GO
ALTER TABLE [dbo].[LaborLote_Insumo] CHECK CONSTRAINT [FK_LaborLoteInsumo_Insumo]
GO
ALTER TABLE [dbo].[LaborLote_Insumo]  WITH CHECK ADD  CONSTRAINT [FK_LaborLoteInsumo_LaborLote] FOREIGN KEY([idLabor_Lote])
REFERENCES [dbo].[Labor_Lote] ([idLabor_Lote])
GO
ALTER TABLE [dbo].[LaborLote_Insumo] CHECK CONSTRAINT [FK_LaborLoteInsumo_LaborLote]
GO
ALTER TABLE [dbo].[Lote]  WITH CHECK ADD  CONSTRAINT [FK_Lote_Finca] FOREIGN KEY([idFinca])
REFERENCES [dbo].[Finca] ([idFinca])
GO
ALTER TABLE [dbo].[Lote] CHECK CONSTRAINT [FK_Lote_Finca]
GO
ALTER TABLE [dbo].[MovimientoProduccion]  WITH CHECK ADD  CONSTRAINT [FK_MovimientoProduccion_Produccion] FOREIGN KEY([idProduccion])
REFERENCES [dbo].[Produccion] ([idProduccion])
GO
ALTER TABLE [dbo].[MovimientoProduccion] CHECK CONSTRAINT [FK_MovimientoProduccion_Produccion]
GO
ALTER TABLE [dbo].[MovimientoProduccion]  WITH CHECK ADD  CONSTRAINT [FK_MovimientoProduccion_Venta] FOREIGN KEY([idVenta])
REFERENCES [dbo].[Venta] ([idVenta])
GO
ALTER TABLE [dbo].[MovimientoProduccion] CHECK CONSTRAINT [FK_MovimientoProduccion_Venta]
GO
ALTER TABLE [dbo].[MovimientosArboles]  WITH CHECK ADD  CONSTRAINT [FK_MovimientosArboles_Arboles] FOREIGN KEY([idArboles])
REFERENCES [dbo].[Arboles] ([idArboles])
GO
ALTER TABLE [dbo].[MovimientosArboles] CHECK CONSTRAINT [FK_MovimientosArboles_Arboles]
GO
ALTER TABLE [dbo].[Municipio]  WITH CHECK ADD  CONSTRAINT [FK_Municipio_Departamento] FOREIGN KEY([idDepartamento])
REFERENCES [dbo].[Departamento] ([idDepartamento])
GO
ALTER TABLE [dbo].[Municipio] CHECK CONSTRAINT [FK_Municipio_Departamento]
GO
ALTER TABLE [dbo].[Produccion]  WITH CHECK ADD  CONSTRAINT [FK_Produccion_Lote] FOREIGN KEY([idLote])
REFERENCES [dbo].[Lote] ([idLote])
GO
ALTER TABLE [dbo].[Produccion] CHECK CONSTRAINT [FK_Produccion_Lote]
GO
ALTER TABLE [dbo].[RegistroPagoSalario]  WITH CHECK ADD  CONSTRAINT [FK_RegistroPagoSalario_idSalarioPersonaTemporal] FOREIGN KEY([idSalarioPersonaTemporal])
REFERENCES [dbo].[SalarioPersonaTemporal] ([idSalarioPersonaTemporal])
GO
ALTER TABLE [dbo].[RegistroPagoSalario] CHECK CONSTRAINT [FK_RegistroPagoSalario_idSalarioPersonaTemporal]
GO
ALTER TABLE [dbo].[RegistroPagoSalario]  WITH CHECK ADD  CONSTRAINT [FK_RegistroPagoSalario_RegistroPago] FOREIGN KEY([idRegistroPago])
REFERENCES [dbo].[RegistroPago] ([idRegistroPago])
GO
ALTER TABLE [dbo].[RegistroPagoSalario] CHECK CONSTRAINT [FK_RegistroPagoSalario_RegistroPago]
GO
ALTER TABLE [dbo].[SalarioPersonaPermanente]  WITH CHECK ADD  CONSTRAINT [FK_SalarioPersonaPermanente_Persona] FOREIGN KEY([DocumentoPersona])
REFERENCES [dbo].[Persona] ([DocumentoPersona])
GO
ALTER TABLE [dbo].[SalarioPersonaPermanente] CHECK CONSTRAINT [FK_SalarioPersonaPermanente_Persona]
GO
ALTER TABLE [dbo].[SalarioPersonaPermanente]  WITH CHECK ADD  CONSTRAINT [FK_SalarioPersonaPermanente_RegistroPago] FOREIGN KEY([IdRegistroPago])
REFERENCES [dbo].[RegistroPago] ([idRegistroPago])
GO
ALTER TABLE [dbo].[SalarioPersonaPermanente] CHECK CONSTRAINT [FK_SalarioPersonaPermanente_RegistroPago]
GO
ALTER TABLE [dbo].[SalarioPersonaTemporal]  WITH CHECK ADD  CONSTRAINT [FK_SalarioPersonaTemporal_LaborLote] FOREIGN KEY([idLabor_Lote])
REFERENCES [dbo].[Labor_Lote] ([idLabor_Lote])
GO
ALTER TABLE [dbo].[SalarioPersonaTemporal] CHECK CONSTRAINT [FK_SalarioPersonaTemporal_LaborLote]
GO
ALTER TABLE [dbo].[SalarioPersonaTemporal]  WITH CHECK ADD  CONSTRAINT [FK_SalarioPersonaTemporal_Persona] FOREIGN KEY([DocumentoPersona])
REFERENCES [dbo].[Persona] ([DocumentoPersona])
GO
ALTER TABLE [dbo].[SalarioPersonaTemporal] CHECK CONSTRAINT [FK_SalarioPersonaTemporal_Persona]
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD  CONSTRAINT [FK_Venta_Producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[Producto] ([idProducto])
GO
ALTER TABLE [dbo].[Venta] CHECK CONSTRAINT [FK_Venta_Producto]
GO
/****** Object:  StoredProcedure [dbo].[AbonoCompraProveedor]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[AbonoCompraProveedor](@idCompra int,@valor money,@fecha datetime,@Total money)
as
begin

	if(@valor = @Total)
		begin
			insert into AbonoCompra values(@idCompra,@valor,@fecha)

			update Compra 
			set
			EstadoCuenta = 'P'
			where idCompra = @idCompra


		end
	else		
		begin		
			insert into AbonoCompra values(@idCompra,@valor,@fecha)	
		end

	select  'Registro exitoso!' as Mensaje
end





GO
/****** Object:  StoredProcedure [dbo].[ActializarCantidadArboles]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ActializarCantidadArboles]
as

begin

update a
			set a.Cantidad = t1.total
			from Arboles a
			inner join  (select idArboles, sum(Cantidad)as total from MovimientosArboles
			group by idArboles) t1 
			on a.idArboles = t1.idArboles

end





GO
/****** Object:  StoredProcedure [dbo].[asociarLaborLote]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[asociarLaborLote]
(@idLabor int, @idLote int, @fecha date)

as
begin

	set nocount on
	declare @mensaje varchar(50)
	
	
					insert into Labor_Lote (idLabor,idLote,Fecha)
					values (@idLabor,@idLote,@fecha)
					set @mensaje = (select top 1 idLabor_Lote from Labor_Lote order by idLabor_Lote desc)
			
			
		select @mensaje as mensaje
end








GO
/****** Object:  StoredProcedure [dbo].[ComprasProveedor]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


  
  
  CREATE procedure [dbo].[ComprasProveedor]
  (@nit varchar(45))
 as
	  begin
		 select c.idCompra,isnull(sum(d.Cantidad*d.Precio),Cb.Precio) as total,isnull(isnull(sum(d.Cantidad*d.Precio),Cb.Precio)-
			(select SUM(a.Valor) from AbonoCompra a where a.idCompra = c.idCompra),isnull(sum(d.Cantidad*d.Precio),Cb.Precio)) as adeuda
			,c.Fecha,c.NumeroFactura from Compra c
		 
		  left join Compra_Insumo d
		  on c.idCompra = d.idCompra
		  left join CostoBeneficio CB
		  on c.idCompra = Cb.idCompra
		 
		  where c.NitProveedor = @nit and c.EstadoCuenta = 'D'
		  group by c.idCompra,c.Fecha,c.NumeroFactura,cb.Precio
	 end
  



GO
/****** Object:  StoredProcedure [dbo].[consultarMovimientosArboles]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[consultarMovimientosArboles]
(@idArbol int)

as

begin

	select ma.idMovimientosArboles,ta.NombreTipoArbol,ma.Cantidad,ma.Fecha from TipoArbol ta
	inner join Arboles a
	on ta.idTipoArbol = a.idTIpoArbol
	inner join MovimientosArboles ma
	on a.idArboles = ma.idArboles
	where a.idArboles = @idArbol

end










GO
/****** Object:  StoredProcedure [dbo].[Consultasproduccion]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Consultasproduccion]
 as
	  begin
		 
		  select convert (decimal(10,2),(SUM (t1.CantidadRestante)/125)) as Cargas from (
		  select  isnull((p.Cantidad)-
			(select SUM(mp.Cantidad) from MovimientoProduccion mp  where mp.idProduccion = p.idProduccion),(p.Cantidad)) as CantidadRestante
			 from Produccion p
		  where p.EstadoProduccion = 'NV')t1
		  
		  
	 end




GO
/****** Object:  StoredProcedure [dbo].[DetalleSalario]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[DetalleSalario](@cedula int)
as
begin
	
		select LAB.NombreLabor , LBL.Fecha,SPT.Cantidad, SPT.Valor , (SPT.Cantidad * SPT.Valor) as subtotal  from SalarioPersonaTemporal SPT
		join Labor_Lote LBL on  SPT.idLabor_Lote= LBL.idLabor_Lote
		join Labor LAB on LBL.idLabor= LAB.idLabor
		where SPT.EstadoCuenta='D' and  SPT.DocumentoPersona=@cedula

end






GO
/****** Object:  StoredProcedure [dbo].[exportarDB]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[exportarDB]
(@rutaEscritorio varchar (1000))
as
begin

DECLARE @mensaje varchar(100)
DECLARE @backupPath varchar(1000);
SET @backupPath = @rutaEscritorio +'\BDFinca.Bak'

BACKUP DATABASE DBFinca
TO DISK = @backupPath
   WITH FORMAT,
      MEDIANAME = 'C_SQLServerBackups',
      NAME = 'Backup of DBFINCA';

set @mensaje = 'Exportación exitosa! EL archivo se encuentra en '+ @backupPath

select @mensaje as Mensaje

end
GO
/****** Object:  StoredProcedure [dbo].[gestionArboles]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[gestionArboles]
(@idLote smallint ,@idTipoArbol tinyint, @cantidad int, @fecha datetime, @idMovimiento int, @opcion int)

as
begin

	set nocount on
	declare @mensaje varchar(50)

	declare @idArboles int
	

	if( @opcion = 1 ) 
	begin
	       
		if ((select count(idLote) from Arboles where idLote =  @idLote ) = 0)
		 begin
		       insert into Arboles(idLote,idTIpoArbol,Cantidad)
				      values (@idLote,@idTipoArbol,'0')
		 end
		else
		 begin
		    if ((select count(idTIpoArbol) from Arboles where idTIpoArbol =  @idTipoArbol and idLote = @idLote) = 0)
				begin
					insert into Arboles(idLote,idTIpoArbol,Cantidad)
					values (@idLote,@idTipoArbol,'0')	
			end
		 end

			set @idArboles = (select idArboles from Arboles where idTIpoArbol = @idTipoArbol and idLote = @idLote)

			insert into MovimientosArboles (idArboles, Fecha, Cantidad)
			values (@idArboles, @fecha,  @cantidad)

			update Arboles set Cantidad = (select sum(Cantidad) from MovimientosArboles where idArboles = @idArboles ) where idArboles = @idArboles

		 set @mensaje = 'Registro exitoso!'
	end
	else if (@opcion = 2)
	begin 
			update MovimientosArboles
			set
			Fecha = @fecha,
			Cantidad = @cantidad			
			where idMovimientosArboles = @idMovimiento

			set @idArboles = (select idArboles from Arboles where idTIpoArbol = @idTipoArbol and idLote = @idLote)
			update Arboles set Cantidad = (select sum(Cantidad) from MovimientosArboles where idArboles = @idArboles ) where idArboles = @idArboles

			set @mensaje = 'Actualización exitosa!'
	end
	else if (@opcion = 3)
	begin
	      delete MovimientosArboles 
		    where idMovimientosArboles = @idMovimiento

			set @idArboles = (select idArboles from Arboles where idTIpoArbol = @idTipoArbol and idLote = @idLote)

			if ((select count(idMovimientosArboles) from MovimientosArboles where idArboles =  @idArboles ) = 0)
				begin
				   delete Arboles 
				   where idArboles = @idArboles
				end
			else
			begin
			   update Arboles set Cantidad = (select sum(Cantidad) from MovimientosArboles where idArboles = @idArboles ) where idArboles = @idArboles
			end

			set @mensaje = 'Eliminación exitosa!'
	end


		select @mensaje as Mensaje
end







GO
/****** Object:  StoredProcedure [dbo].[gestionArboles2]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[gestionArboles2]
(@idLote smallint ,@idTipoArbol tinyint, @cantidad int, @fecha datetime)

as
begin

	set nocount on
	declare @mensaje varchar(50)

	declare @idArboles int
	


		if ((select count(idLote) from Arboles where idLote =  @idLote ) = 0)
		 begin
		       insert into Arboles(idLote,idTIpoArbol,Cantidad)
				      values (@idLote,@idTipoArbol,'0')
		 end
		else
		 begin
		    if ((select count(idTIpoArbol) from Arboles where idTIpoArbol =  @idTipoArbol and idLote = @idLote) = 0)
				begin
					insert into Arboles(idLote,idTIpoArbol,Cantidad)
					values (@idLote,@idTipoArbol,'0')	
			end
		 end

			set @idArboles = (select idArboles from Arboles where idTIpoArbol = @idTipoArbol and idLote = @idLote)

			insert into MovimientosArboles (idArboles, Fecha, Cantidad)
			values (@idArboles, @fecha,  @cantidad)

			

		 set @mensaje = 'Registro exitoso!'
	


		select @mensaje as Mensaje
end






GO
/****** Object:  StoredProcedure [dbo].[gestionConcepto]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[gestionConcepto]
(@NombreConcepto varchar (45),@Descripcion varchar(45),@idConcepto int, @opc int)
as
begin

	set nocount on
	declare @mensaje varchar(50)
	if	(@opc =1)
		begin

			if ((select count(NombreConcepto) from Concepto where NombreConcepto = @NombreConcepto) = 0)
				begin
					insert into Concepto (NombreConcepto,Descripcion)
					values (@NombreConcepto,@Descripcion)
					set @mensaje = 'Registro exitoso!'
				end

			 else
				begin
					set @mensaje = 'Existe un concepto con este nombre'
				end
		end

	 else if (@opc =2)
	 begin
		 if ((select count(NombreConcepto) from Concepto where NombreConcepto = @NombreConcepto) = 0)

			begin
				update Concepto
				set
				NombreConcepto = @NombreConcepto,
				Descripcion = @Descripcion
				where idConcepto = @idConcepto
				set @mensaje = 'Actualización exitosa!'
			end

		else

			begin

				set @mensaje = 'Existe un concepto con este nombre'

			end
	end
	else if (@opc=3)   
		
		begin
			update Concepto
			set
			EstadoConcepto = 'I'
			where idConcepto= @idConcepto

			set @mensaje = 'Inhabilitación exitosa!'
		end
	else if (@opc=4)   
		
		begin
			update Concepto
			set
			EstadoConcepto = 'A'
			where idConcepto= @idConcepto

			set @mensaje = 'Habilitación exitosa!'
		end
	select @mensaje  as Mensaje

end





GO
/****** Object:  StoredProcedure [dbo].[gestionInsumo]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------INSUMOS------------

 CREATE proc [dbo].[gestionInsumo] 
 (@idTipoInsumo tinyint, @nombreInsumo varchar(45), 
 @descripcion varchar(150), @marca varchar(45),
 @unidadMedida varchar(45),@idInsumo int,@opc int)

 as
 begin
	 set nocount on
	 declare @mensaje varchar(50)
	 if(@opc = 1)
		begin
			if ((select count(nombreInsumo) from Insumo where NombreInsumo = @nombreInsumo) = 0)
				 begin
					 insert into Insumo (idTipoInsumo, NombreInsumo,Descripcion,Marca,UnidadMedida)
					 values (@idTipoInsumo,@nombreInsumo,@descripcion,@marca,@unidadMedida)
					 set @mensaje = 'Registro exitoso!'
				 end

			else
				begin
					set @mensaje = 'Existe un Insumo con este nombre'
				end
		end

	  else if(@opc = 2)
			 begin
			 if ((select count(nombreInsumo) from Insumo where NombreInsumo = @nombreInsumo) = 0)

				begin
					 update Insumo
					 set
					 idTipoInsumo = @idTipoInsumo,
					 NombreInsumo = @nombreInsumo,
					 Descripcion = @descripcion,
					 Marca = @marca,
					 UnidadMedida = UnidadMedida
					 where idInsumo = @idInsumo
					 set @mensaje = 'Actualización exitosa!'
				 end
			else
				begin
					set @mensaje = 'Existe un Insumo con este nombre'
				end
			end

	 else if (@opc = 3)
			begin
				update Insumo
				set
				EstadoInsumo = 'I'
				where idinsumo= @idInsumo

				set @mensaje = 'Inhabilitación exitosa!'
		end
	else if (@opc=4)   
		begin
			update Insumo
			set
			EstadoInsumo = 'A'
			where idinsumo= @idInsumo

			set @mensaje = 'Habilitación exitosa!'
		end

			select @mensaje  as Mensaje
 end





GO
/****** Object:  StoredProcedure [dbo].[gestionLabor]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-------------------------------------------LABOR----------------------------------

CREATE proc [dbo].[gestionLabor]
 ( @nombreLabor varchar(25),@modificaArbol bit,@requiereInsumo bit,  
 @descripcion varchar(150),@idLabor int,@opc int)

 as
 begin
	 set nocount on
	 declare @mensaje varchar(50)
	 if(@opc = 1)
		begin
			if ((select count(NombreLabor) from Labor where NombreLabor = @nombreLabor) = 0)
				 begin
					 insert into Labor(NombreLabor,ModificaArboles,RequiereInsumo,Descripcion)
					 values (@nombreLabor,@modificaArbol,@requiereInsumo,@descripcion)
					 set @mensaje = 'Registro exitoso!'
				 end

			else
				begin
					set @mensaje = 'Existe una labor con este nombre'
				end
		end

	  else if(@opc = 2)

			 begin

			 if ((select count(NombreLabor) from Labor where NombreLabor = @nombreLabor) = 0)

				begin
					 update Labor
					 set
				
					 NombreLabor = @nombreLabor,
					 ModificaArboles = @modificaArbol,
					 RequiereInsumo=@requiereInsumo,
					 Descripcion = @descripcion
					 where idLabor = @idLabor
					 set @mensaje = 'Actualización exitosa!'
				 end
			else
				begin
					set @mensaje = 'Existe una labor con este nombre'
				end
			end

	 else if (@opc = 3)
			begin
				update Labor
				set
				EstadoLabor = 'I'
				where idLabor= @idLabor

				set @mensaje = 'Inhabilitación exitosa'
		end
	else if (@opc=4)   
		
		begin
			update Labor
			set
			EstadoLabor = 'A'
			where idLabor= @idLabor

			set @mensaje = 'Habilitación exitosa!'
		end

			select @mensaje  as Mensaje
 end





GO
/****** Object:  StoredProcedure [dbo].[gestionLotes]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[gestionLotes]
(@nombreLote varchar(25), @observaciones varchar(100),@cuadras varchar(15) , @idLote int, @opc int)

as
begin

	set nocount on
	declare @mensaje varchar(50)
	
	if (@opc = 1)
		begin
			if ((select count(NombreLote) from Lote where NombreLote = @nombreLote) = 0)
				begin
					declare @idFinca tinyint = (select idFinca from Finca )
					insert into Lote (idFinca,NombreLote,Observaciones,Cuadras)
					values (@idFinca,@nombreLote,@observaciones,@cuadras)
					set @mensaje = 'Registro exitoso'
				end
				else
					begin
						set @mensaje = 'Existe un lote con este nombre'
					end
		end

    else if (@opc = 2)
		begin
		if ((select count(NombreLote) from Lote where NombreLote = @nombreLote) = 0)
			begin
				update Lote
				set
				NombreLote = @nombreLote,
				Observaciones = @observaciones,
				Cuadras = @cuadras
				where idLote= @idLote

			

				set @mensaje = 'Actualización exitosa'
			end
		else
			begin
				set @mensaje = 'Existe un lote con este nombre'
			end

		end

    else if (@opc = 3)
		begin
		
			update Lote
			set
			EstadoLote = 'i'
			where idLote= @idLote

			set @mensaje = 'Inhabilitación exitosa'
		end
      else if (@opc=4)   
		
		begin
			update Lote
			set
			EstadoLote = 'A'
			where idLote= @idLote

			set @mensaje = 'Habilitación exitosa!'
		end

		select @mensaje as Mensaje
end





GO
/****** Object:  StoredProcedure [dbo].[gestionPersona]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[gestionPersona]
(
@nombrePersona varchar(50),@genero char(1),@telefono varchar(10),@fechaNacimiento date, @documentoPerosna int, @opc int,
@TipoDocumento varchar(2) , @TipoContrato Varchar(10)
)

as
begin

set nocount on
declare @mensaje varchar(50)

if(@opc=1)
	 begin
		if ((select count(DocumentoPersona) from Persona where DocumentoPersona = @documentoPerosna) = 0)

			begin
				insert into Persona (TipoDocumento, DocumentoPersona,TipoContratoPersona,NombrePersona, Genero,Telefono,FechaNacimineto)
				values (@TipoDocumento,@documentoPerosna,@TipoContrato,@nombrePersona,@genero,@telefono,@fechaNacimiento)
				set @mensaje = 'Registro exitoso'
			end
		else
			begin
				set @mensaje = 'Existe un Empleado con este documento'
			end
	 end

else if(@opc=2)

		begin
		update Persona
			set
			NombrePersona=@nombrePersona,
			Genero=@genero,
			Telefono=@telefono,
			FechaNacimineto=@fechaNacimiento,
			TipoContratoPersona=@TipoContrato
			where DocumentoPersona = @documentoPerosna
			set @mensaje = 'Actualización exitosa!'

		end
else if (@opc = 3)
		
		begin
			update Persona
			set
			EstadoPersona = 'I'
			where DocumentoPersona = @documentoPerosna

			set @mensaje = 'Inhabilitación exitosa!'
		end

else if (@opc=4)   
		
		begin
			update Persona
			set
			EstadoPersona = 'A'
			where DocumentoPersona= @documentoPerosna

			set @mensaje = 'Habilitación exitosa!'
		end
		
	select @mensaje  as Mensaje


end





GO
/****** Object:  StoredProcedure [dbo].[gestionProducto]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------PRODUCTO---------------------
CREATE proc [dbo].[gestionProducto]
(@NombreProducto varchar (45),@Descripcion varchar(45),@idProducto int, @opc int)
as
begin

	set nocount on
	declare @mensaje varchar(50)
	if	(@opc =1)
		begin

			if ((select count(NombreProducto) from Producto where NombreProducto = @NombreProducto) = 0)
				begin
					insert into Producto(NombreProducto,Descripcion)
					values (@NombreProducto,@Descripcion)
					set @mensaje = 'Registro exitoso'
				end

			 else
				begin
					set @mensaje = 'Existe un producto con este nombre'
				end
		end

	 else if (@opc =2)

		begin

			if ((select count(NombreProducto) from Producto where NombreProducto = @NombreProducto) = 0)
				begin
					update Producto
					set
					NombreProducto = @NombreProducto,
					Descripcion = @Descripcion
					where idProducto = @idProducto
					set @mensaje = 'Actualización exitosa!'
				end
			 else
				begin
					set @mensaje = 'Existe un producto con este nombre'
				end
		end

	else if (@opc = 3)
		
		begin
			update Producto
			set
			EstadoProducto = 'I'
			where idProducto= @idProducto

			set @mensaje = 'Inhabilitación exitosa!'
		end
	else if (@opc=4)   
		
		begin
			update Producto
			set
			EstadoProducto = 'A'
			where idProducto= @idProducto

			set @mensaje = 'Habilitación exitosa!'
		end
	select @mensaje  as Mensaje

end





GO
/****** Object:  StoredProcedure [dbo].[gestionProveedor]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[gestionProveedor]
 (@nit varchar(20),@nombreProveedor varchar(45), @telefono varchar(10),@direccion varchar(45),@TipoDocumento varchar (20),@opc int)

 as
 begin
     set nocount on
	 declare @mensaje varchar(50)

 if(@opc=1)
	begin	
		if ((select COUNT(Nit) from Proveedor where Nit = @nit) = 0)
			begin
				if ((select count(NombreProveedor) from Proveedor where NombreProveedor = @nombreProveedor) = 0)
					 begin
						 insert into Proveedor (Nit,NombreProveedor,Telefono,Direccion,TipoDocumento)
						 values (@nit,@nombreProveedor,@telefono,@direccion,@TipoDocumento)
						 set @mensaje = 'Registro exitoso!'
					 end

				else
					begin
						set @mensaje = 'Existe un proveedor con este nombre'
					end
			end
			else
				begin
					set @mensaje = 'Existe un proveedor con este nit'
				end

	end

 else if(@opc=2)

		begin
			update Proveedor
			set

			NombreProveedor=@nombreProveedor,
			Telefono=@telefono,
			Direccion=@direccion,
			TipoDocumento = @TipoDocumento
			where Nit = @nit 
			set @mensaje = 'Actualización exitosa!'
		end

  else if (@opc = 3)
		
		begin
			update Proveedor
			set
			EstadoProveedor = 'I'
			where Nit= @nit

			set @mensaje = 'Inhabilitación exitosa!'
		end
 else if (@opc=4)   
		begin
			update Proveedor
			set
			EstadoProveedor = 'A'
			where Nit= @nit

			set @mensaje = 'Habilitación exitosa!'
		end
		select @mensaje  as Mensaje
 end






GO
/****** Object:  StoredProcedure [dbo].[gestionTipoArboles]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------GESTION TIPO ARBOLES---------------------
CREATE proc [dbo].[gestionTipoArboles]
(@NombreArbol varchar (45),@Descripcion varchar(45),@idTipoArbol int, @tiempoProduccion int, @opc int)
as
begin

	set nocount on
	declare @mensaje varchar(50)
	if	(@opc =1)
		begin

			if ((select count(NombreTipoArbol) from TipoArbol where NombreTipoArbol = @NombreArbol) = 0)
				begin
					insert into TipoArbol(NombreTipoArbol,Descripcion, TiempoProduccion)
					values (@NombreArbol,@Descripcion, @tiempoProduccion)
					set @mensaje = 'Registro exitoso!'
				end

			 else
				begin
					set @mensaje = 'Existe un tipo de arbol con este nombre'
				end
		end

	 else if (@opc =2)

		begin
			if ((select count(NombreTipoArbol) from TipoArbol where NombreTipoArbol = @NombreArbol) = 0)
				begin
					update TipoArbol
					set
					NombreTipoArbol = @NombreArbol,
					Descripcion = @Descripcion,
					TiempoProduccion = @tiempoProduccion
					where idTipoArbol = @idTipoArbol
					set @mensaje = 'Actualización exitosa!'
				end
			else
				begin
					set @mensaje = 'Existe un tipo de arbol con este nombre'
				end
		end

	else if (@opc = 3)
		
		begin
			update TipoArbol
			set
			EstadoTipoArbol = 'i'
			where idTipoArbol= @idTipoArbol

			set @mensaje = 'Inhabilitación exitosa!'
		end
   else if (@opc=4)   
		
		begin
			update TipoArbol
			set
			EstadoTipoArbol = 'A'
			where idTipoArbol= @idTipoArbol

			set @mensaje = 'Habilitación exitosa!'
		end

	select @mensaje  as Mensaje

end






GO
/****** Object:  StoredProcedure [dbo].[gestionTipoInsumo]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----------------------------------TIPO INSUMO---------------------------------

CREATE proc [dbo].[gestionTipoInsumo]
(@NombreTipoInsumo varchar (45),@Descripcion varchar(45),@idTipoInsumo int, @opc int)
as
begin

	set nocount on
	declare @mensaje varchar(50)
	if	(@opc =1)
		begin

			if ((select count(NombreTipoInsumo) from TipoInsumo where NombreTipoInsumo = @NombreTipoInsumo) = 0)
				begin
					insert into TipoInsumo (NombreTipoInsumo,Descripcion)
					values (@NombreTipoInsumo,@Descripcion)
					set @mensaje = 'Registro exitoso!'
				end

			 else
				begin
					set @mensaje = 'Existe un Tipo de insumo con este nombre'
				end
		end

	 else if (@opc =2)

		begin
			if ((select count(NombreTipoInsumo) from TipoInsumo where NombreTipoInsumo = @NombreTipoInsumo) = 0)
				begin
					update TipoInsumo
					set
					NombreTipoInsumo = @NombreTipoInsumo,
					Descripcion = @Descripcion
					where idTipoInsumo = @idTipoInsumo
					set @mensaje = 'Actualización exitosa!'
				end
			else
				begin
					set @mensaje = 'Existe un Tipo de insumo con este nombre'
				end
		end

	else if (@opc = 3)
		
		begin
			update TipoInsumo
			set
			EstadoTipoInsumo = 'I'
			where idTipoInsumo= @idTipoInsumo

			set @mensaje = 'Inhabilitación exitosa!'
		end
    else if (@opc=4)   
		
		begin
			update TipoInsumo
			set
			EstadoTipoInsumo = 'A'
			where idTipoInsumo= @idTipoInsumo

			set @mensaje = 'Habilitación exitosa!'
		end
	select @mensaje  as Mensaje

end





GO
/****** Object:  StoredProcedure [dbo].[GestionUsuario]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GestionUsuario](
@idUsuario int,
@nickName varchar(15),
@rol varchar(15),
@contrasena varchar(40),
@preguntaSeguridad varchar(70),
@respuesta varchar(30),
@opc int)

as

begin

declare @Mensaje varchar(50) 

	if(@opc = 1)
		begin	

			if ((select count(Nickname) from Usuario where Nickname = @nickName) = 0)
				begin
					insert into Usuario (Nickname,Rol,Contrasena,PreguntaSeguridad,Respuesta,EstadoUsuario)
					values (@nickName,@rol,@contrasena,@preguntaSeguridad,@respuesta,'A')
				
					set @Mensaje = 'Registro exitoso'
				end
			else
				begin
					set @mensaje = 'Existe un usuario con este nombre'
				end
		end

	else if(@opc = 2)

		begin
		if ((select count(Nickname) from Usuario where Nickname = @nickName) = 0)
				begin
					update Usuario
					set
					Nickname = @nickName,
					Rol = @rol,
					Contrasena = @contrasena,
					PreguntaSeguridad = @preguntaSeguridad,
					Respuesta = @respuesta
					where idUsuario =@idUsuario

					set @Mensaje = 'Actualización exitosa'
				end
		else
				begin
					set @mensaje = 'Existe un usuario con este nombre'
				end
		end

	else if (@opc = 3)
		begin
			
			update Usuario
			set
			EstadoUsuario = 'I'
			where idUsuario = @idUsuario
			set @Mensaje = 'Inhabilitación exitosa'
		end
	else if (@opc = 4)
		begin
			
			update Usuario
			set
			EstadoUsuario = 'A'
			where idUsuario = @idUsuario

			set @Mensaje = 'Habilitación exitosa'
		end


	else if(@opc = 5)
		begin

		update Usuario 
		set 
		Contrasena = @contrasena
		where idUsuario = @idUsuario

		set @Mensaje = 'La contraseña se ha cambiado correctamente'


		end

		select @Mensaje as Mensaje

end




GO
/****** Object:  StoredProcedure [dbo].[GestionVenta]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GestionVenta]
(@nit int,@fecha date,@numeroFactura 
int,@idProducto int,@PrecioCarga 
money,@CantidadCargas decimal(10,2),@PrecioBeneficio money) 

as
	begin

		insert into Compra (NitProveedor,Fecha,NumeroFactura,EstadoCuenta)
				values(@nit,@fecha,@numeroFactura,'D')


	   declare @IdCompra int = (select top 1 idCompra from Compra order by idCompra desc)


	   insert into Venta (idProducto,Fecha,PrecioCarga,CantidadCargas)
				values (@idProducto,@fecha,@PrecioCarga,@CantidadCargas)

	   declare @IdVenta int = (select top 1 idVenta from Venta order by idVenta desc)

	   insert into CostoBeneficio (idCompra,idVenta,Precio,EstadoCuenta)
				values (@IdCompra,@IdVenta,@PrecioBeneficio,'D')

	end




GO
/****** Object:  StoredProcedure [dbo].[ImportarDB]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ImportarDB]
(@ruta varchar (1000))
as
begin
    DECLARE @mensaje varchar(100)
	DECLARE @backupPath nvarchar(400);
	DECLARE @sourceDb nvarchar(50);
	DECLARE @destDb nvarchar(50);


	SET @sourceDb = 'DBFinca'
	SET @backupPath = @ruta


	SET @destDb = 'DBFinca'


	RESTORE DATABASE @destDb FROM DISK = @backupPath
	set @mensaje = 'Importación exitosa!'

    select @mensaje as Mensaje

   end
GO
/****** Object:  StoredProcedure [dbo].[Insercion_RegistroPago_SalarioPersonaPermanente]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[Insercion_RegistroPago_SalarioPersonaPermanente]
	@DTPagosPersonaPermanente EstructuraSalarioPersonaPermanente READONLY
AS
	INSERT INTO RegistroPago (Fecha)
        values (CONVERT (date, GETDATE())) ;

     DECLARE @UltimoIdRegistroPago int
	 set @UltimoIdRegistroPago=  (SELECT top 1 idRegistroPago FROM  RegistroPago order by idRegistroPago desc);

	INSERT INTO SalarioPersonaPermanente (DocumentoPersona,idRegistroPago, Valor)
        SELECT DocumentoPersona,@UltimoIdRegistroPago, Valor_a_Pagar FROM  @DTPagosPersonaPermanente;





GO
/****** Object:  StoredProcedure [dbo].[Insercion_RegistroPago_SalarioPersonaTemporal]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Insercion_RegistroPago_SalarioPersonaTemporal]
	
AS
	INSERT INTO RegistroPago (Fecha)
        values (CONVERT (date, GETDATE())) ;

     DECLARE @UltimoIdRegistroPago int
	 set @UltimoIdRegistroPago=  (SELECT top 1 idRegistroPago FROM  RegistroPago order by idRegistroPago desc);




		
	
	INSERT INTO RegistroPagoSalario(idRegistroPago,idSalarioPersonaTemporal)
        (SELECT @UltimoIdRegistroPago,spt.idSalarioPersonaTemporal from SalarioPersonaTemporal spt
				 WHERE spt.EstadoCuenta = 'D')



		update SalarioPersonaTemporal 
				   set
				   EstadoCuenta = 'P'
				  
				   WHERE EstadoCuenta = 'D'
	
		
		

GO
/****** Object:  StoredProcedure [dbo].[insercionAbonoDeuda]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[insercionAbonoDeuda]
(
 @valor money, 
 @fecha date,
 @idDeuda int,
 @opc int
)
as
begin

set nocount on
declare @mensaje varchar(50)      

	if(@opc = 1) /* ingreso cuando el prestamo es igual al abono*/
	 begin
			insert into AbonoDeuda(idDeudaPersona ,Valor,Fecha)
			values (@idDeuda,@valor,@fecha)

			update DeudaPersona 
				   set
				   EstadoCuenta = 'P'
				   WHERE idDeudaPersona = @idDeuda 

			set @mensaje = 'Prestamo de la fecha: '+ CONVERT(VARCHAR, (select fecha from DeudaPersona where idDeudaPersona = @idDeuda ), 101) +' Cancelado!';
	 end

	else if (@opc = 2) /* ingreso cuando el valor del prestamo es mayor a el abono*/
	 begin
			insert into AbonoDeuda(idDeudaPersona ,Valor,Fecha)
			values (@idDeuda,@valor,@fecha)

	   set @mensaje = 'Abono Exitoso'
	 end

	 else if (@opc = 3) /* Ingreso cuando el valor del abono es mayor a el prestamo*/
		begin
			
			insert into AbonoDeuda(idDeudaPersona ,Valor,Fecha)
			values (@idDeuda,@valor,@fecha)

			update DeudaPersona 
				   set
				   EstadoCuenta = 'P'
				   WHERE idDeudaPersona = @idDeuda 

			set @mensaje = 'Prestamo de la fecha: '+ CONVERT(VARCHAR, (select fecha from DeudaPersona where idDeudaPersona = @idDeuda ), 101) +' Cancelado!';
		end

select @mensaje as Mensaje

end








GO
/****** Object:  StoredProcedure [dbo].[insercionDeudaEmpleado]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[insercionDeudaEmpleado]
(
@DocumentoPersona varchar(15), @valor money, @fecha date, @descripcion varchar(150)
)

as
begin

set nocount on
declare @mensaje varchar(50)


			insert into DeudaPersona( DocumentoPersona,Valor,Fecha, Descripcion)
			values (@DocumentoPersona,@valor,@fecha,@descripcion)
			set @mensaje = 'Registro exitoso'
	

select @mensaje as Mensaje

end





GO
/****** Object:  StoredProcedure [dbo].[InsercionInsumoLaborLote]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [dbo].[InsercionInsumoLaborLote] 
	@DTInsumo InsumoLaborLote READONLY
AS
    
	INSERT INTO LaborLote_Insumo
           (idLabor_Lote,idInsumo,Cantidad,Precio)
        SELECT idLabor_Lote,idInsumo,Cantidad,Precio FROM  @DTInsumo;

		update I 
		set CantidadExistente=CantidadExistente - DTI.Cantidad FROM  Insumo as I
		inner join @DTInsumo as DTI  on i.idInsumo = DTI.idInsumo





GO
/****** Object:  StoredProcedure [dbo].[InsertarDetalleCompra]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertarDetalleCompra]
	(@dtDetalleCompra DetalleCompra READONLY)
AS
    
	
	INSERT INTO Compra_Insumo
           (idCompra
           ,idInsumo,Cantidad,Precio)
        SELECT idCompra, idInsumo,Cantidad,Precio FROM  @dtDetalleCompra


		UPDATE P
		SET P.CantidadExistente = P.CantidadExistente + DC.cantidad, 
		P.PrecioPromedio= (ISNULL((P.PrecioPromedio * P.CantidadExistente),0) + CONVERT(int,DC.Subtotal))/(P.CantidadExistente + DC.cantidad)
		FROM Insumo AS P
		INNER JOIN @dtDetalleCompra AS DC
		ON P.idInsumo = DC.idInsumo





GO
/****** Object:  StoredProcedure [dbo].[insertarMovimentoArboles]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[insertarMovimentoArboles]
	(@dtMovimientoArboles MovimientoArboles READONLY)
AS
begin

	
	update ma
	set ma.Cantidad = ma.Cantidad - dtma.Cantidad
	from MovimientosArboles ma
	inner join @dtMovimientoArboles as dtma
	on ma.idMovimientosArboles = dtma.idMovimiento
	
	delete from MovimientosArboles  
	where Cantidad = 0


end


GO
/****** Object:  StoredProcedure [dbo].[ModificarFinca]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ModificarFinca]
(
@nombreFinca varchar(25),
@Propietario varchar(45),
@idMunicipio int,
@Vereda varchar(50),
@telefono varchar(10),
@hectareas varchar(5)
)


as

begin

 declare @Mensaje varchar(50) 

	update Finca
	set
	NombreFinca = @nombreFinca,
	Propietario = @Propietario,
	idMunicipio = @idMunicipio,
	Vereda = @Vereda,
	Telefono = @telefono,
	Cuadras = @hectareas

 set @Mensaje = 'Actualización exitosa'

 select @Mensaje as Mensaje
	
end




GO
/****** Object:  StoredProcedure [dbo].[PagosPersona]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[PagosPersona](@opcionPago int)
as
begin
	if(@opcionPago = 1)--PERSONA TEMPORAL
		begin
			
			
			select tab.DocumentoPersona,tab.Nombre,tab.Valor_a_pagar,tab.Valor_Deuda from
				(select SPT.DocumentoPersona, PER.NombrePersona as "Nombre" ,sum(SPT.Cantidad * SPT.Valor) as Valor_a_pagar, 
					(select sum(t.abonos) from (
						(select isnull(DP.Valor - SUM(AB.Valor),DP.Valor) as "abonos" from DeudaPersona DP 
							left join AbonoDeuda AB on DP.idDeudaPersona = AB.idDeudaPersona
							where DP.DocumentoPersona = SPT.DocumentoPersona and DP.EstadoCuenta='D'
							group by DP.Valor)) t) as "Valor_Deuda"
				from SalarioPersonaTemporal SPT
				join Persona PER on SPT.DocumentoPersona = PER.DocumentoPersona
				where SPT.EstadoCuenta='D'
				group by SPT.DocumentoPersona,  PER.NombrePersona ) tab
			group by tab.DocumentoPersona,tab.Nombre,tab.Valor_a_pagar,tab.Valor_Deuda

		end
	
	if(@opcionPago = 2)--PERSONA PERMANENTE
		begin		
											select tab.DocumentoPersona,tab.Nombre,cast(0 as decimal) as "Valor_a_pagar",cast(isnull(tab.Valor_Deuda,0) as decimal) as "Valor_Deuda" from
											   (select PER.DocumentoPersona, PER.NombrePersona as "Nombre" , 
/*devuelve la deuda de una persona permanente*/	(select sum(t.abonos) from (
/*resta los abonos de una persona permanente*/(select isnull(DP.Valor - SUM(AB.Valor),DP.Valor) as "abonos" from DeudaPersona DP 
													 left join AbonoDeuda AB on DP.idDeudaPersona = AB.idDeudaPersona
													 join Persona PER1 on DP.DocumentoPersona = PER1.DocumentoPersona
													where  DP.EstadoCuenta='D' and DP.DocumentoPersona =  PER.DocumentoPersona and PER1.TipoContratoPersona='Permanente'
													group by DP.Valor)) t) as "Valor_Deuda"
										from Persona PER where PER.TipoContratoPersona='Permanente'
										group by PER.DocumentoPersona, PER.NombrePersona) tab
									group by tab.DocumentoPersona,tab.Nombre,tab.Valor_Deuda
		end
	
end




	





GO
/****** Object:  StoredProcedure [dbo].[RegistrarCompra]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[RegistrarCompra]
(@nit varchar(20),@fecha date,@numeroFactura int)
as
begin

	declare @Compra varchar(50)

	if ((select COUNT(idCompra) from Compra where NitProveedor = @nit and NumeroFactura = @numeroFactura) = 0)

		begin

			insert into Compra values (@nit,@fecha,@numeroFactura,'D')

			set @Compra = (select top 1 idCompra from Compra order by idCompra desc)

		end

	else 

		begin 
	
			set @compra = 0
	
		end

	select @Compra  as Mensaje

	

end





GO
/****** Object:  StoredProcedure [dbo].[registroProduccion]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[registroProduccion]
(@idLote int,@fecha date, @cantidad int)
as
begin

	set nocount on
	
	
		begin

			insert into Produccion(idLote,Fecha,Cantidad)
			values (@idLote,@fecha,@cantidad)
			
		end
		
	
end




GO
/****** Object:  StoredProcedure [dbo].[ReporteCompraInsumos]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ReporteCompraInsumos] 
(@fecha_ini date, @fecha_fin date)

as 
begin

		select i.NombreInsumo, i.Marca,c.NumeroFactura,c.Fecha, p.NombreProveedor, sum(ci.Cantidad*ci.Precio) as Valor from Compra c
		join Proveedor p on p.Nit=c.NitProveedor
		join Compra_Insumo ci on ci.idCompra=c.idCompra
		join Insumo i on i.idInsumo= ci.idInsumo
		where c.Fecha between @fecha_ini and @fecha_fin
		group by i.NombreInsumo, i.Marca,c.NumeroFactura,c.Fecha, p.NombreProveedor
		order by c.Fecha

end




GO
/****** Object:  StoredProcedure [dbo].[ReporteDeudaEmpleados]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[ReporteDeudaEmpleados]
(@fecha_ini date, @fecha_fin date)

as

begin 

select t.DocumentoPersona,p.NombrePersona,sum(t.valor) as "Valor Deuda" from
		(select dp.DocumentoPersona,sum(dp.Valor)-isnull(a.total_abono,0) as valor from DeudaPersona dp
			join Persona p on p.DocumentoPersona= dp.DocumentoPersona
			left join (select ab.idDeudaPersona, SUM(ab.Valor) as total_abono from AbonoDeuda ab group by  ab.idDeudaPersona) a on a.idDeudaPersona=dp.idDeudaPersona
		where dp.Fecha between @fecha_ini and @fecha_fin and dp.EstadoCuenta='D'
		group by dp.DocumentoPersona,a.total_abono)  t
join Persona p on p.DocumentoPersona=t.DocumentoPersona
group by t.DocumentoPersona,p.NombrePersona


end




GO
/****** Object:  StoredProcedure [dbo].[ReporteIngresosLote]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ReporteIngresosLote]
 (@idLote int, @fecha_ini datetime, @fecha_fin datetime)

 as

 begin

		select cast(((sum(MP.Cantidad) /125)*v.PrecioCarga) as decimal ) as valor,l.NombreLote, v.Fecha from Venta v
		inner join MovimientoProduccion MP
		on v.idVenta = MP.idVenta
		inner join Produccion P
		on P.idProduccion = MP.idProduccion
		inner join Lote L
		on l.idLote = P.idLote
		where v.Fecha  between cast(@fecha_ini as date) and cast(@fecha_fin as date) and  L.idLote= @idLote   
		group by v.PrecioCarga,l.NombreLote,v.Fecha

end





GO
/****** Object:  StoredProcedure [dbo].[reporteIngresosTotales]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[reporteIngresosTotales]
(@fecha_inicial date, @fecha_fin date )

as

begin 

		select p.NombreProducto,Fecha,sum(v.PrecioCarga*v.CantidadCargas) as Valor from Venta v
		join Producto p on p.idProducto=v.idProducto
		where Fecha between @fecha_inicial and @fecha_fin
		group by  p.NombreProducto ,v.Fecha, v.PrecioCarga, v.CantidadCargas


end




GO
/****** Object:  StoredProcedure [dbo].[ReporteLaboresLote]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ReporteLaboresLote]
(@idLote int, @fecha_ini date, @fecha_fin date)

as

begin
	
		select lt.NombreLote, l.NombreLabor,lbl.Fecha from Labor_Lote lbl
		join Labor l on l.idLabor= lbl.idLabor
		join Lote lt on lt.idLote = lbl.idLote
		where lbl.idLote = @idLote and lbl.Fecha between @fecha_ini and @fecha_fin
		order by lbl.Fecha
end




GO
/****** Object:  StoredProcedure [dbo].[reporteProduccion]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[reporteProduccion]
(@idLote int, @fecha_ini date, @fecha_fin date) 

as

begin
    
	select l.NombreLote, p.Fecha, p.Cantidad from Produccion p
    join Lote l on l.idLote = p.idLote
	where p.Fecha between @fecha_ini  and @fecha_fin and l.idLote=@idLote
	order by p.Fecha
end




GO
/****** Object:  StoredProcedure [dbo].[SalariosEmpleados]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[SalariosEmpleados] 
	@DTsalario SalarioPeronaTemporal READONLY
AS
    
	INSERT INTO SalarioPersonaTemporal
           (DocumentoPersona,idLabor_Lote,Cantidad,Valor)
        SELECT DocumentoPersona, idLabor_Lote,Cantidad,Valor FROM  @DTsalario;


GO
/****** Object:  StoredProcedure [dbo].[SP_CONSULTA_EGRESO]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_CONSULTA_EGRESO]
(@FECHA_INI datetime,@FECHA_FIN datetime)
AS
BEGIN
	SELECT C.NombreConcepto, E.Descripcion, E.Fecha, E.Valor FROM Egreso E
	JOIN Concepto C on C.idConcepto= E.idConcepto
	WHERE E.Fecha BETWEEN cast(@FECHA_INI as date) AND cast(@FECHA_FIN as date )
END

--EXECUTE SP_CONSULTA_EGRESO '2015-01-01','2015-03-01'





GO
/****** Object:  StoredProcedure [dbo].[SP_InsertMultiplesGastos]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[SP_InsertMultiplesGastos] 
	@DTgastos RegistrarGasto READONLY
AS
    
	INSERT INTO Egreso
           (idConcepto,Descripcion,Fecha,Valor,EstadoCuenta)
        SELECT idConcepto, Descripcion,Fecha,Valor,EstadoCuenta FROM  @DTgastos;





GO
/****** Object:  StoredProcedure [dbo].[SP_ReporteEgresosPorLote]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_ReporteEgresosPorLote](@idLote int, @fecha_ini date, @fecha_fin date)
AS
BEGIN

	SELECT lot.NombreLote, lab.NombreLabor ,lbl.Fecha,(sum(spt1.Cantidad * spt1.Valor)+isnull(t.[Valor Insumo],0)) as "Total Egreso" from SalarioPersonaTemporal spt1 
			join Labor_Lote lbl on lbl.idLabor_Lote= spt1.idLabor_Lote
			join Labor lab on lab.idLabor=lbl.idLabor
			join Lote lot on lot.idLote=lbl.idLote
			left join (select sum(Cantidad*Precio) as "Valor Insumo", idLabor_Lote from LaborLote_Insumo group by idLabor_Lote) t on t.idLabor_Lote=lbl.idLabor_Lote
			where  spt1.idLabor_Lote=lbl.idLabor_Lote and lbl.idLote=@idLote and lbl.Fecha between @fecha_ini and @fecha_fin
			group by spt1.idLabor_Lote, lot.NombreLote,lab.NombreLabor,lbl.Fecha,t.[Valor Insumo]
			
			

											
end	




GO
/****** Object:  StoredProcedure [dbo].[SP_ReportePagos]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[SP_ReportePagos]
(@FECHA_INI date,@FECHA_FIN date)
AS
BEGIN

SELECT SPT.DocumentoPersona, PER.NombrePersona,LABLO.Fecha,LAB.NombreLabor ,sum(SPT.Cantidad * SPT.Valor) as "TotalPagado" FROM SalarioPersonaTemporal SPT
join (SELECT RPS.idSalarioPersonaTemporal FROM RegistroPagoSalario RPS
join (select REP_PAGOS.idRegistroPago from (SELECT RP.idRegistroPago FROM RegistroPago RP
									  WHERE RP.Fecha BETWEEN cast(@FECHA_INI as date) 
									  AND cast(@FECHA_FIN as date )) "REP_PAGOS") RP on RP.idRegistroPago = RPS.idRegistroPago) "IDSALPER"  on IDSALPER.idSalarioPersonaTemporal=SPT.idSalarioPersonaTemporal

join Persona PER on PER.DocumentoPersona=SPT.DocumentoPersona
join Labor_Lote LABLO on LABLO.idLabor_Lote= SPT.idLabor_Lote
join Labor LAB on LAB.idLabor= LABLO.idLabor
group by SPT.DocumentoPersona, PER.NombrePersona,SPT.Cantidad,LABLO.Fecha, LAB.NombreLabor , SPT.Valor
END




GO
/****** Object:  StoredProcedure [dbo].[SP_ReportePagosPerma]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[SP_ReportePagosPerma]
(@FECHA_INI date,@FECHA_FIN date)
AS
BEGIN

SELECT SPP.DocumentoPersona, PER.NombrePersona,RP.Fecha ,sum(SPP.Valor) as "TotalPagado" FROM SalarioPersonaPermanente SPP
join (select REP_PAGOS.idRegistroPago, REP_PAGOS.Fecha from (SELECT RP.idRegistroPago, RP.Fecha FROM RegistroPago RP
									  WHERE RP.Fecha BETWEEN cast(@FECHA_INI as date) 
									  AND cast(@FECHA_FIN as date )) "REP_PAGOS") RP on RP.idRegistroPago = SPP.IdRegistroPago

join Persona PER on PER.DocumentoPersona=SPP.DocumentoPersona
group by SPP.DocumentoPersona, PER.NombrePersona,RP.Fecha ,SPP.Valor

END




GO
/****** Object:  StoredProcedure [dbo].[VentaProduccion]    Script Date: 13/03/2016 12:07:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[VentaProduccion]
(@Cantidad decimal(10,2))
as


	begin 
		declare @cantidadKilos decimal(10,2) = @Cantidad * 125
		declare @idProduccion int
		declare @CantidadRestante int
		
		declare @idVenta int = (select TOP 1 idVenta from Venta order by idVenta desc )
		
	

		while(@cantidadKilos != 0 )

			begin  

				set @idProduccion  = (select TOP 1 idProduccion from Produccion where EstadoProduccion  = 'NV' order by idProduccion ) 

				set @CantidadRestante  = (select  isnull (P.Cantidad - sum(MP.Cantidad),P.Cantidad) as CantidadRestante from Produccion P
				left join MovimientoProduccion MP
				on p.idProduccion = MP.idProduccion
				where P.idProduccion = @idProduccion and P.EstadoProduccion = 'NV'
				group by P.Cantidad)
			

				if (@CantidadRestante < @cantidadKilos)
					begin

					insert into MovimientoProduccion (idProduccion,idVenta,Cantidad)
							values (@idProduccion,@idVenta,@CantidadRestante)
					
					set @cantidadKilos = @cantidadKilos-@CantidadRestante

					update Produccion
						set 
						EstadoProduccion = 'VC'

						where idProduccion = @idProduccion


					
					end

				else

					begin

						insert into MovimientoProduccion (idProduccion,idVenta,Cantidad)
								values (@idProduccion,@idVenta,@cantidadKilos)

						set @cantidadKilos = 0
						

					end

			end
			select 'Registro exitoso' as Mensaje		

	end




GO
USE [master]
GO
ALTER DATABASE [DBFinca] SET  READ_WRITE 
GO
