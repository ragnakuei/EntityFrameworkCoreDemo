CREATE TABLE [dbo].[CompCvEducation](
	[EducationId] [uniqueidentifier] NOT NULL,
	[CvId] [uniqueidentifier] NOT NULL,
	[AcademicDegree] [tinyint] NOT NULL,
	[AcademyName] [nvarchar](50) NOT NULL,
	[AcademicDeptCategory] [tinyint] NOT NULL,
	[AcademicDeptName] [nvarchar](50) NOT NULL,
	[AttendanceSatrt] [datetime2](7) NULL,
	[AttendanceEnd] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[EducationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CompCvEducation]  WITH CHECK ADD  CONSTRAINT [FK_CompCvEducation_CompCV] FOREIGN KEY([CvId])
REFERENCES [dbo].[CompCV] ([CvId])
GO

ALTER TABLE [dbo].[CompCvEducation] CHECK CONSTRAINT [FK_CompCvEducation_CompCV]
GO
