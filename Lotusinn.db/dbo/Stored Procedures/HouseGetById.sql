CREATE PROCEDURE [dbo].[HouseGetById]
	@id nvarchar(20)
AS
	SELECT *
	FROM House 
	WHERE Id = @id
