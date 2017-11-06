CREATE PROCEDURE [dbo].[RoomTypeGetByHouseId]
	@houseId nvarchar(20)
AS
	SELECT * FROM RoomType
	WHERE HouseId = @houseId
