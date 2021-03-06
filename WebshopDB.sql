USE [TBSDB]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 23-Mar-22 8:25:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 23-Mar-22 8:25:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 23-Mar-22 8:25:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 23-Mar-22 8:25:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 23-Mar-22 8:25:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[BOD] [datetime] NULL,
	[Gender] [int] NULL,
	[Address] [nvarchar](500) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[CreateBy] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[IsValid] [bit] NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbs_Cart]    Script Date: 23-Mar-22 8:25:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbs_Cart](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NULL,
	[TotalPrice] [float] NULL,
	[TotalQty] [int] NULL,
	[IsCheckout] [bit] NULL,
 CONSTRAINT [PK_Tbs_Cart] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbs_CartItem]    Script Date: 23-Mar-22 8:25:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbs_CartItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL,
	[CartID] [int] NULL,
	[Price] [float] NULL,
	[Qty] [int] NULL,
 CONSTRAINT [PK_Tbs_CartItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbs_Category]    Script Date: 23-Mar-22 8:25:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbs_Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](100) NULL,
	[Image] [nvarchar](500) NULL,
	[ParentID] [int] NULL,
	[CreateBy] [nvarchar](20) NULL,
	[CreateDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[Last_Updated_Date] [datetime] NULL,
 CONSTRAINT [PK_Tbs_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbs_Order]    Script Date: 23-Mar-22 8:25:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbs_Order](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NULL,
	[CartID] [int] NULL,
	[TotalPrice] [float] NULL,
	[TotalQty] [int] NULL,
	[Status] [int] NULL,
	[OrderDate] [datetime] NULL,
	[CompleteDate] [datetime] NULL,
	[IsCancel] [bit] NULL,
	[CancelBy] [nvarchar](50) NULL,
	[CancelDate] [datetime] NULL,
	[CancelReason] [nvarchar](500) NULL,
 CONSTRAINT [PK_Tbs_Order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbs_Product]    Script Date: 23-Mar-22 8:25:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbs_Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [float] NULL,
	[SKU] [nvarchar](20) NULL,
	[Inventory] [int] NULL,
	[Image] [nvarchar](200) NULL,
	[VAT] [int] NULL,
	[IsFreeShip] [bit] NULL,
	[CategoryID] [int] NULL,
	[CreateBy] [nvarchar](20) NULL,
	[CreateDate] [datetime] NULL,
	[Last_Updated_Date] [datetime] NULL,
	[IsActve] [bit] NULL,
 CONSTRAINT [PK_Tbs_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'Admin')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2', N'User')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'46bc0a9d-9f83-437a-85da-4003bcc0e15f', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'784635bc-1438-4bb4-97db-69a605743ec8', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd0519034-0559-4ba8-8ee1-9ef2ebc7a9e8', N'1')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [UserName], [BOD], [Gender], [Address], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [CreateBy], [CreateDate], [IsValid], [IsDelete]) VALUES (N'46bc0a9d-9f83-437a-85da-4003bcc0e15f', N'admin@gmail.com', N'admin@gmail.com', NULL, NULL, N'Khu Đ. Tú Xương, Lái Thiêu, Thuận An, Bình Dương, Vietnam', 0, N'AHpFtOqL5RTSv5JwQWg1WOZKeonaRGAW31pJ+FlscRFG6bsVmcF7FiDNQF27ZcjJow==', N'107035c9-7f0e-42a7-b2b6-cc71d8fe327b', NULL, 0, 0, NULL, 1, 0, N'user@gmail.com', CAST(N'2022-03-23T15:21:14.543' AS DateTime), 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [UserName], [BOD], [Gender], [Address], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [CreateBy], [CreateDate], [IsValid], [IsDelete]) VALUES (N'784635bc-1438-4bb4-97db-69a605743ec8', N'frankcntt@gmail.com', N'frankcntt@gmail.com', NULL, 1, N'Thuận An, Bình Dương', 0, N'ALUmd1q8YsAtWVgIqcCT7vR4tLeB88swDOI9/v7ix36eN9S48DmKDqPy72UgUCQk2Q==', N'9dc5b287-1052-46d6-8de9-afb2ebad4898', N'0367364522', 0, 0, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [UserName], [BOD], [Gender], [Address], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [CreateBy], [CreateDate], [IsValid], [IsDelete]) VALUES (N'd0519034-0559-4ba8-8ee1-9ef2ebc7a9e8', N'user@gmail.com', N'user@gmail.com', NULL, NULL, N'Thuận An, Bình Dương', 0, N'AGVmCXr8uWQQ1mln0fX0ZRHx6shtSkuZW+/jZxxKOKISdRgICiLGb7loraHLawvihw==', N'f3dc41f4-1107-4f28-b5fb-1024eeee801b', N'0367364522', 0, 0, NULL, 1, 0, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Tbs_Cart] ON 

INSERT [dbo].[Tbs_Cart] ([ID], [UserID], [TotalPrice], [TotalQty], [IsCheckout]) VALUES (17, N'784635bc-1438-4bb4-97db-69a605743ec8', NULL, NULL, 1)
INSERT [dbo].[Tbs_Cart] ([ID], [UserID], [TotalPrice], [TotalQty], [IsCheckout]) VALUES (18, N'784635bc-1438-4bb4-97db-69a605743ec8', NULL, NULL, 1)
INSERT [dbo].[Tbs_Cart] ([ID], [UserID], [TotalPrice], [TotalQty], [IsCheckout]) VALUES (19, N'784635bc-1438-4bb4-97db-69a605743ec8', NULL, NULL, 1)
INSERT [dbo].[Tbs_Cart] ([ID], [UserID], [TotalPrice], [TotalQty], [IsCheckout]) VALUES (20, N'784635bc-1438-4bb4-97db-69a605743ec8', NULL, NULL, 0)
INSERT [dbo].[Tbs_Cart] ([ID], [UserID], [TotalPrice], [TotalQty], [IsCheckout]) VALUES (21, N'd0519034-0559-4ba8-8ee1-9ef2ebc7a9e8', NULL, NULL, 1)
INSERT [dbo].[Tbs_Cart] ([ID], [UserID], [TotalPrice], [TotalQty], [IsCheckout]) VALUES (22, N'd0519034-0559-4ba8-8ee1-9ef2ebc7a9e8', NULL, NULL, 1)
INSERT [dbo].[Tbs_Cart] ([ID], [UserID], [TotalPrice], [TotalQty], [IsCheckout]) VALUES (23, N'd0519034-0559-4ba8-8ee1-9ef2ebc7a9e8', NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Tbs_Cart] OFF
SET IDENTITY_INSERT [dbo].[Tbs_CartItem] ON 

INSERT [dbo].[Tbs_CartItem] ([ID], [ProductID], [CartID], [Price], [Qty]) VALUES (3, 6, 17, 589637, 2)
INSERT [dbo].[Tbs_CartItem] ([ID], [ProductID], [CartID], [Price], [Qty]) VALUES (10, 5, 18, 502986, 1)
INSERT [dbo].[Tbs_CartItem] ([ID], [ProductID], [CartID], [Price], [Qty]) VALUES (11, 4, 18, 1143150, 1)
INSERT [dbo].[Tbs_CartItem] ([ID], [ProductID], [CartID], [Price], [Qty]) VALUES (12, 4, 19, 1143150, 5)
INSERT [dbo].[Tbs_CartItem] ([ID], [ProductID], [CartID], [Price], [Qty]) VALUES (13, 5, 19, 502986, 1)
INSERT [dbo].[Tbs_CartItem] ([ID], [ProductID], [CartID], [Price], [Qty]) VALUES (14, 4, 20, 6858900, 6)
INSERT [dbo].[Tbs_CartItem] ([ID], [ProductID], [CartID], [Price], [Qty]) VALUES (20, 4, 21, 2286300, 2)
INSERT [dbo].[Tbs_CartItem] ([ID], [ProductID], [CartID], [Price], [Qty]) VALUES (21, 5, 21, 502986, 1)
INSERT [dbo].[Tbs_CartItem] ([ID], [ProductID], [CartID], [Price], [Qty]) VALUES (22, 6, 21, 589637, 1)
INSERT [dbo].[Tbs_CartItem] ([ID], [ProductID], [CartID], [Price], [Qty]) VALUES (23, 4, 22, 1143150, 1)
INSERT [dbo].[Tbs_CartItem] ([ID], [ProductID], [CartID], [Price], [Qty]) VALUES (24, 5, 22, 502986, 1)
INSERT [dbo].[Tbs_CartItem] ([ID], [ProductID], [CartID], [Price], [Qty]) VALUES (25, 4, 23, 1143150, 1)
INSERT [dbo].[Tbs_CartItem] ([ID], [ProductID], [CartID], [Price], [Qty]) VALUES (26, 5, 23, 502986, 1)
INSERT [dbo].[Tbs_CartItem] ([ID], [ProductID], [CartID], [Price], [Qty]) VALUES (27, 5, 20, 502986, 1)
SET IDENTITY_INSERT [dbo].[Tbs_CartItem] OFF
SET IDENTITY_INSERT [dbo].[Tbs_Category] ON 

INSERT [dbo].[Tbs_Category] ([ID], [Name], [Description], [Image], [ParentID], [CreateBy], [CreateDate], [IsActive], [Last_Updated_Date]) VALUES (18, N'Modern Chair', N'Modern Chair', N'/Areas/Admin/Images/Category\1.jpg', NULL, N'frankcntt@gmail.com', CAST(N'2022-03-21T20:40:16.123' AS DateTime), 1, NULL)
INSERT [dbo].[Tbs_Category] ([ID], [Name], [Description], [Image], [ParentID], [CreateBy], [CreateDate], [IsActive], [Last_Updated_Date]) VALUES (19, N'Minimalistic Plant Pot', N'Minimalistic Plant Pot', N'/Areas/Admin/Images/Category\2.jpg', NULL, N'frankcntt@gmail.com', CAST(N'2022-03-21T20:41:32.007' AS DateTime), 1, NULL)
INSERT [dbo].[Tbs_Category] ([ID], [Name], [Description], [Image], [ParentID], [CreateBy], [CreateDate], [IsActive], [Last_Updated_Date]) VALUES (20, N'Night Stand', N'Night Stand', N'/Areas/Admin/Images/Category\3.jpg', NULL, N'frankcntt@gmail.com', CAST(N'2022-03-21T20:42:06.090' AS DateTime), 1, NULL)
INSERT [dbo].[Tbs_Category] ([ID], [Name], [Description], [Image], [ParentID], [CreateBy], [CreateDate], [IsActive], [Last_Updated_Date]) VALUES (21, N'Plant Pot', N'Plant Pot', N'/Areas/Admin/Images/Category\5.jpg', NULL, N'frankcntt@gmail.com', CAST(N'2022-03-21T20:42:23.040' AS DateTime), 1, NULL)
INSERT [dbo].[Tbs_Category] ([ID], [Name], [Description], [Image], [ParentID], [CreateBy], [CreateDate], [IsActive], [Last_Updated_Date]) VALUES (22, N'Small Table', N'Small Table', N'/Areas/Admin/Images/Category\6.jpg', NULL, N'frankcntt@gmail.com', CAST(N'2022-03-21T20:42:38.920' AS DateTime), 1, NULL)
INSERT [dbo].[Tbs_Category] ([ID], [Name], [Description], [Image], [ParentID], [CreateBy], [CreateDate], [IsActive], [Last_Updated_Date]) VALUES (23, N'Metallic Chair', N'Metallic Chair', N'/Areas/Admin/Images/Category\7.jpg', NULL, N'frankcntt@gmail.com', CAST(N'2022-03-21T20:42:53.410' AS DateTime), 1, NULL)
INSERT [dbo].[Tbs_Category] ([ID], [Name], [Description], [Image], [ParentID], [CreateBy], [CreateDate], [IsActive], [Last_Updated_Date]) VALUES (24, N'Modern Rocking Chair', N'Modern Rocking Chair', N'/Areas/Admin/Images/Category\8.jpg', NULL, N'frankcntt@gmail.com', CAST(N'2022-03-21T20:43:17.297' AS DateTime), 1, NULL)
INSERT [dbo].[Tbs_Category] ([ID], [Name], [Description], [Image], [ParentID], [CreateBy], [CreateDate], [IsActive], [Last_Updated_Date]) VALUES (25, N'Home Deco', N'Home Deco', N'/Areas/Admin/Images/Category\9.jpg', NULL, N'frankcntt@gmail.com', CAST(N'2022-03-21T20:43:43.660' AS DateTime), 1, NULL)
INSERT [dbo].[Tbs_Category] ([ID], [Name], [Description], [Image], [ParentID], [CreateBy], [CreateDate], [IsActive], [Last_Updated_Date]) VALUES (26, N'Dining Chairs', N'Dining Chairs', NULL, 18, N'frankcntt@gmail.com', CAST(N'2022-03-21T20:50:38.313' AS DateTime), 1, NULL)
INSERT [dbo].[Tbs_Category] ([ID], [Name], [Description], [Image], [ParentID], [CreateBy], [CreateDate], [IsActive], [Last_Updated_Date]) VALUES (27, N'Living Room Chairs', N'Living Room Chairs', NULL, 18, N'frankcntt@gmail.com', CAST(N'2022-03-21T20:50:44.043' AS DateTime), 1, NULL)
INSERT [dbo].[Tbs_Category] ([ID], [Name], [Description], [Image], [ParentID], [CreateBy], [CreateDate], [IsActive], [Last_Updated_Date]) VALUES (28, N'Office Chairs', N'Office Chairs', NULL, 18, N'frankcntt@gmail.com', CAST(N'2022-03-21T20:50:51.100' AS DateTime), 1, NULL)
INSERT [dbo].[Tbs_Category] ([ID], [Name], [Description], [Image], [ParentID], [CreateBy], [CreateDate], [IsActive], [Last_Updated_Date]) VALUES (29, N'Gaming Chairs', N'Gaming Chairs', NULL, 18, N'frankcntt@gmail.com', CAST(N'2022-03-21T20:50:57.220' AS DateTime), 1, NULL)
SET IDENTITY_INSERT [dbo].[Tbs_Category] OFF
SET IDENTITY_INSERT [dbo].[Tbs_Order] ON 

INSERT [dbo].[Tbs_Order] ([ID], [UserID], [CartID], [TotalPrice], [TotalQty], [Status], [OrderDate], [CompleteDate], [IsCancel], [CancelBy], [CancelDate], [CancelReason]) VALUES (5, N'784635bc-1438-4bb4-97db-69a605743ec8', 17, 589637, 2, 1, CAST(N'2022-03-22T21:58:29.870' AS DateTime), NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[Tbs_Order] ([ID], [UserID], [CartID], [TotalPrice], [TotalQty], [Status], [OrderDate], [CompleteDate], [IsCancel], [CancelBy], [CancelDate], [CancelReason]) VALUES (6, N'784635bc-1438-4bb4-97db-69a605743ec8', 18, 1646136, 2, 4, CAST(N'2022-03-22T22:01:25.180' AS DateTime), NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[Tbs_Order] ([ID], [UserID], [CartID], [TotalPrice], [TotalQty], [Status], [OrderDate], [CompleteDate], [IsCancel], [CancelBy], [CancelDate], [CancelReason]) VALUES (7, N'784635bc-1438-4bb4-97db-69a605743ec8', 19, 1646136, 6, 1, CAST(N'2022-03-23T08:27:06.413' AS DateTime), NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[Tbs_Order] ([ID], [UserID], [CartID], [TotalPrice], [TotalQty], [Status], [OrderDate], [CompleteDate], [IsCancel], [CancelBy], [CancelDate], [CancelReason]) VALUES (8, N'd0519034-0559-4ba8-8ee1-9ef2ebc7a9e8', 21, 3378923, 4, 2, CAST(N'2022-03-23T09:33:23.367' AS DateTime), NULL, 1, N'user@gmail.com', CAST(N'2022-03-23T12:21:37.157' AS DateTime), N'hết hàng')
INSERT [dbo].[Tbs_Order] ([ID], [UserID], [CartID], [TotalPrice], [TotalQty], [Status], [OrderDate], [CompleteDate], [IsCancel], [CancelBy], [CancelDate], [CancelReason]) VALUES (9, N'd0519034-0559-4ba8-8ee1-9ef2ebc7a9e8', 22, 1646136, 2, 1, CAST(N'2022-03-23T10:37:33.577' AS DateTime), NULL, 1, N'user@gmail.com', CAST(N'2022-03-23T11:19:11.620' AS DateTime), N'không muốn mua nữa')
INSERT [dbo].[Tbs_Order] ([ID], [UserID], [CartID], [TotalPrice], [TotalQty], [Status], [OrderDate], [CompleteDate], [IsCancel], [CancelBy], [CancelDate], [CancelReason]) VALUES (10, N'd0519034-0559-4ba8-8ee1-9ef2ebc7a9e8', 22, 1646136, 2, 1, CAST(N'2022-03-23T12:30:28.237' AS DateTime), NULL, 1, N'user@gmail.com', CAST(N'2022-03-23T12:34:15.450' AS DateTime), N'không mua')
INSERT [dbo].[Tbs_Order] ([ID], [UserID], [CartID], [TotalPrice], [TotalQty], [Status], [OrderDate], [CompleteDate], [IsCancel], [CancelBy], [CancelDate], [CancelReason]) VALUES (11, N'd0519034-0559-4ba8-8ee1-9ef2ebc7a9e8', 23, 1646136, 2, 4, CAST(N'2022-03-23T12:35:46.133' AS DateTime), CAST(N'2022-03-23T12:44:58.890' AS DateTime), 0, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Tbs_Order] OFF
SET IDENTITY_INSERT [dbo].[Tbs_Product] ON 

INSERT [dbo].[Tbs_Product] ([ID], [Name], [Description], [Price], [SKU], [Inventory], [Image], [VAT], [IsFreeShip], [CategoryID], [CreateBy], [CreateDate], [Last_Updated_Date], [IsActve]) VALUES (4, N'Chairs Chair Home Furniture', N'Chairs Chair Home Furniture Coffee Hotel Solid Wood Dining Chairs Wishbone Chair', 1143150, N'1600404413354', 10, N'/Areas/Admin/Images/Product\hd58d541eeef1487880d65cd188256015g.jpg', 0, 1, 26, N'frankcntt@gmail.com', CAST(N'2022-03-21T21:09:14.927' AS DateTime), CAST(N'2022-03-21T22:12:13.947' AS DateTime), 1)
INSERT [dbo].[Tbs_Product] ([ID], [Name], [Description], [Price], [SKU], [Inventory], [Image], [VAT], [IsFreeShip], [CategoryID], [CreateBy], [CreateDate], [Last_Updated_Date], [IsActve]) VALUES (5, N'Wholesale Dining Chair Modern', N'Wholesale Dining Chair Modern Dining Room Furniture Metal Nordic Dining Chair Dinning Chair', 502986, N'1600463067534', 2, N'/Areas/Admin/Images/Product\hbc3dfb2a41014ca7902b900115524a86e.png', 10, 1, 26, N'frankcntt@gmail.com', CAST(N'2022-03-21T21:10:07.580' AS DateTime), CAST(N'2022-03-21T22:12:23.453' AS DateTime), 1)
INSERT [dbo].[Tbs_Product] ([ID], [Name], [Description], [Price], [SKU], [Inventory], [Image], [VAT], [IsFreeShip], [CategoryID], [CreateBy], [CreateDate], [Last_Updated_Date], [IsActve]) VALUES (6, N'Backrest dining chair', N'Backrest dining chair simple home furniture dining room modern leather chairs', 589637, N'1600437963998', 20, N'/Areas/Admin/Images/Product\h3b88f58e1f474fa29b9b7777772b005e3.jpg', 0, 0, 26, N'frankcntt@gmail.com', CAST(N'2022-03-21T21:10:52.740' AS DateTime), CAST(N'2022-03-21T22:12:33.173' AS DateTime), 1)
INSERT [dbo].[Tbs_Product] ([ID], [Name], [Description], [Price], [SKU], [Inventory], [Image], [VAT], [IsFreeShip], [CategoryID], [CreateBy], [CreateDate], [Last_Updated_Date], [IsActve]) VALUES (7, N'Italian Classic Luxury Modern', N'Italian Classic Luxury Modern Cheap Black Cafe Chair Velvet Restaurant Chairs Metal Gym Kitchen OEM Building Style Living Fabric', 308651, N'1600418126851', 100, N'/Areas/Admin/Images/Product\h466e9501ffbe4c9bb05a2c49de74371f5.jpg', 0, 1, 26, N'frankcntt@gmail.com', CAST(N'2022-03-21T21:12:01.653' AS DateTime), CAST(N'2022-03-21T22:12:41.357' AS DateTime), 1)
INSERT [dbo].[Tbs_Product] ([ID], [Name], [Description], [Price], [SKU], [Inventory], [Image], [VAT], [IsFreeShip], [CategoryID], [CreateBy], [CreateDate], [Last_Updated_Date], [IsActve]) VALUES (8, N'Luxury Velvet Simple Design Modern', N'Luxury Velvet Simple Design Modern Manufacturers Factory Price Dining Chair', 1188876, N'1600456862667', 50, N'/Areas/Admin/Images/Product\h6a9f68fc16d54e21a70d0e246aae6beex.png', 0, 1, 26, N'frankcntt@gmail.com', CAST(N'2022-03-21T21:13:15.943' AS DateTime), CAST(N'2022-03-21T22:12:49.430' AS DateTime), 1)
INSERT [dbo].[Tbs_Product] ([ID], [Name], [Description], [Price], [SKU], [Inventory], [Image], [VAT], [IsFreeShip], [CategoryID], [CreateBy], [CreateDate], [Last_Updated_Date], [IsActve]) VALUES (9, N'Living Room Metal Leg', N'AH 8380 75 Living Room Metal Leg Leisure Armchair Chair Cafe Fabric Restaurant Velvet Style Time Packing Modern Furniture Pcs', 983109, N'1600454197441', 10, N'/Areas/Admin/Images/Product\h334d7d24020348b59e5fc885e5c35a18q.jpg', 0, 1, 27, N'frankcntt@gmail.com', CAST(N'2022-03-21T21:13:59.283' AS DateTime), CAST(N'2022-03-21T22:14:52.790' AS DateTime), 1)
INSERT [dbo].[Tbs_Product] ([ID], [Name], [Description], [Price], [SKU], [Inventory], [Image], [VAT], [IsFreeShip], [CategoryID], [CreateBy], [CreateDate], [Last_Updated_Date], [IsActve]) VALUES (10, N'Luxury Grey Velvet Relaxing Lounge Chair', N'Luxury Grey Velvet Relaxing Lounge Chair Tufted Chair Living Room Chair with Golden Legs', 1.506672, N'1600454197441', 0, N'/Areas/Admin/Images/Product\h9b833066471048649a8fa15aed00e2d92.jpg', 0, 1, 27, N'frankcntt@gmail.com', CAST(N'2022-03-21T21:15:08.967' AS DateTime), CAST(N'2022-03-21T22:13:25.757' AS DateTime), 1)
INSERT [dbo].[Tbs_Product] ([ID], [Name], [Description], [Price], [SKU], [Inventory], [Image], [VAT], [IsFreeShip], [CategoryID], [CreateBy], [CreateDate], [Last_Updated_Date], [IsActve]) VALUES (11, N'Living room Upholstered Luxury Velvet', N'Living room Upholstered Luxury Velvet accent chair luxury with shinning legs', 845.931, N'1600455553994', 0, N'/Areas/Admin/Images/Product\hcfded61cc2ca44adb186313523cd1fc3p.jpg', 0, 1, 27, N'frankcntt@gmail.com', CAST(N'2022-03-21T21:15:38.190' AS DateTime), CAST(N'2022-03-21T22:13:33.707' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Tbs_Product] OFF
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Tbs_Cart]  WITH CHECK ADD  CONSTRAINT [FK_Tbs_Cart_AspNetUsers] FOREIGN KEY([UserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Tbs_Cart] CHECK CONSTRAINT [FK_Tbs_Cart_AspNetUsers]
GO
ALTER TABLE [dbo].[Tbs_CartItem]  WITH CHECK ADD  CONSTRAINT [FK_Tbs_CartItem_Tbs_Cart] FOREIGN KEY([CartID])
REFERENCES [dbo].[Tbs_Cart] ([ID])
GO
ALTER TABLE [dbo].[Tbs_CartItem] CHECK CONSTRAINT [FK_Tbs_CartItem_Tbs_Cart]
GO
ALTER TABLE [dbo].[Tbs_CartItem]  WITH CHECK ADD  CONSTRAINT [FK_Tbs_CartItem_Tbs_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Tbs_Product] ([ID])
GO
ALTER TABLE [dbo].[Tbs_CartItem] CHECK CONSTRAINT [FK_Tbs_CartItem_Tbs_Product]
GO
ALTER TABLE [dbo].[Tbs_Order]  WITH CHECK ADD  CONSTRAINT [FK_Tbs_Order_AspNetUsers] FOREIGN KEY([UserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Tbs_Order] CHECK CONSTRAINT [FK_Tbs_Order_AspNetUsers]
GO
ALTER TABLE [dbo].[Tbs_Order]  WITH CHECK ADD  CONSTRAINT [FK_Tbs_Order_Tbs_Cart] FOREIGN KEY([CartID])
REFERENCES [dbo].[Tbs_Cart] ([ID])
GO
ALTER TABLE [dbo].[Tbs_Order] CHECK CONSTRAINT [FK_Tbs_Order_Tbs_Cart]
GO
ALTER TABLE [dbo].[Tbs_Product]  WITH CHECK ADD  CONSTRAINT [FK_Tbs_Product_Tbs_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Tbs_Category] ([ID])
GO
ALTER TABLE [dbo].[Tbs_Product] CHECK CONSTRAINT [FK_Tbs_Product_Tbs_Category]
GO
