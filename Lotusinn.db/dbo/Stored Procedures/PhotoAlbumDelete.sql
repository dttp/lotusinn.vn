CREATE PROCEDURE [dbo].[PhotoAlbumDelete]
	@id nvarchar(20)
AS
	DELETE FROM PhotoAlbum
	WHERE Id = @id
