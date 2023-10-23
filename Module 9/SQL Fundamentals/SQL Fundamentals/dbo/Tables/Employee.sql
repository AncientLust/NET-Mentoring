CREATE TABLE [dbo].[Employee]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AddressId] INT NOT NULL, 
    [PersonId] INT NOT NULL, 
    [CompanyName] VARCHAR(20) NOT NULL, 
    [Position] VARCHAR(30) NULL, 
    [EmployeeName] NCHAR(100) NULL, 
    CONSTRAINT [FK_Employee_Address] FOREIGN KEY ([AddressId]) REFERENCES [Address]([Id]), 
    CONSTRAINT [FK_Employee_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person]([Id])
)
