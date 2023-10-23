CREATE TRIGGER [dbo].[TrInsertCompanyOnEmployeeInsert]
	ON [dbo].[Employee]
	FOR INSERT
	AS
	BEGIN
		INSERT INTO dbo.Company([Name], [AddressId] ) 
		SELECT i.CompanyName, i.AddressId
		FROM INSERTED i
		LEFT JOIN dbo.Company c ON c.Name = i.CompanyName AND c.AddressId = i.AddressId
		WHERE c.Name IS NULL
	END
