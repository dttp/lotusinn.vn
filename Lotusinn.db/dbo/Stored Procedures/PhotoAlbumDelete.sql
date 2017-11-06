CREATE PROCEDURE [dbo].[PhotoAlbumDelete]
	@id nvarchar(20)
AS
	DELETE FROM Image WHERE Id IN (SELECT ImageId FROM AlbumImage WHERE AlbumId = @id)

	DELETE FROM AlbumImage WHERE AlbumId = @id

	DELETE FROM PhotoAlbum WHERE Id = @id
