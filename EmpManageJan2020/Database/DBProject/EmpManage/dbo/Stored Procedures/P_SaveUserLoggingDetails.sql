

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROC [dbo].[P_SaveUserLoggingDetails] 
							@UserId     [BIGINT]
                        , @UserName     [NVARCHAR](MAX)
						  , @LoggingIpAddress    [NVARCHAR](MAX)
						   ,  @LoggingBrowser     [NVARCHAR](MAX)
							,   @IsUserAuthenticated    BIT
							,@IsInCorrectLogging BIT
                             ,@CreatedOn   DATETIME 
AS
    BEGIN
	

	INSERT INTO [dbo].[UserLoggingLog]
           ([UserId]
           ,[UserName]
           ,[LoggingIpAddress]
           ,[LoggingBrowser]
           ,[IsUserAuthenticated]
           ,[CreatedOn]
           ,[CreatedBy]
           ,[ModifiedOn]
           ,[ModifiedBy])
		SELECT 
				@UserId
				,@UserName
				,@LoggingIpAddress
				,@LoggingBrowser
				,@IsUserAuthenticated
                ,@CreatedOn
                ,@UserId    
                ,@CreatedOn
                ,@UserId 
	
	DECLARE @InCorrectLoggingCount [BIGINT]

	SELECT @InCorrectLoggingCount  = [InCorrectLoggingCount]
	FROM  [dbo].[UserInCorrectAuthLog] 
	WHERE [UserId] = @UserId

	IF @InCorrectLoggingCount = 2
		BEGIN
			UPDATE [dbo].[User]
		   SET 
			  IsLocked  = 1  
			  ,[ModifiedOn] = @CreatedOn
			  ,[ModifiedBy] = @UserId
			WHERE [UserId] = @UserId
		END

	IF @IsInCorrectLogging = 1 AND @UserId > 0 AND @InCorrectLoggingCount > 0
	BEGIN	
			UPDATE [dbo].[UserInCorrectAuthLog]
			   SET 
				   [InCorrectLoggingCount] = @InCorrectLoggingCount + 1   
				  ,[ModifiedOn] = @CreatedOn
				  ,[ModifiedBy] = @UserId
			 WHERE [UserId] = @UserId
		END
		
	ELSE IF @IsInCorrectLogging = 1 AND @UserId > 0 
		BEGIN
			INSERT INTO [dbo].[UserInCorrectAuthLog]
				   ([UserId]
				   ,[UserName]
				   ,[InCorrectLoggingCount]
				   ,[LoggingIpAddress]
				   ,[LoggingBrowser]
				   ,[IsUserAuthenticated]
				   ,[CreatedOn]
				   ,[CreatedBy]
				   ,[ModifiedOn]
				   ,[ModifiedBy])
			SELECT 
					@UserId
					,@UserName
					,1
					,@LoggingIpAddress
					,@LoggingBrowser
					,@IsUserAuthenticated
					,@CreatedOn
					,@UserId    
					,@CreatedOn
					,@UserId 
		END	
	ELSE IF @IsInCorrectLogging = 0 AND @UserId > 0
	BEGIN
		DELETE FROM [dbo].[UserInCorrectAuthLog] WHERE [UserId] = @UserId
	END

  END;