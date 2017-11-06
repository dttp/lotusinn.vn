CREATE TABLE [dbo].[RoomTypeImage] (
    [RoomTypeId] NVARCHAR (20) NOT NULL,
    [ImageId]    NVARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([ImageId] ASC, [RoomTypeId] ASC)
);

