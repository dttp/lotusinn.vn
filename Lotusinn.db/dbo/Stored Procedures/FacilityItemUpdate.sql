CREATE PROCEDURE [dbo].[FacilityItemUpdate]
	@id nvarchar(20),
	@name nvarchar(100),
	@description nvarchar(500),
	@icon nvarchar(256)
AS
	UPDATE FacilityItem
	SET Name = @name, 
		Description = @description,
		Icon = @icon
	WHERE Id = @id
