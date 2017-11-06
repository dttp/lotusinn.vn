CREATE TABLE [dbo].[FacilityItem] (
    [Id]          NVARCHAR (20)  NOT NULL,
    [Name]        NVARCHAR (100) NULL,
    [Description] NVARCHAR (500) NULL,
    [Icon]        NVARCHAR (256) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

