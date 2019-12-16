USE [master]
GO
/****** Object:  Database [NominaDB]    Script Date: 16/12/2019 12:18:03 a. m. ******/
CREATE DATABASE [NominaDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NominaDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\NominaDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NominaDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\NominaDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [NominaDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NominaDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NominaDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NominaDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NominaDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NominaDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NominaDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [NominaDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NominaDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NominaDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NominaDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NominaDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NominaDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NominaDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NominaDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NominaDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NominaDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NominaDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NominaDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NominaDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NominaDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NominaDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NominaDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NominaDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NominaDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [NominaDB] SET  MULTI_USER 
GO
ALTER DATABASE [NominaDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NominaDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NominaDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NominaDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NominaDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NominaDB] SET QUERY_STORE = OFF
GO
USE [NominaDB]
GO
/****** Object:  Table [dbo].[Departamentos]    Script Date: 16/12/2019 12:18:03 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departamentos](
	[IdDepartamento] [int] IDENTITY(1,1) NOT NULL,
	[NomDepartamento] [varchar](70) NOT NULL,
 CONSTRAINT [PK_Departamentos] PRIMARY KEY CLUSTERED 
(
	[IdDepartamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleado]    Script Date: 16/12/2019 12:18:03 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleado](
	[IdEmpleado] [int] IDENTITY(1,1) NOT NULL,
	[IdDepartamento] [int] NOT NULL,
	[NomEmpleado] [varchar](150) NOT NULL,
	[Activo] [bit] NOT NULL,
	[Sueldo] [float] NOT NULL,
 CONSTRAINT [PK_Empleado] PRIMARY KEY CLUSTERED 
(
	[IdEmpleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nomina]    Script Date: 16/12/2019 12:18:03 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nomina](
	[IdNomina] [int] IDENTITY(1,1) NOT NULL,
	[IdEmpleado] [int] NOT NULL,
	[Fecha] [varchar](50) NOT NULL,
	[Dias] [int] NOT NULL,
	[SueldoQuincenal] [float] NOT NULL,
 CONSTRAINT [PK_Nomina] PRIMARY KEY CLUSTERED 
(
	[IdNomina] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 16/12/2019 12:18:03 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id_Usuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Email] [varchar](75) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Rol] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD  CONSTRAINT [FK_Empleado_Departamentos] FOREIGN KEY([IdDepartamento])
REFERENCES [dbo].[Departamentos] ([IdDepartamento])
GO
ALTER TABLE [dbo].[Empleado] CHECK CONSTRAINT [FK_Empleado_Departamentos]
GO
ALTER TABLE [dbo].[Nomina]  WITH CHECK ADD  CONSTRAINT [FK_Nomina_Empleado] FOREIGN KEY([IdEmpleado])
REFERENCES [dbo].[Empleado] ([IdEmpleado])
GO
ALTER TABLE [dbo].[Nomina] CHECK CONSTRAINT [FK_Nomina_Empleado]
GO
USE [master]
GO
ALTER DATABASE [NominaDB] SET  READ_WRITE 
GO
