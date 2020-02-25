CREATE TABLE [dbo].[State] (
    [StateId]   BIGINT        NOT NULL,
    [StateName] NVARCHAR (50) NOT NULL,
    [CountryId] BIGINT        NULL,
    CONSTRAINT [PK_dbo.State] PRIMARY KEY CLUSTERED ([StateId] ASC)
);

