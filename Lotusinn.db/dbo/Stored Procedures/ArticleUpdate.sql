CREATE PROCEDURE [dbo].[ArticleUpdate]
	@id nvarchar(20),
	@name nvarchar(100),
	@title nvarchar(200),
	@description nvarchar(500),
	@content nvarchar(MAX)
AS
	UPDATE Article
	SET Name = @name,
		Title = @title,
		Description = @description,
		Content = @content
	WHERE Id = @id
