CREATE PROCEDURE [dbo].[ArticleDelete]
	@id nvarchar(20)
AS
	DELETE FROM Article
	WHERE Id = @id
