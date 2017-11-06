CREATE PROCEDURE [dbo].[AlbumImageInsert]
	@albumId nvarchar(20),
	@imageId nvarchar(20)
AS
	INSERT INTO AlbumImage(AlbumId, ImageId)
	VALUES (@albumId, @imageId)
