CREATE PROCEDURE [dbo].[ImageGetByRoomTypeId]
	@roomTypeId nvarchar(20)
AS
	SELECT i.* 
	FROM Image i INNER JOIN RoomTypeImage ri ON i.Id = ri.ImageId
	WHERE ri.RoomTypeId = @roomTypeId
