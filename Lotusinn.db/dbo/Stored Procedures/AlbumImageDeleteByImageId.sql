CREATE PROCEDURE [dbo].[AlbumImageDeleteByImageId]
	@imageId nvarchar(20)
AS
	DELETE FROM AlbumImage
	WHERE ImageId = @imageId
