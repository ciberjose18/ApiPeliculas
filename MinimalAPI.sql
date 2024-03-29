USE [master]
GO
/****** Object:  Database [MinimalPeliculaApi]    Script Date: 11/03/2024 1:08:11 p. m. ******/
CREATE DATABASE [MinimalPeliculaApi]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MinimalGameApi', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS2\MSSQL\DATA\MinimalGameApi.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MinimalGameApi_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS2\MSSQL\DATA\MinimalGameApi_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MinimalPeliculaApi] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MinimalPeliculaApi].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MinimalPeliculaApi] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MinimalPeliculaApi] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MinimalPeliculaApi] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MinimalPeliculaApi] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MinimalPeliculaApi] SET ARITHABORT OFF 
GO
ALTER DATABASE [MinimalPeliculaApi] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [MinimalPeliculaApi] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MinimalPeliculaApi] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MinimalPeliculaApi] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MinimalPeliculaApi] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MinimalPeliculaApi] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MinimalPeliculaApi] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MinimalPeliculaApi] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MinimalPeliculaApi] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MinimalPeliculaApi] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MinimalPeliculaApi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MinimalPeliculaApi] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MinimalPeliculaApi] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MinimalPeliculaApi] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MinimalPeliculaApi] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MinimalPeliculaApi] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [MinimalPeliculaApi] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MinimalPeliculaApi] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MinimalPeliculaApi] SET  MULTI_USER 
GO
ALTER DATABASE [MinimalPeliculaApi] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MinimalPeliculaApi] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MinimalPeliculaApi] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MinimalPeliculaApi] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MinimalPeliculaApi] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MinimalPeliculaApi] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [MinimalPeliculaApi] SET QUERY_STORE = ON
GO
ALTER DATABASE [MinimalPeliculaApi] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MinimalPeliculaApi]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/03/2024 1:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Actores]    Script Date: 11/03/2024 1:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Actores](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](150) NOT NULL,
	[FechaNacimiento] [datetime2](7) NOT NULL,
	[Foto] [nvarchar](max) NULL,
 CONSTRAINT [PK_Actores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActoresPeliculas]    Script Date: 11/03/2024 1:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActoresPeliculas](
	[ActorId] [int] NOT NULL,
	[PeliculaId] [int] NOT NULL,
	[Orden] [int] NOT NULL,
	[Personaje] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ActoresPeliculas] PRIMARY KEY CLUSTERED 
(
	[ActorId] ASC,
	[PeliculaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comentarios]    Script Date: 11/03/2024 1:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comentarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Cuerpo] [nvarchar](max) NOT NULL,
	[PeliculaId] [int] NOT NULL,
	[UsuarioId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Comentarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Errores]    Script Date: 11/03/2024 1:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Errores](
	[Id] [uniqueidentifier] NOT NULL,
	[MensajeDeError] [nvarchar](max) NULL,
	[StackTrace] [nvarchar](max) NULL,
	[Fecha] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Errores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GeneroPeliculas]    Script Date: 11/03/2024 1:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GeneroPeliculas](
	[PeliculaId] [int] NOT NULL,
	[GeneroId] [int] NOT NULL,
 CONSTRAINT [PK_GeneroPeliculas] PRIMARY KEY CLUSTERED 
(
	[GeneroId] ASC,
	[PeliculaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Generos]    Script Date: 11/03/2024 1:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Generos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
 CONSTRAINT [PK_Generos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Peliculas]    Script Date: 11/03/2024 1:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Peliculas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [nvarchar](150) NOT NULL,
	[EnCines] [bit] NOT NULL,
	[FechaLanzamiento] [datetime2](7) NOT NULL,
	[Poster] [nvarchar](max) NULL,
 CONSTRAINT [PK_Peliculas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 11/03/2024 1:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolesClaims]    Script Date: 11/03/2024 1:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolesClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_RolesClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 11/03/2024 1:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuariosClaims]    Script Date: 11/03/2024 1:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuariosClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UsuariosClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuariosLogins]    Script Date: 11/03/2024 1:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuariosLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UsuariosLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuariosRoles]    Script Date: 11/03/2024 1:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuariosRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UsuariosRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuariosTokens]    Script Date: 11/03/2024 1:08:12 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuariosTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UsuariosTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240129010403_Generos', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240129011817_NombreConfigurado', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240204164447_Actores', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240207030708_Movies', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240211162309_Update-Database', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240211233111_Comentarios', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240215025928_Generopeliculas', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240216031607_ActoresPeliculas', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240217015939_ActualizarPersonaje', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240222033108_Error', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240225161055_SistemaUsuarios', N'8.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240226015729_ComentariosUsuario', N'8.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240304022633_enCines', N'8.0.2')
GO
SET IDENTITY_INSERT [dbo].[Actores] ON 

INSERT [dbo].[Actores] ([Id], [Nombre], [FechaNacimiento], [Foto]) VALUES (10, N'Chris Evans', CAST(N'1981-02-17T00:00:00.0000000' AS DateTime2), N'https://localhost:7233/actores/1784991b-6475-4ed1-90ec-558e5724d394.jpg')
INSERT [dbo].[Actores] ([Id], [Nombre], [FechaNacimiento], [Foto]) VALUES (11, N'Scarlett Johansson', CAST(N'1984-10-21T00:00:00.0000000' AS DateTime2), N'https://localhost:7233/actores/28194be0-032e-4ff0-a278-703a6fe31dcb.jpg')
INSERT [dbo].[Actores] ([Id], [Nombre], [FechaNacimiento], [Foto]) VALUES (12, N'Aisling Franciosi', CAST(N'1993-12-22T00:00:00.0000000' AS DateTime2), N'https://localhost:7233/actores/5bdc5408-2a34-4315-9a79-4162e72485c5.jpg')
INSERT [dbo].[Actores] ([Id], [Nombre], [FechaNacimiento], [Foto]) VALUES (13, N'Tom Hardy', CAST(N'1977-10-05T00:00:00.0000000' AS DateTime2), N'https://localhost:7233/actores/0415606e-5b1e-46ba-9da9-6561ed5fae99.jpg')
INSERT [dbo].[Actores] ([Id], [Nombre], [FechaNacimiento], [Foto]) VALUES (14, N'Anne Hathaway', CAST(N'1982-01-05T00:00:00.0000000' AS DateTime2), N'https://localhost:7233/actores/bf68afc2-2b23-44a8-92e4-2c762c7db8b2.jpg')
INSERT [dbo].[Actores] ([Id], [Nombre], [FechaNacimiento], [Foto]) VALUES (15, N'Timothée Chalamet', CAST(N'1993-05-10T00:00:00.0000000' AS DateTime2), N'https://localhost:7233/actores/0bea8b5a-101e-4359-a57a-a19e245024c5.jpg')
INSERT [dbo].[Actores] ([Id], [Nombre], [FechaNacimiento], [Foto]) VALUES (16, N'Zendaya', CAST(N'1996-10-11T00:00:00.0000000' AS DateTime2), N'https://localhost:7233/actores/ec967445-be3f-4fbc-8dfd-07901d7cfc26.jpg')
SET IDENTITY_INSERT [dbo].[Actores] OFF
GO
INSERT [dbo].[ActoresPeliculas] ([ActorId], [PeliculaId], [Orden], [Personaje]) VALUES (10, 9, 1, N'Capitan America')
INSERT [dbo].[ActoresPeliculas] ([ActorId], [PeliculaId], [Orden], [Personaje]) VALUES (11, 9, 2, N'Black Widow')
INSERT [dbo].[ActoresPeliculas] ([ActorId], [PeliculaId], [Orden], [Personaje]) VALUES (12, 14, 2, N'Ella Blake')
INSERT [dbo].[ActoresPeliculas] ([ActorId], [PeliculaId], [Orden], [Personaje]) VALUES (13, 12, 1, N'Max Rockatansky')
INSERT [dbo].[ActoresPeliculas] ([ActorId], [PeliculaId], [Orden], [Personaje]) VALUES (14, 13, 1, N'Amelia Brand')
INSERT [dbo].[ActoresPeliculas] ([ActorId], [PeliculaId], [Orden], [Personaje]) VALUES (14, 14, 1, N'Suzanne')
INSERT [dbo].[ActoresPeliculas] ([ActorId], [PeliculaId], [Orden], [Personaje]) VALUES (15, 10, 1, N'Paul Muad''Dib Atreides')
INSERT [dbo].[ActoresPeliculas] ([ActorId], [PeliculaId], [Orden], [Personaje]) VALUES (16, 10, 2, N' Chani')
GO
SET IDENTITY_INSERT [dbo].[Comentarios] ON 

INSERT [dbo].[Comentarios] ([Id], [Cuerpo], [PeliculaId], [UsuarioId]) VALUES (8, N'Me gusto', 9, N'91af49cd-22cd-47ae-abcf-64c2a195e95d')
INSERT [dbo].[Comentarios] ([Id], [Cuerpo], [PeliculaId], [UsuarioId]) VALUES (9, N'No Me gusto', 9, N'91af49cd-22cd-47ae-abcf-64c2a195e95d')
INSERT [dbo].[Comentarios] ([Id], [Cuerpo], [PeliculaId], [UsuarioId]) VALUES (10, N'Zzzzz', 10, N'91af49cd-22cd-47ae-abcf-64c2a195e95d')
INSERT [dbo].[Comentarios] ([Id], [Cuerpo], [PeliculaId], [UsuarioId]) VALUES (11, N'Impripresionante', 12, N'91af49cd-22cd-47ae-abcf-64c2a195e95d')
INSERT [dbo].[Comentarios] ([Id], [Cuerpo], [PeliculaId], [UsuarioId]) VALUES (12, N'Un clasico', 12, N'91af49cd-22cd-47ae-abcf-64c2a195e95d')
INSERT [dbo].[Comentarios] ([Id], [Cuerpo], [PeliculaId], [UsuarioId]) VALUES (13, N'Muy buena, de principio a fin', 13, N'91af49cd-22cd-47ae-abcf-64c2a195e95d')
INSERT [dbo].[Comentarios] ([Id], [Cuerpo], [PeliculaId], [UsuarioId]) VALUES (14, N'Me gusto mucho, el final no tanto', 13, N'91af49cd-22cd-47ae-abcf-64c2a195e95d')
SET IDENTITY_INSERT [dbo].[Comentarios] OFF
GO
INSERT [dbo].[Errores] ([Id], [MensajeDeError], [StackTrace], [Fecha]) VALUES (N'56874008-d3cf-45fb-5004-08dc3357f25b', N'error de ejemplo', N'   at Program.<>c.<<Main>$>b__0_4() in C:\Users\Administrador\Documents\ApiJuegos\ApiJuegos\Program.cs:line 110
   at lambda_method12(Closure, Object, HttpContext)
   at Microsoft.AspNetCore.OutputCaching.OutputCacheMiddleware.Invoke(HttpContext httpContext)
   at Microsoft.AspNetCore.StaticFiles.StaticFileMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.StatusCodePagesMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddlewareImpl.<Invoke>g__Awaited|10_0(ExceptionHandlerMiddlewareImpl middleware, HttpContext context, Task task)', CAST(N'2024-02-22T03:39:59.7078073' AS DateTime2))
INSERT [dbo].[Errores] ([Id], [MensajeDeError], [StackTrace], [Fecha]) VALUES (N'17560748-932e-4bd2-4b7b-08dc3e4cce47', N'The value ''pene'' is not valid for ''EnCines''.', N'   at Microsoft.AspNetCore.Http.RequestDelegateFactory.Log.FormDataMappingFailed(HttpContext httpContext, String parameterTypeName, String parameterName, FormDataMappingException exception, Boolean shouldThrow)
   at lambda_method86(Closure, Object, HttpContext, Object)
   at Microsoft.AspNetCore.Http.RequestDelegateFactory.<>c__DisplayClass104_2.<<HandleRequestBodyAndCompileRequestDelegateForForm>b__2>d.MoveNext()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.StatusCodePagesMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddlewareImpl.<Invoke>g__Awaited|10_0(ExceptionHandlerMiddlewareImpl middleware, HttpContext context, Task task)', CAST(N'2024-03-07T02:17:57.8954445' AS DateTime2))
INSERT [dbo].[Errores] ([Id], [MensajeDeError], [StackTrace], [Fecha]) VALUES (N'f015b342-685e-4590-ceba-08dc3e522bca', N'Failed to read parameter "CrearComentarioDTO crearComentarioDTO" from the request body as JSON.', N'   at Microsoft.AspNetCore.Http.RequestDelegateFactory.Log.InvalidJsonRequestBody(HttpContext httpContext, String parameterTypeName, String parameterName, Exception exception, Boolean shouldThrow)
   at Microsoft.AspNetCore.Http.RequestDelegateFactory.<HandleRequestBodyAndCompileRequestDelegateForJson>g__TryReadBodyAsync|102_0(HttpContext httpContext, Type bodyType, String parameterTypeName, String parameterName, Boolean allowEmptyRequestBody, Boolean throwOnBadRequest, JsonTypeInfo jsonTypeInfo)
   at Microsoft.AspNetCore.Http.RequestDelegateFactory.<>c__DisplayClass102_2.<<HandleRequestBodyAndCompileRequestDelegateForJson>b__2>d.MoveNext()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.StatusCodePagesMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddlewareImpl.<Invoke>g__Awaited|10_0(ExceptionHandlerMiddlewareImpl middleware, HttpContext context, Task task)', CAST(N'2024-03-07T02:56:22.2576910' AS DateTime2))
INSERT [dbo].[Errores] ([Id], [MensajeDeError], [StackTrace], [Fecha]) VALUES (N'49142c52-9cf4-4019-6b2e-08dc3e5806d7', N'For more details look at the `Errors` property.

1. The object type `Mutacion` has to at least define one field in order to be valid. (HotChocolate.Types.ObjectType<ApiPeliculas.GraphQL.Mutacion>)
', N'   at HotChocolate.Configuration.TypeInitializer.Initialize()
   at HotChocolate.SchemaBuilder.Setup.InitializeTypes(SchemaBuilder builder, IDescriptorContext context, IReadOnlyList`1 types)
   at HotChocolate.SchemaBuilder.Setup.Create(SchemaBuilder builder, LazySchema lazySchema, IDescriptorContext context)
   at HotChocolate.SchemaBuilder.Create(IDescriptorContext context)
   at HotChocolate.SchemaBuilder.HotChocolate.ISchemaBuilder.Create(IDescriptorContext context)
   at HotChocolate.Execution.RequestExecutorResolver.CreateSchemaAsync(ConfigurationContext context, RequestExecutorSetup setup, RequestExecutorOptions executorOptions, IServiceProvider schemaServices, TypeModuleChangeMonitor typeModuleChangeMonitor, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.CreateSchemaServicesAsync(ConfigurationContext context, RequestExecutorSetup setup, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.GetRequestExecutorNoLockAsync(String schemaName, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.GetRequestExecutorAsync(String schemaName, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorProxy.GetRequestExecutorAsync(CancellationToken cancellationToken)
   at HotChocolate.AspNetCore.HttpPostMiddlewareBase.HandleRequestAsync(HttpContext context)
   at HotChocolate.AspNetCore.HttpPostMiddlewareBase.InvokeAsync(HttpContext context)
   at Microsoft.AspNetCore.Builder.EndpointRouteBuilderExtensions.<>c__DisplayClass19_0.<<UseCancellation>b__1>d.MoveNext()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.StatusCodePagesMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddlewareImpl.<Invoke>g__Awaited|10_0(ExceptionHandlerMiddlewareImpl middleware, HttpContext context, Task task)', CAST(N'2024-03-07T03:38:16.7700313' AS DateTime2))
INSERT [dbo].[Errores] ([Id], [MensajeDeError], [StackTrace], [Fecha]) VALUES (N'8bdacfba-b93d-45e0-6b2f-08dc3e5806d7', N'For more details look at the `Errors` property.

1. The object type `Mutacion` has to at least define one field in order to be valid. (HotChocolate.Types.ObjectType<ApiPeliculas.GraphQL.Mutacion>)
', N'   at HotChocolate.Configuration.TypeInitializer.Initialize()
   at HotChocolate.SchemaBuilder.Setup.InitializeTypes(SchemaBuilder builder, IDescriptorContext context, IReadOnlyList`1 types)
   at HotChocolate.SchemaBuilder.Setup.Create(SchemaBuilder builder, LazySchema lazySchema, IDescriptorContext context)
   at HotChocolate.SchemaBuilder.Create(IDescriptorContext context)
   at HotChocolate.SchemaBuilder.HotChocolate.ISchemaBuilder.Create(IDescriptorContext context)
   at HotChocolate.Execution.RequestExecutorResolver.CreateSchemaAsync(ConfigurationContext context, RequestExecutorSetup setup, RequestExecutorOptions executorOptions, IServiceProvider schemaServices, TypeModuleChangeMonitor typeModuleChangeMonitor, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.CreateSchemaServicesAsync(ConfigurationContext context, RequestExecutorSetup setup, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.GetRequestExecutorNoLockAsync(String schemaName, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.GetRequestExecutorAsync(String schemaName, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorProxy.GetRequestExecutorAsync(CancellationToken cancellationToken)
   at HotChocolate.AspNetCore.HttpPostMiddlewareBase.HandleRequestAsync(HttpContext context)
   at HotChocolate.AspNetCore.HttpPostMiddlewareBase.InvokeAsync(HttpContext context)
   at Microsoft.AspNetCore.Builder.EndpointRouteBuilderExtensions.<>c__DisplayClass19_0.<<UseCancellation>b__1>d.MoveNext()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.StatusCodePagesMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddlewareImpl.<Invoke>g__Awaited|10_0(ExceptionHandlerMiddlewareImpl middleware, HttpContext context, Task task)', CAST(N'2024-03-07T03:38:16.8542384' AS DateTime2))
INSERT [dbo].[Errores] ([Id], [MensajeDeError], [StackTrace], [Fecha]) VALUES (N'e1266cf5-432b-4019-6b30-08dc3e5806d7', N'For more details look at the `Errors` property.

1. The object type `Mutacion` has to at least define one field in order to be valid. (HotChocolate.Types.ObjectType<ApiPeliculas.GraphQL.Mutacion>)
', N'   at HotChocolate.Configuration.TypeInitializer.Initialize()
   at HotChocolate.SchemaBuilder.Setup.InitializeTypes(SchemaBuilder builder, IDescriptorContext context, IReadOnlyList`1 types)
   at HotChocolate.SchemaBuilder.Setup.Create(SchemaBuilder builder, LazySchema lazySchema, IDescriptorContext context)
   at HotChocolate.SchemaBuilder.Create(IDescriptorContext context)
   at HotChocolate.SchemaBuilder.HotChocolate.ISchemaBuilder.Create(IDescriptorContext context)
   at HotChocolate.Execution.RequestExecutorResolver.CreateSchemaAsync(ConfigurationContext context, RequestExecutorSetup setup, RequestExecutorOptions executorOptions, IServiceProvider schemaServices, TypeModuleChangeMonitor typeModuleChangeMonitor, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.CreateSchemaServicesAsync(ConfigurationContext context, RequestExecutorSetup setup, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.GetRequestExecutorNoLockAsync(String schemaName, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.GetRequestExecutorAsync(String schemaName, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorProxy.GetRequestExecutorAsync(CancellationToken cancellationToken)
   at HotChocolate.AspNetCore.HttpPostMiddlewareBase.HandleRequestAsync(HttpContext context)
   at HotChocolate.AspNetCore.HttpPostMiddlewareBase.InvokeAsync(HttpContext context)
   at Microsoft.AspNetCore.Builder.EndpointRouteBuilderExtensions.<>c__DisplayClass19_0.<<UseCancellation>b__1>d.MoveNext()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.StatusCodePagesMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddlewareImpl.<Invoke>g__Awaited|10_0(ExceptionHandlerMiddlewareImpl middleware, HttpContext context, Task task)', CAST(N'2024-03-07T03:38:16.8123492' AS DateTime2))
INSERT [dbo].[Errores] ([Id], [MensajeDeError], [StackTrace], [Fecha]) VALUES (N'0c5f1804-5559-4aeb-6b31-08dc3e5806d7', N'For more details look at the `Errors` property.

1. The object type `Mutacion` has to at least define one field in order to be valid. (HotChocolate.Types.ObjectType<ApiPeliculas.GraphQL.Mutacion>)
', N'   at HotChocolate.Configuration.TypeInitializer.Initialize()
   at HotChocolate.SchemaBuilder.Setup.InitializeTypes(SchemaBuilder builder, IDescriptorContext context, IReadOnlyList`1 types)
   at HotChocolate.SchemaBuilder.Setup.Create(SchemaBuilder builder, LazySchema lazySchema, IDescriptorContext context)
   at HotChocolate.SchemaBuilder.Create(IDescriptorContext context)
   at HotChocolate.SchemaBuilder.HotChocolate.ISchemaBuilder.Create(IDescriptorContext context)
   at HotChocolate.Execution.RequestExecutorResolver.CreateSchemaAsync(ConfigurationContext context, RequestExecutorSetup setup, RequestExecutorOptions executorOptions, IServiceProvider schemaServices, TypeModuleChangeMonitor typeModuleChangeMonitor, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.CreateSchemaServicesAsync(ConfigurationContext context, RequestExecutorSetup setup, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.GetRequestExecutorNoLockAsync(String schemaName, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.GetRequestExecutorAsync(String schemaName, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorProxy.GetRequestExecutorAsync(CancellationToken cancellationToken)
   at HotChocolate.AspNetCore.HttpPostMiddlewareBase.HandleRequestAsync(HttpContext context)
   at HotChocolate.AspNetCore.HttpPostMiddlewareBase.InvokeAsync(HttpContext context)
   at Microsoft.AspNetCore.Builder.EndpointRouteBuilderExtensions.<>c__DisplayClass19_0.<<UseCancellation>b__1>d.MoveNext()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.StatusCodePagesMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddlewareImpl.<Invoke>g__Awaited|10_0(ExceptionHandlerMiddlewareImpl middleware, HttpContext context, Task task)', CAST(N'2024-03-07T03:38:16.9014371' AS DateTime2))
INSERT [dbo].[Errores] ([Id], [MensajeDeError], [StackTrace], [Fecha]) VALUES (N'd214dc07-8cb5-453c-2318-08dc3e582142', N'For more details look at the `Errors` property.

1. The object type `Mutacion` has to at least define one field in order to be valid. (HotChocolate.Types.ObjectType<ApiPeliculas.GraphQL.Mutacion>)
', N'   at HotChocolate.Configuration.TypeInitializer.Initialize()
   at HotChocolate.SchemaBuilder.Setup.InitializeTypes(SchemaBuilder builder, IDescriptorContext context, IReadOnlyList`1 types)
   at HotChocolate.SchemaBuilder.Setup.Create(SchemaBuilder builder, LazySchema lazySchema, IDescriptorContext context)
   at HotChocolate.SchemaBuilder.Create(IDescriptorContext context)
   at HotChocolate.SchemaBuilder.HotChocolate.ISchemaBuilder.Create(IDescriptorContext context)
   at HotChocolate.Execution.RequestExecutorResolver.CreateSchemaAsync(ConfigurationContext context, RequestExecutorSetup setup, RequestExecutorOptions executorOptions, IServiceProvider schemaServices, TypeModuleChangeMonitor typeModuleChangeMonitor, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.CreateSchemaServicesAsync(ConfigurationContext context, RequestExecutorSetup setup, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.GetRequestExecutorNoLockAsync(String schemaName, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.GetRequestExecutorAsync(String schemaName, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorProxy.GetRequestExecutorAsync(CancellationToken cancellationToken)
   at HotChocolate.AspNetCore.HttpPostMiddlewareBase.HandleRequestAsync(HttpContext context)
   at HotChocolate.AspNetCore.HttpPostMiddlewareBase.InvokeAsync(HttpContext context)
   at Microsoft.AspNetCore.Builder.EndpointRouteBuilderExtensions.<>c__DisplayClass19_0.<<UseCancellation>b__1>d.MoveNext()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.StatusCodePagesMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddlewareImpl.<Invoke>g__Awaited|10_0(ExceptionHandlerMiddlewareImpl middleware, HttpContext context, Task task)', CAST(N'2024-03-07T03:39:01.2224804' AS DateTime2))
INSERT [dbo].[Errores] ([Id], [MensajeDeError], [StackTrace], [Fecha]) VALUES (N'4ed12a3a-daa7-4b6a-2319-08dc3e582142', N'For more details look at the `Errors` property.

1. The object type `Mutacion` has to at least define one field in order to be valid. (HotChocolate.Types.ObjectType<ApiPeliculas.GraphQL.Mutacion>)
', N'   at HotChocolate.Configuration.TypeInitializer.Initialize()
   at HotChocolate.SchemaBuilder.Setup.InitializeTypes(SchemaBuilder builder, IDescriptorContext context, IReadOnlyList`1 types)
   at HotChocolate.SchemaBuilder.Setup.Create(SchemaBuilder builder, LazySchema lazySchema, IDescriptorContext context)
   at HotChocolate.SchemaBuilder.Create(IDescriptorContext context)
   at HotChocolate.SchemaBuilder.HotChocolate.ISchemaBuilder.Create(IDescriptorContext context)
   at HotChocolate.Execution.RequestExecutorResolver.CreateSchemaAsync(ConfigurationContext context, RequestExecutorSetup setup, RequestExecutorOptions executorOptions, IServiceProvider schemaServices, TypeModuleChangeMonitor typeModuleChangeMonitor, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.CreateSchemaServicesAsync(ConfigurationContext context, RequestExecutorSetup setup, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.GetRequestExecutorNoLockAsync(String schemaName, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.GetRequestExecutorAsync(String schemaName, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorProxy.GetRequestExecutorAsync(CancellationToken cancellationToken)
   at HotChocolate.AspNetCore.HttpPostMiddlewareBase.HandleRequestAsync(HttpContext context)
   at HotChocolate.AspNetCore.HttpPostMiddlewareBase.InvokeAsync(HttpContext context)
   at Microsoft.AspNetCore.Builder.EndpointRouteBuilderExtensions.<>c__DisplayClass19_0.<<UseCancellation>b__1>d.MoveNext()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.StatusCodePagesMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddlewareImpl.<Invoke>g__Awaited|10_0(ExceptionHandlerMiddlewareImpl middleware, HttpContext context, Task task)', CAST(N'2024-03-07T03:39:01.0907021' AS DateTime2))
INSERT [dbo].[Errores] ([Id], [MensajeDeError], [StackTrace], [Fecha]) VALUES (N'7c6a451e-867d-492c-231a-08dc3e582142', N'For more details look at the `Errors` property.

1. The object type `Mutacion` has to at least define one field in order to be valid. (HotChocolate.Types.ObjectType<ApiPeliculas.GraphQL.Mutacion>)
', N'   at HotChocolate.Configuration.TypeInitializer.Initialize()
   at HotChocolate.SchemaBuilder.Setup.InitializeTypes(SchemaBuilder builder, IDescriptorContext context, IReadOnlyList`1 types)
   at HotChocolate.SchemaBuilder.Setup.Create(SchemaBuilder builder, LazySchema lazySchema, IDescriptorContext context)
   at HotChocolate.SchemaBuilder.Create(IDescriptorContext context)
   at HotChocolate.SchemaBuilder.HotChocolate.ISchemaBuilder.Create(IDescriptorContext context)
   at HotChocolate.Execution.RequestExecutorResolver.CreateSchemaAsync(ConfigurationContext context, RequestExecutorSetup setup, RequestExecutorOptions executorOptions, IServiceProvider schemaServices, TypeModuleChangeMonitor typeModuleChangeMonitor, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.CreateSchemaServicesAsync(ConfigurationContext context, RequestExecutorSetup setup, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.GetRequestExecutorNoLockAsync(String schemaName, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.GetRequestExecutorAsync(String schemaName, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorProxy.GetRequestExecutorAsync(CancellationToken cancellationToken)
   at HotChocolate.AspNetCore.HttpPostMiddlewareBase.HandleRequestAsync(HttpContext context)
   at HotChocolate.AspNetCore.HttpPostMiddlewareBase.InvokeAsync(HttpContext context)
   at Microsoft.AspNetCore.Builder.EndpointRouteBuilderExtensions.<>c__DisplayClass19_0.<<UseCancellation>b__1>d.MoveNext()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.StatusCodePagesMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddlewareImpl.<Invoke>g__Awaited|10_0(ExceptionHandlerMiddlewareImpl middleware, HttpContext context, Task task)', CAST(N'2024-03-07T03:39:01.1752794' AS DateTime2))
INSERT [dbo].[Errores] ([Id], [MensajeDeError], [StackTrace], [Fecha]) VALUES (N'e60f83da-1096-42d7-231b-08dc3e582142', N'For more details look at the `Errors` property.

1. The object type `Mutacion` has to at least define one field in order to be valid. (HotChocolate.Types.ObjectType<ApiPeliculas.GraphQL.Mutacion>)
', N'   at HotChocolate.Configuration.TypeInitializer.Initialize()
   at HotChocolate.SchemaBuilder.Setup.InitializeTypes(SchemaBuilder builder, IDescriptorContext context, IReadOnlyList`1 types)
   at HotChocolate.SchemaBuilder.Setup.Create(SchemaBuilder builder, LazySchema lazySchema, IDescriptorContext context)
   at HotChocolate.SchemaBuilder.Create(IDescriptorContext context)
   at HotChocolate.SchemaBuilder.HotChocolate.ISchemaBuilder.Create(IDescriptorContext context)
   at HotChocolate.Execution.RequestExecutorResolver.CreateSchemaAsync(ConfigurationContext context, RequestExecutorSetup setup, RequestExecutorOptions executorOptions, IServiceProvider schemaServices, TypeModuleChangeMonitor typeModuleChangeMonitor, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.CreateSchemaServicesAsync(ConfigurationContext context, RequestExecutorSetup setup, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.GetRequestExecutorNoLockAsync(String schemaName, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorResolver.GetRequestExecutorAsync(String schemaName, CancellationToken cancellationToken)
   at HotChocolate.Execution.RequestExecutorProxy.GetRequestExecutorAsync(CancellationToken cancellationToken)
   at HotChocolate.AspNetCore.HttpPostMiddlewareBase.HandleRequestAsync(HttpContext context)
   at HotChocolate.AspNetCore.HttpPostMiddlewareBase.InvokeAsync(HttpContext context)
   at Microsoft.AspNetCore.Builder.EndpointRouteBuilderExtensions.<>c__DisplayClass19_0.<<UseCancellation>b__1>d.MoveNext()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.StatusCodePagesMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddlewareImpl.<Invoke>g__Awaited|10_0(ExceptionHandlerMiddlewareImpl middleware, HttpContext context, Task task)', CAST(N'2024-03-07T03:39:01.1317316' AS DateTime2))
GO
INSERT [dbo].[GeneroPeliculas] ([PeliculaId], [GeneroId]) VALUES (9, 20)
INSERT [dbo].[GeneroPeliculas] ([PeliculaId], [GeneroId]) VALUES (9, 21)
INSERT [dbo].[GeneroPeliculas] ([PeliculaId], [GeneroId]) VALUES (9, 24)
INSERT [dbo].[GeneroPeliculas] ([PeliculaId], [GeneroId]) VALUES (10, 21)
INSERT [dbo].[GeneroPeliculas] ([PeliculaId], [GeneroId]) VALUES (10, 23)
INSERT [dbo].[GeneroPeliculas] ([PeliculaId], [GeneroId]) VALUES (10, 25)
INSERT [dbo].[GeneroPeliculas] ([PeliculaId], [GeneroId]) VALUES (10, 26)
INSERT [dbo].[GeneroPeliculas] ([PeliculaId], [GeneroId]) VALUES (12, 20)
INSERT [dbo].[GeneroPeliculas] ([PeliculaId], [GeneroId]) VALUES (12, 21)
INSERT [dbo].[GeneroPeliculas] ([PeliculaId], [GeneroId]) VALUES (12, 25)
INSERT [dbo].[GeneroPeliculas] ([PeliculaId], [GeneroId]) VALUES (12, 26)
INSERT [dbo].[GeneroPeliculas] ([PeliculaId], [GeneroId]) VALUES (13, 21)
INSERT [dbo].[GeneroPeliculas] ([PeliculaId], [GeneroId]) VALUES (13, 23)
INSERT [dbo].[GeneroPeliculas] ([PeliculaId], [GeneroId]) VALUES (14, 19)
INSERT [dbo].[GeneroPeliculas] ([PeliculaId], [GeneroId]) VALUES (14, 22)
INSERT [dbo].[GeneroPeliculas] ([PeliculaId], [GeneroId]) VALUES (14, 23)
GO
SET IDENTITY_INSERT [dbo].[Generos] ON 

INSERT [dbo].[Generos] ([Id], [Nombre]) VALUES (19, N'Terror')
INSERT [dbo].[Generos] ([Id], [Nombre]) VALUES (20, N'Accion')
INSERT [dbo].[Generos] ([Id], [Nombre]) VALUES (21, N'Ciencia Ficcion')
INSERT [dbo].[Generos] ([Id], [Nombre]) VALUES (22, N'Suspenso')
INSERT [dbo].[Generos] ([Id], [Nombre]) VALUES (23, N'Drama')
INSERT [dbo].[Generos] ([Id], [Nombre]) VALUES (24, N'Superheroes')
INSERT [dbo].[Generos] ([Id], [Nombre]) VALUES (25, N'Distopia')
INSERT [dbo].[Generos] ([Id], [Nombre]) VALUES (26, N'Aventura')
INSERT [dbo].[Generos] ([Id], [Nombre]) VALUES (28, N'GraphQL')
SET IDENTITY_INSERT [dbo].[Generos] OFF
GO
SET IDENTITY_INSERT [dbo].[Peliculas] ON 

INSERT [dbo].[Peliculas] ([Id], [Titulo], [EnCines], [FechaLanzamiento], [Poster]) VALUES (9, N'Avengers: Endgame', 0, CAST(N'2019-04-25T00:00:00.0000000' AS DateTime2), N'https://localhost:7233/peliculas/8c31a9ac-f910-40d5-8a90-ec5c1dfeb201.jpg')
INSERT [dbo].[Peliculas] ([Id], [Titulo], [EnCines], [FechaLanzamiento], [Poster]) VALUES (10, N'Dune: Part Two', 1, CAST(N'2024-02-29T00:00:00.0000000' AS DateTime2), N'https://localhost:7233/peliculas/f8f3c84d-199a-4708-ac08-361a5c7faa4f.jpeg')
INSERT [dbo].[Peliculas] ([Id], [Titulo], [EnCines], [FechaLanzamiento], [Poster]) VALUES (12, N'Mad Max: Fury Road', 0, CAST(N'2015-05-14T00:00:00.0000000' AS DateTime2), N'https://localhost:7233/peliculas/d2ddb400-80ea-447a-90d0-4618fc487fd2.jpg')
INSERT [dbo].[Peliculas] ([Id], [Titulo], [EnCines], [FechaLanzamiento], [Poster]) VALUES (13, N'Interstellar', 0, CAST(N'2014-10-26T00:00:00.0000000' AS DateTime2), N'https://localhost:7233/peliculas/3cf60be9-5f66-498f-8d9a-9d5d8a4f91bc.jpg')
INSERT [dbo].[Peliculas] ([Id], [Titulo], [EnCines], [FechaLanzamiento], [Poster]) VALUES (14, N'Stopmotion', 1, CAST(N'2024-02-23T00:00:00.0000000' AS DateTime2), N'https://localhost:7233/peliculas/fada03b1-4850-4127-8b99-d0125f5df06a.jpg')
SET IDENTITY_INSERT [dbo].[Peliculas] OFF
GO
INSERT [dbo].[Usuarios] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'91af49cd-22cd-47ae-abcf-64c2a195e95d', N'jose@gmail.com', N'JOSE@GMAIL.COM', N'jose@gmail.com', N'JOSE@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEMANn/vpBuj2KI1NvBrcLrjl0LKaQcJYmjJXEPlxHjyOzih/FgIiZXHNwFvjWBvBXA==', N'Q3TBSPK6HYRHCHNEN5GYU2HQUALMFCNW', N'c5b02a38-8264-4ba5-9bfa-930ee4d857f8', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[Usuarios] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'9fefcd46-1649-467b-922b-13e99507a9f0', N'coki@gmail.com', N'COKI@GMAIL.COM', N'coki@gmail.com', N'COKI@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEHS4Tg7OKhsSJnvtuicV4T4qMh+t2CfaYvsPbWmQwUFxPug9JrijBBfjX/9Z0QDD0g==', N'ATHT6CJRW7OB7BNLZVXD2UFUFTA24GQU', N'bbc92803-8615-4f68-84e2-48f3b775e1c8', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[UsuariosClaims] ON 

INSERT [dbo].[UsuariosClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (2, N'91af49cd-22cd-47ae-abcf-64c2a195e95d', N'admin', N'true')
SET IDENTITY_INSERT [dbo].[UsuariosClaims] OFF
GO
/****** Object:  Index [IX_ActoresPeliculas_PeliculaId]    Script Date: 11/03/2024 1:08:12 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_ActoresPeliculas_PeliculaId] ON [dbo].[ActoresPeliculas]
(
	[PeliculaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comentarios_PeliculaId]    Script Date: 11/03/2024 1:08:12 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Comentarios_PeliculaId] ON [dbo].[Comentarios]
(
	[PeliculaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Comentarios_UsuarioId]    Script Date: 11/03/2024 1:08:12 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Comentarios_UsuarioId] ON [dbo].[Comentarios]
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_GeneroPeliculas_PeliculaId]    Script Date: 11/03/2024 1:08:12 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_GeneroPeliculas_PeliculaId] ON [dbo].[GeneroPeliculas]
(
	[PeliculaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 11/03/2024 1:08:12 p. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[Roles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RolesClaims_RoleId]    Script Date: 11/03/2024 1:08:12 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_RolesClaims_RoleId] ON [dbo].[RolesClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 11/03/2024 1:08:12 p. m. ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[Usuarios]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 11/03/2024 1:08:12 p. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[Usuarios]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UsuariosClaims_UserId]    Script Date: 11/03/2024 1:08:12 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_UsuariosClaims_UserId] ON [dbo].[UsuariosClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UsuariosLogins_UserId]    Script Date: 11/03/2024 1:08:12 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_UsuariosLogins_UserId] ON [dbo].[UsuariosLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UsuariosRoles_RoleId]    Script Date: 11/03/2024 1:08:12 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_UsuariosRoles_RoleId] ON [dbo].[UsuariosRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Actores] ADD  DEFAULT (N'') FOR [Nombre]
GO
ALTER TABLE [dbo].[Comentarios] ADD  DEFAULT (N'') FOR [UsuarioId]
GO
ALTER TABLE [dbo].[Peliculas] ADD  DEFAULT (CONVERT([bit],(0))) FOR [EnCines]
GO
ALTER TABLE [dbo].[ActoresPeliculas]  WITH CHECK ADD  CONSTRAINT [FK_ActoresPeliculas_Actores_ActorId] FOREIGN KEY([ActorId])
REFERENCES [dbo].[Actores] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ActoresPeliculas] CHECK CONSTRAINT [FK_ActoresPeliculas_Actores_ActorId]
GO
ALTER TABLE [dbo].[ActoresPeliculas]  WITH CHECK ADD  CONSTRAINT [FK_ActoresPeliculas_Peliculas_PeliculaId] FOREIGN KEY([PeliculaId])
REFERENCES [dbo].[Peliculas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ActoresPeliculas] CHECK CONSTRAINT [FK_ActoresPeliculas_Peliculas_PeliculaId]
GO
ALTER TABLE [dbo].[Comentarios]  WITH CHECK ADD  CONSTRAINT [FK_Comentarios_Peliculas_PeliculaId] FOREIGN KEY([PeliculaId])
REFERENCES [dbo].[Peliculas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comentarios] CHECK CONSTRAINT [FK_Comentarios_Peliculas_PeliculaId]
GO
ALTER TABLE [dbo].[Comentarios]  WITH CHECK ADD  CONSTRAINT [FK_Comentarios_Usuarios_UsuarioId] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comentarios] CHECK CONSTRAINT [FK_Comentarios_Usuarios_UsuarioId]
GO
ALTER TABLE [dbo].[GeneroPeliculas]  WITH CHECK ADD  CONSTRAINT [FK_GeneroPeliculas_Generos_GeneroId] FOREIGN KEY([GeneroId])
REFERENCES [dbo].[Generos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GeneroPeliculas] CHECK CONSTRAINT [FK_GeneroPeliculas_Generos_GeneroId]
GO
ALTER TABLE [dbo].[GeneroPeliculas]  WITH CHECK ADD  CONSTRAINT [FK_GeneroPeliculas_Peliculas_PeliculaId] FOREIGN KEY([PeliculaId])
REFERENCES [dbo].[Peliculas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GeneroPeliculas] CHECK CONSTRAINT [FK_GeneroPeliculas_Peliculas_PeliculaId]
GO
ALTER TABLE [dbo].[RolesClaims]  WITH CHECK ADD  CONSTRAINT [FK_RolesClaims_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RolesClaims] CHECK CONSTRAINT [FK_RolesClaims_Roles_RoleId]
GO
ALTER TABLE [dbo].[UsuariosClaims]  WITH CHECK ADD  CONSTRAINT [FK_UsuariosClaims_Usuarios_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Usuarios] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsuariosClaims] CHECK CONSTRAINT [FK_UsuariosClaims_Usuarios_UserId]
GO
ALTER TABLE [dbo].[UsuariosLogins]  WITH CHECK ADD  CONSTRAINT [FK_UsuariosLogins_Usuarios_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Usuarios] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsuariosLogins] CHECK CONSTRAINT [FK_UsuariosLogins_Usuarios_UserId]
GO
ALTER TABLE [dbo].[UsuariosRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsuariosRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsuariosRoles] CHECK CONSTRAINT [FK_UsuariosRoles_Roles_RoleId]
GO
ALTER TABLE [dbo].[UsuariosRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsuariosRoles_Usuarios_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Usuarios] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsuariosRoles] CHECK CONSTRAINT [FK_UsuariosRoles_Usuarios_UserId]
GO
ALTER TABLE [dbo].[UsuariosTokens]  WITH CHECK ADD  CONSTRAINT [FK_UsuariosTokens_Usuarios_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Usuarios] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsuariosTokens] CHECK CONSTRAINT [FK_UsuariosTokens_Usuarios_UserId]
GO
USE [master]
GO
ALTER DATABASE [MinimalPeliculaApi] SET  READ_WRITE 
GO
