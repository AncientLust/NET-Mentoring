CREATE TABLE [dbo].[Order]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Status] NVARCHAR(50) NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    [ProductId] INT NOT NULL, 
    CONSTRAINT [FK_Order_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id]),
    CONSTRAINT [CK_Order_Status] CHECK ([Status] IN ('NotStarted', 'Loading', 'InProgress', 'Arrived', 'Unloading', 'Cancelled', 'Done'))
)
