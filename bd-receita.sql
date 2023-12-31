USE [master]
GO
/****** Object:  Database [assembly_receitas]    Script Date: 24/11/2023 16:00:47 ******/
CREATE DATABASE [assembly_receitas]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'assembly_receitas', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\assembly_receitas.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'assembly_receitas_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\assembly_receitas_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [assembly_receitas] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [assembly_receitas].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [assembly_receitas] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [assembly_receitas] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [assembly_receitas] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [assembly_receitas] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [assembly_receitas] SET ARITHABORT OFF 
GO
ALTER DATABASE [assembly_receitas] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [assembly_receitas] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [assembly_receitas] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [assembly_receitas] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [assembly_receitas] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [assembly_receitas] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [assembly_receitas] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [assembly_receitas] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [assembly_receitas] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [assembly_receitas] SET  DISABLE_BROKER 
GO
ALTER DATABASE [assembly_receitas] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [assembly_receitas] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [assembly_receitas] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [assembly_receitas] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [assembly_receitas] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [assembly_receitas] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [assembly_receitas] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [assembly_receitas] SET RECOVERY FULL 
GO
ALTER DATABASE [assembly_receitas] SET  MULTI_USER 
GO
ALTER DATABASE [assembly_receitas] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [assembly_receitas] SET DB_CHAINING OFF 
GO
ALTER DATABASE [assembly_receitas] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [assembly_receitas] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [assembly_receitas] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [assembly_receitas] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'assembly_receitas', N'ON'
GO
ALTER DATABASE [assembly_receitas] SET QUERY_STORE = ON
GO
ALTER DATABASE [assembly_receitas] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [assembly_receitas]
GO
/****** Object:  Table [dbo].[categorias]    Script Date: 24/11/2023 16:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categorias](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nomecategoria] [varchar](100) NOT NULL,
	[datacadastro] [datetime] NULL,
 CONSTRAINT [PK_categorias] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[comentariosreceita]    Script Date: 24/11/2023 16:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[comentariosreceita](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idreceita] [int] NOT NULL,
	[comentario] [varchar](200) NULL,
	[avaliacao] [int] NULL,
	[aprovado] [int] NOT NULL,
	[datacadastro] [datetime] NULL,
 CONSTRAINT [PK_comentariosreceita] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[dificuldade]    Script Date: 24/11/2023 16:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dificuldade](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[graudificuldade] [varchar](100) NOT NULL,
	[datacadastro] [datetime] NULL,
 CONSTRAINT [PK_dificuldades] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[favoritosreceita]    Script Date: 24/11/2023 16:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[favoritosreceita](
	[idusuario] [int] NOT NULL,
	[idreceita] [int] NOT NULL,
	[datacadastro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idusuario] ASC,
	[idreceita] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[fotosreceita]    Script Date: 24/11/2023 16:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fotosreceita](
	[id] [int] NOT NULL,
	[idreceita] [int] NOT NULL,
	[arquivofoto] [varchar](100) NULL,
	[principal] [int] NOT NULL,
	[ativo] [int] NOT NULL,
 CONSTRAINT [PK_fotosreceita] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[itensreceita]    Script Date: 24/11/2023 16:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[itensreceita](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idreceita] [int] NOT NULL,
	[ingredientes] [varchar](50) NULL,
	[qtdeingredientes] [varchar](20) NULL,
	[datacadastro] [datetime] NULL,
 CONSTRAINT [PK_itensreceita] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[receitas]    Script Date: 24/11/2023 16:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[receitas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[iduser] [int] NOT NULL,
	[titulo] [varchar](50) NOT NULL,
	[descricao] [varchar](1000) NULL,
	[preparo] [ntext] NULL,
	[idcategoria] [int] NOT NULL,
	[iddificuldade] [int] NOT NULL,
	[tempo] [int] NULL,
	[servepessoas] [int] NULL,
	[tipoprato] [int] NULL,
	[status] [int] NOT NULL,
	[datacadastro] [datetime] NULL,
	[fotoreceita] [varchar](100) NULL,
 CONSTRAINT [PK_receitas] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 24/11/2023 16:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL,
	[sobrenome] [varchar](100) NULL,
	[email] [varchar](100) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[senha] [varchar](100) NOT NULL,
	[ativo] [int] NOT NULL,
	[tipousuario] [int] NOT NULL,
	[datacadastro] [datetime] NULL,
 CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[comentariosreceita]  WITH CHECK ADD  CONSTRAINT [fk_comentarios] FOREIGN KEY([idreceita])
REFERENCES [dbo].[receitas] ([id])
GO
ALTER TABLE [dbo].[comentariosreceita] CHECK CONSTRAINT [fk_comentarios]
GO
ALTER TABLE [dbo].[favoritosreceita]  WITH CHECK ADD  CONSTRAINT [fk_receita] FOREIGN KEY([idreceita])
REFERENCES [dbo].[receitas] ([id])
GO
ALTER TABLE [dbo].[favoritosreceita] CHECK CONSTRAINT [fk_receita]
GO
ALTER TABLE [dbo].[favoritosreceita]  WITH CHECK ADD  CONSTRAINT [fk_usuario] FOREIGN KEY([idusuario])
REFERENCES [dbo].[usuarios] ([id])
GO
ALTER TABLE [dbo].[favoritosreceita] CHECK CONSTRAINT [fk_usuario]
GO
ALTER TABLE [dbo].[fotosreceita]  WITH CHECK ADD  CONSTRAINT [fk_fotos] FOREIGN KEY([idreceita])
REFERENCES [dbo].[receitas] ([id])
GO
ALTER TABLE [dbo].[fotosreceita] CHECK CONSTRAINT [fk_fotos]
GO
ALTER TABLE [dbo].[itensreceita]  WITH CHECK ADD  CONSTRAINT [fk_itens] FOREIGN KEY([idreceita])
REFERENCES [dbo].[receitas] ([id])
GO
ALTER TABLE [dbo].[itensreceita] CHECK CONSTRAINT [fk_itens]
GO
ALTER TABLE [dbo].[receitas]  WITH CHECK ADD  CONSTRAINT [fk_receita_categoria] FOREIGN KEY([idcategoria])
REFERENCES [dbo].[categorias] ([id])
GO
ALTER TABLE [dbo].[receitas] CHECK CONSTRAINT [fk_receita_categoria]
GO
ALTER TABLE [dbo].[receitas]  WITH CHECK ADD  CONSTRAINT [fk_receita_dificuldade] FOREIGN KEY([iddificuldade])
REFERENCES [dbo].[dificuldade] ([id])
GO
ALTER TABLE [dbo].[receitas] CHECK CONSTRAINT [fk_receita_dificuldade]
GO
ALTER TABLE [dbo].[receitas]  WITH CHECK ADD  CONSTRAINT [fk_receita_usuario] FOREIGN KEY([iduser])
REFERENCES [dbo].[usuarios] ([id])
GO
ALTER TABLE [dbo].[receitas] CHECK CONSTRAINT [fk_receita_usuario]
GO
USE [master]
GO
ALTER DATABASE [assembly_receitas] SET  READ_WRITE 
GO
