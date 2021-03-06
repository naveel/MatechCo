USE [Matechco]
GO
/****** Object:  StoredProcedure [dbo].[SP_GenerateProductCode]    Script Date: 11/25/2020 2:08:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GenerateProductCode]
	-- Add the parameters for the stored procedure here
	@Id int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select 
	(select [code] from tbl_productType where Id=@Id) as code , (select RIGHT(1000 + isnull(Max(Id),1), 3)  from tbl_product where ProductTypeId = @Id) as Id
END

GO
/****** Object:  Table [dbo].[tbl_product]    Script Date: 11/25/2020 2:08:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductTypeId] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Code] [nvarchar](50) NULL,
 CONSTRAINT [PK_tbl_product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_productType]    Script Date: 11/25/2020 2:08:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_productType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductType] [nvarchar](50) NULL,
	[Code] [nvarchar](50) NULL,
 CONSTRAINT [PK_tbl_productType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_users]    Script Date: 11/25/2020 2:08:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_users](
	[user_sk] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](500) NULL,
	[password] [nvarchar](500) NULL,
	[UserFullName] [varchar](50) NULL,
	[email] [nvarchar](50) NULL,
	[created_by] [int] NULL,
	[created_on] [datetime] NULL,
 CONSTRAINT [PK_tbl_users] PRIMARY KEY CLUSTERED 
(
	[user_sk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_product] ON 

INSERT [dbo].[tbl_product] ([Id], [ProductTypeId], [Name], [Code]) VALUES (2, 1, N'Test', N'abc-001')
INSERT [dbo].[tbl_product] ([Id], [ProductTypeId], [Name], [Code]) VALUES (3, 2, N'Test2', N'def-001')
INSERT [dbo].[tbl_product] ([Id], [ProductTypeId], [Name], [Code]) VALUES (4, 1, N'Test3', N'abc-002')
SET IDENTITY_INSERT [dbo].[tbl_product] OFF
SET IDENTITY_INSERT [dbo].[tbl_productType] ON 

INSERT [dbo].[tbl_productType] ([Id], [ProductType], [Code]) VALUES (1, N'Simple', N'abc')
INSERT [dbo].[tbl_productType] ([Id], [ProductType], [Code]) VALUES (2, N'Group', N'def')
SET IDENTITY_INSERT [dbo].[tbl_productType] OFF
SET IDENTITY_INSERT [dbo].[tbl_users] ON 

INSERT [dbo].[tbl_users] ([user_sk], [username], [password], [UserFullName], [email], [created_by], [created_on]) VALUES (1, N'wsZGEndsbBs=', N'lu0c+GqlVMQ=', N'Naveel 1', N'abc@abc.con', 1, NULL)
INSERT [dbo].[tbl_users] ([user_sk], [username], [password], [UserFullName], [email], [created_by], [created_on]) VALUES (2, N'LMsD34w/its=', N'lu0c+GqlVMQ=', N'Naveel2', N'abc@abc.com', NULL, NULL)
SET IDENTITY_INSERT [dbo].[tbl_users] OFF
