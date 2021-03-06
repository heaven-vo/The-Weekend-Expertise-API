
USE [dbTWE]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2/14/2022 1:16:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cafes]    Script Date: 2/14/2022 1:16:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cafes](
	[Id] [nvarchar](50) NOT NULL,
	[Image] [nvarchar](250) NULL,
	[OpenTime] [nvarchar](50) NULL,
	[CloseTime] [nvarchar](50) NULL,
	[Street] [nvarchar](150) NOT NULL,
	[Longitude] [nvarchar](50) NULL,
	[Latitude] [nvarchar](50) NULL,
	[Distric] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](350) NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Cafes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Major]    Script Date: 2/14/2022 1:16:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Major](
	[Id] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[Status] [nvarchar](20) NULL,
 CONSTRAINT [PK_Specialized] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Members]    Script Date: 2/14/2022 1:16:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Members](
	[Id] [nvarchar](50) NOT NULL,
	[Fullname] [nvarchar](150) NULL,
	[Image] [nvarchar](250) NULL,
	[Address] [nvarchar](350) NULL,
	[Phone] [nvarchar](20) NULL,
	[Sex] [nvarchar](20) NULL,
	[MajorId] [nvarchar](50) NULL,
	[Birthday] [datetime2](2) NOT NULL,
	[Status] [nvarchar](20) NULL,
 CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MemberSession]    Script Date: 2/14/2022 1:16:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberSession](
	[Id] [nvarchar](50) NOT NULL,
	[Status] [nvarchar](50) NULL,
	[MemberId] [nvarchar](50) NULL,
	[Voting] [float] NULL,
	[Feedback] [nvarchar](450) NULL,
	[SessionId] [nvarchar](50) NULL,
 CONSTRAINT [PK_MemberSession] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MentorMajor]    Script Date: 2/14/2022 1:16:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MentorMajor](
	[Id] [nvarchar](50) NOT NULL,
	[MentorId] [nvarchar](50) NULL,
	[MajorId] [nvarchar](50) NULL,
 CONSTRAINT [PK_MentorMajor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mentors]    Script Date: 2/14/2022 1:16:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mentors](
	[Id] [nvarchar](50) NOT NULL,
	[Fullname] [nvarchar](150) NULL,
	[Address] [nvarchar](250) NULL,
	[Phone] [nvarchar](20) NULL,
	[Image] [nvarchar](250) NULL,
	[Sex] [nvarchar](20) NULL,
	[Price] [float] NOT NULL,
	[Birthday] [datetime2](2) NOT NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Mentors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MentorSession]    Script Date: 2/14/2022 1:16:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MentorSession](
	[Id] [nvarchar](50) NOT NULL,
	[MentorId] [nvarchar](50) NULL,
	[SessionId] [nvarchar](50) NULL,
	[RequestDate] [datetime2](2) NULL,
	[AcceptDate] [datetime2](2) NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MentorSession] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MentorSkills]    Script Date: 2/14/2022 1:16:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MentorSkills](
	[Id] [nvarchar](50) NOT NULL,
	[SkillId] [nvarchar](50) NULL,
	[MentorId] [nvarchar](50) NULL,
 CONSTRAINT [PK_MentorSkills] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Payment]    Script Date: 2/14/2022 1:16:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[Id] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[Status] [nvarchar](50) NULL,
	[SessionId] [nvarchar](50) NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 2/14/2022 1:16:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Session]    Script Date: 2/14/2022 1:16:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[Id] [nvarchar](50) NOT NULL,
	[Slot] [int] NOT NULL,
	[Date] [datetime2](2) NOT NULL,
	[MaxPerson] [int] NOT NULL,
	[Status] [nvarchar](50) NULL,
	[MentorId] [nvarchar](50) NULL,
	[MemberId] [nvarchar](50) NULL,
	[SubjectId] [nvarchar](50) NULL,
	[CafeId] [nvarchar](50) NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Skills]    Script Date: 2/14/2022 1:16:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skills](
	[Id] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Skills] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Subject]    Script Date: 2/14/2022 1:16:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[Id] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[MajorId] [nvarchar](50) NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 2/14/2022 1:16:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[RoleId] [nvarchar](50) NOT NULL,
	[Status] [nvarchar](20) NULL,
	[Email] [nvarchar](250) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Voucher]    Script Date: 2/14/2022 1:16:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voucher](
	[Id] [nvarchar](450) NOT NULL,
	[DayFilter] [nchar](10) NULL,
	[HourFilter] [nchar](10) NULL,
	[DiscountRate] [nchar](10) NULL,
	[MinPerson] [nchar](10) NULL,
	[MaxAmount] [nchar](10) NULL,
	[StartDate] [nchar](10) NULL,
	[EndDate] [nchar](10) NULL,
	[CafeId] [nvarchar](50) NULL,
 CONSTRAINT [PK_Voucher] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220117154805_CreatDB', N'5.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220118143716_Add Skill relation', N'5.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220118152857_Updatev2.0', N'5.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220121073216_Add-Migration fixTabeMemberSessionName', N'5.0.13')
INSERT [dbo].[Major] ([Id], [Name], [Status]) VALUES (N'm1', N'IT', N'true')
INSERT [dbo].[Major] ([Id], [Name], [Status]) VALUES (N'm2', N'Bussiness', N'true')
INSERT [dbo].[Major] ([Id], [Name], [Status]) VALUES (N'm3', N'Accounting', N'true')
INSERT [dbo].[Major] ([Id], [Name], [Status]) VALUES (N'm4', N'Language', N'true')
INSERT [dbo].[Major] ([Id], [Name], [Status]) VALUES (N'm5', N'Commerce', N'true')
INSERT [dbo].[MentorMajor] ([Id], [MentorId], [MajorId]) VALUES (N'1', N'm1', N'm1')
INSERT [dbo].[MentorMajor] ([Id], [MentorId], [MajorId]) VALUES (N'2', N'm1', N'm2')
INSERT [dbo].[MentorMajor] ([Id], [MentorId], [MajorId]) VALUES (N'3', N'm1', N'm3')
INSERT [dbo].[MentorMajor] ([Id], [MentorId], [MajorId]) VALUES (N'4', N'm2', N'm2')
INSERT [dbo].[MentorMajor] ([Id], [MentorId], [MajorId]) VALUES (N'5', N'm3', N'm5')
INSERT [dbo].[MentorMajor] ([Id], [MentorId], [MajorId]) VALUES (N'6', N'm4', N'm4')
INSERT [dbo].[Mentors] ([Id], [Fullname], [Address], [Phone], [Image], [Sex], [Price], [Birthday], [Status]) VALUES (N'm1', N'duongas', N'asd', N'124545', N'xxxxx', N'male', 654, CAST(N'2000-12-01 00:00:00.0000000' AS DateTime2), N'true')
INSERT [dbo].[Mentors] ([Id], [Fullname], [Address], [Phone], [Image], [Sex], [Price], [Birthday], [Status]) VALUES (N'm2', N'conglead', N'ghls', N'124445', N'xxxx', N'male', 32, CAST(N'2000-12-01 00:00:00.0000000' AS DateTime2), N'true')
INSERT [dbo].[Mentors] ([Id], [Fullname], [Address], [Phone], [Image], [Sex], [Price], [Birthday], [Status]) VALUES (N'm3', N'tamlol', N'herf', N'633335', N'yyyyy', N'female', 54, CAST(N'2000-12-01 00:00:00.0000000' AS DateTime2), N'true')
INSERT [dbo].[Mentors] ([Id], [Fullname], [Address], [Phone], [Image], [Sex], [Price], [Birthday], [Status]) VALUES (N'm4', N'thaidb', N'jutt', N'3545', N'vvvvv', N'male', 577, CAST(N'2000-12-01 00:00:00.0000000' AS DateTime2), N'true')
INSERT [dbo].[MentorSkills] ([Id], [SkillId], [MentorId]) VALUES (N'1', N's1', N'm1')
INSERT [dbo].[MentorSkills] ([Id], [SkillId], [MentorId]) VALUES (N'2', N's2', N'm1')
INSERT [dbo].[MentorSkills] ([Id], [SkillId], [MentorId]) VALUES (N'3', N's4', N'm1')
INSERT [dbo].[MentorSkills] ([Id], [SkillId], [MentorId]) VALUES (N'4', N's2', N'm2')
INSERT [dbo].[MentorSkills] ([Id], [SkillId], [MentorId]) VALUES (N'5', N's3', N'm3')
INSERT [dbo].[MentorSkills] ([Id], [SkillId], [MentorId]) VALUES (N'6', N's5', N'm4')
INSERT [dbo].[Role] ([Id], [Name], [Status]) VALUES (N'1         ', N'E         ', N'E         ')
INSERT [dbo].[Role] ([Id], [Name], [Status]) VALUES (N'2         ', N'A         ', N'A         ')
INSERT [dbo].[Skills] ([Id], [Name]) VALUES (N's1', N'eat')
INSERT [dbo].[Skills] ([Id], [Name]) VALUES (N's2', N'run')
INSERT [dbo].[Skills] ([Id], [Name]) VALUES (N's3', N'talk')
INSERT [dbo].[Skills] ([Id], [Name]) VALUES (N's4', N'do')
INSERT [dbo].[Skills] ([Id], [Name]) VALUES (N's5', N'quatre')
INSERT [dbo].[Subject] ([Id], [Name], [MajorId]) VALUES (N's1', N'PRM', N'm1')
INSERT [dbo].[Subject] ([Id], [Name], [MajorId]) VALUES (N's2', N'AEH', N'm2')
INSERT [dbo].[Subject] ([Id], [Name], [MajorId]) VALUES (N's3', N'DHY', N'm3')
INSERT [dbo].[Subject] ([Id], [Name], [MajorId]) VALUES (N's4', N'ACC', N'm4')
INSERT [dbo].[Subject] ([Id], [Name], [MajorId]) VALUES (N's5', N'MOH', N'm5')
INSERT [dbo].[Subject] ([Id], [Name], [MajorId]) VALUES (N's6', N'HCI', N'm1')
INSERT [dbo].[Subject] ([Id], [Name], [MajorId]) VALUES (N's7', N'CSI', N'm1')
INSERT [dbo].[Subject] ([Id], [Name], [MajorId]) VALUES (N's8', N'EAC', N'm1')
INSERT [dbo].[User] ([Id], [Password], [Name], [RoleId], [Status], [Email]) VALUES (N'1', N'1', N'as', N'1', N'true', NULL)
INSERT [dbo].[User] ([Id], [Password], [Name], [RoleId], [Status], [Email]) VALUES (N'2', N'2', N'df', N'2', N'true', NULL)
ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [FK_Members_Major] FOREIGN KEY([MajorId])
REFERENCES [dbo].[Major] ([Id])
GO
ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [FK_Members_Major]
GO
ALTER TABLE [dbo].[MemberSession]  WITH CHECK ADD  CONSTRAINT [FK_MemberSession_Members_MemberId] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[MemberSession] CHECK CONSTRAINT [FK_MemberSession_Members_MemberId]
GO
ALTER TABLE [dbo].[MemberSession]  WITH CHECK ADD  CONSTRAINT [FK_MemberSession_Session_SessionId] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([Id])
GO
ALTER TABLE [dbo].[MemberSession] CHECK CONSTRAINT [FK_MemberSession_Session_SessionId]
GO
ALTER TABLE [dbo].[MentorMajor]  WITH CHECK ADD  CONSTRAINT [FK_MentorMajor_Major] FOREIGN KEY([MajorId])
REFERENCES [dbo].[Major] ([Id])
GO
ALTER TABLE [dbo].[MentorMajor] CHECK CONSTRAINT [FK_MentorMajor_Major]
GO
ALTER TABLE [dbo].[MentorMajor]  WITH CHECK ADD  CONSTRAINT [FK_MentorMajor_Mentors] FOREIGN KEY([MentorId])
REFERENCES [dbo].[Mentors] ([Id])
GO
ALTER TABLE [dbo].[MentorMajor] CHECK CONSTRAINT [FK_MentorMajor_Mentors]
GO
ALTER TABLE [dbo].[MentorSession]  WITH CHECK ADD  CONSTRAINT [FK_MentorSession_Mentors] FOREIGN KEY([MentorId])
REFERENCES [dbo].[Mentors] ([Id])
GO
ALTER TABLE [dbo].[MentorSession] CHECK CONSTRAINT [FK_MentorSession_Mentors]
GO
ALTER TABLE [dbo].[MentorSession]  WITH CHECK ADD  CONSTRAINT [FK_MentorSession_Session] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([Id])
GO
ALTER TABLE [dbo].[MentorSession] CHECK CONSTRAINT [FK_MentorSession_Session]
GO
ALTER TABLE [dbo].[MentorSkills]  WITH CHECK ADD  CONSTRAINT [FK_MentorSkills_Mentors_MentorId] FOREIGN KEY([MentorId])
REFERENCES [dbo].[Mentors] ([Id])
GO
ALTER TABLE [dbo].[MentorSkills] CHECK CONSTRAINT [FK_MentorSkills_Mentors_MentorId]
GO
ALTER TABLE [dbo].[MentorSkills]  WITH CHECK ADD  CONSTRAINT [FK_MentorSkills_Skills_SkillId] FOREIGN KEY([SkillId])
REFERENCES [dbo].[Skills] ([Id])
GO
ALTER TABLE [dbo].[MentorSkills] CHECK CONSTRAINT [FK_MentorSkills_Skills_SkillId]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Session] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([Id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Session]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_Cafes_CafeId] FOREIGN KEY([CafeId])
REFERENCES [dbo].[Cafes] ([Id])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_Cafes_CafeId]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_Members] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_Members]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_Mentors] FOREIGN KEY([MentorId])
REFERENCES [dbo].[Mentors] ([Id])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_Mentors]
GO
ALTER TABLE [dbo].[Subject]  WITH CHECK ADD  CONSTRAINT [FK_Subject_Major] FOREIGN KEY([MajorId])
REFERENCES [dbo].[Major] ([Id])
GO
ALTER TABLE [dbo].[Subject] CHECK CONSTRAINT [FK_Subject_Major]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_Cafes] FOREIGN KEY([CafeId])
REFERENCES [dbo].[Cafes] ([Id])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_Cafes]
GO
USE [master]
GO
ALTER DATABASE [dbTWE] SET  READ_WRITE 
GO
