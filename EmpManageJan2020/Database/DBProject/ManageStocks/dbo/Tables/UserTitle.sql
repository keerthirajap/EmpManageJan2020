CREATE TABLE [dbo].[UserTitle] (
    [UserTitleId]   SMALLINT       IDENTITY (50, 1) NOT NULL,
    [UserTitleDesc] NVARCHAR (500) NOT NULL,
    CONSTRAINT [PK_dbo.UserTitle] PRIMARY KEY CLUSTERED ([UserTitleId] ASC)
);

