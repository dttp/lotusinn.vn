CREATE TABLE [dbo].[House]
(
	[Id] NVARCHAR(20) NOT NULL PRIMARY KEY, 
    [Order] INT NOT NULL DEFAULT 0, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Address] NVARCHAR(1000) NULL, 
    [Latitude] FLOAT NULL DEFAULT 0.0, 
    [Longitude] FLOAT NULL DEFAULT 0.0, 
    [ArticleId] NVARCHAR(20) NULL, 
    [Thumbnail] NVARCHAR(256) NULL
)
