CREATE PROCEDURE [dbo].[PrcInsertNewEmployee]
	@EmployeeName NVARCHAR(100) = NULL,	--(optional)
	@FirstName NVARCHAR(50),			--(optional) -- In task3 marked as optional but Person table has it not nullable (task1)
	@LastName NVARCHAR(50),				--(optional) -- In task3 marked as optional but Person table has it not nullable (task1)
	@CompanyName NVARCHAR(20),			--(required)
	@Position NVARCHAR(30) = NULL,		--(optional)
	@Street	NVARCHAR(50),				--(required)
	@City NVARCHAR(20) = NULL,			--(optional)
	@State NVARCHAR(50) = NULL,			--(optional)
	@ZipCode NVARCHAR(50) = NULL		--(optional)
AS
	IF 
		(@EmployeeName IS NULL OR TRIM(@EmployeeName) = '') AND 
		(@FirstName IS NULL OR TRIM(@FirstName) = '') AND 
		(@LastName IS NULL OR TRIM(@LastName) = '') 
	BEGIN
		PRINT 'At least one field (EmployeeName, FirstName, or LastName) must not be NULL, an empty string, or consist solely of space characters.'
		RETURN
	END
	
	SET @CompanyName = LEFT(@CompanyName, 20)

	-- Fill Person
	DECLARE @personId INT
	
	SELECT @personId = Id 
	FROM dbo.Person 
	WHERE (FirstName = @FirstName OR (FirstName IS NULL AND @FirstName IS NULL))
	AND (LastName = @LastName OR (LastName IS NULL AND @LastName IS NULL))

	IF @personId IS NULL
	BEGIN 
		INSERT INTO dbo.Person (FirstName, LastName) 
		VALUES (@FirstName, @LastName)
		SET @personId = SCOPE_IDENTITY()
	END

	-- Fill address
	DECLARE @addressId INT
	
	SELECT @addressId = Id
	FROM dbo.Address 
	WHERE (Street = @Street OR (Street IS NULL AND @Street IS NULL))
	AND (City = @City OR (City IS NULL AND @City IS NULL))
	AND (State = @State OR (State IS NULL AND @State IS NULL))
	AND (ZipCode = @ZipCode OR (ZipCode IS NULL AND @ZipCode IS NULL))

	IF @addressId IS NULL
	BEGIN 
		INSERT INTO dbo.Address(Street, City, State, ZipCode) 
		VALUES (@Street, @City, @State, @ZipCode)
		SET @addressId = SCOPE_IDENTITY()
	END

	-- Fill employee
	DECLARE @employeeId INT
	
	SELECT @employeeId = Id 
	FROM dbo.Employee 
	WHERE (CompanyName = @CompanyName OR (CompanyName IS NULL AND @CompanyName IS NULL))
	AND (EmployeeName = @EmployeeName OR (EmployeeName IS NULL AND @EmployeeName IS NULL))
	AND (AddressId = @AddressId OR (AddressId IS NULL AND @AddressId IS NULL))
	AND (PersonId = @PersonId OR (PersonId IS NULL AND @PersonId IS NULL))

	IF @employeeId IS NULL
	BEGIN 
		INSERT INTO dbo.Employee(AddressId, PersonId, CompanyName, Position, EmployeeName) 
		VALUES (@AddressId, @PersonId, @CompanyName, @Position, @EmployeeName)
	END

RETURN 0