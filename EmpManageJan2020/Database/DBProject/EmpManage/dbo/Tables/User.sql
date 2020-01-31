CREATE TABLE [dbo].[User] (
    [UserId]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [UserName]     NVARCHAR (50)  NOT NULL,
    [UserTitleId]  SMALLINT       NULL,
    [FullName]     NVARCHAR (400) NULL,
    [FirstName]    NVARCHAR (200) NULL,
    [LastName]     NVARCHAR (200) NULL,
    [Email]        NVARCHAR (500) NOT NULL,
    [UserGenderId] SMALLINT       NULL,
    [PasswordHash] NVARCHAR (500) NOT NULL,
    [PasswordSalt] NVARCHAR (100) NOT NULL,
    [IsActive]     BIT            NOT NULL,
    [IsLocked]     BIT            NOT NULL,
    [CreatedOn]    DATETIME       NOT NULL,
    [CreatedBy]    BIGINT         NOT NULL,
    [ModifiedOn]   DATETIME       NOT NULL,
    [ModifiedBy]   BIGINT         NOT NULL,
    CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED ([UserId] ASC),
    FOREIGN KEY ([UserGenderId]) REFERENCES [dbo].[UserGender] ([UserGenderId]),
    FOREIGN KEY ([UserGenderId]) REFERENCES [dbo].[UserGender] ([UserGenderId]),
    FOREIGN KEY ([UserTitleId]) REFERENCES [dbo].[UserTitle] ([UserTitleId])
);

