

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Execution : EXEC [dbo].[P_GetUserLoginHistory]  30054
-- =============================================
CREATE PROC [dbo].[P_GetUserLoginHistory] 
	 @UserId   BIGINT   					
AS
  BEGIN

	SELECT TOP (2000)  USRLoggingLog.[UserLoggingLogId]
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