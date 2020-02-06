


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Execution : EXEC [dbo].[P_UpdateUserAccountPassword]
-- =============================================
CREATE PROC [dbo].[P_UpdateUserAccountPassword]
								  @UserId       [BIGINT], 
								  @PasswordHash [NVARCHAR](500), 
                                  @PasswordSalt [NVARCHAR](100),							
                                  @ModifiedBy    [BIGINT]
						
AS
  BEGIN

		DECLARE @TodaysDate DATETIME= GETDATE();


		UPDATE [dbo].[User]
		   SET 
			    PasswordHash = @PasswordHash
			  ,PasswordSalt = @PasswordSalt
			  ,[ModifiedOn] = @TodaysDate
			  ,[ModifiedBy] = @ModifiedBy
		 WHERE UserId = @UserId

		 SELECT CAST(1 AS BIT) Success
  END;