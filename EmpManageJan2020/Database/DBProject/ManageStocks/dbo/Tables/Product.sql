CREATE TABLE [dbo].[Product] (
    [ProductId]          BIGINT          IDENTITY (600000, 1) NOT NULL,
    [ProductName]        NVARCHAR (50)   NOT NULL,
    [ProductDescription] NVARCHAR (500)  NULL,
    [ProductPrice]       MONEY           NULL,
    [ProductImage]       VARBINARY (MAX) NULL,
    [IsActive]           BIT             NOT NULL,
    [CreatedOn]          DATETIME        NOT NULL,
    [CreatedBy]          BIGINT          NOT NULL,
    [ModifiedOn]         DATETIME        NOT NULL,
    [ModifiedBy]         BIGINT          NOT NULL,
    CONSTRAINT [PK_dbo.Product] PRIMARY KEY CLUSTERED ([ProductId] ASC)
);

