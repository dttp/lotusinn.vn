CREATE PROCEDURE [dbo].[PhotoAlbumGetById]
	@id nvarchar(20)
AS
	SELECT * FROM PhotoAlbum
	WHERE Id = @id
