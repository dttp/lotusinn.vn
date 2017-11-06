CREATE PROCEDURE [dbo].[RoomTypeImageDeleteByImageId]
	@imageId nvarchar(20)
AS
	DELETE FROM RoomTypeImage
	WHERE ImageId = @imageId

	DELETE FROM Image
	WHERE Id = @imageId