USE [master]
GO

/****** Object:  Database [EmpManage]    Script Date: 14-02-2020 13:23:24 ******/
CREATE DATABASE [EmpManage]

GO

USE [EmpManage]
GO
/****** Object:  StoredProcedure [dbo].[P_UpdateUserAccountPassword]    Script Date: 14-02-2020 13:21:34 ******/
DROP PROCEDURE IF EXISTS [dbo].[P_UpdateUserAccountPassword]
GO
/****** Object:  StoredProcedure [dbo].[P_UpdateUserAccountLockedStatus]    Script Date: 14-02-2020 13:21:34 ******/
DROP PROCEDURE IF EXISTS [dbo].[P_UpdateUserAccountLockedStatus]
GO
/****** Object:  StoredProcedure [dbo].[P_UpdateUserAccountDetails]    Script Date: 14-02-2020 13:21:34 ******/
DROP PROCEDURE IF EXISTS [dbo].[P_UpdateUserAccountDetails]
GO
/****** Object:  StoredProcedure [dbo].[P_UpdateUserAccountActiveStatus]    Script Date: 14-02-2020 13:21:34 ******/
DROP PROCEDURE IF EXISTS [dbo].[P_UpdateUserAccountActiveStatus]
GO
/****** Object:  StoredProcedure [dbo].[P_SaveUserLoggingDetails]    Script Date: 14-02-2020 13:21:34 ******/
DROP PROCEDURE IF EXISTS [dbo].[P_SaveUserLoggingDetails]
GO
/****** Object:  StoredProcedure [dbo].[P_RegisterUser]    Script Date: 14-02-2020 13:21:34 ******/
DROP PROCEDURE IF EXISTS [dbo].[P_RegisterUser]
GO
/****** Object:  StoredProcedure [dbo].[P_GetUserDetailsForLoginValidation]    Script Date: 14-02-2020 13:21:34 ******/
DROP PROCEDURE IF EXISTS [dbo].[P_GetUserDetailsForLoginValidation]
GO
/****** Object:  StoredProcedure [dbo].[P_GetUserAccountDetails]    Script Date: 14-02-2020 13:21:34 ******/
DROP PROCEDURE IF EXISTS [dbo].[P_GetUserAccountDetails]
GO
/****** Object:  StoredProcedure [dbo].[P_GetAllUserAccounts]    Script Date: 14-02-2020 13:21:34 ******/
DROP PROCEDURE IF EXISTS [dbo].[P_GetAllUserAccounts]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
ALTER TABLE [dbo].[User] DROP CONSTRAINT IF EXISTS [FK__User__UserTitleI__3D2915A8]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
ALTER TABLE [dbo].[User] DROP CONSTRAINT IF EXISTS [FK__User__UserGender__60A75C0F]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
ALTER TABLE [dbo].[User] DROP CONSTRAINT IF EXISTS [FK__User__UserGender__5FB337D6]
GO
/****** Object:  Table [dbo].[UserTitle]    Script Date: 14-02-2020 13:21:34 ******/
DROP TABLE IF EXISTS [dbo].[UserTitle]
GO
/****** Object:  Table [dbo].[UserLoggingLog]    Script Date: 14-02-2020 13:21:34 ******/
DROP TABLE IF EXISTS [dbo].[UserLoggingLog]
GO
/****** Object:  Table [dbo].[UserInCorrectAuthLog]    Script Date: 14-02-2020 13:21:34 ******/
DROP TABLE IF EXISTS [dbo].[UserInCorrectAuthLog]
GO
/****** Object:  Table [dbo].[UserGender]    Script Date: 14-02-2020 13:21:34 ******/
DROP TABLE IF EXISTS [dbo].[UserGender]
GO
/****** Object:  Table [dbo].[User]    Script Date: 14-02-2020 13:21:34 ******/
DROP TABLE IF EXISTS [dbo].[User]
GO
/****** Object:  Table [dbo].[User]    Script Date: 14-02-2020 13:21:34 ******/
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
/****** Object:  Table [dbo].[UserGender]    Script Date: 14-02-2020 13:21:34 ******/
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
/****** Object:  Table [dbo].[UserInCorrectAuthLog]    Script Date: 14-02-2020 13:21:34 ******/
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
/****** Object:  Table [dbo].[UserLoggingLog]    Script Date: 14-02-2020 13:21:34 ******/
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
/****** Object:  Table [dbo].[UserTitle]    Script Date: 14-02-2020 13:21:34 ******/
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
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([UserId], [UserName], [UserTitleId], [FullName], [FirstName], [LastName], [EmailId], [UserGenderId], [PasswordHash], [PasswordSalt], [IsActive], [IsLocked], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (30038, N'Register', 55, N'111111111111 222222222222', N'111111111111', N'222222222222', N'Register@123.123', 12, N'HOyT8wjyKoTC5vpE/idePn8JjHIdY4Wo526E4nTw0m7+P6DXOGMJzdlMlSb8E43aY5WLRS1YhrayYM1V2a61fQ==', N'kEcEvgYP7WIF1NM25SHL', 1, 0, CAST(N'2020-02-02T21:42:10.027' AS DateTime), 1, CAST(N'2020-02-08T18:55:53.250' AS DateTime), 30042)
GO
INSERT [dbo].[User] ([UserId], [UserName], [UserTitleId], [FullName], [FirstName], [LastName], [EmailId], [UserGenderId], [PasswordHash], [PasswordSalt], [IsActive], [IsLocked], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (30039, N'Register1', 55, N'TEST LAST', N'TEST', N'LAST', N'Register@123.123', 11, N'LL0sIYsRQGESKGgIz80cvqkiPv/sZn9vflPfCcLpl8dYf0Cvm61Q2tYdj/2fvlZpy9YXpnUBOXAQddXt4PnuVg==', N'Ro4O9vlB3LH1UCfGbUMM', 1, 0, CAST(N'2020-02-02T21:42:49.550' AS DateTime), 1, CAST(N'2020-02-06T16:20:01.307' AS DateTime), 0)
GO
INSERT [dbo].[User] ([UserId], [UserName], [UserTitleId], [FullName], [FirstName], [LastName], [EmailId], [UserGenderId], [PasswordHash], [PasswordSalt], [IsActive], [IsLocked], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (30040, N'questions', NULL, NULL, NULL, NULL, N'Register@123.123', NULL, N'Pznl50vQvi5rBFTJsZAlSULYVxHu2V1HvpHItg9QZTfEsER/VVBaWwJ1TMAb3Pr+s13BjCZuD00piX4HlgzgCQ==', N'hlxLCt1mX4pE+KulLX9d', 1, 0, CAST(N'2020-02-02T21:46:17.797' AS DateTime), 1, CAST(N'2020-02-02T21:46:17.797' AS DateTime), 1)
GO
INSERT [dbo].[User] ([UserId], [UserName], [UserTitleId], [FullName], [FirstName], [LastName], [EmailId], [UserGenderId], [PasswordHash], [PasswordSalt], [IsActive], [IsLocked], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (30041, N'questions1', NULL, NULL, NULL, NULL, N'Register@123.1231', NULL, N'fBIDvJ/aoUtHoAqQSENgcXMKrKkq70YW4ZXiw/ACvx/hCHX5nMdiX89/Nr/thoq6JR7JHQ5dA+WoZk4bRGb0NQ==', N'DVNgUhFRXs/h4rkBdej7', 1, 0, CAST(N'2020-02-02T21:51:28.050' AS DateTime), 1, CAST(N'2020-02-02T21:51:28.050' AS DateTime), 1)
GO
INSERT [dbo].[User] ([UserId], [UserName], [UserTitleId], [FullName], [FirstName], [LastName], [EmailId], [UserGenderId], [PasswordHash], [PasswordSalt], [IsActive], [IsLocked], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (30042, N'localhost', 55, N'localhost localhost', N'localhost', N'localhost', N'localhost@localhost.00', 11, N'rCMFbxvoX4dif/hfCTw0qWM+gidKsqo+GyLBzIyqGpw/vN4H3sj2xgeqt8HZGktLuOCbEn02qg6tQeA91LwjsQ==', N'oZKXH/nn/+FvCJRcbvEO', 1, 0, CAST(N'2020-02-02T23:35:08.240' AS DateTime), 1, CAST(N'2020-02-06T23:08:27.343' AS DateTime), 30042)
GO
INSERT [dbo].[User] ([UserId], [UserName], [UserTitleId], [FullName], [FirstName], [LastName], [EmailId], [UserGenderId], [PasswordHash], [PasswordSalt], [IsActive], [IsLocked], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (30043, N'Register123', NULL, NULL, NULL, NULL, N'Register@123.123ddd', NULL, N'9XWy7FGkHEk/1+tSIF1DlNjXaYJ8GokougovvSqvuAgqD5tCHwqgNG2Nyu2knidDTiG3Ax733lTEAJLNuxwD7w==', N'FLqAHd719JhzgPYCW+PU', 1, 1, CAST(N'2020-02-02T23:36:46.590' AS DateTime), 1, CAST(N'2020-02-03T00:08:24.617' AS DateTime), 30043)
GO
INSERT [dbo].[User] ([UserId], [UserName], [UserTitleId], [FullName], [FirstName], [LastName], [EmailId], [UserGenderId], [PasswordHash], [PasswordSalt], [IsActive], [IsLocked], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (30044, N'blockquote ', NULL, NULL, NULL, NULL, N'blockquote@4556.1212', NULL, N'QHc4ASHC3mdULkxVtR6xAoIj4FNY+oz944SOgYD8tLfeQJSHtZbsl5W1et5vXxArKNAnYLDYlKw+qPH5i5ScFA==', N'HMbWobaZivPZ29nUd9gL', 1, 0, CAST(N'2020-02-03T00:14:27.457' AS DateTime), 30044, CAST(N'2020-02-03T00:14:27.457' AS DateTime), 30044)
GO
INSERT [dbo].[User] ([UserId], [UserName], [UserTitleId], [FullName], [FirstName], [LastName], [EmailId], [UserGenderId], [PasswordHash], [PasswordSalt], [IsActive], [IsLocked], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (30045, N'confirmButtonText', NULL, NULL, NULL, NULL, N'Register@123.123confirmButtonText', NULL, N'Xgfgby8JK254FoBXF50rMi1yiObZHre0c8PFnneDlM13sO7R/i3yznbKAhjoE+Wvco7k2lmmOKdE4zUQP6NJXw==', N'WI19t6gS2xWvZ6vY+SJw', 1, 0, CAST(N'2020-02-03T00:15:21.320' AS DateTime), 30045, CAST(N'2020-02-03T00:15:21.320' AS DateTime), 30045)
GO
INSERT [dbo].[User] ([UserId], [UserName], [UserTitleId], [FullName], [FirstName], [LastName], [EmailId], [UserGenderId], [PasswordHash], [PasswordSalt], [IsActive], [IsLocked], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (30046, N'confirmButtonColor', NULL, NULL, NULL, NULL, N'confirmButtonColor.#121@23123.112', NULL, N'FsRAAiQdKBMUVYQZccRljDImy7eXYPycl4z3urh2YF+hKJcJUaZf55kPfQEUofoj56/vlDzqstfIDpPXTZtyZA==', N'zTLaw1tdGDl8fEV57aMK', 1, 0, CAST(N'2020-02-03T00:15:56.050' AS DateTime), 30046, CAST(N'2020-02-03T00:15:56.050' AS DateTime), 30046)
GO
INSERT [dbo].[User] ([UserId], [UserName], [UserTitleId], [FullName], [FirstName], [LastName], [EmailId], [UserGenderId], [PasswordHash], [PasswordSalt], [IsActive], [IsLocked], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (30047, N'responded ', NULL, NULL, NULL, NULL, N'Register@123.789', NULL, N'Z1dlOr4UWGw2SjSZHir6y4iaBSXa5dYnXgnsnnma3i6Y0k3JzoyP3RMVcpg2dX3DdPEPzdlbV8YkLb0t/4ZQdQ==', N'L9qooyu7N7kwFOjcdxRN', 1, 0, CAST(N'2020-02-03T12:27:15.007' AS DateTime), 30047, CAST(N'2020-02-03T12:27:15.007' AS DateTime), 30047)
GO
INSERT [dbo].[User] ([UserId], [UserName], [UserTitleId], [FullName], [FirstName], [LastName], [EmailId], [UserGenderId], [PasswordHash], [PasswordSalt], [IsActive], [IsLocked], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (30048, N'attributes', NULL, NULL, NULL, NULL, N'Register@123.123attributes', NULL, N'Brcmre+xlut4oDedsjmO1GE7fsvwP5YEmB/HoZia1ruflN/1F0DUJcM6CyVqhGN024mFOhZxipNwRbnzRUlX4A==', N'SaRESifv6+PAkstZX+IG', 1, 0, CAST(N'2020-02-03T14:03:39.447' AS DateTime), 30048, CAST(N'2020-02-03T14:03:39.447' AS DateTime), 30048)
GO
INSERT [dbo].[User] ([UserId], [UserName], [UserTitleId], [FullName], [FirstName], [LastName], [EmailId], [UserGenderId], [PasswordHash], [PasswordSalt], [IsActive], [IsLocked], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (30049, N'blockquoteyy', NULL, NULL, NULL, NULL, N'blockquoteyy@ff.mm', NULL, N'EC0lC3O8oHKG7HeDEmFa7QfPrVu2W4ZmMNpRBLzI5/IQCy8gvzKaVxuVy48++5dDUZIz72WnXxYwHcVx2UXy4g==', N'UCkskrE1GkppXFr6/D4g', 1, 0, CAST(N'2020-02-04T22:03:47.403' AS DateTime), 30049, CAST(N'2020-02-04T22:03:47.403' AS DateTime), 30049)
GO
INSERT [dbo].[User] ([UserId], [UserName], [UserTitleId], [FullName], [FirstName], [LastName], [EmailId], [UserGenderId], [PasswordHash], [PasswordSalt], [IsActive], [IsLocked], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (30050, N'Register1111', NULL, NULL, NULL, NULL, N'Register@123.123ww', NULL, N'/tyZd9/seN3EXZvJiNeg2W4w41diYcsYxDIbsLY3VZAXfXZRkkW3TfPjn8W1DmSRIWp/6jqj/3GyHS+/xpVxpg==', N'ljThuAdJfjEvgg3W2fM+', 1, 1, CAST(N'2020-02-06T12:19:02.850' AS DateTime), 30050, CAST(N'2020-02-06T16:21:19.633' AS DateTime), 30042)
GO
INSERT [dbo].[User] ([UserId], [UserName], [UserTitleId], [FullName], [FirstName], [LastName], [EmailId], [UserGenderId], [PasswordHash], [PasswordSalt], [IsActive], [IsLocked], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (30051, N'successfully', 56, N'KEERTHI Prtg', N'KEERTHI', N'Prtg', N'successfully@successfully.successfully', 11, N'7MXnAnetdyiHUqGlY1HSjuJdLrSR/QCKTc2m2PIhfw9ZXDiq8aBGI97W3C0SaGHpmgIAdnutU9XeaUbceMvX1w==', N'8NYHE4N/W4DT+LUpLQa+', 1, 0, CAST(N'2020-02-06T16:27:48.473' AS DateTime), 30051, CAST(N'2020-02-06T23:11:31.087' AS DateTime), 30051)
GO
INSERT [dbo].[User] ([UserId], [UserName], [UserTitleId], [FullName], [FirstName], [LastName], [EmailId], [UserGenderId], [PasswordHash], [PasswordSalt], [IsActive], [IsLocked], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (30052, N'Redirecting', NULL, NULL, NULL, NULL, N'Redirecting@Redirecting.Redirecting', NULL, N'rSG/e+IOnCY1fmi8vrTE923fFNKcoxj4R9CQWlUIhwlqF5F/peVf/YprCPMyaZ5C3jUguE7P0iIsJQW+4VEVFg==', N'xwIoioMPKvlZG0mk4Imd', 1, 0, CAST(N'2020-02-06T16:47:32.617' AS DateTime), 30052, CAST(N'2020-02-06T16:47:32.617' AS DateTime), 30052)
GO
INSERT [dbo].[User] ([UserId], [UserName], [UserTitleId], [FullName], [FirstName], [LastName], [EmailId], [UserGenderId], [PasswordHash], [PasswordSalt], [IsActive], [IsLocked], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (30053, N'redirectToHomePage', NULL, NULL, NULL, NULL, N'redirectToHomePage@DD.CC', NULL, N'JDeKqJd8gM8VEhC7SSPbQSe2WZbNPcpIy14PVrlEYg66tQi6u1nsqT7fr5Ra72/avtdzFZjL9PMxPAb7O09yYA==', N'eP4qYA7BaZmcPbT8QMDL', 1, 0, CAST(N'2020-02-06T16:48:25.120' AS DateTime), 30053, CAST(N'2020-02-06T16:48:25.120' AS DateTime), 30053)
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
SET IDENTITY_INSERT [dbo].[UserInCorrectAuthLog] ON 
GO
INSERT [dbo].[UserInCorrectAuthLog] ([UserInCorrectAuthLogId], [UserId], [UserName], [InCorrectLoggingCount], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (14, 30043, N'Register123', 2, N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, 0, CAST(N'2020-02-03T00:06:19.383' AS DateTime), 30043, CAST(N'2020-02-03T00:06:24.450' AS DateTime), 30043)
GO
INSERT [dbo].[UserInCorrectAuthLog] ([UserInCorrectAuthLogId], [UserId], [UserName], [InCorrectLoggingCount], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (15, 30043, N'Register123', 1, N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, 0, CAST(N'2020-02-03T00:07:53.870' AS DateTime), 30043, CAST(N'2020-02-03T00:08:03.363' AS DateTime), 30043)
GO
INSERT [dbo].[UserInCorrectAuthLog] ([UserInCorrectAuthLogId], [UserId], [UserName], [InCorrectLoggingCount], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (16, 30043, N'Register123', 3, N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, 1, CAST(N'2020-02-03T00:08:14.067' AS DateTime), 30043, CAST(N'2020-02-03T00:08:24.617' AS DateTime), 30043)
GO
INSERT [dbo].[UserInCorrectAuthLog] ([UserInCorrectAuthLogId], [UserId], [UserName], [InCorrectLoggingCount], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (17, 30042, N'localhost', 1, N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, 0, CAST(N'2020-02-03T21:53:29.263' AS DateTime), 30042, CAST(N'2020-02-03T21:53:44.830' AS DateTime), 30042)
GO
INSERT [dbo].[UserInCorrectAuthLog] ([UserInCorrectAuthLogId], [UserId], [UserName], [InCorrectLoggingCount], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (18, 30038, N'Register', 3, N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, 0, CAST(N'2020-02-05T10:28:03.187' AS DateTime), 30038, CAST(N'2020-02-05T18:41:10.987' AS DateTime), 30038)
GO
INSERT [dbo].[UserInCorrectAuthLog] ([UserInCorrectAuthLogId], [UserId], [UserName], [InCorrectLoggingCount], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (19, 30038, N'Register', 3, N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, 0, CAST(N'2020-02-05T18:48:41.627' AS DateTime), 30038, CAST(N'2020-02-06T10:59:40.027' AS DateTime), 30042)
GO
INSERT [dbo].[UserInCorrectAuthLog] ([UserInCorrectAuthLogId], [UserId], [UserName], [InCorrectLoggingCount], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (20, 30042, N'localhost', 2, N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, 0, CAST(N'2020-02-06T14:41:06.813' AS DateTime), 30042, CAST(N'2020-02-06T14:41:12.153' AS DateTime), 30042)
GO
INSERT [dbo].[UserInCorrectAuthLog] ([UserInCorrectAuthLogId], [UserId], [UserName], [InCorrectLoggingCount], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (21, 30039, N'Register1', 1, N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, 0, CAST(N'2020-02-06T16:16:15.207' AS DateTime), 30039, CAST(N'2020-02-06T16:16:18.517' AS DateTime), 30039)
GO
SET IDENTITY_INSERT [dbo].[UserInCorrectAuthLog] OFF
GO
SET IDENTITY_INSERT [dbo].[UserLoggingLog] ON 
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (92, 30035, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T19:26:57.970' AS DateTime), 30035, CAST(N'2020-02-02T19:26:57.970' AS DateTime), 30035)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (93, 30035, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T19:27:00.627' AS DateTime), 30035, CAST(N'2020-02-02T19:27:00.627' AS DateTime), 30035)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (94, 30035, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T19:27:02.540' AS DateTime), 30035, CAST(N'2020-02-02T19:27:02.540' AS DateTime), 30035)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (95, 30035, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T19:27:04.420' AS DateTime), 30035, CAST(N'2020-02-02T19:27:04.420' AS DateTime), 30035)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (96, 30035, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T19:27:08.413' AS DateTime), 30035, CAST(N'2020-02-02T19:27:08.413' AS DateTime), 30035)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (97, 30035, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T19:27:17.023' AS DateTime), 30035, CAST(N'2020-02-02T19:27:17.023' AS DateTime), 30035)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (98, 30036, N'questions', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T20:34:52.593' AS DateTime), 30036, CAST(N'2020-02-02T20:34:52.593' AS DateTime), 30036)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (99, 30035, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T20:40:20.877' AS DateTime), 30035, CAST(N'2020-02-02T20:40:20.877' AS DateTime), 30035)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (100, 0, N'question', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T20:40:34.860' AS DateTime), 0, CAST(N'2020-02-02T20:40:34.860' AS DateTime), 0)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (101, 30036, N'questions', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T20:40:46.847' AS DateTime), 30036, CAST(N'2020-02-02T20:40:46.847' AS DateTime), 30036)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (102, 30036, N'questions', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T20:43:36.287' AS DateTime), 30036, CAST(N'2020-02-02T20:43:36.287' AS DateTime), 30036)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (103, 30037, N'Register1', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T21:04:27.750' AS DateTime), 30037, CAST(N'2020-02-02T21:04:27.750' AS DateTime), 30037)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (104, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T21:42:10.117' AS DateTime), 30038, CAST(N'2020-02-02T21:42:10.117' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (105, 30039, N'Register1', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T21:42:49.557' AS DateTime), 30039, CAST(N'2020-02-02T21:42:49.557' AS DateTime), 30039)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (106, 30040, N'questions', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T21:46:17.873' AS DateTime), 30040, CAST(N'2020-02-02T21:46:17.873' AS DateTime), 30040)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (107, 30041, N'questions1', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T21:51:28.140' AS DateTime), 30041, CAST(N'2020-02-02T21:51:28.140' AS DateTime), 30041)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (108, 30041, N'questions1', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T21:52:04.190' AS DateTime), 30041, CAST(N'2020-02-02T21:52:04.190' AS DateTime), 30041)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (109, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T21:59:04.757' AS DateTime), 30038, CAST(N'2020-02-02T21:59:04.757' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (110, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T22:02:15.060' AS DateTime), 30038, CAST(N'2020-02-02T22:02:15.060' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (111, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T23:16:27.883' AS DateTime), 30038, CAST(N'2020-02-02T23:16:27.883' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (112, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T23:23:48.297' AS DateTime), 30038, CAST(N'2020-02-02T23:23:48.297' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (113, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T23:24:11.957' AS DateTime), 30038, CAST(N'2020-02-02T23:24:11.957' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (114, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T23:34:17.787' AS DateTime), 30038, CAST(N'2020-02-02T23:34:17.787' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (115, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T23:34:45.690' AS DateTime), 30038, CAST(N'2020-02-02T23:34:45.690' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (116, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T23:35:08.253' AS DateTime), 30042, CAST(N'2020-02-02T23:35:08.253' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (117, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T23:36:46.593' AS DateTime), 30043, CAST(N'2020-02-02T23:36:46.593' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (118, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T23:37:04.167' AS DateTime), 30042, CAST(N'2020-02-02T23:37:04.167' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (119, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:48:19.920' AS DateTime), 30043, CAST(N'2020-02-02T23:48:19.920' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (120, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:48:34.187' AS DateTime), 30043, CAST(N'2020-02-02T23:48:34.187' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (121, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:48:36.887' AS DateTime), 30043, CAST(N'2020-02-02T23:48:36.887' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (122, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:48:40.093' AS DateTime), 30043, CAST(N'2020-02-02T23:48:40.093' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (123, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:48:50.007' AS DateTime), 30043, CAST(N'2020-02-02T23:48:50.007' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (124, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T23:50:53.833' AS DateTime), 30043, CAST(N'2020-02-02T23:50:53.833' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (125, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:51:06.277' AS DateTime), 30043, CAST(N'2020-02-02T23:51:06.277' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (126, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:51:51.470' AS DateTime), 30043, CAST(N'2020-02-02T23:51:51.470' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (127, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T23:51:55.193' AS DateTime), 30043, CAST(N'2020-02-02T23:51:55.193' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (128, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:52:30.530' AS DateTime), 30043, CAST(N'2020-02-02T23:52:30.530' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (129, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T23:52:43.033' AS DateTime), 30043, CAST(N'2020-02-02T23:52:43.033' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (130, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T23:53:06.400' AS DateTime), 30043, CAST(N'2020-02-02T23:53:06.400' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (131, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:53:15.803' AS DateTime), 30043, CAST(N'2020-02-02T23:53:15.803' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (132, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:53:24.493' AS DateTime), 30043, CAST(N'2020-02-02T23:53:24.493' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (133, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-02T23:53:31.057' AS DateTime), 30043, CAST(N'2020-02-02T23:53:31.057' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (134, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:53:46.070' AS DateTime), 30043, CAST(N'2020-02-02T23:53:46.070' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (135, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:53:51.820' AS DateTime), 30043, CAST(N'2020-02-02T23:53:51.820' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (136, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:53:53.580' AS DateTime), 30043, CAST(N'2020-02-02T23:53:53.580' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (137, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:53:59.527' AS DateTime), 30043, CAST(N'2020-02-02T23:53:59.527' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (138, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:57:05.560' AS DateTime), 30043, CAST(N'2020-02-02T23:57:05.560' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (139, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:57:20.117' AS DateTime), 30043, CAST(N'2020-02-02T23:57:20.117' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (140, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:57:22.630' AS DateTime), 30043, CAST(N'2020-02-02T23:57:22.630' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (141, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:57:24.800' AS DateTime), 30043, CAST(N'2020-02-02T23:57:24.800' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (142, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:57:34.780' AS DateTime), 30043, CAST(N'2020-02-02T23:57:34.780' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (143, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:57:36.960' AS DateTime), 30043, CAST(N'2020-02-02T23:57:36.960' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (144, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:57:38.870' AS DateTime), 30043, CAST(N'2020-02-02T23:57:38.870' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (145, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:58:00.377' AS DateTime), 30043, CAST(N'2020-02-02T23:58:00.377' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (146, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:58:02.677' AS DateTime), 30043, CAST(N'2020-02-02T23:58:02.677' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (147, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:58:04.820' AS DateTime), 30043, CAST(N'2020-02-02T23:58:04.820' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (148, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:58:07.217' AS DateTime), 30043, CAST(N'2020-02-02T23:58:07.217' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (149, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:58:34.367' AS DateTime), 30043, CAST(N'2020-02-02T23:58:34.367' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (150, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:58:45.910' AS DateTime), 30043, CAST(N'2020-02-02T23:58:45.910' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (151, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:58:55.637' AS DateTime), 30043, CAST(N'2020-02-02T23:58:55.637' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (152, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:59:01.253' AS DateTime), 30043, CAST(N'2020-02-02T23:59:01.253' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (153, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-02T23:59:07.220' AS DateTime), 30043, CAST(N'2020-02-02T23:59:07.220' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (154, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:00:43.590' AS DateTime), 30043, CAST(N'2020-02-03T00:00:43.590' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (155, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:00:52.003' AS DateTime), 30043, CAST(N'2020-02-03T00:00:52.003' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (156, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T00:00:58.957' AS DateTime), 30043, CAST(N'2020-02-03T00:00:58.957' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (157, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T00:02:04.507' AS DateTime), 30043, CAST(N'2020-02-03T00:02:04.507' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (158, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T00:02:16.553' AS DateTime), 30043, CAST(N'2020-02-03T00:02:16.553' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (159, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:02:26.743' AS DateTime), 30043, CAST(N'2020-02-03T00:02:26.743' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (160, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:02:28.790' AS DateTime), 30043, CAST(N'2020-02-03T00:02:28.790' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (161, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:02:30.633' AS DateTime), 30043, CAST(N'2020-02-03T00:02:30.633' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (162, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:02:33.093' AS DateTime), 30043, CAST(N'2020-02-03T00:02:33.093' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (163, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:02:35.803' AS DateTime), 30043, CAST(N'2020-02-03T00:02:35.803' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (164, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:03:19.190' AS DateTime), 30043, CAST(N'2020-02-03T00:03:19.190' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (165, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:03:25.500' AS DateTime), 30043, CAST(N'2020-02-03T00:03:25.500' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (166, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:03:27.257' AS DateTime), 30043, CAST(N'2020-02-03T00:03:27.257' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (167, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:03:29.473' AS DateTime), 30043, CAST(N'2020-02-03T00:03:29.473' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (168, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:05:29.357' AS DateTime), 30043, CAST(N'2020-02-03T00:05:29.357' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (169, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:05:31.060' AS DateTime), 30043, CAST(N'2020-02-03T00:05:31.060' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (170, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:05:33.327' AS DateTime), 30043, CAST(N'2020-02-03T00:05:33.327' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (171, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:05:35.123' AS DateTime), 30043, CAST(N'2020-02-03T00:05:35.123' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (172, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:05:41.547' AS DateTime), 30043, CAST(N'2020-02-03T00:05:41.547' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (173, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T00:06:06.417' AS DateTime), 30043, CAST(N'2020-02-03T00:06:06.417' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (174, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:06:19.383' AS DateTime), 30043, CAST(N'2020-02-03T00:06:19.383' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (175, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:06:21.517' AS DateTime), 30043, CAST(N'2020-02-03T00:06:21.517' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (176, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T00:06:24.450' AS DateTime), 30043, CAST(N'2020-02-03T00:06:24.450' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (177, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:06:38.943' AS DateTime), 30043, CAST(N'2020-02-03T00:06:38.943' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (178, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:06:44.647' AS DateTime), 30043, CAST(N'2020-02-03T00:06:44.647' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (179, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:07:15.220' AS DateTime), 30043, CAST(N'2020-02-03T00:07:15.220' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (180, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T00:07:41.637' AS DateTime), 30043, CAST(N'2020-02-03T00:07:41.637' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (181, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:07:53.870' AS DateTime), 30043, CAST(N'2020-02-03T00:07:53.870' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (182, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T00:08:03.363' AS DateTime), 30043, CAST(N'2020-02-03T00:08:03.363' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (183, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:08:14.067' AS DateTime), 30043, CAST(N'2020-02-03T00:08:14.067' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (184, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:08:19.177' AS DateTime), 30043, CAST(N'2020-02-03T00:08:19.177' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (185, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:08:24.617' AS DateTime), 30043, CAST(N'2020-02-03T00:08:24.617' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (186, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:08:30.190' AS DateTime), 30043, CAST(N'2020-02-03T00:08:30.190' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (187, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T00:08:35.627' AS DateTime), 30043, CAST(N'2020-02-03T00:08:35.627' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (188, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T00:09:18.137' AS DateTime), 30042, CAST(N'2020-02-03T00:09:18.137' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (189, 30044, N'blockquote ', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T00:14:27.470' AS DateTime), 30044, CAST(N'2020-02-03T00:14:27.470' AS DateTime), 30044)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (190, 30045, N'confirmButtonText', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T00:15:21.323' AS DateTime), 30045, CAST(N'2020-02-03T00:15:21.323' AS DateTime), 30045)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (191, 30046, N'confirmButtonColor', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T00:15:56.050' AS DateTime), 30046, CAST(N'2020-02-03T00:15:56.050' AS DateTime), 30046)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (192, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T00:17:30.067' AS DateTime), 30042, CAST(N'2020-02-03T00:17:30.067' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (193, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T12:06:01.470' AS DateTime), 30038, CAST(N'2020-02-03T12:06:01.470' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (194, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T12:07:47.427' AS DateTime), 30038, CAST(N'2020-02-03T12:07:47.427' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (195, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T12:08:26.770' AS DateTime), 30038, CAST(N'2020-02-03T12:08:26.770' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (196, 30047, N'responded ', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T12:27:15.083' AS DateTime), 30047, CAST(N'2020-02-03T12:27:15.083' AS DateTime), 30047)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (197, 30047, N'responded ', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T12:27:33.560' AS DateTime), 30047, CAST(N'2020-02-03T12:27:33.560' AS DateTime), 30047)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (198, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T13:51:14.787' AS DateTime), 30038, CAST(N'2020-02-03T13:51:14.787' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (199, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T13:52:21.483' AS DateTime), 30038, CAST(N'2020-02-03T13:52:21.483' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (200, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T14:03:06.853' AS DateTime), 30042, CAST(N'2020-02-03T14:03:06.853' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (201, 30048, N'attributes', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T14:03:39.460' AS DateTime), 30048, CAST(N'2020-02-03T14:03:39.460' AS DateTime), 30048)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (202, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T14:16:03.923' AS DateTime), 30038, CAST(N'2020-02-03T14:16:03.923' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (203, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T14:22:46.890' AS DateTime), 30042, CAST(N'2020-02-03T14:22:46.890' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (204, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T19:35:14.837' AS DateTime), 30038, CAST(N'2020-02-03T19:35:14.837' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (205, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T20:07:40.617' AS DateTime), 30042, CAST(N'2020-02-03T20:07:40.617' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (206, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T20:21:33.267' AS DateTime), 30038, CAST(N'2020-02-03T20:21:33.267' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (207, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T21:52:20.913' AS DateTime), 30042, CAST(N'2020-02-03T21:52:20.913' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (208, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T21:53:29.263' AS DateTime), 30042, CAST(N'2020-02-03T21:53:29.263' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (209, 30043, N'Register123', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-03T21:53:35.263' AS DateTime), 30043, CAST(N'2020-02-03T21:53:35.263' AS DateTime), 30043)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (210, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T21:53:44.830' AS DateTime), 30042, CAST(N'2020-02-03T21:53:44.830' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (211, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T23:03:39.903' AS DateTime), 30042, CAST(N'2020-02-03T23:03:39.903' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (212, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-03T23:07:05.717' AS DateTime), 30042, CAST(N'2020-02-03T23:07:05.717' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (213, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-04T21:44:49.917' AS DateTime), 30038, CAST(N'2020-02-04T21:44:49.917' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (214, 30049, N'blockquoteyy', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-04T22:03:47.440' AS DateTime), 30049, CAST(N'2020-02-04T22:03:47.440' AS DateTime), 30049)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (215, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-04T22:06:31.637' AS DateTime), 30038, CAST(N'2020-02-04T22:06:31.637' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (216, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-04T22:07:17.510' AS DateTime), 30038, CAST(N'2020-02-04T22:07:17.510' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (217, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-05T09:47:39.133' AS DateTime), 30042, CAST(N'2020-02-05T09:47:39.133' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (218, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-05T10:27:50.567' AS DateTime), 30038, CAST(N'2020-02-05T10:27:50.567' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (219, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-05T10:28:03.187' AS DateTime), 30038, CAST(N'2020-02-05T10:28:03.187' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (220, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-05T10:28:05.343' AS DateTime), 30038, CAST(N'2020-02-05T10:28:05.343' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (221, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-05T10:28:07.293' AS DateTime), 30038, CAST(N'2020-02-05T10:28:07.293' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (222, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-05T10:28:08.997' AS DateTime), 30038, CAST(N'2020-02-05T10:28:08.997' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (223, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-05T10:54:31.637' AS DateTime), 30042, CAST(N'2020-02-05T10:54:31.637' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (224, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-05T15:09:59.500' AS DateTime), 30042, CAST(N'2020-02-05T15:09:59.500' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (225, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-05T16:16:31.880' AS DateTime), 30042, CAST(N'2020-02-05T16:16:31.880' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (226, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-05T16:19:49.077' AS DateTime), 30042, CAST(N'2020-02-05T16:19:49.077' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (227, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-05T16:27:15.177' AS DateTime), 30042, CAST(N'2020-02-05T16:27:15.177' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (228, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-05T17:38:59.967' AS DateTime), 30042, CAST(N'2020-02-05T17:38:59.967' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (229, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-05T18:40:34.680' AS DateTime), 30038, CAST(N'2020-02-05T18:40:34.680' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (230, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-05T18:41:10.987' AS DateTime), 30038, CAST(N'2020-02-05T18:41:10.987' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (231, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-05T18:41:14.727' AS DateTime), 30038, CAST(N'2020-02-05T18:41:14.727' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (232, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-05T18:48:03.013' AS DateTime), 30038, CAST(N'2020-02-05T18:48:03.013' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (233, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-05T18:48:27.947' AS DateTime), 30038, CAST(N'2020-02-05T18:48:27.947' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (234, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-05T18:48:41.627' AS DateTime), 30038, CAST(N'2020-02-05T18:48:41.627' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (235, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-05T18:48:43.677' AS DateTime), 30038, CAST(N'2020-02-05T18:48:43.677' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (236, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-05T18:48:45.343' AS DateTime), 30038, CAST(N'2020-02-05T18:48:45.343' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (237, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-05T18:48:47.187' AS DateTime), 30038, CAST(N'2020-02-05T18:48:47.187' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (238, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-05T18:49:16.453' AS DateTime), 30038, CAST(N'2020-02-05T18:49:16.453' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (239, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-06T10:27:46.313' AS DateTime), 30042, CAST(N'2020-02-06T10:27:46.313' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (240, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-06T11:39:15.810' AS DateTime), 30038, CAST(N'2020-02-06T11:39:15.810' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (241, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-06T11:39:28.797' AS DateTime), 30038, CAST(N'2020-02-06T11:39:28.797' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (242, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-06T11:41:00.777' AS DateTime), 30042, CAST(N'2020-02-06T11:41:00.777' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (243, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-06T11:41:30.583' AS DateTime), 30038, CAST(N'2020-02-06T11:41:30.583' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (244, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-06T11:41:50.077' AS DateTime), 30038, CAST(N'2020-02-06T11:41:50.077' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (245, 30050, N'Register1111', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-06T12:19:02.873' AS DateTime), 30050, CAST(N'2020-02-06T12:19:02.873' AS DateTime), 30050)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (246, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-06T13:52:02.533' AS DateTime), 30038, CAST(N'2020-02-06T13:52:02.533' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (247, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-06T13:52:15.253' AS DateTime), 30042, CAST(N'2020-02-06T13:52:15.253' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (248, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-06T14:40:49.823' AS DateTime), 30042, CAST(N'2020-02-06T14:40:49.823' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (249, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-06T14:41:06.813' AS DateTime), 30042, CAST(N'2020-02-06T14:41:06.813' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (250, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-06T14:41:09.367' AS DateTime), 30042, CAST(N'2020-02-06T14:41:09.367' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (251, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-06T14:41:12.153' AS DateTime), 30042, CAST(N'2020-02-06T14:41:12.153' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (252, 30039, N'Register1', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-06T16:16:15.207' AS DateTime), 30039, CAST(N'2020-02-06T16:16:15.207' AS DateTime), 30039)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (253, 30039, N'Register1', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-06T16:16:18.517' AS DateTime), 30039, CAST(N'2020-02-06T16:16:18.517' AS DateTime), 30039)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (254, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-06T16:20:35.240' AS DateTime), 30042, CAST(N'2020-02-06T16:20:35.240' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (255, 30051, N'successfully', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-06T16:27:48.513' AS DateTime), 30051, CAST(N'2020-02-06T16:27:48.513' AS DateTime), 30051)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (256, 30051, N'successfully', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-06T16:28:15.740' AS DateTime), 30051, CAST(N'2020-02-06T16:28:15.740' AS DateTime), 30051)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (257, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-06T16:40:35.280' AS DateTime), 30042, CAST(N'2020-02-06T16:40:35.280' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (258, 30052, N'Redirecting', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-06T16:47:32.660' AS DateTime), 30052, CAST(N'2020-02-06T16:47:32.660' AS DateTime), 30052)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (259, 30053, N'redirectToHomePage', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-06T16:48:25.127' AS DateTime), 30053, CAST(N'2020-02-06T16:48:25.127' AS DateTime), 30053)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (260, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 0, CAST(N'2020-02-06T22:11:29.773' AS DateTime), 30038, CAST(N'2020-02-06T22:11:29.773' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (261, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-06T22:11:39.177' AS DateTime), 30042, CAST(N'2020-02-06T22:11:39.177' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (262, 30051, N'successfully', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-06T23:10:37.830' AS DateTime), 30051, CAST(N'2020-02-06T23:10:37.830' AS DateTime), 30051)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (263, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-07T11:04:11.860' AS DateTime), 30042, CAST(N'2020-02-07T11:04:11.860' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (264, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-07T11:50:36.993' AS DateTime), 30042, CAST(N'2020-02-07T11:50:36.993' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (265, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-07T11:52:34.397' AS DateTime), 30042, CAST(N'2020-02-07T11:52:34.397' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (266, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-07T11:55:11.737' AS DateTime), 30042, CAST(N'2020-02-07T11:55:11.737' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (267, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-07T11:56:07.233' AS DateTime), 30042, CAST(N'2020-02-07T11:56:07.233' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (268, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-07T12:08:09.477' AS DateTime), 30042, CAST(N'2020-02-07T12:08:09.477' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (269, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-07T12:09:03.880' AS DateTime), 30042, CAST(N'2020-02-07T12:09:03.880' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (270, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-07T12:17:00.773' AS DateTime), 30042, CAST(N'2020-02-07T12:17:00.773' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (271, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-07T12:22:00.040' AS DateTime), 30042, CAST(N'2020-02-07T12:22:00.040' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (272, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-07T12:23:51.997' AS DateTime), 30042, CAST(N'2020-02-07T12:23:51.997' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (273, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-07T12:33:14.243' AS DateTime), 30042, CAST(N'2020-02-07T12:33:14.243' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (274, 30042, N'localhost', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-08T18:53:52.013' AS DateTime), 30042, CAST(N'2020-02-08T18:53:52.013' AS DateTime), 30042)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (275, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-14T09:34:08.967' AS DateTime), 30038, CAST(N'2020-02-14T09:34:08.967' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (276, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-14T10:55:44.470' AS DateTime), 30038, CAST(N'2020-02-14T10:55:44.470' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (277, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-14T12:51:36.233' AS DateTime), 30038, CAST(N'2020-02-14T12:51:36.233' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (278, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-14T12:52:00.283' AS DateTime), 30038, CAST(N'2020-02-14T12:52:00.283' AS DateTime), 30038)
GO
INSERT [dbo].[UserLoggingLog] ([UserLoggingLogId], [UserId], [UserName], [LoggingIpAddress], [LoggingBrowser], [IsUserAuthenticated], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (279, 30038, N'Register', N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36', 1, CAST(N'2020-02-14T13:04:59.867' AS DateTime), 30038, CAST(N'2020-02-14T13:04:59.867' AS DateTime), 30038)
GO
SET IDENTITY_INSERT [dbo].[UserLoggingLog] OFF
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
/****** Object:  StoredProcedure [dbo].[P_GetAllUserAccounts]    Script Date: 14-02-2020 13:21:34 ******/
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
/****** Object:  StoredProcedure [dbo].[P_GetUserAccountDetails]    Script Date: 14-02-2020 13:21:34 ******/
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
/****** Object:  StoredProcedure [dbo].[P_GetUserDetailsForLoginValidation]    Script Date: 14-02-2020 13:21:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
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

    END;
GO
/****** Object:  StoredProcedure [dbo].[P_RegisterUser]    Script Date: 14-02-2020 13:21:34 ******/
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
/****** Object:  StoredProcedure [dbo].[P_SaveUserLoggingDetails]    Script Date: 14-02-2020 13:21:34 ******/
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
/****** Object:  StoredProcedure [dbo].[P_UpdateUserAccountActiveStatus]    Script Date: 14-02-2020 13:21:34 ******/
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
/****** Object:  StoredProcedure [dbo].[P_UpdateUserAccountDetails]    Script Date: 14-02-2020 13:21:34 ******/
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
/****** Object:  StoredProcedure [dbo].[P_UpdateUserAccountLockedStatus]    Script Date: 14-02-2020 13:21:34 ******/
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
/****** Object:  StoredProcedure [dbo].[P_UpdateUserAccountPassword]    Script Date: 14-02-2020 13:21:34 ******/
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
