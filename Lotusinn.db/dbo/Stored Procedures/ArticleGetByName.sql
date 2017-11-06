CREATE PROCEDURE [dbo].[ArticleGetByName]
	@name nvarchar(100)
AS
	SELECT * FROM Article
	WHERE Name = @name
