CREATE TABLE [dbo].[Article]
(
	[Id] nvarchar(20) NOT NULL PRIMARY KEY,
	[Name] nvarchar(100) NOT NULL,
	[Title] nvarchar(200) NOT NULL,
	[Description] nvarchar(500),
	[Content] nvarchar(max)
)
