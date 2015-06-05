CREATE TABLE [dbo].[SystemParameters](
	[ParameterID] [int] IDENTITY(1,1) NOT NULL,
	[ParameterCode] [nvarchar](5) NOT NULL,
	[ParameterDescription] [nvarchar](200) NULL,
	[ParameterValue] [nchar](200) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[LastUpdated] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_SystemParameters] PRIMARY KEY CLUSTERED 
(
	[ParameterID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
)

GO

ALTER TABLE [dbo].[SystemParameters] ADD  CONSTRAINT [DF_SystemParameters_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO