CREATE TABLE [dbo].[HandPickedMatches](
	[MatchUpID] [int] IDENTITY(1,1) NOT NULL,
	[DerbyID] [int] NOT NULL,
	[Rooster1ID] [int] NOT NULL,
	[Rooster1LB] [nvarchar](100) NOT NULL,
	[Rooster1EntryNumber] [int] NOT NULL,
	[Rooster1Entry] [nvarchar](200) NOT NULL,
	[Rooster1Weight] [decimal](6, 2) NOT NULL,
	[Rooster2ID] [int] NOT NULL,
	[Rooster2LB] [nvarchar](100) NOT NULL,
	[Rooster2EntryNumber] [int] NOT NULL,
	[Rooster2Entry] [nvarchar](200) NULL,
	[Rooster2Weight] [decimal](6, 2) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[LastUpdated] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_HandPickedMatch] PRIMARY KEY CLUSTERED 
(
	[MatchUpID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
)

GO

ALTER TABLE [dbo].[HandPickedMatches] ADD  CONSTRAINT [DF_HandPickedMatches_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO