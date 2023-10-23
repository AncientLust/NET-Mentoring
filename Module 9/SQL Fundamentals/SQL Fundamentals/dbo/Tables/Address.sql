CREATE TABLE [dbo].[Address]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Street] VARCHAR(50) NOT NULL, 
    [City] VARCHAR(20) NULL, 
    [State] VARCHAR(50) NULL, 
    [ZipCode] VARCHAR(50) NULL
)
