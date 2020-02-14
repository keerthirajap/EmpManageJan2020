

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

		SELECT    USRInCorrectAuthLog.[UserInCorrectAuthLogId]
			  ,USRInCorrectAuthLog.[UserId]
			  ,USRInCorrectAuthLog.[UserName]
			  ,USRInCorrectAuthLog.[InCorrectLoggingCount]
			  ,USRInCorrectAuthLog.[LoggingIpAddress]
			  ,USRInCorrectAuthLog.[LoggingBrowser]
			  ,USRInCorrectAuthLog.[IsUserAuthenticated]
			  ,USRInCorrectAuthLog.[IsActive]
			  ,USRInCorrectAuthLog.[CreatedOn]
			  ,USRInCorrectAuthLog.[CreatedBy]	
			   ,(SELECT TOP 1 [UserName] FROM [dbo].[User] WHERE [UserId] = USRInCorrectAuthLog.[CreatedBy])	CreatedByUserName	  
			  ,USRInCorrectAuthLog.[ModifiedOn]
			  ,USRInCorrectAuthLog.[ModifiedBy]
			  ,(SELECT TOP 1 [UserName] FROM [dbo].[User] WHERE [UserId] = USRInCorrectAuthLog.[ModifiedBy])	ModifiedByUserName			
		  FROM [dbo].[UserInCorrectAuthLog] USRInCorrectAuthLog
	
		  WHERE USRInCorrectAuthLog.UserId = @UserId
		  ORDER BY 1 DESC

	SELECT TOP (1000)  USRLoggingLog.[UserLoggingLogId]
					  ,USRLoggingLog.[UserId]
					  ,USRLoggingLog.[UserName]
					  ,USRLoggingLog.[LoggingIpAddress]
					  ,USRLoggingLog.[LoggingBrowser]
					  ,USRLoggingLog.[IsUserAuthenticated]
					  ,USRLoggingLog.[CreatedOn]
					  ,USRLoggingLog.[CreatedBy]
					  ,(SELECT TOP 1 [UserName] FROM [dbo].[User] WHERE [UserId] = USRLoggingLog.[CreatedBy])	CreatedByUserName				  				
					  ,USRLoggingLog.[ModifiedOn]
					  ,USRLoggingLog.[ModifiedBy]
					  ,(SELECT TOP 1 [UserName] FROM [dbo].[User] WHERE [UserId] = USRLoggingLog.ModifiedBy) AS ModifiedByUserName
	  FROM [dbo].[UserLoggingLog] USRLoggingLog	  	
	  WHERE USRLoggingLog.UserId = @UserId
	  ORDER BY 1 DESC

 
  END;