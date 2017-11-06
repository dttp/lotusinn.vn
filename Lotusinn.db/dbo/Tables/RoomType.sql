CREATE TABLE [dbo].[RoomType] (
    [Id]        NVARCHAR (20)  NOT NULL,
    [HouseId]   NVARCHAR (20)  NULL,
    [Name]      NVARCHAR (100) NOT NULL,
    [ArticleId] NVARCHAR (20)  NULL,
    [Features]  NVARCHAR (MAX) NULL,
    [Price] FLOAT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

