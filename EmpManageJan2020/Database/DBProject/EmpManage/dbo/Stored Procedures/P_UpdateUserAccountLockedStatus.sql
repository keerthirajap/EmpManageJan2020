


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Execution : [dbo].[P_UpdateUserAccountLockedStatus]
-- =============================================
CREATE PROC [dbo].[P_UpdateUserAccountLockedStatus]
								  @UserId       [BIGINT],   
								  @IsLocked   BIT,                            							
                                  @ModifiedBy    [BIGINT] 
						
AS
  BEGIN

	DECLARE @TodaysDate DATETIME= GETDATE();


UPDATE [dbo].[User]
   SET 
       IsLocked = @IsLocked       
      ,[ModifiedOn] = @TodaysDate
      ,[ModifiedBy] = @ModifiedBy
 WHERE UserId = @UserId

 
UPDATE [dbo].[UserInCorrectAuthLog]
   SET 
       [IsActive] = 0     
      ,[ModifiedOn] = @TodaysDate
      ,[ModifiedBy] = @ModifiedBy
 WHERE  UserId = @UserId AND [IsActive] = 1

		 SELECT CAST(1 AS BIT) Success

  END;