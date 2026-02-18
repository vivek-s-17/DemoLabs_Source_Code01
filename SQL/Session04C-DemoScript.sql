 USE [MyDemoDb]
 GO

/*
 *  Working with MASTER and TRANSACTION TABLE
 *  (a) Naming Convention for table, column, CHK, DF, PK and FK
 *  (b) Using IDENTITY column
 *  (c) Using PRIMARY KEY and FOREIGN KEY
 *  (d) Using CONSTRAINTS - DEFAULT VALUES
 *  (e) Using CONSTRAINTS - CHECK constraints for Validation
 *  (f) Using CHECK CONSTRAINTS for Enumerations
 *  (g) Altering the Table Schema
 ******************************************************/

 CREATE TABLE [Countries]		-- pluralized NAME
 (
	[Code] varchar(3) NOT NULL PRIMARY KEY				
	, [Name] varchar(50) NOT NULL
 )
 GO
 
 CREATE TABLE Persons			-- pluralized NAME
 (
    [PersonId] smallint NOT NULL IDENTITY(1,1)
    , [Name] varchar(50) NOT NULL
    , [Age] int NULL 
	, [CountryCode] varchar(3) NOT NULL		-- Recommendation: name should match with the FK Table's PK
	, [CreatedOn] datetime NOT NULL
	, [IsActive] bit NOT NULL DEFAULT (1)
	, [IsEnabled] bit NOT NULL CONSTRAINT [DF_IsEnabled] DEFAULT (1)
	, [Gender] varchar(9) NOT NULL

	, CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED ( [PersonId] ASC )
	, CONSTRAINT [FK_Persons_Countries] FOREIGN KEY ([CountryCode]) REFERENCES [Countries] ([Code])
	, CONSTRAINT [CHK_Age] CHECK ( [Age] >= 18 AND [Age] <= 65 )
	, CONSTRAINT [CHK_Gender] CHECK ( [Gender] IN ('Male', 'Female', 'Unknown') )
 )
 GO

 ALTER TABLE [Persons]
	-- ADD CONSTRAINT [DF_CreatedOn] DEFAULT ( getdate() ) FOR [CreatedOn]
	ADD CONSTRAINT [DF_CreatedOn] DEFAULT ( sysdatetimeoffset() ) FOR [CreatedOn]
 GO



 --- Cleanup (NOTE: The Type of Object)
 IF OBJECT_ID('sp_GetCustomerSubset', 'P') IS NOT NULL
	DROP PROCEDURE [dbo].[sp_GetCustomerSubset]
 GO
 IF OBJECT_ID('sp_GetCustomerSubsetNoCount', 'P') IS NOT NULL
	DROP PROCEDURE [dbo].[sp_GetCustomerSubsetNoCount]
 GO
 IF OBJECT_ID('funcScalar', 'FN') IS NOT NULL
	DROP FUNCTION [dbo].[funcScalar]
 GO
 IF OBJECT_ID('func_GetCustomers', 'IF') IS NOT NULL
	DROP FUNCTION [dbo].[func_GetCustomers]
 GO
 IF OBJECT_ID('VW_Customers', 'V') IS NOT NULL
	DROP VIEW [VW_Customers]
 GO
 IF OBJECT_ID('CustomersSubset', 'U') IS NOT NULL
	DROP TABLE [CustomersSubset]
 GO
