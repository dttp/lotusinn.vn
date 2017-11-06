CREATE PROCEDURE [dbo].[AlbumImageDeleteByAlnumId]
	@albumId nvarchar(20)
AS
	DELETE FROM AlbumImage
	WHERE AlbumId = @albumId
