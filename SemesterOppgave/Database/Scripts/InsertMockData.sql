
-- Inserts data

-- RoomQuality
INSERT INTO [dbo].[RoomQuality] VALUES
(0, 'Good'),
(1, 'Fantastic'),
(2, 'Exceptional');

-- RoomSize
INSERT INTO [dbo].[RoomSize] VALUES
(0, 'Small'),
(1, 'Medium'),
(2, 'Large');

-- RoomBeds
INSERT INTO [dbo].[RoomBeds] VALUES
(0, 1),
(1, 2),
(2, 3);
 
-- Room
INSERT INTO [dbo].[Room] VALUES
(0, 0, 0, 0),
(1, 1, 0, 2),
(2, 1, 2, 0),
(3, 2, 1, 1),
(4, 1, 0, 2),
(5, 1, 1, 1),
(6, 2, 2, 2),
(7, 1, 2, 0),
(8, 2, 0, 1);

-- Customer
INSERT INTO [dbo].[Customer] VALUES
(0, 'something@gmail.com', 'Some', 'Thing', 'something'),
(1, 'max_mekker@gmail.com', 'Max', 'Mekker', 'sesam'),
(2, 'sort_gull@gmail.com', 'Kaptein', 'Sabeltann', 'money');

-- TaskType
INSERT INTO [dbo].[TaskType] VALUES
(0, 'RoomService'),
(1, 'Maintenance'),
(2, 'Cleaning');

-- RoomTasks
INSERT INTO [dbo].[RoomTask] VALUES
(0, 0, 2, '', 0),
(1, 2, 2, 'Filty filty room......', 1),
(2, 1, 0, 'Wants a burger', 0),
(3, 1, 1, 'Needs a new lightbulb.', 0);

-- Reservation
INSERT INTO [dbo].[Reservation] VALUES
(0, 0, 0, CAST('2016-02-10T14:00:00.000' AS datetime2), CAST('2016-02-20T12:00:00.000' AS datetime2)),
(1, 1, 1, CAST('2016-02-18T14:00:00.000' AS datetime2), CAST('2017-02-18T12:00:00.000' AS datetime2)),
(2, 1, 2, CAST('2016-02-16T14:00:00.000' AS datetime2), CAST('2016-02-18T12:00:00.000' AS datetime2)),
(3, 1, 2, CAST('2017-02-18T14:00:00.000' AS datetime2), CAST('2017-02-28T12:00:00.000' AS datetime2));