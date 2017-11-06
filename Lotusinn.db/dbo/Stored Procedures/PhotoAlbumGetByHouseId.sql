CREATE PROCEDURE [dbo].[PhotoAlbumGetByHouseId]
	@houseId nvarchar(20)
AS
	SELECT * FROM PhotoAlbum
	WHERE HouseId = @houseId