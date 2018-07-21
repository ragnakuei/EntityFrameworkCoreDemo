CREATE TABLE [dbo].[CompCvLanguageRequirement](
	[LanguageRequirement] [uniqueidentifier] NOT NULL,
	[CvId] [uniqueidentifier] NOT NULL,
	[Language] [tinyint] NOT NULL,
	[Listening] [tinyint] NOT NULL,
	[Reading] [tinyint] NOT NULL,
	[Speaking] [tinyint] NOT NULL,
	[Writing] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LanguageRequirement] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CompCvLanguageRequirement]  WITH CHECK ADD  CONSTRAINT [FK_CompCvLanguageRequirement_CompCV] FOREIGN KEY([CvId])
REFERENCES [dbo].[CompCV] ([CvId])
GO

ALTER TABLE [dbo].[CompCvLanguageRequirement] CHECK CONSTRAINT [FK_CompCvLanguageRequirement_CompCV]
GO

