CREATE PROCEDURE [dbo].[FacilityItemInsert]
	@id nvarchar(20),
	@name nvarchar(100),
	@description nvarchar(500),
	@icon nvarchar(256)
AS
	INSERT INTO FacilityItem(Id, Name, Description, Icon)
	VALUES (@id, @name, @description, @icon)
