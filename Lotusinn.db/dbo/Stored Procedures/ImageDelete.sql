CREATE PROCEDURE [dbo].[ImageDelete]
	@id nvarchar(20)
AS
	DELETE FROM Image
	WHERE Id = @id
