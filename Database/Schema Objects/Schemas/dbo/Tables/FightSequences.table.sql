CREATE TABLE [dbo].[FightSequences]
(
	[SequenceID] [int] IDENTITY(1,1) NOT NULL,
	[DerbyID] [int] NOT NULL,
	[FightNumber] [int] NOT NULL,
	[Rooster1EntryNumber] [int] NOT NULL,
	[Rooster1Entry] [nvarchar](200) NOT NULL,
	[Rooster1ID] [int] NOT NULL,
	[Rooster1LB] [nvarchar](100) NOT NULL,
	[Rooster1Weight] [decimal](6, 2) NOT NULL,
	[Rooster2EntryNumber] [int] NOT NULL,
	[Rooster2Entry] [nvarchar](200) NOT NULL,
	[Rooster2ID] [int] NOT NULL,
	[Rooster2LB] [nvarchar](100) NOT NULL,
	[Rooster2Weight] [decimal](6, 2) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[LastUpdated] [datetime] NULL,
	[UpdatedBy] [nvarchar](200) NULL,
 CONSTRAINT [PK_FightSequences] PRIMARY KEY CLUSTERED 
(
	[SequenceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
)
GO

ALTER TABLE [dbo].[FightSequences]  WITH CHECK ADD  CONSTRAINT [FK_FightSequences_Derbies] FOREIGN KEY([DerbyID])
REFERENCES [dbo].[Derbies] ([DerbyID])
GO

ALTER TABLE [dbo].[FightSequences] CHECK CONSTRAINT [FK_FightSequences_Derbies]
GO


ALTER TABLE [dbo].[FightSequences]  WITH CHECK ADD  CONSTRAINT [FK_FightSequences_Roosters1] FOREIGN KEY([Rooster1ID])
REFERENCES [dbo].[Roosters] ([RoosterID])
GO

ALTER TABLE [dbo].[FightSequences] CHECK CONSTRAINT [FK_FightSequences_Roosters1]
GO


ALTER TABLE [dbo].[FightSequences]  WITH CHECK ADD  CONSTRAINT [FK_FightSequences_Roosters2] FOREIGN KEY([Rooster2ID])
REFERENCES [dbo].[Roosters] ([RoosterID])
GO

ALTER TABLE [dbo].[FightSequences] CHECK CONSTRAINT [FK_FightSequences_Roosters2]
GO


ALTER TABLE [dbo].[FightSequences] ADD  CONSTRAINT [DF_FightSequences_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO