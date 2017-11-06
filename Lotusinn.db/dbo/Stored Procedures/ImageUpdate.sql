CREATE PROCEDURE [dbo].[ImageUpdate]
	@id nvarchar(20),
	@name nvarchar(100),
	@description nvarchar(500),
	@path nvarchar(256)
AS
	UPDATE Image
	SET Name = @name,
		Description = @description,
		Path =@path
	WHERE Id = @id
