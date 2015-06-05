CREATE TABLE [dbo].[Roosters](
	[RoosterID] [int] IDENTITY(1,1) NOT NULL,
	[RoosterLegBandNumber] [nvarchar](20) NOT NULL,
	[RoosterName] [nvarchar](100) NULL,
	[RoosterWeight] [decimal](6, 2) NOT NULL,
	[EntryID] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[LastUpdated] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Roosters] PRIMARY KEY CLUSTERED 
(
	[RoosterID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
)

GO

ALTER TABLE [dbo].[Roosters]  WITH CHECK ADD  CONSTRAINT [FK_Roosters_Entries] FOREIGN KEY([EntryID])
REFERENCES [dbo].[Entries] ([EntryID])
GO

ALTER TABLE [dbo].[Roosters] CHECK CONSTRAINT [FK_Roosters_Entries]
GO

ALTER TABLE [dbo].[Roosters] ADD  CONSTRAINT [DF_Roosters_RoosterLegBandNumber]  DEFAULT ((0)) FOR [RoosterLegBandNumber]
GO

ALTER TABLE [dbo].[Roosters] ADD  CONSTRAINT [DF_Roosters_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO


