CREATE TABLE [dbo].[Article] (
    [Id]          NVARCHAR (20)  NOT NULL,
    [Name]        NVARCHAR (100) NOT NULL,
    [Title]       NVARCHAR (200) NOT NULL,
    [Description] NVARCHAR (500) NULL,
    [Content]     NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

