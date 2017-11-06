CREATE PROCEDURE [dbo].[ImageGetByAlbumId]
	@albumId nvarchar(20)
AS
	SELECT i.* from Image i INNER JOIN AlbumImage ai ON i.Id = ai.ImageId
	WHERE ai.AlbumId = @albumId
