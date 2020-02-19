

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Execution : EXEC [dbo].[P_GetUserRoles]   30054
-- =============================================
CREATE PROC [dbo].[P_GetUserRoles] 
	 @UserId   BIGINT   					
AS
  BEGIN

	 SELECT
		  USRRLS.UserRoleId
		,USRRLS.UserId
       ,RLS.[RoleId]
      ,RLS.[RoleName]
	  ,USRRLS.IsActive
	  ,USRRLS.CreatedOn
		,USRRLS.CreatedBy
	,(SELECT TOP 1 [UserName] FROM [dbo].[User] WHERE [UserId] = USRRLS.[CreatedBy]) CreatedByUserName	
  		,USRRLS.ModifiedOn
		,USRRLS.ModifiedBy
	,(SELECT TOP 1 [UserName] FROM [dbo].[User] WHERE [UserId] = USRRLS.[ModifiedBy])	ModifiedByUserName	
		FROM [dbo].Roles RLS
		INNER JOIN  [dbo].[UserRoles] USRRLS
		ON RLS.RoleId = USRRLS.RoleId
		WHERE [UserId] = @UserId  
  UNION ALL
  SELECT 0
		,@UserId
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
  WHERE  RLS.[RoleId] NOT IN (SELECT [RoleId] FROM [dbo].[UserRoles] WHERE [UserId] = @UserId )
 
  END;