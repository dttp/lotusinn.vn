CREATE PROCEDURE [dbo].[HouseInsert]
	@id nvarchar(20),
	@order int,
	@name nvarchar(100),
	@address nvarchar(1000),
	@latitude float,
	@longitude float,
	@articleId nvarchar(20),
	@thumbnail nvarchar(256)
AS
	INSERT INTO House(Id, [Order], Name, Address, Latitude, Longitude, ArticleId, Thumbnail)
	VALUES (@id, @order, @name, @address, @latitude, @longitude, @articleId, @thumbnail)

