CREATE PROCEDURE [dbo].[PhotoAlbumUpdate]
	@id nvarchar(20),
	@name nvarchar(100),
	@description nvarchar(500),
	@houseId nvarchar(20),
	@thumbnail nvarchar(256)
AS
	UPDATE PhotoAlbum
	SET	Name = @name,
		Description = @description,
		HouseId = @houseId,
		Thumbnail = @thumbnail
	WHERE Id = @id
