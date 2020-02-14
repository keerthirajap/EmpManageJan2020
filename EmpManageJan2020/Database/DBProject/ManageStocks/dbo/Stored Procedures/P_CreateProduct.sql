

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Execution : EXEC [dbo].[P_CreateProduct] 
-- =============================================
CREATE PROC [dbo].[P_CreateProduct] 
				@ProductId [bigint]	,				
				@ProductName [nvarchar](50)	,		
				@ProductDescription [nvarchar](500)	,
				@ProductPrice money	,				
				@ProductImage [varbinary](max)	,
				 @CreatedBy    [BIGINT] 		

AS
    BEGIN
        DECLARE @TodaysDate DATETIME= GETDATE();
		BEGIN TRANSACTION;
		
		INSERT INTO [dbo].[Product]
				   ([ProductName]
				   ,[ProductDescription]
				   ,[ProductPrice]
				   ,[ProductImage]
				   ,[IsActive]
				   ,[CreatedOn]
				   ,[CreatedBy]
				   ,[ModifiedOn]
				   ,[ModifiedBy])
       SELECT  
				  		
				   @ProductName ,		
				   @ProductDescription ,
				   @ProductPrice,			
				   @ProductImage ,
				   1,
				   @TodaysDate,
					@CreatedBy ,  		
					 @TodaysDate,
					@CreatedBy 

       SELECT @ProductId = SCOPE_IDENTITY()


        COMMIT TRANSACTION;

		SELECT @ProductId  AS ProductId 
END;