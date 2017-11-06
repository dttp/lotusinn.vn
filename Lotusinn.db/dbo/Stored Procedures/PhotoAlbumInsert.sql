CREATE PROCEDURE [dbo].[PhotoAlbumInsert]
	@id nvarchar(20),
	@name nvarchar(100),
	@description nvarchar(500),
	@houseId nvarchar(20),
	@thumbnail nvarchar(256)
AS
	INSERT INTO PhotoAlbum(Id, Name, Description, HouseId, Thumbnail)
	VALUES (@id, @name, @description, @houseId, @thumbnail)
