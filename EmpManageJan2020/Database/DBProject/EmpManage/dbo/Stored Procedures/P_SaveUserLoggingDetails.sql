

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROC [dbo].[P_SaveUserLoggingDetails] 
                         @UserName     [NVARCHAR](MAX)
						  , @LoggingIpAddress    [NVARCHAR](MAX)
						   ,  @LoggingBrowser     [NVARCHAR](MAX)
							,   @IsLogginSuccess     BIT
                                 
AS
    BEGIN
	 
	 SELECT TOP 1 [UserId]
		  ,[UserName]      
		  ,[PasswordHash]
		  ,[PasswordSalt]     
	  FROM [dbo].[User]
	  WHERE [UserName] = @UserName

    END;