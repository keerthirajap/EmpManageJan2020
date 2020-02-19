

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Execution : EXEC [dbo].[EditUserRoles]
-- =============================================
CREATE PROC [dbo].[P_EditUserRoles]		
				@EditUserRoles T_EditUserRoles READONLY,						  							
                                  @ModifiedBy    [BIGINT]
						
AS
  BEGIN

		DECLARE @TodaysDate DATETIME= GETDATE();

		MERGE [dbo].[UserRoles] T 
		USING @EditUserRoles S
		ON (s.[UserRoleId] = t.[UserRoleId] AND s.[IsActive] = 1)		
		WHEN NOT MATCHED BY TARGET AND (S.[IsActive] = 1) THEN
		INSERT 
					([UserId]
				   ,[RoleId]
				   ,[RoleName]
				   ,[IsActive]
				   ,[CreatedOn]
				   ,[CreatedBy]
				   ,[ModifiedOn]
				   ,[ModifiedBy])

		Values (    S.[UserId]
				   ,S.[RoleId]
				   ,S.[RoleName]
				   ,1
				   ,@TodaysDate
				   ,@ModifiedBy
				   ,@TodaysDate
				   ,@ModifiedBy
			)  


		;


		 SELECT CAST(1 AS BIT) Success
  END;