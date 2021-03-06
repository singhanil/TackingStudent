USE [master]
GO
/****** Object:  Database [StudentTracking]    Script Date: 04/23/2016 10:27:33 ******/
CREATE DATABASE [StudentTracking] ON  PRIMARY 
( NAME = N'StudentTracking', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\StudentTracking.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'StudentTracking_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\StudentTracking_1.ldf' , SIZE = 1280KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [StudentTracking] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StudentTracking].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StudentTracking] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [StudentTracking] SET ANSI_NULLS OFF
GO
ALTER DATABASE [StudentTracking] SET ANSI_PADDING OFF
GO
ALTER DATABASE [StudentTracking] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [StudentTracking] SET ARITHABORT OFF
GO
ALTER DATABASE [StudentTracking] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [StudentTracking] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [StudentTracking] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [StudentTracking] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [StudentTracking] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [StudentTracking] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [StudentTracking] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [StudentTracking] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [StudentTracking] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [StudentTracking] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [StudentTracking] SET  DISABLE_BROKER
GO
ALTER DATABASE [StudentTracking] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [StudentTracking] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [StudentTracking] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [StudentTracking] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [StudentTracking] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [StudentTracking] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [StudentTracking] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [StudentTracking] SET  READ_WRITE
GO
ALTER DATABASE [StudentTracking] SET RECOVERY FULL
GO
ALTER DATABASE [StudentTracking] SET  MULTI_USER
GO
ALTER DATABASE [StudentTracking] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [StudentTracking] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'StudentTracking', N'ON'
GO
USE [StudentTracking]
GO
/****** Object:  Table [dbo].[Section]    Script Date: 04/23/2016 10:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Section](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Section] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SecondaryTag]    Script Date: 04/23/2016 10:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SecondaryTag](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TagId] [nvarchar](200) NOT NULL,
	[IP Address] [nvarchar](50) NULL,
	[TagInformation1] [nvarchar](50) NULL,
	[TagInformation2] [nvarchar](50) NULL,
 CONSTRAINT [PK_SecondaryTag_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_SecondaryTag] ON [dbo].[SecondaryTag] 
(
	[TagId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[School]    Script Date: 04/23/2016 10:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[School](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Address] [nvarchar](500) NULL,
	[Contact Detail] [nvarchar](50) NULL,
 CONSTRAINT [PK_School] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PrimaryTag]    Script Date: 04/23/2016 10:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrimaryTag](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TagId] [nvarchar](200) NOT NULL,
	[IP Address] [nvarchar](50) NULL,
	[TagInformation1] [nvarchar](50) NULL,
	[TagInformation2] [nvarchar](50) NULL,
 CONSTRAINT [PK_PrimaryTag_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_PrimaryTag] ON [dbo].[PrimaryTag] 
(
	[TagId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 04/23/2016 10:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](1000) NOT NULL,
	[MessageCode] [nchar](10) NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Holiday]    Script Date: 04/23/2016 10:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Holiday](
	[Id] [int] NOT NULL,
	[HolidayDate] [date] NULL,
	[HolidayDay] [nvarchar](50) NULL,
 CONSTRAINT [PK_Holiday] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 04/23/2016 10:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 04/23/2016 10:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Hometown] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers] 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 04/23/2016 10:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles] 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 04/23/2016 10:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_GetMessage]    Script Date: 04/23/2016 10:27:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetMessage]
	
AS

BEGIN
	SET NOCOUNT ON;
	
	
		SELECT * FROM dbo.Message WITH(NOLOCK)
	
END
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 04/23/2016 10:27:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles] 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 04/23/2016 10:27:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 04/23/2016 10:27:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SchoolBranch]    Script Date: 04/23/2016 10:27:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SchoolBranch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Address] [nvarchar](500) NULL,
	[SchoolId] [int] NULL,
	[Contact Detail] [nvarchar](500) NULL,
	[Branch Code] [nvarchar](50) NULL,
 CONSTRAINT [PK_SchoolBranch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentAttendance]    Script Date: 04/23/2016 10:27:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentAttendance](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PrimaryTagId] [bigint] NULL,
	[SecondaryTagId] [bigint] NULL,
	[InTime] [datetime] NULL,
	[OutTime] [datetime] NULL,
	[IntimeMessageSendStatus] [bit] NULL,
	[OutTimeMessageSentStatus] [bit] NULL,
	[NextRuntime] [datetime] NULL,
	[LastRuntime] [datetime] NULL,
	[LockAcquired] [datetime] NULL,
	[LockExpire] [datetime] NULL,
	[LockUid] [uniqueidentifier] NULL,
	[LockProcessId] [int] NULL,
	[ProcessRetryCount] [int] NULL,
	[LockRetryCount] [int] NULL,
	[TagReadingTime] [datetime] NULL,
	[NoOftimeTimeTagRead] [smallint] NULL,
	[IpAddress] [nvarchar](50) NULL,
 CONSTRAINT [PK_StudentAttendance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StaffDetail]    Script Date: 04/23/2016 10:27:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StaffDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StaffId] [nvarchar](50) NULL,
	[StaffName] [nvarchar](100) NULL,
	[Department] [tinyint] NULL,
	[PrimaryTagId] [bigint] NULL,
	[ReportingEmailId] [nvarchar](200) NULL,
	[StaffMobileNo] [nvarchar](50) NULL,
	[ClassId] [int] NULL,
	[SectionId] [int] NULL,
 CONSTRAINT [PK_StaffDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateMessagingStatus]    Script Date: 04/23/2016 10:27:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateMessagingStatus]
@AttendanceId BIGINT,
@IsInTime bit	
AS

BEGIN
	SET NOCOUNT ON;
	IF @IsInTime = 1
	BEGIN
		UPDATE dbo.StudentAttendance SET IntimeMessageSendStatus = 1 WHERE ID = @AttendanceId and  IntimeMessageSendStatus = 0
	END
	ELSE
	BEGIN
		UPDATE dbo.StudentAttendance SET OutTimeMessageSentStatus = 1 WHERE ID = @AttendanceId and  OutTimeMessageSentStatus = 0
	END
	
	
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateAttendenceStatus]    Script Date: 04/23/2016 10:27:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateAttendenceStatus]
@AttendanceId BIGINT,
@IsInTime bit	
AS

BEGIN
	SET NOCOUNT ON;
	IF @IsInTime = 1
	BEGIN
		UPDATE dbo.StudentAttendance SET IntimeMessageSendStatus = 1 WHERE ID = @AttendanceId and  IntimeMessageSendStatus = 0
	END
	ELSE
	BEGIN
		UPDATE dbo.StudentAttendance SET OutTimeMessageSentStatus = 1 WHERE ID = @AttendanceId and  OutTimeMessageSentStatus = 0
	END
	
	
END
GO
/****** Object:  Table [dbo].[StudentDetail]    Script Date: 04/23/2016 10:27:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [nvarchar](50) NULL,
	[ParentMobileNo] [nvarchar](50) NULL,
	[PrimaryTagId] [bigint] NULL,
	[SecondaryTagId] [bigint] NULL,
	[EmailId] [nvarchar](200) NULL,
	[StudentName] [nvarchar](100) NULL,
	[SchoolBranchId] [int] NULL,
	[ClassId] [int] NULL,
	[SectionId] [int] NULL,
 CONSTRAINT [PK_StudentDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAttendanceInfoForMessageSend]    Script Date: 04/23/2016 10:27:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAttendanceInfoForMessageSend]
	@IsInTime BIT,
	@StartTime DATETIME,
	@EndTime DATETIME
AS

BEGIN
	SET NOCOUNT ON;
	IF @IsInTime = 1
	BEGIN
		SELECT SA.Id, ParentMobileNo FROM 	dbo.StudentAttendance SA	WITH(NOLOCK) INNER JOIN 	dbo.StudentDetail SD ON	SA.PrimaryTagId = SD.PrimaryTagId
		WHERE IntimeMessageSendStatus = 0 AND InTime BETWEEN @StartTime AND @EndTime
	END
	ELSE
	BEGIN
		SELECT SA.Id,ParentMobileNo FROM 	dbo.StudentAttendance SA	WITH(NOLOCK) INNER JOIN 	dbo.StudentDetail SD ON	SA.PrimaryTagId = SD.PrimaryTagId
		WHERE OutTimeMessageSentStatus = 0 AND OutTime BETWEEN @StartTime AND @EndTime
		
	END
	
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertUpdateAttendance]    Script Date: 04/23/2016 10:27:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_InsertUpdateAttendance]
	-- Add the parameters for the stored procedure here
	@TagId NVARCHAR(200),	
	@StartTime DATETIME,
	@EndTime DATETIME,
	@IsInTime BIT,
	@IpAddress NVARCHAR(200)
	
AS

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
	SET NOCOUNT ON;
	DECLARE @Count BIGINT
	DECLARE @OutTime DATETIME
	DECLARE @PrimaryTagId BIGINT
	DECLARE @Action NVARCHAR(50)
	DECLARE @TodayDate DATE	
	DECLARE @Id  NUMERIC(38,0)
	DECLARE @Attendence TABLE (AttendanceId NUMERIC(38,0))
	SELECT @TodayDate = CONVERT (DATE, GETDATE()) 
	
	SET @Count = 0
	SELECT @PrimaryTagId=id FROM dbo.PrimaryTag WITH(NOLOCK) WHERE TagId=@TagId AND [IP Address]=@IpAddress
	PRINT @PrimaryTagId
	SET @Action='UNACTIONED'
	IF @IsInTime=1
	
	BEGIN
		SELECT @Count = COUNT(1) FROM dbo.StudentAttendance WITH(NOLOCK) 
		WHERE PrimaryTagId = @PrimaryTagId AND  @TodayDate  = CONVERT (DATE, InTime) AND [IPAddress]=@IpAddress
		IF	@Count = 0
		BEGIN
			INSERT INTO [dbo].[StudentAttendance]
			([PrimaryTagId]
           ,[InTime]
           ,[InTimeMessageSendStatus]
           ,[IpAddress]
           )
     VALUES
     ( 
         @PrimaryTagId
           ,GETDATE()
           ,0
           ,@IpAddress)
           SET @Action='INSERTED'	
		END
		SET @Id = @@IDENTITY
	END
	ELSE
	BEGIN
	 SELECT @Count = COUNT(1) FROM dbo.StudentAttendance WITH(NOLOCK) 
	 WHERE  PrimaryTagId=@PrimaryTagId   AND  @TodayDate  = CONVERT (DATE, OutTime) AND [IPAddress]=@IpAddress
	 PRINT @count
	    IF @Count = 0
	    BEGIN
	    	UPDATE	[dbo].[StudentAttendance] SET OutTime=GETDATE(),OutTimeMessageSentStatus=0 OUTPUT Inserted.Id INTO @Attendence
	    	WHERE PrimaryTagId = @PrimaryTagId AND  OutTime  
	    	IS NULL AND OutTimeMessageSentStatus IS NULL  AND [IPAddress]=@IpAddress
	    	SELECT TOP(1) @Id = AttendanceId FROM @Attendence
	    		SET @Action='UPDATED'
	    		
	    END
	END
	SELECT @Id AS [AttendanceID],ParentMobileNo,@Action AS [Status] FROM dbo.StudentDetail WITH(NOLOCK) WHERE PrimaryTagId = @PrimaryTagId
GO
/****** Object:  StoredProcedure [dbo].[sp_GetMobileInfoForMessageSend]    Script Date: 04/23/2016 10:27:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetMobileInfoForMessageSend]
	@IsInTime BIT,
	@StartTime DATETIME,
	@EndTime DATETIME, 
	@SchoolBranchCode NVARCHAR(50)
AS

BEGIN
	SET NOCOUNT ON;
	IF @IsInTime = 1
	BEGIN
		SELECT SA.Id, ParentMobileNo FROM 	dbo.StudentAttendance SA	WITH(NOLOCK) INNER JOIN 	dbo.StudentDetail SD ON	SA.PrimaryTagId = SD.PrimaryTagId
		INNER JOIN dbo.SchoolBranch SB ON	SD.SchoolBranchId = SB.Id
		WHERE IntimeMessageSendStatus = 0 AND InTime BETWEEN @StartTime AND @EndTime AND BranchCode=@SchoolBranchCode
	END
	ELSE
	BEGIN
		SELECT SA.Id,ParentMobileNo FROM 	dbo.StudentAttendance SA	WITH(NOLOCK) INNER JOIN 	dbo.StudentDetail SD ON	SA.PrimaryTagId = SD.PrimaryTagId
		INNER JOIN dbo.SchoolBranch SB ON	SD.SchoolBranchId = SB.Id
		WHERE OutTimeMessageSentStatus = 0 AND OutTime BETWEEN @StartTime AND @EndTime AND  BranchCode=@SchoolBranchCode
		
	END
	
END
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]    Script Date: 04/23/2016 10:27:35 ******/
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]    Script Date: 04/23/2016 10:27:35 ******/
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]    Script Date: 04/23/2016 10:27:35 ******/
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]    Script Date: 04/23/2016 10:27:35 ******/
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
/****** Object:  ForeignKey [FK_SchoolBranch_School]    Script Date: 04/23/2016 10:27:35 ******/
ALTER TABLE [dbo].[SchoolBranch]  WITH CHECK ADD  CONSTRAINT [FK_SchoolBranch_School] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[School] ([Id])
GO
ALTER TABLE [dbo].[SchoolBranch] CHECK CONSTRAINT [FK_SchoolBranch_School]
GO
/****** Object:  ForeignKey [FK_StudentAttendance_PrimaryTag]    Script Date: 04/23/2016 10:27:35 ******/
ALTER TABLE [dbo].[StudentAttendance]  WITH CHECK ADD  CONSTRAINT [FK_StudentAttendance_PrimaryTag] FOREIGN KEY([PrimaryTagId])
REFERENCES [dbo].[PrimaryTag] ([Id])
GO
ALTER TABLE [dbo].[StudentAttendance] CHECK CONSTRAINT [FK_StudentAttendance_PrimaryTag]
GO
/****** Object:  ForeignKey [FK_StudentAttendance_SecondaryTag]    Script Date: 04/23/2016 10:27:35 ******/
ALTER TABLE [dbo].[StudentAttendance]  WITH CHECK ADD  CONSTRAINT [FK_StudentAttendance_SecondaryTag] FOREIGN KEY([SecondaryTagId])
REFERENCES [dbo].[SecondaryTag] ([Id])
GO
ALTER TABLE [dbo].[StudentAttendance] CHECK CONSTRAINT [FK_StudentAttendance_SecondaryTag]
GO
/****** Object:  ForeignKey [FK_StaffDetail_Class]    Script Date: 04/23/2016 10:27:35 ******/
ALTER TABLE [dbo].[StaffDetail]  WITH CHECK ADD  CONSTRAINT [FK_StaffDetail_Class] FOREIGN KEY([ClassId])
REFERENCES [dbo].[Class] ([Id])
GO
ALTER TABLE [dbo].[StaffDetail] CHECK CONSTRAINT [FK_StaffDetail_Class]
GO
/****** Object:  ForeignKey [FK_StaffDetail_PrimaryTag]    Script Date: 04/23/2016 10:27:35 ******/
ALTER TABLE [dbo].[StaffDetail]  WITH CHECK ADD  CONSTRAINT [FK_StaffDetail_PrimaryTag] FOREIGN KEY([PrimaryTagId])
REFERENCES [dbo].[PrimaryTag] ([Id])
GO
ALTER TABLE [dbo].[StaffDetail] CHECK CONSTRAINT [FK_StaffDetail_PrimaryTag]
GO
/****** Object:  ForeignKey [FK_StaffDetail_Section]    Script Date: 04/23/2016 10:27:35 ******/
ALTER TABLE [dbo].[StaffDetail]  WITH CHECK ADD  CONSTRAINT [FK_StaffDetail_Section] FOREIGN KEY([SectionId])
REFERENCES [dbo].[Section] ([Id])
GO
ALTER TABLE [dbo].[StaffDetail] CHECK CONSTRAINT [FK_StaffDetail_Section]
GO
/****** Object:  ForeignKey [FK_StudentDetail_Class1]    Script Date: 04/23/2016 10:27:35 ******/
ALTER TABLE [dbo].[StudentDetail]  WITH CHECK ADD  CONSTRAINT [FK_StudentDetail_Class1] FOREIGN KEY([ClassId])
REFERENCES [dbo].[Class] ([Id])
GO
ALTER TABLE [dbo].[StudentDetail] CHECK CONSTRAINT [FK_StudentDetail_Class1]
GO
/****** Object:  ForeignKey [FK_StudentDetail_PrimaryTag]    Script Date: 04/23/2016 10:27:35 ******/
ALTER TABLE [dbo].[StudentDetail]  WITH CHECK ADD  CONSTRAINT [FK_StudentDetail_PrimaryTag] FOREIGN KEY([PrimaryTagId])
REFERENCES [dbo].[PrimaryTag] ([Id])
GO
ALTER TABLE [dbo].[StudentDetail] CHECK CONSTRAINT [FK_StudentDetail_PrimaryTag]
GO
/****** Object:  ForeignKey [FK_StudentDetail_SchoolBranch]    Script Date: 04/23/2016 10:27:35 ******/
ALTER TABLE [dbo].[StudentDetail]  WITH CHECK ADD  CONSTRAINT [FK_StudentDetail_SchoolBranch] FOREIGN KEY([SchoolBranchId])
REFERENCES [dbo].[SchoolBranch] ([Id])
GO
ALTER TABLE [dbo].[StudentDetail] CHECK CONSTRAINT [FK_StudentDetail_SchoolBranch]
GO
/****** Object:  ForeignKey [FK_StudentDetail_SecondaryTag]    Script Date: 04/23/2016 10:27:35 ******/
ALTER TABLE [dbo].[StudentDetail]  WITH CHECK ADD  CONSTRAINT [FK_StudentDetail_SecondaryTag] FOREIGN KEY([SecondaryTagId])
REFERENCES [dbo].[SecondaryTag] ([Id])
GO
ALTER TABLE [dbo].[StudentDetail] CHECK CONSTRAINT [FK_StudentDetail_SecondaryTag]
GO
/****** Object:  ForeignKey [FK_StudentDetail_Section]    Script Date: 04/23/2016 10:27:35 ******/
ALTER TABLE [dbo].[StudentDetail]  WITH CHECK ADD  CONSTRAINT [FK_StudentDetail_Section] FOREIGN KEY([SectionId])
REFERENCES [dbo].[Section] ([Id])
GO
ALTER TABLE [dbo].[StudentDetail] CHECK CONSTRAINT [FK_StudentDetail_Section]
GO
