

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

 
  END;