CREATE TABLE [dbo].[Handlers](
	[HandlerID] [int] IDENTITY(1,1) NOT NULL,
	[HandlerName] [nvarchar](200) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[LastUpdated] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Handlers] PRIMARY KEY CLUSTERED 
(
	[HandlerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
)

GO

ALTER TABLE [dbo].[Handlers] ADD  CONSTRAINT [DF_Handlers_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO


