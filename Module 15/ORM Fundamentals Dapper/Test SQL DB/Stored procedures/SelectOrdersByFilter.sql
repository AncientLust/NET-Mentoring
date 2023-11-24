CREATE PROCEDURE [dbo].[SelectOrdersByFilter]
	@orderCreatedMonth INT = NULL,
	@orderCreatedYear INT = NULL,
	@status NVARCHAR(50) = NULL,
	@productName NVARCHAR(50) = NULL
AS
	DECLARE @FetchedProductId INT

	IF @productName IS NOT NULL
	BEGIN
		SELECT @FetchedProductId = Id
		FROM dbo.Product
		WHERE [Name] = @productName

		IF @FetchedProductId IS NULL
			RETURN
	END

	SELECT * 
	FROM [dbo].[Order]
	WHERE (@orderCreatedMonth IS NULL OR month([CreatedDate]) = @orderCreatedMonth)
	AND (@status IS NULL OR [Status] = @status) 
	AND (@orderCreatedYear IS NULL OR year([CreatedDate]) = @orderCreatedYear)
	AND (@productName IS NULL OR ProductId = @FetchedProductId)
