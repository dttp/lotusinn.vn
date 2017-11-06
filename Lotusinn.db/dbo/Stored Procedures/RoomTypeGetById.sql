CREATE PROCEDURE [dbo].[RoomTypeGetById]
	@id nvarchar(20)
AS
	SELECT * FROM RoomType
	WHERE Id = @id
