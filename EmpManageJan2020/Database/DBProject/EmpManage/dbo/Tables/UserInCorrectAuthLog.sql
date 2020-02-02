CREATE TABLE [dbo].[UserInCorrectAuthLog] (
    [UserInCorrectAuthLogId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [UserId]                 BIGINT         NOT NULL,
    [UserName]               NVARCHAR (50)  NOT NULL,
    [InCorrectLoggingCount]  BIGINT         NOT NULL,
    [LoggingIpAddress]       NVARCHAR (500) NOT NULL,
    [LoggingBrowser]         NVARCHAR (MAX) NOT NULL,
    [IsUserAuthenticated]    BIT            NOT NULL,
    [IsActive]               BIT            NOT NULL,
    [CreatedOn]              DATETIME       NOT NULL,
    [CreatedBy]              BIGINT         NULL,
    [ModifiedOn]             DATETIME       NOT NULL,
    [ModifiedBy]             BIGINT         NULL,
    CONSTRAINT [PK_dbo.UserInCorrectAuthLog] PRIMARY KEY CLUSTERED ([UserInCorrectAuthLogId] ASC)
);



