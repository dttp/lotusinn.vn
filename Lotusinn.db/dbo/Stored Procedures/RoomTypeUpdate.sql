CREATE PROCEDURE [dbo].[RoomTypeUpdate]
	@id nvarchar(20),
	@houseId nvarchar(20),
	@name nvarchar(100),
	@features nvarchar(MAX),
	@price float
AS
	UPDATE RoomType
	SET HouseId = @houseId,
		Name = @name,
		Features = @features,
		Price = @price
	WHERE Id = @id