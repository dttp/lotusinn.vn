CREATE PROCEDURE [dbo].[RoomTypeInsert]
	@id nvarchar(20),
	@houseId nvarchar(20),
	@name nvarchar(100),
	@articleId nvarchar(20),
	@features nvarchar(MAX),
	@price float
AS
	INSERT INTO RoomType(Id, HouseId, Name, ArticleId, Features, Price)
	VALUES (@id, @houseId, @name, @articleId, @features, @price)
