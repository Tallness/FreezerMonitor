CREATE TABLE [Freezer].[Sensors]
(
	[ID]           INT          IDENTITY NOT NULL,
	[SensorNumber] NVARCHAR(10)          NOT NULL,
	[Description]  NVARCHAR(25)          NOT NULL,

	CONSTRAINT PK_Sensors_ID PRIMARY KEY CLUSTERED (ID)
)
