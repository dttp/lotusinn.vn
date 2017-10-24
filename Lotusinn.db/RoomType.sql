CREATE TABLE [dbo].[RoomType]
(
	[Id] NVARCHAR(20) NOT NULL PRIMARY KEY, 
    [HouseId] NVARCHAR(20) NULL, 
    [Name] NVARCHAR(100) NOT NULL, 
    [ArticleId] NVARCHAR(20) NULL, 
    [Features] NVARCHAR(MAX) NULL
)
