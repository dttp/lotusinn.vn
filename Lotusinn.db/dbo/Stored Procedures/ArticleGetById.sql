CREATE PROCEDURE [dbo].[ArticleGetById]
	@id nvarchar(20)
AS
	SELECT * FROM Article
	WHERE Id = @id
