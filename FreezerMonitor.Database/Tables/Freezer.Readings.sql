CREATE TABLE [Freezer].[Readings]
(
	[ID]          INT          IDENTITY NOT NULL,
	[Time]        DATETIME2(7)          NOT NULL,
	[SensorID]    INT                   NOT NULL,
	[Temperature] DECIMAL(7,4)          NOT NULL,

	CONSTRAINT PK_Readings_ID PRIMARY KEY CLUSTERED (ID),
	CONSTRAINT FK_Sensors_SensorID FOREIGN KEY (SensorID) REFERENCES Freezer.Sensors (ID)
)
