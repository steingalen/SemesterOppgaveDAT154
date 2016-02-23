CREATE TABLE [dbo].[Reservation]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Room] INT NOT NULL, 
    [Customer] INT NOT NULL, 
    [Start] DATETIME NOT NULL, 
    [Slutt] DATETIME NOT NULL,
    CONSTRAINT [FK_Reservation_Customer] FOREIGN KEY ([Customer]) REFERENCES [Customer]([Id]), 
    CONSTRAINT [FK_Reservation_Room] FOREIGN KEY ([Room]) REFERENCES [Room]([RoomNumber])
)
