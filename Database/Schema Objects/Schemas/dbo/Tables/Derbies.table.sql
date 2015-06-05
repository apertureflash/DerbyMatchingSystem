CREATE TABLE [dbo].[Derbies](
	[DerbyID] [int] IDENTITY(1,1) NOT NULL,
	[LocationID] [int] NULL,
	[DerbyName] [nvarchar](200) NOT NULL,
	[DerbyDate] [datetime] NULL,
	[NumberOfEntries] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](200) NOT NULL,
	[LastUpdated] [datetime] NULL,
	[UpdatedBy] [nvarchar](200) NULL,
 CONSTRAINT [PK_Derbies] PRIMARY KEY CLUSTERED 
(
	[DerbyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
)
GO

ALTER TABLE [dbo].[Derbies]  WITH CHECK ADD  CONSTRAINT [FK_Derbies_Locations] FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([LocationID])
GO

ALTER TABLE [dbo].[Derbies] CHECK CONSTRAINT [FK_Derbies_Locations]
GO

ALTER TABLE [dbo].[Derbies] ADD  CONSTRAINT [DF_Derbies_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO