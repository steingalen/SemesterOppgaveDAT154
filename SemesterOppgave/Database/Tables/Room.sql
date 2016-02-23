CREATE TABLE [dbo].[Room]
(
	[RoomNumber] INT NOT NULL PRIMARY KEY, 
    [Size] INT NOT NULL, 
    [Beds] INT NOT NULL, 
    [Quality] INT NOT NULL, 
    CONSTRAINT [FK_Room_RoomQuality] FOREIGN KEY ([Quality]) REFERENCES [RoomQuality]([Id]),
    CONSTRAINT [FK_Room_RoomBeds] FOREIGN KEY ([Beds]) REFERENCES [RoomBeds]([Id]),
    CONSTRAINT [FK_Room_RoomSize] FOREIGN KEY ([Size]) REFERENCES [RoomSize]([Id])
)

