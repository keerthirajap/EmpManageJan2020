

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Execution : EXEC [dbo].[P_SaveUserAccountDetails]
-- =============================================
CREATE PROC [dbo].[P_UpdateUserAccountDetails]
								  @UserId       [BIGINT], 
                                  @UserName     [NVARCHAR](MAX), 
								  @UserTitleId	[smallint],
                                  @FirstName    [NVARCHAR](MAX),
                                  @LastName     [NVARCHAR](MAX), 
								  @FullName		 [NVARCHAR](MAX),
                                  @EmailId        [NVARCHAR](MAX),
								  @UserGenderId		[smallint],								
                                  @ModifiedBy    [BIGINT]
						
AS
  BEGIN

		DECLARE @TodaysDate DATETIME= GETDATE();


		UPDATE [dbo].[User]
		   SET 
			   [UserTitleId] = @UserTitleId
			  ,[FullName] = @FullName
			  ,[FirstName] = @FirstName
			  ,[LastName] = @LastName
			  ,[EmailId] = @EmailId
			  ,[UserGenderId] = @UserGenderId      
			  ,[ModifiedOn] = @TodaysDate
			  ,[ModifiedBy] = @ModifiedBy
		 WHERE UserId = @UserId

		 SELECT CAST(1 AS BIT) Success
  END;