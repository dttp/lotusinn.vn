CREATE TABLE [dbo].[House] (
    [Id]        NVARCHAR (20)   NOT NULL,
    [Order]     INT             DEFAULT ((0)) NOT NULL,
    [Name]      NVARCHAR (100)  NOT NULL,
    [Address]   NVARCHAR (1000) NULL,
    [Latitude]  FLOAT (53)      DEFAULT ((0.0)) NULL,
    [Longitude] FLOAT (53)      DEFAULT ((0.0)) NULL,
    [ArticleId] NVARCHAR (20)   NULL,
    [Thumbnail] NVARCHAR (256)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

