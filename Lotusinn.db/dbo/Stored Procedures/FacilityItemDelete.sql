CREATE PROCEDURE [dbo].[FacilityItemDelete]
	@id nvarchar(20)
AS
	DELETE FROM FacilityItem
	WHERE Id = @id
