


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Execution : EXEC  [dbo].[P_GetAllCountryStateCity] 
-- =============================================
CREATE PROC [dbo].[P_GetAllCountryStateCity] 
						
AS
  BEGIN

  SELECT [CountryId]
      ,[ShortName]
      ,[CountryName]
      ,[CountryPhoneCode]
  FROM [dbo].[Country]

	SELECT  [StateId]
		  ,[StateName]
		  ,[CountryId]
	  FROM [dbo].[State]
 
 SELECT [CityId]
      ,[CityName]
      ,[StateId]
  FROM [dbo].[City]

  END;