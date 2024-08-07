USE [master]
GO
/****** Object:  Database [TasksDB]    Script Date: 7/11/2024 5:51:09 AM ******/
CREATE DATABASE [TasksDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TasksDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\TasksDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TasksDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\TasksDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [TasksDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TasksDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TasksDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TasksDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TasksDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TasksDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TasksDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TasksDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TasksDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TasksDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TasksDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TasksDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TasksDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TasksDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TasksDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TasksDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TasksDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TasksDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TasksDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TasksDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TasksDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TasksDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TasksDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TasksDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TasksDB] SET RECOVERY FULL 
GO
ALTER DATABASE [TasksDB] SET  MULTI_USER 
GO
ALTER DATABASE [TasksDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TasksDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TasksDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TasksDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TasksDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TasksDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TasksDB', N'ON'
GO
ALTER DATABASE [TasksDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [TasksDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [TasksDB]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CatID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[PersonID] [bigint] NOT NULL,
	[IsArchived] [bit] NOT NULL,
 CONSTRAINT [PK__Categori__6A1C8ADA8300BC39] PRIMARY KEY CLUSTERED 
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoriesLog]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoriesLog](
	[CategoryID] [bigint] NOT NULL,
	[CategoryName] [nvarchar](50) NULL,
	[PersonID] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PasswordLog]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PasswordLog](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[PersonId] [bigint] NULL,
	[OldPassword] [nvarchar](62) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[People]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[PersonID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[Password] [nvarchar](64) NOT NULL,
	[IsArchived] [bit] NOT NULL,
 CONSTRAINT [PK__Pepole__AA2FFB85502DF248] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PeopleInfoLog]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PeopleInfoLog](
	[PersonID] [bigint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[UpdateDate] [date] NOT NULL,
 CONSTRAINT [PK_PeopleInfoLog] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[TaskId] [bigint] IDENTITY(1,1) NOT NULL,
	[PersonID] [bigint] NOT NULL,
	[TaskName] [nvarchar](500) NOT NULL,
	[CreationDate] [date] NOT NULL,
	[DueDate] [date] NULL,
	[LastTaskModificationDate] [date] NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[LastNoteModificationDate] [date] NOT NULL,
	[State] [tinyint] NOT NULL,
	[IsArchived] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TasksLog]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TasksLog](
	[TaskID] [bigint] NOT NULL,
	[PersonID] [bigint] NOT NULL,
	[TaskName] [nvarchar](60) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[State] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TaskID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TasksWithCategoris]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TasksWithCategoris](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TaskID] [bigint] NOT NULL,
	[CatId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categories] ADD  DEFAULT ((0)) FOR [IsArchived]
GO
ALTER TABLE [dbo].[People] ADD  CONSTRAINT [DfValueForAR]  DEFAULT ((0)) FOR [IsArchived]
GO
ALTER TABLE [dbo].[Tasks] ADD  DEFAULT ((0)) FOR [IsArchived]
GO
ALTER TABLE [dbo].[TasksLog] ADD  DEFAULT ((1)) FOR [State]
GO
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK__Categorie__Perso__398D8EEE] FOREIGN KEY([PersonID])
REFERENCES [dbo].[People] ([PersonID])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK__Categorie__Perso__398D8EEE]
GO
ALTER TABLE [dbo].[PasswordLog]  WITH CHECK ADD FOREIGN KEY([PersonId])
REFERENCES [dbo].[People] ([PersonID])
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK__Tasks__PersonID__3C69FB99] FOREIGN KEY([PersonID])
REFERENCES [dbo].[People] ([PersonID])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK__Tasks__PersonID__3C69FB99]
GO
ALTER TABLE [dbo].[TasksWithCategoris]  WITH CHECK ADD  CONSTRAINT [FK__TaskWithC__CatId__4316F928] FOREIGN KEY([CatId])
REFERENCES [dbo].[Categories] ([CatID])
GO
ALTER TABLE [dbo].[TasksWithCategoris] CHECK CONSTRAINT [FK__TaskWithC__CatId__4316F928]
GO
ALTER TABLE [dbo].[TasksWithCategoris]  WITH CHECK ADD FOREIGN KEY([TaskID])
REFERENCES [dbo].[Tasks] ([TaskId])
GO
/****** Object:  StoredProcedure [dbo].[sp_AddCat]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_AddCat]
@Name nvarchar(50),
@PersonId bigint ,
@CatID bigint output
as 
begin
if not exists(select 1 from Categories where PersonID=@PersonId and Name=@Name)
begin
	insert into Categories (Name,PersonID)
	values(@Name,@PersonID)
	set @CatID=SCOPE_IDENTITY();
end


end
GO
/****** Object:  StoredProcedure [dbo].[sp_AddCategoryToTask]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[sp_AddCategoryToTask]
@TaskID bigint ,
@CatID bigint 
as 
begin 
if  not exists(select 1 from TasksWithCategoris c where c.TaskID=@TaskID and c.CatId=@CatID)
begin
	insert into TasksWithCategoris values(@TaskID,@CatID)
	return 1 
end
else
return 0 
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_AddNewPerson]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_AddNewPerson]
@Name nvarchar(50),
@Email nvarchar(100),
@BirthDate date,
@Password  nvarchar(64),
@PersonID bigint output
as 
begin
insert into People values(@Name,@Email,@BirthDate,@Password)
set @PersonID=SCOPE_IDENTITY();
end

GO
/****** Object:  StoredProcedure [dbo].[sp_AddTask]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_AddTask]
@TaskName nvarchar(500),
@DueDate date,
@Note nvarchar(max),
-- 1 normal 2-- overdue 3-- Completed
@personId bigint,
@TaskID bigint output
as
begin
INSERT INTO [dbo].[Tasks]
           ([PersonID]
           ,[TaskName]
           ,[CreationDate]
           ,[DueDate]
           ,[LastTaskModificationDate]
           ,[Notes]
           ,[LastNoteModificationDate]
           ,[State])
     VALUES
           (@personId,@TaskName,GETDATE(),@DueDate,GETDATE(),@Note,GETDATE(),1);
set @TaskID=SCOPE_IDENTITY();
end
GO
/****** Object:  StoredProcedure [dbo].[sp_CategoriesByPersonID]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_CategoriesByPersonID]
@PersonID bigint 
as
begin 
select c.CatID,c.Name from Categories c where PersonID=@PersonID
end
GO
/****** Object:  StoredProcedure [dbo].[sp_FindByPersonID]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_FindByPersonID]
@personID nvarchar(100)
as 
begin
select People.PersonID,People.Name,People.BirthDate,People.Email from People where PersonID=@personID;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_FindPerson]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_FindPerson]
@Email nvarchar(100),
@Password nvarchar(64)
as 
begin
select People.PersonID,People.Name,People.BirthDate from People where Email=@Email and Password=@Password
end
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTaskCategories]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_GetTaskCategories]
@Taskid bigint 
as 
begin
if exists (select 1 from TasksWithCategoris t where t.TaskID=@Taskid)
	begin
	select c.Name from Categories c
	join TasksWithCategoris TC on TC.CatId=c.CatID
	where TC.TaskID=@Taskid
	end
return 0

end 
GO
/****** Object:  StoredProcedure [dbo].[sp_IsEmailExists]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_IsEmailExists]
@email nvarchar(100)
as 
begin 
	if Exists(select 1 from People where Email=@email)
		return 1;
	else
		return 0;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_TasksByPersonID]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_TasksByPersonID]
@PersonID bigint 
as
begin 
select t.TaskId,t.TaskName,t.State,t.Notes,t.DueDate,t.LastTaskModificationDate,t.LastNoteModificationDate
,t.DueDate ,t.CreationDate  from Tasks t where t.PersonID=@PersonID
end
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdatePerson]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_UpdatePerson]
@PersonId bigint,
@Email nvarchar(100),
@Name nvarchar(50),
@BirthDate Date
as
begin
update People 
set Name=@Name, Email=@Email ,BirthDate=@BirthDate where People.PersonID=@PersonId;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdatePersonPasswoerd]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_UpdatePersonPasswoerd]
@PersonID bigint ,
@passwoerd nvarchar(62)
as 
begin
update Pepole set Password=@passwoerd where PersonID=@PersonID;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateTask]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_UpdateTask]
@TaskName nvarchar(500),
@DueDate date,
@TaskID bigint
as 
begin 
update Tasks
set TaskName=@TaskName,DueDate=@DueDate,@TaskID=@TaskID,LastTaskModificationDate=GETDATE()
where TaskId=@TaskID;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateTaskNote]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_UpdateTaskNote]
@Note nvarchar(500),
@TaskID bigint
as 
begin 
update Tasks
set Notes=@Note,LastNoteModificationDate=@Note
where TaskId=@TaskID
end
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateTaskState]    Script Date: 7/11/2024 5:51:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_UpdateTaskState]
-- 1 normal 2-- overdue 3-- Completed
@state tinyint,
@TaskID bigint
as 
begin 
update Tasks
set State=@state,LastNoteModificationDate=GETDATE()
where TaskId=@TaskID
end
GO
USE [master]
GO
ALTER DATABASE [TasksDB] SET  READ_WRITE 
GO
