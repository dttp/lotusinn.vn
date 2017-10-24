CREATE TABLE [dbo].[PhotoAlbum]
(
	[Id] NVARCHAR(20) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(100) NULL, 
    [Description] NVARCHAR(500) NULL, 
    [HouseId] NVARCHAR(20) NULL, 
    [Thumbnail] NVARCHAR(256) NULL
)
