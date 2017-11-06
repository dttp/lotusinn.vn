CREATE PROCEDURE [dbo].[AlbumImageDeleteByImageId]
	@imageId nvarchar(20)
AS
	DELETE FROM Image WHERE Id = @imageId

	DELETE FROM AlbumImage
	WHERE ImageId = @imageId
