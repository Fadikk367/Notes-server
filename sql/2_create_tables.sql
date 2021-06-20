USE [notes_db]
GO


/****** Object:  FullTextCatalog [notes_catalog]    Script Date: 20.06.2021 02:12:34 ******/
CREATE FULLTEXT CATALOG [notes_catalog] WITH ACCENT_SENSITIVITY = ON
AS DEFAULT
GO


/****** Object:  Table [dbo].[Subjects]    Script Date: 20.06.2021 02:11:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Subjects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Subjects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



USE [notes_db]
GO

/****** Object:  Table [dbo].[Notes]    Script Date: 20.06.2021 02:11:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Notes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[SubjectId] [int] NOT NULL,
 CONSTRAINT [PK_Notes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Notes] ADD  DEFAULT ((0)) FOR [SubjectId]
GO

ALTER TABLE [dbo].[Notes]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Subjects_SubjectId] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subjects] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Notes] CHECK CONSTRAINT [FK_Notes_Subjects_SubjectId]
GO

/****** Object: SqlFullTextIndex Full-text Index on [dbo].[Notes] Script Date: 20.06.2021 02:17:01 ******/
CREATE FULLTEXT INDEX ON [dbo].[Notes]
    ([Content] LANGUAGE 2057)
    KEY INDEX [PK_Notes]
    ON [notes_catalog];

