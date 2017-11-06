CREATE PROCEDURE [dbo].[HouseUpdate]
	@id nvarchar(20),
	@order int,
	@name nvarchar(100),
	@address nvarchar(1000),
	@latitude float,
	@longitude float,	
	@thumbnail nvarchar(256)
AS
	UPDATE House
	SET [Order] = @order,
		Name = @name,
		Address = @address,
		Latitude = @latitude,
		Longitude = @longitude,		
		Thumbnail = @thumbnail
	WHERE Id = @id
