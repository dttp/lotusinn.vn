CREATE PROCEDURE [dbo].[RoomTypeImageInsert]
	@roomTypeId nvarchar(20),
	@imageId nvarchar(20)
AS
	INSERT INTO RoomTypeImage(RoomTypeId, ImageId)
	VALUES (@roomTypeId, @imageId)
