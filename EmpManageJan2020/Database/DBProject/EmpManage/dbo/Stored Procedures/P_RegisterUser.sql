-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROC [dbo].[P_RegisterUser] @UserId       [BIGINT], 
                                  @UserName     [NVARCHAR](MAX), 
                                  @FirstName    [NVARCHAR](MAX), 
                                  @LastName     [NVARCHAR](MAX), 
                                  @Email        [NVARCHAR](MAX), 
                                  @Password     [NVARCHAR](MAX), 
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
        ([UserName], 
         [FullName], 
         [FirstName], 
         [LastName], 
         [Email], 
         [Password], 
         [PasswordHash], 
         [PasswordSalt], 
         [IsActive], 
         [IsLocked], 
         [CreatedOn], 
         [CreatedBy], 
         [ModifiedOn], 
         [ModifiedBy]
        )
               SELECT @UserName, 
                      @FirstName + ' ' + @LastName, 
                      @FirstName, 
                      @LastName, 
                      @Email, 
                      @Password, 
                      @PasswordHash, 
                      @PasswordSalt, 
                      1, 
                      0, 
                      @TodaysDate, 
                      @CreatedBy, 
                      @TodaysDate, 
                      @CreatedBy

    --    DECLARE @ID BIGINT= SCOPE_IDENTITY()

    --    INSERT INTO [dbo].[UserRoles]
				--([UserId], 
				-- [RoleId], 
				-- [RoleName], 
				-- [IsActive], 
				-- [CreatedOn], 
				-- [CreatedBy], 
				-- [ModifiedOn], 
				-- [ModifiedBy]
				--)
    --           SELECT @ID, 
    --                  4, 
    --                  'User', 
    --                  1, 
    --                  @TodaysDate, 
    --                  @CreatedBy, 
    --                  @TodaysDate, 
    --                  @CreatedBy

        COMMIT TRANSACTION;
       -- SELECT ID AS UserId;
    END;