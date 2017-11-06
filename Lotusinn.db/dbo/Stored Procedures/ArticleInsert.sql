CREATE PROCEDURE [dbo].[ArticleInsert]
	@id nvarchar(20),
	@name nvarchar(100),
	@title nvarchar(200),
	@description nvarchar(500),
	@content nvarchar(MAX)
AS
	INSERT INTO Article(Id, Name, Title, Description, Content)
	VALUES (@id, @name, @title, @description, @content)
