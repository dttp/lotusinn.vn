CREATE PROCEDURE [dbo].[RoomTypeDeleteByHouseId]
	@houseId nvarchar(20)
AS
	DECLARE @roomTypeId nvarchar(20)
	DECLARE roomTypeId_cursor CURSOR FOR
	SELECT Id FROM RoomType
	WHERE HouseId = @houseId

	OPEN roomTypeId_cursor

	FETCH NEXT FROM roomTypeId_cursor INTO @roomTypeId

	WHILE @@FETCH_STATUS = 0
	BEGIN
		EXEC RoomTypeDelete @roomTypeId
	END

	CLOSE roomTypeId_cursor
	DEALLOCATE roomTypeId_cursor