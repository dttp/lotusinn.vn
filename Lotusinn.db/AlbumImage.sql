﻿CREATE TABLE [dbo].[AlbumImage]
(
	[AlbumId] NVARCHAR(20) NOT NULL , 
    [ImageId] NVARCHAR(20) NOT NULL, 
    PRIMARY KEY ([ImageId], [AlbumId])
)
