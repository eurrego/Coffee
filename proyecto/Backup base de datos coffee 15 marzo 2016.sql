USE [master]
GO
/****** Object:  Database [DBFinca]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
ALTER DATABASE [DBFinca] SET  DISABLE_BROKER 
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
/****** Object:  UserDefinedTableType [dbo].[DetalleCompra]    Script Date: 15/03/2016 3:43:28 p. m. ******/
CREATE TYPE [dbo].[DetalleCompra] AS TABLE(
	[Nombre] [varchar](45) NULL,
	[Cantidad] [varchar](10) NULL,
	[Precio] [varchar](10) NULL,
	[Subtotal] [varchar](20) NULL,
	[idInsumo] [varchar](20) NULL,
	[idCompra] [varchar](20) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[EstructuraSalarioPersonaPermanente]    Script Date: 15/03/2016 3:43:28 p. m. ******/
CREATE TYPE [dbo].[EstructuraSalarioPersonaPermanente] AS TABLE(
	[DocumentoPersona] [varchar](15) NULL,
	[Valor_a_Pagar] [money] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[InsumoLaborLote]    Script Date: 15/03/2016 3:43:28 p. m. ******/
CREATE TYPE [dbo].[InsumoLaborLote] AS TABLE(
	[idLabor_Lote] [int] NULL,
	[idInsumo] [int] NULL,
	[Cantidad] [int] NULL,
	[Precio] [money] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[MovimientoArboles]    Script Date: 15/03/2016 3:43:28 p. m. ******/
CREATE TYPE [dbo].[MovimientoArboles] AS TABLE(
	[idMovimiento] [varchar](10) NULL,
	[idArbol] [varchar](10) NULL,
	[NombreArbol] [varchar](45) NULL,
	[Cantidad] [varchar](10) NULL,
	[Fecha] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[RegistrarGasto]    Script Date: 15/03/2016 3:43:28 p. m. ******/
CREATE TYPE [dbo].[RegistrarGasto] AS TABLE(
	[idConcepto] [int] NULL,
	[Descripcion] [varchar](150) NULL,
	[Fecha] [date] NULL,
	[Valor] [money] NULL,
	[EstadoCuenta] [char](1) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[SalarioPeronaTemporal]    Script Date: 15/03/2016 3:43:28 p. m. ******/
CREATE TYPE [dbo].[SalarioPeronaTemporal] AS TABLE(
	[DocumentoPersona] [varchar](15) NULL,
	[idLabor_Lote] [int] NULL,
	[Cantidad] [int] NULL,
	[Valor] [money] NULL
)
GO
/****** Object:  Table [dbo].[AbonoCompra]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[AbonoDeuda]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[Arboles]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[Compra]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
	[NumeroFactura] [varchar](50) NOT NULL,
	[EstadoCuenta] [char](1) NOT NULL CONSTRAINT [DF_Compra]  DEFAULT ('D'),
 CONSTRAINT [PK_Compra] PRIMARY KEY CLUSTERED 
(
	[idCompra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Compra_Insumo]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[Concepto]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[CostoBeneficio]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[Departamento]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[DeudaPersona]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[Egreso]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[Finca]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[Insumo]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
	[PrecioPromedio] [money] NULL,
 CONSTRAINT [PK_Insumo] PRIMARY KEY CLUSTERED 
(
	[idInsumo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Labor]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[Labor_Lote]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[LaborLote_Insumo]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[Lote]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[MovimientoProduccion]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[MovimientosArboles]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[Municipio]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[Persona]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[Produccion]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[Producto]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[Proveedor]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[RegistroPago]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[RegistroPagoSalario]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[SalarioPersonaPermanente]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[SalarioPersonaTemporal]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[TipoArbol]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[TipoInsumo]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[Usuario]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  Table [dbo].[Venta]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[AbonoCompraProveedor]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[ActializarCantidadArboles]    Script Date: 15/03/2016 3:43:28 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ActializarCantidadArboles]
as

begin

			update a
			set a.Cantidad = t1.total
			from Arboles a
			inner join  (select a.idArboles, isnull(sum(ma.Cantidad),0)as total from Arboles a
			left join MovimientosArboles ma
			on a.idArboles = ma.idArboles
			group by a.idArboles) t1 
			on a.idArboles = t1.idArboles



			delete from Arboles
			where Cantidad = 0

		

end






GO
/****** Object:  StoredProcedure [dbo].[asociarLaborLote]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[ComprasProveedor]    Script Date: 15/03/2016 3:43:28 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


  
  
  CREATE procedure [dbo].[ComprasProveedor]
  (@nit varchar(45), @fechaInicial datetime, @fechaFinal datetime, @opc int )
 as
	  begin

	  if(@opc = 1)
	     begin
				 select c.idCompra,isnull(sum(d.Cantidad*d.Precio),Cb.Precio) as total,isnull(isnull(sum(d.Cantidad*d.Precio),Cb.Precio)-
					(select SUM(a.Valor) from AbonoCompra a where a.idCompra = c.idCompra),isnull(sum(d.Cantidad*d.Precio),Cb.Precio)) as adeuda
					,c.Fecha,c.NumeroFactura, p.Nit , p.NombreProveedor,c.EstadoCuenta  from Compra c		 
				  left join Compra_Insumo d
				  on c.idCompra = d.idCompra
				  left join CostoBeneficio CB
				  on c.idCompra = Cb.idCompra	
				  join Proveedor p
				  on c.NitProveedor = p.Nit 	 
				  where c.NitProveedor = @nit and c.EstadoCuenta = 'D'
				  group by c.idCompra,c.Fecha,c.NumeroFactura,cb.Precio, p.Nit , p.NombreProveedor,c.EstadoCuenta
		  end
	   else if (@opc = 2)
	    begin
				 select c.idCompra,isnull(sum(d.Cantidad*d.Precio),Cb.Precio) as total,isnull(isnull(sum(d.Cantidad*d.Precio),Cb.Precio)-
					(select SUM(a.Valor) from AbonoCompra a where a.idCompra = c.idCompra),isnull(sum(d.Cantidad*d.Precio),Cb.Precio)) as adeuda
					,c.Fecha,c.NumeroFactura, p.Nit , p.NombreProveedor,c.EstadoCuenta from Compra c		 
				  left join Compra_Insumo d
				  on c.idCompra = d.idCompra
				  left join CostoBeneficio CB
				  on c.idCompra = Cb.idCompra	
				  join Proveedor p
				  on c.NitProveedor = p.Nit 
				  where c.EstadoCuenta = 'P'
				  group by c.idCompra,c.Fecha,c.NumeroFactura,cb.Precio, p.Nit , p.NombreProveedor,c.EstadoCuenta
		end 
		 else if (@opc = 3)
		 begin
		    select c.idCompra,isnull(sum(d.Cantidad*d.Precio),Cb.Precio) as total,isnull(isnull(sum(d.Cantidad*d.Precio),Cb.Precio)-
				  (select SUM(a.Valor) from AbonoCompra a where a.idCompra = c.idCompra),isnull(sum(d.Cantidad*d.Precio),Cb.Precio)) as adeuda
				  ,c.Fecha,c.NumeroFactura, p.Nit , p.NombreProveedor,c.EstadoCuenta  from Compra c		 
				  left join Compra_Insumo d
				  on c.idCompra = d.idCompra
				  left join CostoBeneficio CB
				  on c.idCompra = Cb.idCompra	
				  join Proveedor p
				  on c.NitProveedor = p.Nit 	 
				  where c.EstadoCuenta = 'D'
				  group by c.idCompra,c.Fecha,c.NumeroFactura,cb.Precio, p.Nit , p.NombreProveedor,c.EstadoCuenta

		 end 
		   else if (@opc = 4)
		    begin
				select c.idCompra,isnull(sum(d.Cantidad*d.Precio),Cb.Precio) as total,isnull(isnull(sum(d.Cantidad*d.Precio),Cb.Precio)-
				(select SUM(a.Valor) from AbonoCompra a where a.idCompra = c.idCompra),isnull(sum(d.Cantidad*d.Precio),Cb.Precio)) as adeuda
				,c.Fecha,c.NumeroFactura, p.Nit , p.NombreProveedor,c.EstadoCuenta  from Compra c		 
			    left join Compra_Insumo d
			    on c.idCompra = d.idCompra
			    left join CostoBeneficio CB
			    on c.idCompra = Cb.idCompra
				join Proveedor p
				  on c.NitProveedor = p.Nit 
			    group by c.idCompra,c.Fecha,c.NumeroFactura,cb.Precio, p.Nit , p.NombreProveedor,c.EstadoCuenta
			end
		  else if (@opc = 5)
		    begin
				 select c.idCompra,isnull(sum(d.Cantidad*d.Precio),Cb.Precio) as total,isnull(isnull(sum(d.Cantidad*d.Precio),Cb.Precio)-
					(select SUM(a.Valor) from AbonoCompra a where a.idCompra = c.idCompra),isnull(sum(d.Cantidad*d.Precio),Cb.Precio)) as adeuda
					,c.Fecha,c.NumeroFactura, p.Nit , p.NombreProveedor,c.EstadoCuenta from Compra c		 
				  left join Compra_Insumo d
				  on c.idCompra = d.idCompra
				  left join CostoBeneficio CB
				  on c.idCompra = Cb.idCompra	
				  join Proveedor p
				  on c.NitProveedor = p.Nit 	 
				  where c.Fecha >= @fechaInicial and c.Fecha <= @fechaFinal
				  group by c.idCompra,c.Fecha,c.NumeroFactura,cb.Precio, p.Nit , p.NombreProveedor,c.EstadoCuenta

			end
	 end
  




GO
/****** Object:  StoredProcedure [dbo].[consultarMovimientosArboles]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[Consultasproduccion]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[DetalleSalario]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[exportarDB]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[gestionArboles]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[gestionArboles2]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[gestionConcepto]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
		 if ((select count(NombreConcepto) from Concepto where NombreConcepto = @NombreConcepto and idConcepto != @idConcepto) = 1)

			begin

				set @mensaje = 'Existe un concepto con este nombre'


			end

		else

			begin

			update Concepto
				set
				NombreConcepto = @NombreConcepto,
				Descripcion = @Descripcion
				where idConcepto = @idConcepto
				set @mensaje = 'Actualización exitosa!'
				

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
/****** Object:  StoredProcedure [dbo].[gestionInsumo]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
			 if ((select count(nombreInsumo) from Insumo where NombreInsumo = @nombreInsumo and idInsumo != @idInsumo) = 1)

				begin

					set @mensaje = 'Existe un Insumo con este nombre'

				 end
			else
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
/****** Object:  StoredProcedure [dbo].[gestionLabor]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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

			 if ((select count(NombreLabor) from Labor where NombreLabor = @nombreLabor and idLabor != @idLabor) = 1)

				begin

					set @mensaje = 'Existe una labor con este nombre'

				 end
			else
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
/****** Object:  StoredProcedure [dbo].[gestionLotes]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
		if ((select count(NombreLote) from Lote where NombreLote = @nombreLote and idLote != @idLote) = 1)
			begin
				
				set @mensaje = 'Existe un lote con este nombre'
			end
		else
			begin

			update Lote
				set
				NombreLote = @nombreLote,
				Observaciones = @observaciones,
				Cuadras = @cuadras
				where idLote= @idLote

			

				set @mensaje = 'Actualización exitosa'
				
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
/****** Object:  StoredProcedure [dbo].[gestionPersona]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[gestionProducto]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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

			if ((select count(NombreProducto) from Producto where NombreProducto = @NombreProducto and idProducto != @idProducto) = 1)
				begin
				
				set @mensaje = 'Existe un producto con este nombre'

				end
			 else
				begin
					
					update Producto
					set
					NombreProducto = @NombreProducto,
					Descripcion = @Descripcion
					where idProducto = @idProducto
					set @mensaje = 'Actualización exitosa!'
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
/****** Object:  StoredProcedure [dbo].[gestionProveedor]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[gestionTipoArboles]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
			if ((select count(NombreTipoArbol) from TipoArbol where NombreTipoArbol = @NombreArbol and idTipoArbol != @idTipoArbol) = 1)
				begin

				set @mensaje = 'Existe un tipo de arbol con este nombre'
					
				end
			else
				begin

				update TipoArbol
					set
					NombreTipoArbol = @NombreArbol,
					Descripcion = @Descripcion,
					TiempoProduccion = @tiempoProduccion
					where idTipoArbol = @idTipoArbol
					set @mensaje = 'Actualización exitosa!'
					
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
/****** Object:  StoredProcedure [dbo].[gestionTipoInsumo]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
			if ((select count(NombreTipoInsumo) from TipoInsumo where NombreTipoInsumo = @NombreTipoInsumo and idTipoInsumo != @idTipoInsumo) = 1)
				begin
					
					set @mensaje = 'Existe un Tipo de insumo con este nombre'

				end
			else
				begin
					

					update TipoInsumo
					set
					NombreTipoInsumo = @NombreTipoInsumo,
					Descripcion = @Descripcion
					where idTipoInsumo = @idTipoInsumo
					set @mensaje = 'Actualización exitosa!'
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
/****** Object:  StoredProcedure [dbo].[GestionUsuario]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
		if ((select count(Nickname) from Usuario where Nickname = @nickName and idUsuario != @idUsuario) = 1)
				begin
					
					set @mensaje = 'Existe un usuario con este nombre'

				end
		else
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
/****** Object:  StoredProcedure [dbo].[GestionVenta]    Script Date: 15/03/2016 3:43:28 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GestionVenta]
(@nit varchar(20),@fecha date,@numeroFactura 
varchar(50),@idProducto int,@PrecioCarga 
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
/****** Object:  StoredProcedure [dbo].[ImportarDB]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[Insercion_RegistroPago_SalarioPersonaPermanente]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[Insercion_RegistroPago_SalarioPersonaTemporal]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[insercionAbonoDeuda]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[insercionDeudaEmpleado]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[InsercionInsumoLaborLote]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[InsertarDetalleCompra]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
		P.PrecioPromedio= (ISNULL((P.PrecioPromedio * P.CantidadExistente),0) + CONVERT(bigint,DC.Subtotal))/(P.CantidadExistente + DC.cantidad)
		FROM Insumo AS P
		INNER JOIN @dtDetalleCompra AS DC
		ON P.idInsumo = DC.idInsumo






GO
/****** Object:  StoredProcedure [dbo].[insertarMovimentoArboles]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[ModificarFinca]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[PagosPersona]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[RegistrarCompra]    Script Date: 15/03/2016 3:43:28 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[RegistrarCompra]
(@nit varchar(20),@fecha date,@numeroFactura varchar(50))
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
/****** Object:  StoredProcedure [dbo].[registroProduccion]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[ReporteCompraInsumos]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[ReporteDeudaEmpleados]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[ReporteIngresosLote]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[reporteIngresosTotales]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[ReporteLaboresLote]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[reporteProduccion]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[SalariosEmpleados]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[SP_CONSULTA_EGRESO]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[SP_InsertMultiplesGastos]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[SP_ReporteEgresosPorLote]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[SP_ReportePagos]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[SP_ReportePagosPerma]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[VentaProduccion]    Script Date: 15/03/2016 3:43:28 p. m. ******/
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
			

				if (@CantidadRestante <= @cantidadKilos)
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
