CREATE TYPE [dbo].[T_EditUserRoles] AS TABLE (
    [UserRoleId] INT            NULL,
    [UserId]     INT            NULL,
    [RoleId]     INT            NULL,
    [RoleName]   NVARCHAR (MAX) NULL,
    [IsActive]   BIT            NULL,
    [CreatedOn]  DATETIME       NULL,
    [CreatedBy]  BIGINT         NULL,
    [ModifiedOn] DATETIME       NULL,
    [ModifiedBy] BIGINT         NULL);

