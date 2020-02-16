USE [master]
GO

/****** Object:  Database [ManageStocks]    Script Date: 16-02-2020 17:35:25 ******/
CREATE DATABASE [ManageStocks]
GO


USE [ManageStocks]
GO
/****** Object:  StoredProcedure [dbo].[P_UpdateUserAccountPassword]    Script Date: 16-02-2020 17:36:52 ******/
DROP PROCEDURE IF EXISTS [dbo].[P_UpdateUserAccountPassword]
GO
/****** Object:  StoredProcedure [dbo].[P_UpdateUserAccountLockedStatus]    Script Date: 16-02-2020 17:36:52 ******/
DROP PROCEDURE IF EXISTS [dbo].[P_UpdateUserAccountLockedStatus]
GO
/****** Object:  StoredProcedure [dbo].[P_UpdateUserAccountDetails]    Script Date: 16-02-2020 17:36:52 ******/
DROP PROCEDURE IF EXISTS [dbo].[P_UpdateUserAccountDetails]
GO
/****** Object:  StoredProcedure [dbo].[P_UpdateUserAccountActiveStatus]    Script Date: 16-02-2020 17:36:52 ******/
DROP PROCEDURE IF EXISTS [dbo].[P_UpdateUserAccountActiveStatus]
GO
/****** Object:  StoredProcedure [dbo].[P_SaveUserLoggingDetails]    Script Date: 16-02-2020 17:36:52 ******/
DROP PROCEDURE IF EXISTS [dbo].[P_SaveUserLoggingDetails]
GO
/****** Object:  StoredProcedure [dbo].[P_RegisterUser]    Script Date: 16-02-2020 17:36:52 ******/
DROP PROCEDURE IF EXISTS [dbo].[P_RegisterUser]
GO
/****** Object:  StoredProcedure [dbo].[P_GetUserDetailsForLoginValidation]    Script Date: 16-02-2020 17:36:52 ******/
DROP PROCEDURE IF EXISTS [dbo].[P_GetUserDetailsForLoginValidation]
GO
/****** Object:  StoredProcedure [dbo].[P_GetUserAccountDetails]    Script Date: 16-02-2020 17:36:52 ******/
DROP PROCEDURE IF EXISTS [dbo].[P_GetUserAccountDetails]
GO
/****** Object:  StoredProcedure [dbo].[P_GetAllUserAccounts]    Script Date: 16-02-2020 17:36:52 ******/
DROP PROCEDURE IF EXISTS [dbo].[P_GetAllUserAccounts]
GO
/****** Object:  StoredProcedure [dbo].[P_CreateProduct]    Script Date: 16-02-2020 17:36:52 ******/
DROP PROCEDURE IF EXISTS [dbo].[P_CreateProduct]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
ALTER TABLE [dbo].[User] DROP CONSTRAINT IF EXISTS [FK__User__UserTitleI__412EB0B6]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
ALTER TABLE [dbo].[User] DROP CONSTRAINT IF EXISTS [FK__User__UserGender__403A8C7D]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
ALTER TABLE [dbo].[User] DROP CONSTRAINT IF EXISTS [FK__User__UserGender__3F466844]
GO
/****** Object:  Table [dbo].[UserTitle]    Script Date: 16-02-2020 17:36:52 ******/
DROP TABLE IF EXISTS [dbo].[UserTitle]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 16-02-2020 17:36:52 ******/
DROP TABLE IF EXISTS [dbo].[UserRoles]
GO
/****** Object:  Table [dbo].[UserLoggingLog]    Script Date: 16-02-2020 17:36:52 ******/
DROP TABLE IF EXISTS [dbo].[UserLoggingLog]
GO
/****** Object:  Table [dbo].[UserInCorrectAuthLog]    Script Date: 16-02-2020 17:36:52 ******/
DROP TABLE IF EXISTS [dbo].[UserInCorrectAuthLog]
GO
/****** Object:  Table [dbo].[UserGender]    Script Date: 16-02-2020 17:36:52 ******/
DROP TABLE IF EXISTS [dbo].[UserGender]
GO
/****** Object:  Table [dbo].[User]    Script Date: 16-02-2020 17:36:52 ******/
DROP TABLE IF EXISTS [dbo].[User]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 16-02-2020 17:36:52 ******/
DROP TABLE IF EXISTS [dbo].[Roles]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 16-02-2020 17:36:52 ******/
DROP TABLE IF EXISTS [dbo].[Product]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 16-02-2020 17:36:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [bigint] IDENTITY(600000,1) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[ProductDescription] [nvarchar](500) NULL,
	[ProductPrice] [money] NULL,
	[ProductImage] [varbinary](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[ModifiedBy] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 16-02-2020 17:36:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(100,1) NOT NULL,
	[RoleName] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 16-02-2020 17:36:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [bigint] IDENTITY(30000,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[UserTitleId] [smallint] NULL,
	[FullName] [nvarchar](400) NULL,
	[FirstName] [nvarchar](200) NULL,
	[LastName] [nvarchar](200) NULL,
	[EmailId] [nvarchar](500) NOT NULL,
	[UserGenderId] [smallint] NULL,
	[PasswordHash] [nvarchar](500) NOT NULL,
	[PasswordSalt] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsLocked] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[ModifiedBy] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserGender]    Script Date: 16-02-2020 17:36:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGender](
	[UserGenderId] [smallint] IDENTITY(10,1) NOT NULL,
	[UserGenderDesc] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_dbo.UserGender] PRIMARY KEY CLUSTERED 
(
	[UserGenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInCorrectAuthLog]    Script Date: 16-02-2020 17:36:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInCorrectAuthLog](
	[UserInCorrectAuthLogId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[InCorrectLoggingCount] [bigint] NOT NULL,
	[LoggingIpAddress] [nvarchar](500) NOT NULL,
	[LoggingBrowser] [nvarchar](max) NOT NULL,
	[IsUserAuthenticated] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[ModifiedBy] [bigint] NULL,
 CONSTRAINT [PK_dbo.UserInCorrectAuthLog] PRIMARY KEY CLUSTERED 
(
	[UserInCorrectAuthLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLoggingLog]    Script Date: 16-02-2020 17:36:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLoggingLog](
	[UserLoggingLogId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[LoggingIpAddress] [nvarchar](500) NOT NULL,
	[LoggingBrowser] [nvarchar](max) NOT NULL,
	[IsUserAuthenticated] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[ModifiedBy] [bigint] NULL,
 CONSTRAINT [PK_dbo.UserLoggingLog] PRIMARY KEY CLUSTERED 
(
	[UserLoggingLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 16-02-2020 17:36:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserRoleId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[RoleName] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [bigint] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTitle]    Script Date: 16-02-2020 17:36:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTitle](
	[UserTitleId] [smallint] IDENTITY(50,1) NOT NULL,
	[UserTitleDesc] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_dbo.UserTitle] PRIMARY KEY CLUSTERED 
(
	[UserTitleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Product] ON 
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductDescription], [ProductPrice], [ProductImage], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (600000, N'Product', N'Product', 555.0000, NULL, 1, CAST(N'2020-02-14T22:51:03.233' AS DateTime), 0, CAST(N'2020-02-14T22:51:03.233' AS DateTime), 0)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductDescription], [ProductPrice], [ProductImage], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (600001, N'Product', N'Product', 555.0000, NULL, 1, CAST(N'2020-02-14T22:52:06.120' AS DateTime), 0, CAST(N'2020-02-14T22:52:06.120' AS DateTime), 0)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductDescription], [ProductPrice], [ProductImage], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (600002, N'Product', N'Product', 555.0000, NULL, 1, CAST(N'2020-02-14T22:55:11.467' AS DateTime), 0, CAST(N'2020-02-14T22:55:11.467' AS DateTime), 0)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductDescription], [ProductPrice], [ProductImage], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (600003, N'5555', N'55555', 55555.0000, NULL, 1, CAST(N'2020-02-14T23:00:04.007' AS DateTime), 0, CAST(N'2020-02-14T23:00:04.007' AS DateTime), 0)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductDescription], [ProductPrice], [ProductImage], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (600004, N'4444444', N'4444444', 4444444.0000, NULL, 1, CAST(N'2020-02-14T23:00:24.747' AS DateTime), 0, CAST(N'2020-02-14T23:00:24.747' AS DateTime), 0)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductDescription], [ProductPrice], [ProductImage], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (600005, N'4444444', N'4444444', 4444444.0000, NULL, 1, CAST(N'2020-02-14T23:00:32.447' AS DateTime), 0, CAST(N'2020-02-14T23:00:32.447' AS DateTime), 0)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductDescription], [ProductPrice], [ProductImage], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (600006, N'55555', N'55555', 55555.0000, NULL, 1, CAST(N'2020-02-14T23:01:36.873' AS DateTime), 0, CAST(N'2020-02-14T23:01:36.873' AS DateTime), 0)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductDescription], [ProductPrice], [ProductImage], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (600007, N'55555', N'55555', 55555.0000, NULL, 1, CAST(N'2020-02-14T23:03:21.027' AS DateTime), 0, CAST(N'2020-02-14T23:03:21.027' AS DateTime), 0)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductDescription], [ProductPrice], [ProductImage], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (600008, N'dddd', N'dddd', 4444.0000, NULL, 1, CAST(N'2020-02-15T10:09:15.150' AS DateTime), 0, CAST(N'2020-02-15T10:09:15.150' AS DateTime), 0)
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 
GO
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (100, N'BasicUser')
GO
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (101, N'Administrator')
GO
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (102, N'Manager')
GO
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (103, N'Approver')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([UserId], [UserName], [UserTitleId], [FullName], [FirstName], [LastName], [EmailId], [UserGenderId], [PasswordHash], [PasswordSalt], [IsActive], [IsLocked], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (30054, N'Register', 51, N'John Hjknm', N'John', N'Hjknm', N'Register@123.123', 11, N'oOgBNInuhhYunNjv0EOFbQcXHS69QPOIQfNP3uQTpv3hU0mpGRR2lllUundcI1GS0Ws4E6tM9S+Hf7+0Ht28Iw==', N'4rZQfHmGNc2wsHEPZbiW', 1, 0, CAST(N'2020-02-15T11:19:46.420' AS DateTime), 30054, CAST(N'2020-02-16T16:17:04.200' AS DateTime), 30054)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserGender] ON 
GO
INSERT [dbo].[UserGender] ([UserGenderId], [UserGenderDesc]) VALUES (10, N'--Select--')
GO
INSERT [dbo].[UserGender] ([UserGenderId], [UserGenderDesc]) VALUES (11, N'MALE')
GO
INSERT [dbo].[UserGender] ([UserGenderId], [UserGenderDesc]) VALUES (12, N'FEMALE')
GO
SET IDENTITY_INSERT [dbo].[UserGender] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 
GO
INSERT [dbo].[UserRoles] ([UserRoleId], [UserId], [RoleId], [RoleName], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, 30054, 100, N'BasicUser', 1, CAST(N'2020-02-15T11:19:46.420' AS DateTime), 30054, CAST(N'2020-02-15T11:19:46.420' AS DateTime), 30054)
GO
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[UserTitle] ON 
GO
INSERT [dbo].[UserTitle] ([UserTitleId], [UserTitleDesc]) VALUES (50, N'--Select--')
GO
INSERT [dbo].[UserTitle] ([UserTitleId], [UserTitleDesc]) VALUES (51, N'Mr ')
GO
INSERT [dbo].[UserTitle] ([UserTitleId], [UserTitleDesc]) VALUES (52, N'Mrs ')
GO
INSERT [dbo].[UserTitle] ([UserTitleId], [UserTitleDesc]) VALUES (53, N'Miss ')
GO
INSERT [dbo].[UserTitle] ([UserTitleId], [UserTitleDesc]) VALUES (54, N'Sir ')
GO
INSERT [dbo].[UserTitle] ([UserTitleId], [UserTitleDesc]) VALUES (55, N'Madam ')
GO
INSERT [dbo].[UserTitle] ([UserTitleId], [UserTitleDesc]) VALUES (56, N'Dr ')
GO
SET IDENTITY_INSERT [dbo].[UserTitle] OFF
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([UserGenderId])
REFERENCES [dbo].[UserGender] ([UserGenderId])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([UserGenderId])
REFERENCES [dbo].[UserGender] ([UserGenderId])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([UserTitleId])
REFERENCES [dbo].[UserTitle] ([UserTitleId])
GO
/****** Object:  StoredProcedure [dbo].[P_CreateProduct]    Script Date: 16-02-2020 17:36:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Execution : EXEC [dbo].[P_CreateProduct] 
-- =============================================
CREATE PROC [dbo].[P_CreateProduct] 
				@ProductId [bigint]	,				
				@ProductName [nvarchar](50)	,		
				@ProductDescription [nvarchar](500)	,
				@ProductPrice money	,				
				@ProductImage [varbinary](max)	,
				 @CreatedBy    [BIGINT] 		

AS
    BEGIN
        DECLARE @TodaysDate DATETIME= GETDATE();
		BEGIN TRANSACTION;
		
		INSERT INTO [dbo].[Product]
				   ([ProductName]
				   ,[ProductDescription]
				   ,[ProductPrice]
				   ,[ProductImage]
				   ,[IsActive]
				   ,[CreatedOn]
				   ,[CreatedBy]
				   ,[ModifiedOn]
				   ,[ModifiedBy])
       SELECT  
				  		
				   @ProductName ,		
				   @ProductDescription ,
				   @ProductPrice,			
				   @ProductImage ,
				   1,
				   @TodaysDate,
					@CreatedBy ,  		
					 @TodaysDate,
					@CreatedBy 

       SELECT @ProductId = SCOPE_IDENTITY()


        COMMIT TRANSACTION;

		SELECT @ProductId  AS ProductId 
END;
GO
/****** Object:  StoredProcedure [dbo].[P_GetAllUserAccounts]    Script Date: 16-02-2020 17:36:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Execution : EXEC [dbo].[P_GetAllUserAccounts] 
-- =============================================
CREATE PROC [dbo].[P_GetAllUserAccounts] 
						
AS
  BEGIN

		SELECT USR.[UserId]
			  ,USR.[UserName]
			  ,USR.[UserTitleId]
			  ,USRTTL.UserTitleDesc
			  ,USR.[FullName]
			  ,USR.[FirstName]
			  ,USR.[LastName]
			  ,USR.[EmailId]
			  ,USR.[UserGenderId] 
			  ,USRGNDR.[UserGenderDesc]   
			  ,USR.[IsActive]
			  ,USR.[IsLocked]
			  ,USR.[CreatedOn]
			  ,USR.[CreatedBy]
			  ,(SELECT TOP 1 [UserName] FROM [dbo].[User] WHERE [UserId] = USR.[CreatedBy] )	CreatedByUserName	  
			 
			  
			  ,USR.[ModifiedOn]
			  ,USR.[ModifiedBy]	
			  ,(SELECT TOP 1 [UserName] FROM [dbo].[User] WHERE [UserId] = USR.[ModifiedBy] )	CreatedByUserName	  

		  FROM [dbo].[User] USR
		  LEFT JOIN [dbo].UserGender USRGNDR
			ON USR.[UserGenderId] = USRGNDR.UserGenderId
		  LEFT JOIN [dbo].UserTitle USRTTL
			ON USR.[UserTitleId] = USRTTL.[UserTitleId]
  		
  END;
GO
/****** Object:  StoredProcedure [dbo].[P_GetUserAccountDetails]    Script Date: 16-02-2020 17:36:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Execution : EXEC [dbo].[P_GetUserAccountDetails]  30043
-- =============================================
CREATE PROC [dbo].[P_GetUserAccountDetails] 
	 @UserId   BIGINT   					
AS
  BEGIN

		SELECT USR.[UserId]
			  ,USR.[UserName]
			  ,USR.[UserTitleId]
			  ,USRTTL.UserTitleDesc
			  ,USR.[FullName]
			  ,USR.[FirstName]
			  ,USR.[LastName]
			  ,USR.[EmailId]
			  ,USR.[UserGenderId] 
			  ,USRGNDR.[UserGenderDesc]   
			  ,USR.[IsActive]
			  ,USR.[IsLocked]
			  ,USR.[CreatedOn]
			  ,USR.[CreatedBy]
			  ,USRCreatedBy.[UserName] AS CreatedByUserName
			  ,USR.[ModifiedOn]
			  ,USR.[ModifiedBy]	
			  ,USRModifiedBy.[UserName] AS ModifiedByUserName
		  FROM [dbo].[User] USR
		  LEFT JOIN [dbo].UserGender USRGNDR
			ON USR.[UserGenderId] = USRGNDR.UserGenderId
		  LEFT JOIN [dbo].UserTitle USRTTL
			ON USR.[UserTitleId] = USRTTL.[UserTitleId]
  		  LEFT JOIN [dbo].[User] USRCreatedBy
			ON USRCreatedBy.[CreatedBy] = USR.[UserId]
 		  LEFT JOIN [dbo].[User] USRModifiedBy
			ON USRModifiedBy.ModifiedBy = USR.[UserId]
		  WHERE USR.UserId = @UserId

 
   SELECT
		  USRRLS.UserRoleId
		,USRRLS.UserId
       ,RLS.[RoleId]
      ,RLS.[RoleName]
	  ,USRRLS.IsActive
	  ,USRRLS.CreatedOn
		,USRRLS.CreatedBy
	,(SELECT TOP 1 [UserName] FROM [dbo].[User] WHERE [UserId] = USRRLS.[CreatedBy])	
  		,USRRLS.ModifiedOn
		,USRRLS.ModifiedBy
	,(SELECT TOP 1 [UserName] FROM [dbo].[User] WHERE [UserId] = USRRLS.[ModifiedBy])	ModifiedByUserName	
		FROM [dbo].Roles RLS
		INNER JOIN  [dbo].[UserRoles] USRRLS
		ON RLS.RoleId = USRRLS.RoleId
		WHERE [UserId] = 30054  AND USRRLS.IsActive = 1
  UNION ALL
  SELECT 0
		,0
		, RLS.[RoleId]
      ,RLS.[RoleName]
	  ,0
	  ,NULL
	  ,NULL
	  ,NULL
	  ,NULL
	  ,NULL
	  ,NULL
  FROM [dbo].Roles RLS
  WHERE  RLS.[RoleId] NOT IN (SELECT [RoleId] FROM [dbo].[UserRoles] WHERE [UserId] = 30054  AND IsActive = 1)

		SELECT    USRInCorrectAuthLog.[UserInCorrectAuthLogId]
			  ,USRInCorrectAuthLog.[UserId]
			  ,USRInCorrectAuthLog.[UserName]
			  ,USRInCorrectAuthLog.[InCorrectLoggingCount]
			  ,USRInCorrectAuthLog.[LoggingIpAddress]
			  ,USRInCorrectAuthLog.[LoggingBrowser]
			  ,USRInCorrectAuthLog.[IsUserAuthenticated]
			  ,USRInCorrectAuthLog.[IsActive]
			  ,USRInCorrectAuthLog.[CreatedOn]
			  ,USRInCorrectAuthLog.[CreatedBy]	
			   ,(SELECT TOP 1 [UserName] FROM [dbo].[User] WHERE [UserId] = USRInCorrectAuthLog.[CreatedBy])	CreatedByUserName	  
			  ,USRInCorrectAuthLog.[ModifiedOn]
			  ,USRInCorrectAuthLog.[ModifiedBy]
			  ,(SELECT TOP 1 [UserName] FROM [dbo].[User] WHERE [UserId] = USRInCorrectAuthLog.[ModifiedBy])	ModifiedByUserName			
		  FROM [dbo].[UserInCorrectAuthLog] USRInCorrectAuthLog
	
		  WHERE USRInCorrectAuthLog.UserId = @UserId
		  ORDER BY 1 DESC

	SELECT TOP (1000)  USRLoggingLog.[UserLoggingLogId]
					  ,USRLoggingLog.[UserId]
					  ,USRLoggingLog.[UserName]
					  ,USRLoggingLog.[LoggingIpAddress]
					  ,USRLoggingLog.[LoggingBrowser]
					  ,USRLoggingLog.[IsUserAuthenticated]
					  ,USRLoggingLog.[CreatedOn]
					  ,USRLoggingLog.[CreatedBy]
					  ,(SELECT TOP 1 [UserName] FROM [dbo].[User] WHERE [UserId] = USRLoggingLog.[CreatedBy])	CreatedByUserName				  				
					  ,USRLoggingLog.[ModifiedOn]
					  ,USRLoggingLog.[ModifiedBy]
					  ,(SELECT TOP 1 [UserName] FROM [dbo].[User] WHERE [UserId] = USRLoggingLog.ModifiedBy) AS ModifiedByUserName
	  FROM [dbo].[UserLoggingLog] USRLoggingLog	  	
	  WHERE USRLoggingLog.UserId = @UserId
	  ORDER BY 1 DESC

 
  END;
GO
/****** Object:  StoredProcedure [dbo].[P_GetUserDetailsForLoginValidation]    Script Date: 16-02-2020 17:36:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Execution : EXEC [dbo].[P_GetUserDetailsForLoginValidation]  'Register'
-- =============================================
CREATE PROC [dbo].[P_GetUserDetailsForLoginValidation] 
                         @UserName     [NVARCHAR](MAX)
                                 
AS
    BEGIN
	  SELECT TOP 1 [UserId]
		  ,[UserName]      
		  ,[PasswordHash]
		  ,[PasswordSalt]
		  ,[IsActive]
		  ,[IsLocked]     
	  FROM [dbo].[User]
	  WHERE [UserName] = @UserName

	  DECLARE @UserId bigint

	  SELECT TOP 1 @UserId = [UserId]		 
	  FROM [dbo].[User]
	  WHERE [UserName] = @UserName

	SELECT [UserRoleId]
      ,[UserId]
      ,[RoleId]
      ,[RoleName]
      ,[IsActive]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
  FROM [dbo].[UserRoles]
	WHERE [UserId] = @UserId AND [IsActive] = 1
END;
GO
/****** Object:  StoredProcedure [dbo].[P_RegisterUser]    Script Date: 16-02-2020 17:36:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROC [dbo].[P_RegisterUser] 

                                  @UserName     [NVARCHAR](MAX),                                  
                                  @EmailId        [NVARCHAR](MAX),                                 
                                  @PasswordHash [NVARCHAR](500), 
                                  @PasswordSalt [NVARCHAR](100), 
                                  @CreatedBy    [BIGINT]
AS
    BEGIN
        DECLARE @TodaysDate DATETIME= GETDATE();

        IF @CreatedBy = 0 OR @CreatedBy = NULL
            BEGIN
                SELECT @CreatedBy = 1;
        END;
        BEGIN TRANSACTION;

			INSERT INTO [dbo].[User]
					   ([UserName]           
					   ,[EmailId]           
					   ,[PasswordHash]
					   ,[PasswordSalt]
					   ,[IsActive]
					   ,[IsLocked]
					   ,[CreatedOn]
					   ,[CreatedBy]
					   ,[ModifiedOn]
					   ,[ModifiedBy])
				SELECT @UserName
						, @EmailId 
						,@PasswordHash
						,@PasswordSalt
						,1
						,0
                      ,@TodaysDate
                      ,@CreatedBy
                      ,@TodaysDate
                      ,@CreatedBy

        DECLARE @UserId BIGINT= SCOPE_IDENTITY()

		
		INSERT INTO [dbo].[UserRoles]
				   ([UserId]
				   ,[RoleId]
				   ,[RoleName]
				   ,[IsActive]
				   ,[CreatedOn]
				   ,[CreatedBy]
				   ,[ModifiedOn]
				   ,[ModifiedBy])
		SELECT			@UserId
						, 100 
						,'BasicUser'						
						,1						
                      ,@TodaysDate
                      ,@UserId
                      ,@TodaysDate
                      ,@UserId

        COMMIT TRANSACTION;


UPDATE [dbo].[User]
   SET [CreatedBy] = @UserId  
      ,[ModifiedBy] = @UserId
 WHERE [UserId] = @UserId
 		     
	   SELECT  [UserId]
			  ,[UserName]      
		FROM [dbo].[User]
		WHERE [UserId] = @UserId

END;
GO
/****** Object:  StoredProcedure [dbo].[P_SaveUserLoggingDetails]    Script Date: 16-02-2020 17:36:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROC [dbo].[P_SaveUserLoggingDetails] 
							@UserId     [BIGINT]
                        , @UserName     [NVARCHAR](MAX)
						  , @LoggingIpAddress    [NVARCHAR](MAX)
						   ,  @LoggingBrowser     [NVARCHAR](MAX)
							,   @IsUserAuthenticated    BIT
							,@IsInCorrectLogging BIT
                             ,@CreatedOn   DATETIME 
AS
    BEGIN
	

	INSERT INTO [dbo].[UserLoggingLog]
           ([UserId]
           ,[UserName]
           ,[LoggingIpAddress]
           ,[LoggingBrowser]
           ,[IsUserAuthenticated]
           ,[CreatedOn]
           ,[CreatedBy]
           ,[ModifiedOn]
           ,[ModifiedBy])
		SELECT 
				@UserId
				,@UserName
				,@LoggingIpAddress
				,@LoggingBrowser
				,@IsUserAuthenticated
                ,@CreatedOn
                ,@UserId    
                ,@CreatedOn
                ,@UserId 
	
	DECLARE @InCorrectLoggingCount [BIGINT]
	DECLARE @IsUserAccountLocked BIT

	SELECT @IsUserAccountLocked  = IsLocked
	FROM  [dbo].[User] 
	WHERE [UserId] = @UserId 

	SELECT @InCorrectLoggingCount  = [InCorrectLoggingCount]
	FROM  [dbo].[UserInCorrectAuthLog] 
	WHERE [UserId] = @UserId AND [IsActive] = 1

	IF @InCorrectLoggingCount = 2
		BEGIN
			UPDATE [dbo].[User]
		   SET 
			  IsLocked  = 1  
			  ,[ModifiedOn] = @CreatedOn
			  ,[ModifiedBy] = @UserId
			WHERE [UserId] = @UserId
		END

	IF @IsInCorrectLogging = 1 AND @UserId > 0 AND @InCorrectLoggingCount > 0 AND @IsUserAccountLocked <> 1
	BEGIN	
			UPDATE [dbo].[UserInCorrectAuthLog]
			   SET 
				   [InCorrectLoggingCount] = @InCorrectLoggingCount + 1   
				  ,[ModifiedOn] = @CreatedOn
				  ,[ModifiedBy] = @UserId
			 WHERE [UserId] = @UserId AND [IsActive] = 1
		END
		
	ELSE IF @IsInCorrectLogging = 1 AND @UserId > 0  AND @IsUserAccountLocked <> 1
		BEGIN
			INSERT INTO [dbo].[UserInCorrectAuthLog]
				   ([UserId]
				   ,[UserName]
				   ,[InCorrectLoggingCount]
				   ,[LoggingIpAddress]
				   ,[LoggingBrowser]
				   ,[IsUserAuthenticated]
				   ,[IsActive]
				   ,[CreatedOn]
				   ,[CreatedBy]
				   ,[ModifiedOn]
				   ,[ModifiedBy])
			SELECT 
					@UserId
					,@UserName
					,1
					,@LoggingIpAddress
					,@LoggingBrowser
					,@IsUserAuthenticated
					,1
					,@CreatedOn
					,@UserId    
					,@CreatedOn
					,@UserId 
		END	
	ELSE IF @IsInCorrectLogging = 0 AND @UserId > 0 AND @IsUserAccountLocked <> 1
	BEGIN

		UPDATE [dbo].[UserInCorrectAuthLog]
			   SET 
				   [IsUserAuthenticated] = 1
				  ,[IsActive] = 0 
				  ,[ModifiedOn] = @CreatedOn
				  ,[ModifiedBy] = @UserId
			 WHERE [UserId] = @UserId AND [IsActive] = 1
		
	END

  END;
GO
/****** Object:  StoredProcedure [dbo].[P_UpdateUserAccountActiveStatus]    Script Date: 16-02-2020 17:36:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Execution : EXEC [dbo].[P_GetAllUserAccounts] 
-- =============================================
CREATE PROC [dbo].[P_UpdateUserAccountActiveStatus]
								  @UserId       [BIGINT],   
								  @IsActive   BIT,                            							
                                  @ModifiedBy    [BIGINT] 
						
AS
  BEGIN

	DECLARE @TodaysDate DATETIME= GETDATE();


UPDATE [dbo].[User]
   SET 
       IsActive = @IsActive         
      ,[ModifiedOn] = @TodaysDate
      ,[ModifiedBy] = @ModifiedBy
 WHERE UserId = @UserId

		 SELECT CAST(1 AS BIT) Success

  END;
GO
/****** Object:  StoredProcedure [dbo].[P_UpdateUserAccountDetails]    Script Date: 16-02-2020 17:36:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Execution : EXEC [dbo].[P_SaveUserAccountDetails]
-- =============================================
CREATE PROC [dbo].[P_UpdateUserAccountDetails]
								  @UserId       [BIGINT], 
                                  @UserName     [NVARCHAR](MAX), 
								  @UserTitleId	[smallint],
                                  @FirstName    [NVARCHAR](MAX),
                                  @LastName     [NVARCHAR](MAX), 
								  @FullName		 [NVARCHAR](MAX),
                                  @EmailId        [NVARCHAR](MAX),
								  @UserGenderId		[smallint],								
                                  @ModifiedBy    [BIGINT]
						
AS
  BEGIN

		DECLARE @TodaysDate DATETIME= GETDATE();


		UPDATE [dbo].[User]
		   SET 
			   [UserTitleId] = @UserTitleId
			  ,[FullName] = @FullName
			  ,[FirstName] = @FirstName
			  ,[LastName] = @LastName
			  ,[EmailId] = @EmailId
			  ,[UserGenderId] = @UserGenderId      
			  ,[ModifiedOn] = @TodaysDate
			  ,[ModifiedBy] = @ModifiedBy
		 WHERE UserId = @UserId

		 SELECT CAST(1 AS BIT) Success
  END;
GO
/****** Object:  StoredProcedure [dbo].[P_UpdateUserAccountLockedStatus]    Script Date: 16-02-2020 17:36:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Execution : [dbo].[P_UpdateUserAccountLockedStatus]
-- =============================================
CREATE PROC [dbo].[P_UpdateUserAccountLockedStatus]
								  @UserId       [BIGINT],   
								  @IsLocked   BIT,                            							
                                  @ModifiedBy    [BIGINT] 
						
AS
  BEGIN

	DECLARE @TodaysDate DATETIME= GETDATE();


UPDATE [dbo].[User]
   SET 
       IsLocked = @IsLocked       
      ,[ModifiedOn] = @TodaysDate
      ,[ModifiedBy] = @ModifiedBy
 WHERE UserId = @UserId

 
UPDATE [dbo].[UserInCorrectAuthLog]
   SET 
       [IsActive] = 0     
      ,[ModifiedOn] = @TodaysDate
      ,[ModifiedBy] = @ModifiedBy
 WHERE  UserId = @UserId AND [IsActive] = 1

		 SELECT CAST(1 AS BIT) Success

  END;
GO
/****** Object:  StoredProcedure [dbo].[P_UpdateUserAccountPassword]    Script Date: 16-02-2020 17:36:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Execution : EXEC [dbo].[P_UpdateUserAccountPassword]
-- =============================================
CREATE PROC [dbo].[P_UpdateUserAccountPassword]
								  @UserId       [BIGINT], 
								  @PasswordHash [NVARCHAR](500), 
                                  @PasswordSalt [NVARCHAR](100),							
                                  @ModifiedBy    [BIGINT]
						
AS
  BEGIN

		DECLARE @TodaysDate DATETIME= GETDATE();


		UPDATE [dbo].[User]
		   SET 
			    PasswordHash = @PasswordHash
			  ,PasswordSalt = @PasswordSalt
			  ,[ModifiedOn] = @TodaysDate
			  ,[ModifiedBy] = @ModifiedBy
		 WHERE UserId = @UserId

		 SELECT CAST(1 AS BIT) Success
  END;
GO
