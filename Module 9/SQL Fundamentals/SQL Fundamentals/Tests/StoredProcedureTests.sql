-- Test 1: Provide only mandatory parameters
EXEC [dbo].[PrcInsertNewEmployee] 
	@FirstName = 'FirstName_sp_test_1',
	@LastName = 'LastName_sp_test_1',
	@CompanyName = 'Company_sp_test_1',
	@Street = 'Street_sp_test_1'

-- Test 2: Provide all parameters
EXEC [dbo].[PrcInsertNewEmployee]
	@EmployeeName = 'EmployeeName_sp_test_2',
	@FirstName = 'FirstName_sp_test_2',
	@LastName = 'LastName_sp_test_2',
	@CompanyName = 'Company_sp_test_2',
	@Position = 'Position_sp_test_2',
	@Street = 'Street_sp_test_2',
	@City = 'City_sp_test_2',
	@State = 'State_sp_test_2',
	@ZipCode = 'ZipCode_sp_test_2'

-- Test 3: Only FirstName and LastName are provided without EmployeeName
EXEC [dbo].[PrcInsertNewEmployee]
	@FirstName = 'FirstName_sp_test_3',
	@LastName = 'LastName_sp_test_3',
	@CompanyName = 'Company_sp_test_3',
	@Street = 'Street_sp_test_3'

-- Test 4: Only EmployeeName is provided, without FirstName and LastName
-- This test should now return an error since FirstName and LastName are mandatory
EXEC [dbo].[PrcInsertNewEmployee]
	@EmployeeName = 'EmployeeName_sp_test_4',
	@CompanyName = 'Company_sp_test_4',
	@Street = 'Street_sp_test_4'
