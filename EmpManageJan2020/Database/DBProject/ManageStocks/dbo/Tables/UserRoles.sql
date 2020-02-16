CREATE TABLE [dbo].[UserRoles] (
    [UserRoleId] INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     INT            NOT NULL,
    [RoleId]     INT            NOT NULL,
    [RoleName]   NVARCHAR (MAX) NULL,
    [IsActive]   BIT            NULL,
    [CreatedOn]  DATETIME       NULL,
    [CreatedBy]  BIGINT         NULL,
    [ModifiedOn] DATETIME       NULL,
    [ModifiedBy] BIGINT         NULL
);

