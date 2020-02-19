

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Execution : EXEC [dbo].[P_GetUserInCorrectLoginHistory]  30054
-- =============================================
CREATE PROC [dbo].[P_GetUserInCorrectLoginHistory] 
	 @UserId   BIGINT   					
AS
  BEGIN

	SELECT TOP (1000)  
			   USRInCorrectAuthLog.[UserInCorrectAuthLogId]
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
 
  END;