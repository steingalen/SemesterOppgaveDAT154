CREATE TABLE [dbo].[RoomTask]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Room] INT NOT NULL, 
    [Type] INT NOT NULL, 
    [Comments] TEXT NOT NULL, 
    [Status] INT NOT NULL, 
    CONSTRAINT [FK_RoomTask_TaskType] FOREIGN KEY ([Type]) REFERENCES [TaskType]([Id]), 
    CONSTRAINT [FK_RoomTask_Room] FOREIGN KEY ([Room]) REFERENCES [Room]([RoomNumber]),
)
