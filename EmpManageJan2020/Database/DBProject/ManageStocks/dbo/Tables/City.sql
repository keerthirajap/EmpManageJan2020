CREATE TABLE [dbo].[City] (
    [CityId]   BIGINT        NOT NULL,
    [CityName] NVARCHAR (50) NOT NULL,
    [StateId]  BIGINT        NULL,
    CONSTRAINT [PK_dbo.City] PRIMARY KEY CLUSTERED ([CityId] ASC)
);

