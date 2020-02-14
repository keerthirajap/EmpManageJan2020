

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