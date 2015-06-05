CREATE TABLE [dbo].[Entries](
	[EntryID] [int] IDENTITY(1,1) NOT NULL,
	[DerbyID] [int] NOT NULL,
	[HandlerID] [int] NULL,
	[EntryNumber] [int] NOT NULL,
	[EntryName] [nvarchar](200) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[LastUpdated] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Entries] PRIMARY KEY CLUSTERED 
(
	[EntryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
)

GO


ALTER TABLE [dbo].[Entries]  WITH CHECK ADD  CONSTRAINT [FK_Entries_Derbies] FOREIGN KEY([DerbyID])
REFERENCES [dbo].[Derbies] ([DerbyID])
GO

ALTER TABLE [dbo].[Entries] CHECK CONSTRAINT [FK_Entries_Derbies]
GO

ALTER TABLE [dbo].[Entries] ADD  CONSTRAINT [DF_Entries_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO


