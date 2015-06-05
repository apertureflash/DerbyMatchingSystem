CREATE TABLE [dbo].[NoFights](
	[NoFightID] [int] IDENTITY(1,1) NOT NULL,
	[EntryID1] [int] NOT NULL,
	[EntryID2] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[LastUpdated] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_NoFight] PRIMARY KEY CLUSTERED 
(
	[NoFightID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
)

GO

ALTER TABLE [dbo].[NoFights]  WITH CHECK ADD  CONSTRAINT [FK_NoFight_Entries] FOREIGN KEY([EntryID1])
REFERENCES [dbo].[Entries] ([EntryID])
GO

ALTER TABLE [dbo].[NoFights] CHECK CONSTRAINT [FK_NoFight_Entries]
GO

ALTER TABLE [dbo].[NoFights]  WITH CHECK ADD  CONSTRAINT [FK_NoFight_Entries1] FOREIGN KEY([EntryID2])
REFERENCES [dbo].[Entries] ([EntryID])
GO

ALTER TABLE [dbo].[NoFights] CHECK CONSTRAINT [FK_NoFight_Entries1]
GO

ALTER TABLE [dbo].[NoFights] ADD  CONSTRAINT [DF_NoFight_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO