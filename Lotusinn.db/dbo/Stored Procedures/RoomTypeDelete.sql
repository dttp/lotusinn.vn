CREATE PROCEDURE [dbo].[RoomTypeDelete]
	@id nvarchar(20)
AS
	DELETE FROM Image
	WHERE Id IN (SELECT ImageId FROM RoomTypeImage WHERE RoomTypeId = @id);

	DELETE FROM RoomTypeImage
	WHERE RoomTypeId = @id;

	DELETE FROM Article
	WHERE Id IN (SELECT ArticleId FROM RoomType WHERE Id = @id)
	
	DELETE FROM RoomType
	WHERE Id = @id;
