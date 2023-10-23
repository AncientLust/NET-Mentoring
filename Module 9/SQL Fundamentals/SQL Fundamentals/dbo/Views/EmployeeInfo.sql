CREATE VIEW [dbo].[EmployeeInfo] AS 
	SELECT 
		e.Id AS EmployeeId,
		COALESCE(e.EmployeeName, p.FirstName + ' ' +p.LastName) AS EmployeeFullName,
		a.ZipCode + '_' + a.State + ',' +  a.City + '-'+ a.Street AS EmployeeFullAddress,
		e.CompanyName + '(' + e.Position + ')' AS  EmployeeCompanyInfo
	FROM dbo.Employee e
	LEFT JOIN dbo.Address a ON e.AddressId = a.Id
	LEFT JOIN dbo.Person p ON e.PersonId = p.Id
