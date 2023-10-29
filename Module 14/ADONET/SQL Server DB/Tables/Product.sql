CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] TEXT NOT NULL, 
    [Weight] FLOAT NOT NULL, 
    [Height] FLOAT NOT NULL, 
    [Width] FLOAT NOT NULL, 
    [Length] FLOAT NOT NULL
)
