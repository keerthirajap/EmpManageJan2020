CREATE TABLE [dbo].[Country] (
    [CountryId]        BIGINT        NOT NULL,
    [ShortName]        NVARCHAR (50) NOT NULL,
    [CountryName]      NVARCHAR (50) NOT NULL,
    [CountryPhoneCode] INT           NULL,
    CONSTRAINT [PK_dbo.Country] PRIMARY KEY CLUSTERED ([CountryId] ASC)
);

