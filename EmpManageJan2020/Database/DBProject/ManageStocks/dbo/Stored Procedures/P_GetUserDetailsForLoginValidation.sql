
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