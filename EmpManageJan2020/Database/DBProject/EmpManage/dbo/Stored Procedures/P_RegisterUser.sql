
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROC [dbo].[P_RegisterUser] 

                                  @UserName     [NVARCHAR](MAX),                                  
                                  @EmailId        [NVARCHAR](MAX),                                 
                                  @PasswordHash [NVARCHAR](500), 
                                  @PasswordSalt [NVARCHAR](100), 
                                  @CreatedBy    [BIGINT]
AS
    BEGIN
        DECLARE @TodaysDate DATETIME= GETDATE();

        IF @CreatedBy = 0 OR @CreatedBy = NULL
            BEGIN
                SELECT @CreatedBy = 1;
        END;
        BEGIN TRANSACTION;

			INSERT INTO [dbo].[User]
					   ([UserName]           
					   ,[EmailId]           
					   ,[PasswordHash]
					   ,[PasswordSalt]
					   ,[IsActive]
					   ,[IsLocked]
					   ,[CreatedOn]
					   ,[CreatedBy]
					   ,[ModifiedOn]
					   ,[ModifiedBy])
				SELECT @UserName
						, @EmailId 
						,@PasswordHash
						,@PasswordSalt
						,1
						,0
                      ,@TodaysDate
                      ,@CreatedBy
                      ,@TodaysDate
                      ,@CreatedBy

        DECLARE @UserId BIGINT= SCOPE_IDENTITY()


        COMMIT TRANSACTION;


UPDATE [dbo].[User]
   SET [CreatedBy] = @UserId  
      ,[ModifiedBy] = @UserId
 WHERE [UserId] = @UserId
 		     
	   SELECT  [UserId]
			  ,[UserName]      
		FROM [dbo].[User]
		WHERE [UserId] = @UserId

END;