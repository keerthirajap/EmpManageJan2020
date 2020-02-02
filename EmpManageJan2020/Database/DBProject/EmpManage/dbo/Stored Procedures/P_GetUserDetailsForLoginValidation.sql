
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROC [dbo].[P_GetUserDetailsForLoginValidation] 
                         @UserName     [NVARCHAR](MAX)
                                 
AS
    BEGIN
	  SELECT TOP 1 [UserId]
		  ,[UserName]      
		  ,[PasswordHash]
		  ,[PasswordSalt]     
	  FROM [dbo].[User]
	  WHERE [UserName] = @UserName

    END;