CREATE PROCEDURE [dbo].[HouseDelete]
	@id nvarchar(20)
AS
	EXEC RoomTypeDeleteByHouseId @id

	DELETE FROM Article
	WHERE Id IN (SELECT ArticleId FROM House WHERE Id = @id)

	DELETE FROM House
	WHERE Id = @id
