CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] TEXT NOT NULL, 
    [Weight] REAL NOT NULL, 
    [Height] REAL NOT NULL, 
    [Width] REAL NOT NULL, 
    [Length] REAL NOT NULL
)
