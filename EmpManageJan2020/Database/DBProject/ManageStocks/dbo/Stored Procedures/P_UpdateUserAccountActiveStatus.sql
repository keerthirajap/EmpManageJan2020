

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Execution : EXEC [dbo].[P_GetAllUserAccounts] 
-- =============================================
CREATE PROC [dbo].[P_UpdateUserAccountActiveStatus]
								  @UserId       [BIGINT],   
								  @IsActive   BIT,                            							
                                  @ModifiedBy    [BIGINT] 
						
AS
  BEGIN

	DECLARE @TodaysDate DATETIME= GETDATE();


UPDATE [dbo].[User]
   SET 
       IsActive = @IsActive         
      ,[ModifiedOn] = @TodaysDate
      ,[ModifiedBy] = @ModifiedBy
 WHERE UserId = @UserId

		 SELECT CAST(1 AS BIT) Success

  END;