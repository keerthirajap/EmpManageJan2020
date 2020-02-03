

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROC [dbo].[P_UpdateUserDetails] 
							@UserId     [BIGINT]
							, @UserName     [NVARCHAR](MAX)
							,@UserTitleId smallint
							,@FullName [nvarchar](400)
							,@FirstName [nvarchar](200)
							,@LastName [nvarchar](200)
							  , @EmailId        [NVARCHAR](MAX)
							   ,   @ModifiedBy    [BIGINT]
AS
    BEGIN
	
	  DECLARE @TodaysDate DATETIME= GETDATE();

		UPDATE [dbo].[User]
		   SET [UserName] = @UserName
		       ,[UserTitleId] = @UserTitleId 
			  ,[FullName] = @FullName
			  ,[FirstName] =@FirstName
			  ,[LastName] = @LastName
			  ,[EmailId] = @EmailId    			      
			  ,[ModifiedOn] = @TodaysDate
			  ,[ModifiedBy] =  @ModifiedBy   
		 WHERE UserId = @UserId

  END;