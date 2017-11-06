CREATE TABLE [dbo].[Feedback] (
    [Id]      NVARCHAR (20)  NOT NULL,
    [Name]    NVARCHAR (100) NULL,
    [Email]   NVARCHAR (100) NULL,
    [Message] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

