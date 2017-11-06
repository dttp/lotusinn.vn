CREATE PROCEDURE [dbo].[ImageInsert]
	@id nvarchar(20),
	@name nvarchar(100),
	@description nvarchar(500),
	@path nvarchar(256)
AS
	INSERT INTO Image(Id, Name, Description, Path)
	VALUES (@id, @name, @description, @path)
