Create database [PRN222_Project]

USE [PRN222_Project]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 4/16/2025 8:13:22 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 4/16/2025 8:13:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 4/16/2025 8:13:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 4/16/2025 8:13:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 4/16/2025 8:13:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 4/16/2025 8:13:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 4/16/2025 8:13:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[Gender] [bit] NOT NULL,
	[Dob] [datetime2](7) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 4/16/2025 8:13:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartItems]    Script Date: 4/16/2025 8:13:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartItems](
	[CartItemId] [int] IDENTITY(1,1) NOT NULL,
	[CartId] [int] NULL,
	[ProductSpecID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[PriceEachItem] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_CartItems] PRIMARY KEY CLUSTERED 
(
	[CartItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carts]    Script Date: 4/16/2025 8:13:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carts](
	[CartId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [nvarchar](450) NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Carts] PRIMARY KEY CLUSTERED 
(
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 4/16/2025 8:13:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedbacks]    Script Date: 4/16/2025 8:13:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedbacks](
	[FeedbackId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[CustomerId] [nvarchar](450) NULL,
	[FeedbackContent] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
	[RatedStar] [int] NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ModifyAt] [datetime2](7) NULL,
 CONSTRAINT [PK_Feedbacks] PRIMARY KEY CLUSTERED 
(
	[FeedbackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 4/16/2025 8:13:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[OrderItemId] [int] IDENTITY(1,1) NOT NULL,
	[CartItemId] [int] NOT NULL,
	[OrderId] [int] NOT NULL,
	[FeedbackId] [int] NULL,
 CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED 
(
	[OrderItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 4/16/2025 8:13:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderCode_GHN] [nvarchar](max) NULL,
	[OrderAt] [datetime2](7) NOT NULL,
	[PaymentMethod] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
	[CustomerId] [nvarchar](450) NULL,
	[StaffId] [nvarchar](max) NULL,
	[OrderStatusId] [int] NOT NULL,
	[TotalCost] [decimal](18, 2) NOT NULL,
	[ReceiverId] [int] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategories]    Script Date: 4/16/2025 8:13:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_ProductCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductImages]    Script Date: 4/16/2025 8:13:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductImages](
	[ProductImageId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[ImageURL] [nvarchar](max) NULL,
 CONSTRAINT [PK_ProductImages] PRIMARY KEY CLUSTERED 
(
	[ProductImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 4/16/2025 8:13:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ProductStatus] [bit] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductSpecs]    Script Date: 4/16/2025 8:13:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductSpecs](
	[ProductSpecId] [int] IDENTITY(1,1) NOT NULL,
	[SpecName] [nvarchar](255) NOT NULL,
	[Quantity] [int] NOT NULL,
	[BasePrice] [decimal](18, 2) NOT NULL,
	[SalePrice] [decimal](18, 2) NULL,
	[ProductId] [int] NOT NULL,
	[ProductStatus] [bit] NOT NULL,
 CONSTRAINT [PK_ProductSpecs] PRIMARY KEY CLUSTERED 
(
	[ProductSpecId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReceiverInformations]    Script Date: 4/16/2025 8:13:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReceiverInformations](
	[ReceiverId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](13) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[ProvinceId_GHN] [nvarchar](max) NULL,
	[DistrictId_GHN] [nvarchar](max) NULL,
	[WardId_GHN] [nvarchar](max) NULL,
	[CustomerId] [nvarchar](450) NULL,
	[IsDefault] [bit] NULL,
	[DistrictName_GHN] [nvarchar](max) NULL,
	[ProvinceName_GHN] [nvarchar](max) NULL,
	[WardName_GHN] [nvarchar](max) NULL,
 CONSTRAINT [PK_ReceiverInformations] PRIMARY KEY CLUSTERED 
(
	[ReceiverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250327102441_UpdateProportiesInOrder', N'8.0.11')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250327110938_AddIsDefaultInReceiver', N'8.0.11')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250327152930_AddMoreInforForGHN', N'8.0.11')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250329030510_DelPropInOrder', N'8.0.11')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250329035601_DelPropInOrderItems', N'8.0.11')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250329040032_DelPropInOrderItemsAgain', N'8.0.11')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250329040450_AlterPropInCarOrderItems', N'8.0.11')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'305cbb44-e18d-4f77-9864-ce4272ee2850', N'Customer', N'CUSTOMER', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'8136d0f3-ac98-43e2-a084-0800602fab23', N'Admin', N'ADMIN', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'e8080917-648f-40d0-be76-b1aa86e0496b', N'Sales', N'SALES', NULL)
GO
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES (N'Google', N'102919994650206182334', N'Google', N'63b865ad-1606-448a-a567-edb83f7759e8')
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES (N'Google', N'114247702920186871713', N'Google', N'cbc51523-7402-4f2e-a932-8abb73164916')
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES (N'Google', N'115752878224945684827', N'Google', N'60de45a5-61da-4212-b11d-502d81d25d0d')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'60de45a5-61da-4212-b11d-502d81d25d0d', N'305cbb44-e18d-4f77-9864-ce4272ee2850')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'63b865ad-1606-448a-a567-edb83f7759e8', N'305cbb44-e18d-4f77-9864-ce4272ee2850')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'cbc51523-7402-4f2e-a932-8abb73164916', N'305cbb44-e18d-4f77-9864-ce4272ee2850')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'94ed20f2-85b4-42c0-a0bf-46c4fc6ebd3b', N'8136d0f3-ac98-43e2-a084-0800602fab23')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'42594951-4a94-4c83-af63-773fdfaad852', N'e8080917-648f-40d0-be76-b1aa86e0496b')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5e2d9c4e-e42c-4c59-8aae-11db359ac828', N'e8080917-648f-40d0-be76-b1aa86e0496b')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c725f5ed-9ec3-46bf-930a-d454e7f79237', N'e8080917-648f-40d0-be76-b1aa86e0496b')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd546c7e4-70f1-4b78-9468-2eaa186d6b32', N'e8080917-648f-40d0-be76-b1aa86e0496b')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Dob], [CreatedDate], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'2641765f-0f6f-4caf-a5d6-b3521e599573', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'racib33288@macho3.com', N'RACIB33288@MACHO3.COM', N'racib33288@macho3.com', N'RACIB33288@MACHO3.COM', 1, N'AQAAAAIAAYagAAAAEPNJyQHLotKikS+5+IWlqwNWYaIfpOicr3Tnf3eTm4CThxRyDOSbxa538tn2B6yjKA==', N'JNUCPFTF4M7BL746WURD6POMAPTZMGNX', N'6578d582-79f5-4c17-8def-6b970071d97c', NULL, 0, 0, NULL, 0, 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Dob], [CreatedDate], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3240b7e8-9673-4e6d-a551-e74719413dd0', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'cokis31321@macho3.com', N'COKIS31321@MACHO3.COM', N'cokis31321@macho3.com', N'COKIS31321@MACHO3.COM', 1, N'AQAAAAIAAYagAAAAEBv5JsZTU3nj8UQW7TXUxUxdwnHqtfuvxh1REqCCVdx2NLqCmWzlHkUAJ/6LQVWjpQ==', N'W42ADYIF4P4C6MAJTHG7D2P2RG54XUTJ', N'8648cf71-971c-4898-b7e3-38805e00d5e6', NULL, 0, 0, NULL, 0, 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Dob], [CreatedDate], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'42594951-4a94-4c83-af63-773fdfaad852', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'Quang', N'QUANG', N'fowite3032@macho3.com', N'FOWITE3032@MACHO3.COM', 0, N'AQAAAAIAAYagAAAAEJ51ti4YLYm2l3OLgicKau/j2SFKqdkWKc9VxMqdZ0PrpN26O//l7RVLf/qzsSFabg==', N'DYFFIHICQUMUK7QT6YZ3LWJXSLN3L2GS', N'7263f278-bd02-43bf-b8ad-9d7351afff61', N'0344338803', 0, 0, NULL, 0, 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Dob], [CreatedDate], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'5e2d9c4e-e42c-4c59-8aae-11db359ac828', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'sepen87895@motivue.com', N'SEPEN87895@MOTIVUE.COM', N'sepen87895@motivue.com', N'SEPEN87895@MOTIVUE.COM', 1, N'AQAAAAIAAYagAAAAEIoYT/KXuIPXHahqBxBAynLtPj1+P5SgMYDxQ4+dhPToYaCRKO1qZGIQ3kyfpcdrHg==', N'T2ZTIDGZLOC6EXXVFPMHWBA3HFLAT4ZB', N'ee1b9242-c0df-48b8-9334-5b4fa158a62e', N'0344338803', 0, 0, NULL, 0, 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Dob], [CreatedDate], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'60de45a5-61da-4212-b11d-502d81d25d0d', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'joshbanselm19@gmail.com', N'JOSHBANSELM19@GMAIL.COM', N'joshbanselm19@gmail.com', N'JOSHBANSELM19@GMAIL.COM', 1, NULL, N'A4DGULBR6WC5LYAWB4QS2FH2X4EUXOZ3', N'09f2aa14-a874-4ea0-9253-944d5c3e8fc4', NULL, 0, 0, NULL, 0, 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Dob], [CreatedDate], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'63b865ad-1606-448a-a567-edb83f7759e8', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'kimbonbelt1201@gmail.com', N'KIMBONBELT1201@GMAIL.COM', N'kimbonbelt1201@gmail.com', N'KIMBONBELT1201@GMAIL.COM', 1, NULL, N'B4L7MYPHVLLXPWGIF4ICCH4EIKZZ4DH4', N'b3fdad49-1c37-43ae-8025-42d31e02de54', NULL, 0, 0, NULL, 0, 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Dob], [CreatedDate], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'94ed20f2-85b4-42c0-a0bf-46c4fc6ebd3b', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'quangtnvhe182422@fpt.edu.vn', N'QUANGTNVHE182422@FPT.EDU.VN', N'quangtnvhe182422@fpt.edu.vn', N'QUANGTNVHE182422@FPT.EDU.VN', 1, N'AQAAAAIAAYagAAAAEPm1eGnDxrLO9PwxQE1Mgjaf9NcwFxFZ7uuhoXG18r+8MuwVkDEfpsf05n57S4FOIQ==', N'S76WH76F3QODVCBH57PA7WGSKY3LBCEL', N'1134a62f-926b-47bb-b7fb-dfc6aab84b2c', NULL, 0, 0, NULL, 0, 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Dob], [CreatedDate], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'c725f5ed-9ec3-46bf-930a-d454e7f79237', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'ponenox508@motivue.com', N'PONENOX508@MOTIVUE.COM', N'ponenox508@motivue.com', N'PONENOX508@MOTIVUE.COM', 1, N'AQAAAAIAAYagAAAAEC0S92azn4vH2gYDoiSbzuiUMt5L+KBQY6r5xtAjYUj5CBvvhpQbyciLSK9vs+sENQ==', N'KKGJMFM5CJL4XRUC5Z5DNS5ARXGHELBA', N'cc6c4e06-c684-4115-b0d6-260c49b4d66d', N'0344338803', 0, 0, NULL, 0, 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Dob], [CreatedDate], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'cbc51523-7402-4f2e-a932-8abb73164916', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'quanglequydon03@gmail.com', N'QUANGLEQUYDON03@GMAIL.COM', N'quanglequydon03@gmail.com', N'QUANGLEQUYDON03@GMAIL.COM', 1, NULL, N'366PG46XXHDK6ZEUBNVI2ANFPCCUSL7O', N'ed1191d7-f772-49df-ac38-e237ecb2d038', NULL, 0, 0, NULL, 0, 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Dob], [CreatedDate], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'cff91abc-ca4c-4309-be1d-aa763faaf4c4', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'levij64101@motivue.com', N'LEVIJ64101@MOTIVUE.COM', N'levij64101@motivue.com', N'LEVIJ64101@MOTIVUE.COM', 1, N'AQAAAAIAAYagAAAAEEcAhVmOgLaRf6QkLvRTdV0unbIPnuOVZMhwDg7AZZ1WzpVied4BZ/AtA3vr3WloZQ==', N'22YLTUGBLLY75MVMNE3RSSTNQNQEDZ2L', N'01190a1e-826f-46ff-ac34-1232ebca7e5f', NULL, 0, 0, NULL, 0, 0)
INSERT [dbo].[AspNetUsers] ([Id], [Gender], [Dob], [CreatedDate], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd546c7e4-70f1-4b78-9468-2eaa186d6b32', 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'La', N'LA', N'bibiko9911@motivue.com', N'BIBIKO9911@MOTIVUE.COM', 1, N'AQAAAAIAAYagAAAAEF4yGkcYp9AOnDtl4thZAr+PQkqezrxkMPtdalVpIvGEExaDnNW2fG34wKQPtlOpcQ==', N'5FGVD2DVZCUUMQMVN2L7R2H6QSRHX7M5', N'3976c704-8037-476b-bfab-8b6781baf470', N'0344338803', 0, 0, NULL, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[CartItems] ON 

INSERT [dbo].[CartItems] ([CartItemId], [CartId], [ProductSpecID], [Quantity], [PriceEachItem]) VALUES (4, 2, 21, 1, CAST(2000.00 AS Decimal(18, 2)))
INSERT [dbo].[CartItems] ([CartItemId], [CartId], [ProductSpecID], [Quantity], [PriceEachItem]) VALUES (5, NULL, 26, 2, CAST(479000.00 AS Decimal(18, 2)))
INSERT [dbo].[CartItems] ([CartItemId], [CartId], [ProductSpecID], [Quantity], [PriceEachItem]) VALUES (6, NULL, 26, 2, CAST(1916000.00 AS Decimal(18, 2)))
INSERT [dbo].[CartItems] ([CartItemId], [CartId], [ProductSpecID], [Quantity], [PriceEachItem]) VALUES (7, 1, 16, 1, CAST(479000.00 AS Decimal(18, 2)))
INSERT [dbo].[CartItems] ([CartItemId], [CartId], [ProductSpecID], [Quantity], [PriceEachItem]) VALUES (8, NULL, 21, 1, CAST(2000.00 AS Decimal(18, 2)))
INSERT [dbo].[CartItems] ([CartItemId], [CartId], [ProductSpecID], [Quantity], [PriceEachItem]) VALUES (9, NULL, 21, 1, CAST(2000.00 AS Decimal(18, 2)))
INSERT [dbo].[CartItems] ([CartItemId], [CartId], [ProductSpecID], [Quantity], [PriceEachItem]) VALUES (10, NULL, 21, 1, CAST(2000.00 AS Decimal(18, 2)))
INSERT [dbo].[CartItems] ([CartItemId], [CartId], [ProductSpecID], [Quantity], [PriceEachItem]) VALUES (11, NULL, 21, 1, CAST(2000.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[CartItems] OFF
GO
SET IDENTITY_INSERT [dbo].[Carts] ON 

INSERT [dbo].[Carts] ([CartId], [CustomerId], [TotalPrice]) VALUES (1, N'94ed20f2-85b4-42c0-a0bf-46c4fc6ebd3b', CAST(479000.00 AS Decimal(18, 2)))
INSERT [dbo].[Carts] ([CartId], [CustomerId], [TotalPrice]) VALUES (2, N'cff91abc-ca4c-4309-be1d-aa763faaf4c4', CAST(2000.00 AS Decimal(18, 2)))
INSERT [dbo].[Carts] ([CartId], [CustomerId], [TotalPrice]) VALUES (3, N'2641765f-0f6f-4caf-a5d6-b3521e599573', CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[Carts] ([CartId], [CustomerId], [TotalPrice]) VALUES (4, N'60de45a5-61da-4212-b11d-502d81d25d0d', CAST(2000.00 AS Decimal(18, 2)))
INSERT [dbo].[Carts] ([CartId], [CustomerId], [TotalPrice]) VALUES (5, N'63b865ad-1606-448a-a567-edb83f7759e8', CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[Carts] ([CartId], [CustomerId], [TotalPrice]) VALUES (6, N'cbc51523-7402-4f2e-a932-8abb73164916', CAST(0.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Carts] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryId], [Name], [Description]) VALUES (1, N'Cherry', N'Loại Cherry')
INSERT [dbo].[Categories] ([CategoryId], [Name], [Description]) VALUES (2, N'Táo', N'Loại Táo')
INSERT [dbo].[Categories] ([CategoryId], [Name], [Description]) VALUES (3, N'Dâu Tây', N'Loại Dâu Tây')
INSERT [dbo].[Categories] ([CategoryId], [Name], [Description]) VALUES (4, N'Nho', N'Loại Nho')
INSERT [dbo].[Categories] ([CategoryId], [Name], [Description]) VALUES (5, N'Dưa', N'Loại Dưa')
INSERT [dbo].[Categories] ([CategoryId], [Name], [Description]) VALUES (6, N'Mùa xuân', N'Quả xuân')
INSERT [dbo].[Categories] ([CategoryId], [Name], [Description]) VALUES (7, N'Mùa hè', N'Quả hè')
INSERT [dbo].[Categories] ([CategoryId], [Name], [Description]) VALUES (8, N'Mùa thu', N'Quả thu')
INSERT [dbo].[Categories] ([CategoryId], [Name], [Description]) VALUES (9, N'Mùa đông', N'Quả đông')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderItems] ON 

INSERT [dbo].[OrderItems] ([OrderItemId], [CartItemId], [OrderId], [FeedbackId]) VALUES (32, 5, 32, NULL)
INSERT [dbo].[OrderItems] ([OrderItemId], [CartItemId], [OrderId], [FeedbackId]) VALUES (33, 6, 33, NULL)
INSERT [dbo].[OrderItems] ([OrderItemId], [CartItemId], [OrderId], [FeedbackId]) VALUES (34, 8, 34, NULL)
INSERT [dbo].[OrderItems] ([OrderItemId], [CartItemId], [OrderId], [FeedbackId]) VALUES (35, 9, 35, NULL)
INSERT [dbo].[OrderItems] ([OrderItemId], [CartItemId], [OrderId], [FeedbackId]) VALUES (36, 10, 36, NULL)
INSERT [dbo].[OrderItems] ([OrderItemId], [CartItemId], [OrderId], [FeedbackId]) VALUES (37, 11, 37, NULL)
SET IDENTITY_INSERT [dbo].[OrderItems] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderId], [OrderCode_GHN], [OrderAt], [PaymentMethod], [Notes], [CustomerId], [StaffId], [OrderStatusId], [TotalCost], [ReceiverId]) VALUES (32, NULL, CAST(N'2025-03-30T14:29:55.5895304' AS DateTime2), N'COD', NULL, N'94ed20f2-85b4-42c0-a0bf-46c4fc6ebd3b', NULL, 3, CAST(978500.00 AS Decimal(18, 2)), 13)
INSERT [dbo].[Orders] ([OrderId], [OrderCode_GHN], [OrderAt], [PaymentMethod], [Notes], [CustomerId], [StaffId], [OrderStatusId], [TotalCost], [ReceiverId]) VALUES (33, N'LB7ATB', CAST(N'2025-03-30T14:50:05.9638235' AS DateTime2), N'COD', N'Tôi ko mua', N'94ed20f2-85b4-42c0-a0bf-46c4fc6ebd3b', NULL, 3, CAST(3852500.00 AS Decimal(18, 2)), 13)
INSERT [dbo].[Orders] ([OrderId], [OrderCode_GHN], [OrderAt], [PaymentMethod], [Notes], [CustomerId], [StaffId], [OrderStatusId], [TotalCost], [ReceiverId]) VALUES (34, N'LB7YAD', CAST(N'2025-03-31T11:57:23.3919874' AS DateTime2), N'vnPay', NULL, N'60de45a5-61da-4212-b11d-502d81d25d0d', NULL, 2, CAST(22500.00 AS Decimal(18, 2)), 15)
INSERT [dbo].[Orders] ([OrderId], [OrderCode_GHN], [OrderAt], [PaymentMethod], [Notes], [CustomerId], [StaffId], [OrderStatusId], [TotalCost], [ReceiverId]) VALUES (35, N'LB7YAP', CAST(N'2025-03-31T11:58:37.1370267' AS DateTime2), N'PayOS', NULL, N'60de45a5-61da-4212-b11d-502d81d25d0d', NULL, 2, CAST(22500.00 AS Decimal(18, 2)), 15)
INSERT [dbo].[Orders] ([OrderId], [OrderCode_GHN], [OrderAt], [PaymentMethod], [Notes], [CustomerId], [StaffId], [OrderStatusId], [TotalCost], [ReceiverId]) VALUES (36, NULL, CAST(N'2025-03-31T12:00:58.8507365' AS DateTime2), N'COD', NULL, N'60de45a5-61da-4212-b11d-502d81d25d0d', NULL, 3, CAST(22500.00 AS Decimal(18, 2)), 15)
INSERT [dbo].[Orders] ([OrderId], [OrderCode_GHN], [OrderAt], [PaymentMethod], [Notes], [CustomerId], [StaffId], [OrderStatusId], [TotalCost], [ReceiverId]) VALUES (37, N'LB7YAB', CAST(N'2025-03-31T12:01:31.4472402' AS DateTime2), N'COD', NULL, N'60de45a5-61da-4212-b11d-502d81d25d0d', NULL, 2, CAST(22500.00 AS Decimal(18, 2)), 15)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductCategories] ON 

INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (1, 1, 1)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (2, 1, 6)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (3, 1, 8)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (4, 2, 1)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (5, 2, 6)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (6, 2, 7)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (7, 3, 2)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (8, 3, 6)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (9, 3, 8)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (10, 3, 9)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (11, 4, 3)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (12, 4, 6)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (13, 4, 8)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (14, 5, 4)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (15, 5, 6)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (16, 5, 9)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (17, 6, 3)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (18, 6, 6)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (19, 6, 8)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (20, 7, 5)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (21, 7, 6)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (22, 7, 8)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (23, 7, 9)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (24, 7, 7)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (25, 8, 4)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (26, 8, 6)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (27, 8, 8)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (28, 8, 9)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (29, 9, 4)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (30, 9, 7)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (31, 9, 9)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (32, 10, 4)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (33, 10, 6)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (34, 10, 9)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (35, 11, 2)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (36, 11, 9)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (37, 11, 7)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (38, 12, 2)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (39, 12, 6)
INSERT [dbo].[ProductCategories] ([Id], [ProductId], [CategoryId]) VALUES (40, 12, 8)
SET IDENTITY_INSERT [dbo].[ProductCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductImages] ON 

INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (1, 1, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742705872/cherry-jumbo-new-zealand-khay-01-kg-900-900_4b1210220ba247d59f0bf217941264ea_master_earzgo.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (2, 1, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742705872/cherry-jumbo-new-zealand-khay-02-kg-900-900_6a071493f7044927bd44f991dc3f51a4_master_cjusfh.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (3, 1, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742705875/thung-nguyen-dai-cherry-jumbo-new-zealand-5kg_73e386644c144ddc81e4f9ddfe655110_master_kdukkx.webpp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (4, 1, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742705870/cherry-jumbo-new-zealand-khay-0_pqpvcs.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (5, 1, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742705876/cherry-jumbo-new-zealand-khay-03-kg-900-900_361f190c174a4dfc9fb4ef8718e7c1d3_master_qlbzjl.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (6, 2, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742705872/cherry-jumbo-new-zealand-khay-01-kg-900-900_4b1210220ba247d59f0bf217941264ea_master_earzgo.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (7, 2, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742705872/cherry-jumbo-new-zealand-khay-02-kg-900-900_6a071493f7044927bd44f991dc3f51a4_master_cjusfh.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (8, 2, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742705875/thung-nguyen-dai-cherry-jumbo-new-zealand-5kg_73e386644c144ddc81e4f9ddfe655110_master_kdukkx.webpp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (9, 2, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742705870/cherry-jumbo-new-zealand-khay-0_pqpvcs.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (10, 2, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742705876/cherry-jumbo-new-zealand-khay-03-kg-900-900_361f190c174a4dfc9fb4ef8718e7c1d3_master_qlbzjl.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (11, 3, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706175/z5119132584349_16739af07b5afae7cf60744beb76581d_24a5df035d1040a2aa81223bda9e39ef_master_jk4qbw.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (12, 3, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706175/z5119132596042_db972c8a9b90c1c8d75a845f4b06af7d_e4faddf30a0c4b6e811757f1995a7288_master_aivkey.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (13, 3, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706175/z5119132573585_8159b6399c8ed2f6802bbe61f5c1d24c_22cf150cd7d04271bfe9b855313a6a8c_master_ly9tkz.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (14, 3, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706175/z5119134015728_f0dc813f35ca2f767d3c96c7e2928038_01ef2a477aad4a39a955da4b0b0a4e67_master_o0bk3y.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (15, 3, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706175/z5119132596730_9cacbf8954b7eebaf131933e7af9c570__1__b3d49d750e0841d7a289eddacbf24a6c_master_tqw1d9.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (16, 4, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706303/dau-tay-vip-han-quoc-hop-840-gram_-_copy_b1507ea5d25641479606abc5e6486144_master_mknbre.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (17, 4, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706303/dau-tay-vip-han-quoc-hop-420-gram_-_copy_626c472e7c234163b35435a7b09988c6_master_ornegz.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (18, 5, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706416/z5334222072666_147590186bb7fb9f7f2cc7c1cae36e82_4266a208323249279642839cb0522d3d_master_bnmjfr.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (19, 5, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706416/z5334222078742_d5428c7668a21dda400f7767758bda7d_ac8d2cb245624967b17e35637a9ef90b_master_et4rsu.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (20, 5, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706416/z5334222078796_a31a44a0e87eb5ca876f74a1787529e7_13c3ec6998f642d784c5d17afbb81c18_master_rk9w4d.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (21, 6, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706555/dau-tay-han-quoc-hop-01-kg_e9807c2726174fe6bfd8e0c230e35c21_master_zl7i3m.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (22, 6, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706556/dau-tay-han-quoc-hop-02-kg_2736a8e11683462c8bc99375b5f8e532_master_yv52kh.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (23, 6, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706556/hop-qua-trai-tim-dau-tay-han-quoc-hop-01-kg_dae6204f9aa44f17aa4affccf32cfb73_master_yxwgqh.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (24, 6, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706556/dau-tay-han-quoc-hop-330-gram_45e0f55d842c4904baa9571571b9ea08_master_ux37fp.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (25, 7, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706668/1_d0b6f93573804d418064c55936640cae_master_udm2dk.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (26, 7, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706669/2_8c8b744a391e419dac149d71f328a749_master_blsam0.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (27, 7, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706670/2323_d02607be7b4b401eb50da2793de4e723_master_dcgkiy.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (28, 8, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706774/z4234238949119_696d16015b31afe9965a42ffb2b0170d_82dee8df55464dd5965b63f193aa24ba_master_vhx3nt.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (29, 8, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706774/z4234238967000_7ffd6ee778bafc3d6db15166eb591672_2ba92534920e4d23abb8ab74ce04f0db_master_j8qm0j.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (30, 8, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706774/z4234238954587_67dd0f91cc3140ae5beaa0410dc36c8b_26a6fbb0a66e476c899997e1f8bab4e0_master_rfloss.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (31, 8, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706777/z4234238967002_1891c1e51a63bfef609c91fbb77e0e5a_db6e742fc94c493ab679d4a2529965ab_master_hh6wdm.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (32, 9, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706981/z4203218116958_b05f1138ac2f7f087bb578909869b5bd_8ed26040f3844ab288b0f33ea9dec6cd_master_jkexte.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (33, 9, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706982/z4203218110952_a5138acba3caf95e6747af8b40646c0e_6ee5b07b08d9409f8f2781b98e2d2761_master_oxhwgs.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (34, 9, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706982/z4203218123423_d850289699e6a6293347ab0fa17ded28_1ee5ba90d2ca40e3bbab322aa171e9e0_master_i92ypx.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (35, 9, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706982/z4203218133095_d00d7867e220453097cb0493915ba558_02fe35de76b94eb1b071245c24dcff30_master_ghyvg3.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (36, 9, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706983/z4203218142398_763c929ca5b77232e24ed96dea92a7f5_8a06a8994e6844749993e43173cca280_master_iguzkc.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (37, 10, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706981/z4203218116958_b05f1138ac2f7f087bb578909869b5bd_8ed26040f3844ab288b0f33ea9dec6cd_master_jkexte.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (38, 10, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706982/z4203218110952_a5138acba3caf95e6747af8b40646c0e_6ee5b07b08d9409f8f2781b98e2d2761_master_oxhwgs.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (39, 10, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706982/z4203218123423_d850289699e6a6293347ab0fa17ded28_1ee5ba90d2ca40e3bbab322aa171e9e0_master_i92ypx.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (40, 10, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706982/z4203218133095_d00d7867e220453097cb0493915ba558_02fe35de76b94eb1b071245c24dcff30_master_ghyvg3.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (41, 10, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742706983/z4203218142398_763c929ca5b77232e24ed96dea92a7f5_8a06a8994e6844749993e43173cca280_master_iguzkc.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (42, 11, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742707138/hop-qua-tao-sekaiichi-nhat-ban-04-qua_6786fc3b907545169050d2fd0caf9e92_master_kjtx28.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (43, 11, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742707138/hop-qua-tao-sekaiichi-nhat-ban-08-qua_dd93188d05be45968a7874d51657b548_master_rypfhe.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (44, 11, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742707138/tao-sekaiichi-nhat-ban-khay-01-qua_bb36c689a7904aec9d099aad605d0be3_master_khkgfa.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (45, 11, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742707138/tao-sekaiichi-nhat-ban-khay-02-qua_1e146d0794f84d79a9b2458cd4501f7b_master_enz6uz.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (46, 11, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742707139/tao-sekaiichi-nhat-ban-khay-03-qua_4304e92db0d643d79d084023b4477abb_master_ypi75p.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (47, 11, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742707138/hop-qua-tao-sekaiichi-nhat-ban-05-qua_1441be2d89914e49a50ff88faf38b409_master_rjxux2.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (48, 12, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742707294/tao-vang-kinsei-nhat-ban-khay-03-qua_898f4e25630c4fa89851df6aa094eafb_master_dvggim.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (49, 12, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742707294/tao-vang-kinsei-nhat-ban-khay-02-qua_b69931b00eb148f489aba961838c9615_master_dws4wy.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (50, 12, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742707294/hop-qua-tao-vang-kinsei-nhat-ban-hop-05-qua_e7e2ff7ca62344f3a4b62d087fee0fa3_master_ib9phb.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (51, 12, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742707293/tao-vang-kinsei-nhat-ban-khay-01-qua_436e62bd077d4ed890b36baae1209fd5_master_aydrct.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (52, 12, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1742707293/hop-qua-tao-vang-kinsei-nhat-ban-hop-04-qua_43fb1fd211c8469aa3ed5619c47ef826_master_don9q2.webp')
INSERT [dbo].[ProductImages] ([ProductImageId], [ProductId], [ImageURL]) VALUES (54, 15, N'https://res.cloudinary.com/dkr13a1ft/image/upload/v1743397631/vonezaq2pstnt3iyfj5d.png')
SET IDENTITY_INSERT [dbo].[ProductImages] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductId], [ProductName], [Description], [CreatedAt], [ProductStatus]) VALUES (1, N'Cherry Jumbo New Zealand', N'Cherry Jumbo New Zealand, đặc biệt là chủng Dunstan Hills, được mệnh danh là "Kim Cương Đỏ" trong các loại trái cây và là loại cherry ngon nhất thế giới. Với chất lượng vượt trội, cherry Dunstan Hills không chỉ nổi bật về hương vị mà còn là sản phẩm độc quyền của Klever Fruit tại thị trường Việt Nam. Cherry New Zealand được chăm sóc tỉ mỉ từ khâu thu hoạch cho đến đóng gói, sau khi thu hoạch chỉ 48h sau những trái Cherry New Zealand tươi ngon đã có mặt tại cửa hàng Klever Fruit, đem lại một món quà tặng đẳng cấp, xứng tầm cho những dịp đặc biệt. Đây là loại trái cây nhập khẩu từ New Zealand, được biết đến với độ giòn, hương vị ngọt ngào và kích thước lớn ấn tượng, chắc chắn sẽ làm hài lòng những người yêu thích trái cây cao cấp.', CAST(N'2025-03-23T10:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Products] ([ProductId], [ProductName], [Description], [CreatedAt], [ProductStatus]) VALUES (2, N'Cherry Đỏ New Zealand', N'Cherry Dunstan Hills New Zealand là dòng cherry cao cấp nổi tiếng toàn cầu, được biết đến với chất lượng vượt trội và hương vị độc đáo. Đây là một trong những loại cherry đặc biệt được thiết kế dành riêng cho thị trường Việt Nam, với mong muốn mang đến trải nghiệm trái cây đẳng cấp nhất cho người tiêu dùng.', CAST(N'2025-03-23T10:10:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Products] ([ProductId], [ProductName], [Description], [CreatedAt], [ProductStatus]) VALUES (3, N'Táo Haruka Nhật Bản', N'Táo Haruka Nhật Bản được mệnh danh là một trong những giống táo quý hiếm và độc đáo bậc nhất thế giới. Với xuất xứ từ Nhật Bản, nơi nổi tiếng với kỹ thuật canh tác sạch và độc đáo, táo Haruka gây ấn tượng mạnh bởi hương vị ngọt ngào, kích thước lớn và lõi mật ong đặc biệt mà không có bất kỳ một loại táo nào có được.', CAST(N'2025-03-23T10:20:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Products] ([ProductId], [ProductName], [Description], [CreatedAt], [ProductStatus]) VALUES (4, N'Dâu Tây Hàn Quốc VIP', N'Dâu Tây Hàn Quốc VIP độc quyền tại Klever Fruit là dòng sản phẩm cao cấp, đại diện cho triết lý "Trái cây chín đúng thời điểm." Với mỗi trái dâu được trồng trong điều kiện tiêu chuẩn và lựa chọn thủ công bởi chuyên gia hàng đầu Ms. Jo Hyang Ran – Người nếm trái cây số 1 Hàn Quốc, loại dâu này không chỉ mang vẻ đẹp tự nhiên mà còn đạt đỉnh cao về hương vị và giá trị dinh dưỡng. Dâu tây Hàn Quốc VIP không chỉ là món ăn ngon mà còn là biểu tượng của sự tinh tế, sang trọng và sự quan tâm đến sức khỏe của người nhận.', CAST(N'2025-03-23T11:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Products] ([ProductId], [ProductName], [Description], [CreatedAt], [ProductStatus]) VALUES (5, N'Nho Muscat Beauty Chile', N'• Xuất xứ: Nho Muscat Beauty được nhập khẩu trực tiếp từ Chile – quốc gia nổi tiếng với khí hậu đa dạng và điều kiện đất đai lý tưởng để trồng nho.\n\n• Mùa vụ: Mùa vụ nho Muscat Beauty kéo dài từ tháng 1 đến tháng 5 hàng năm. Đặc biệt, Klever Fruit cung cấp sản phẩm từ nguồn nho tươi, đảm bảo chất lượng trong suốt mùa thu hoạch.\n\n• Chủng loại: Nho Muscat Beauty Chile là giống nho không hạt cao cấp, đáp ứng tiêu chuẩn xuất khẩu sang các thị trường khó tính như châu Âu và Mỹ.\n\n• Quy cách đóng gói: Sản phẩm được đóng gói trong hộp nhựa cao cấp, đảm bảo giữ nguyên độ tươi ngon và an toàn trong suốt quá trình vận chuyển.', CAST(N'2025-03-23T11:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Products] ([ProductId], [ProductName], [Description], [CreatedAt], [ProductStatus]) VALUES (6, N'Dâu Tây Hàn Quốc', N'Dâu Tây Hàn Quốc chủng Maehyang là dòng trái cây cao cấp được trồng tại các nhà vườn hiện đại ở Hàn Quốc, nơi có điều kiện khí hậu lý tưởng. Với sắc đỏ rực rỡ, vị ngọt thanh hòa quyện chút chua nhẹ và hương thơm quyến rũ, loại dâu này mang đến trải nghiệm ẩm thực tuyệt vời. Không chỉ ngon miệng, dâu Maehyang còn giàu giá trị dinh dưỡng, phù hợp với mọi lứa tuổi, đặc biệt tốt cho sức khỏe người dùng.', CAST(N'2025-03-23T11:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Products] ([ProductId], [ProductName], [Description], [CreatedAt], [ProductStatus]) VALUES (7, N'Dưa Giống Nhật Musk Melon', N'• Xuất xứ: 100% hạt giống và công nghệ từ Nhật Bản, được trồng và chăm sóc bởi các kỹ sư, chuyên gia Nhật Bản tại Đà Lạt, Việt Nam – vùng đất có khí hậu mát mẻ, lý tưởng cho nông nghiệp chất lượng cao.\n\n• Khối lượng: Mỗi quả dưa nặng từ 1.5 - 1.8kg, kích thước chuẩn xác, đảm bảo sự đồng đều.\n\n• Mùa vụ: Thu hoạch định kỳ 3 tháng/lần, đảm bảo dưa luôn tươi ngon và chất lượng cao.\n\n• Chủng loại: Earl – dòng dưa quý hiếm, nổi bật với độ ngọt hoàn hảo và hình dáng kiêu sa.\n\n• Quy cách đóng gói: Dưa được đặt trong khay gỗ KF nhỏ sang trọng, phù hợp làm quà biếu hoặc sử dụng tại gia đình.', CAST(N'2025-03-23T11:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Products] ([ProductId], [ProductName], [Description], [CreatedAt], [ProductStatus]) VALUES (8, N'Nho Đen Kẹo Candy Crunch Úc', N'• Xuất xứ: Nho kẹo đen Candy Crunch được trồng tại các trang trại hàng đầu ở Australia, nơi có khí hậu và thổ nhưỡng lý tưởng để nho phát triển đạt chất lượng cao nhất.\n\n• Mùa vụ: Được thu hoạch vào thời điểm mùa hè, nho Candy Crunch luôn giữ được độ tươi mới, hương vị ngọt ngào tự nhiên.\n\n• Chủng loại: Candy Crunch – giống nho không hạt cao cấp với chất lượng vượt trội.\n\n• Khối lượng và Quy cách đóng gói: Mỗi chùm nho có trọng lượng trung bình từ 500g - 1kg, được đóng gói trong hộp nhựa cao cấp hoặc thùng carton để bảo vệ quả khỏi va đập, đảm bảo giữ nguyên độ tươi ngon.', CAST(N'2025-03-23T11:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Products] ([ProductId], [ProductName], [Description], [CreatedAt], [ProductStatus]) VALUES (9, N'Nho Xanh Không Hạt Úc VIP', N'Nho xanh không hạt Úc là sản phẩm cao cấp được nhập khẩu trực tiếp từ các vùng trồng nho nổi tiếng của Úc. Với điều kiện khí hậu ôn đới đặc trưng, những chùm nho ở đây đạt chất lượng vượt trội, đáp ứng các tiêu chuẩn quốc tế.\n\nMùa vụ: Nho xanh không hạt Úc được thu hoạch từ tháng 2 đến tháng 5 hàng năm. Đây là thời điểm nho đạt độ ngọt và giòn hoàn hảo.\n\nChủng loại: Loại nho nổi bật nhất là Autumn Crisp, với quả to, tròn, màu xanh hổ phách đẹp mắt.\n\nQuy cách đóng gói: • Nho được đóng gói trong hộp kín hoặc túi hút chân không, đảm bảo giữ được độ tươi ngon khi đến tay người tiêu dùng.\n\n• Trọng lượng trung bình mỗi hộp từ 500g đến 1kg.0', CAST(N'2025-03-23T11:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Products] ([ProductId], [ProductName], [Description], [CreatedAt], [ProductStatus]) VALUES (10, N'Nho Xanh Không Hạt Nam Phi Vip', N'• Xuất xứ: Nho xanh không hạt được trồng tại Nam Phi, nơi sở hữu khí hậu lý tưởng với mùa đông khô hanh, mùa hè nóng và hệ thống thủy lợi hiện đại, giúp nho phát triển đạt chất lượng vượt trội.\n\n• Khối lượng: Mỗi chùm nho có trọng lượng trung bình từ 500g - 1kg, phù hợp sử dụng cho gia đình hoặc làm quà biếu cao cấp.\n\n• Mùa vụ: Từ tháng 1 đến tháng 5 hàng năm, nho xanh Nam Phi được thu hoạch đúng độ chín để đảm bảo hương vị và giá trị dinh dưỡng tốt nhất.\n\n• Quy cách đóng gói: Nho được đóng gói cẩn thận trong hộp nhựa hoặc thùng carton, giúp bảo vệ nho khỏi va đập và giữ độ tươi ngon trong quá trình vận chuyển.', CAST(N'2025-03-23T11:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Products] ([ProductId], [ProductName], [Description], [CreatedAt], [ProductStatus]) VALUES (11, N'Táo Đỏ Sekaiichi Nhật Bản', N'Táo Đỏ Sekaiichi là một trong những loại táo nổi tiếng thế giới với kích thước khổng lồ và hương vị đặc biệt. Được trồng tại tỉnh Aomori, Nhật Bản – nơi nổi tiếng với lịch sử trồng táo lâu dài, táo Sekaiichi mang lại sự trải nghiệm tuyệt vời về độ ngọt, giòn và hương thơm tự nhiên, không sử dụng bất kỳ chất hóa học hay bảo quản nào.', CAST(N'2025-03-23T11:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Products] ([ProductId], [ProductName], [Description], [CreatedAt], [ProductStatus]) VALUES (12, N'Táo Vàng Kinsei Nhật Bản', N'Táo Vàng Kinsei là một loại táo cao cấp đến từ tỉnh Aomori, Nhật Bản – vùng đất nổi tiếng với những loại trái cây sạch và chất lượng vượt trội. Với màu vàng kem đặc trưng, hương vị thơm ngon cùng quy trình trồng trọt nghiêm ngặt, táo Kinsei mang đến trải nghiệm vị giác độc đáo với hương thơm của trái cây nhiệt đới và độ ngọt cao.', CAST(N'2025-03-23T11:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Products] ([ProductId], [ProductName], [Description], [CreatedAt], [ProductStatus]) VALUES (15, N'Mini ~ 0.6kg hêhe', N'fsdfsdfs', CAST(N'2025-03-31T12:07:08.5504446' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductSpecs] ON 

INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (2, N'Nhỏ ~ 1kg', 0, CAST(999000.00 AS Decimal(18, 2)), CAST(799000.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (3, N'Trung ~ 2kg', 100, CAST(1998000.00 AS Decimal(18, 2)), CAST(1598000.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (4, N'Lớn ~ 3kg', 100, CAST(2997000.00 AS Decimal(18, 2)), CAST(2397000.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (5, N'Thùng nguyên đai ~ 5kg', 100, CAST(4995000.00 AS Decimal(18, 2)), CAST(3995000.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (6, N'Mini ~ 0.6kg', 100, CAST(599000.00 AS Decimal(18, 2)), CAST(479000.00 AS Decimal(18, 2)), 2, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (7, N'Nhỏ ~ 1kg', 100, CAST(999000.00 AS Decimal(18, 2)), CAST(799000.00 AS Decimal(18, 2)), 2, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (8, N'Trung ~ 2kg', 100, CAST(1998000.00 AS Decimal(18, 2)), CAST(1598000.00 AS Decimal(18, 2)), 2, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (9, N'Lớn ~ 3kg', 100, CAST(2997000.00 AS Decimal(18, 2)), CAST(2397000.00 AS Decimal(18, 2)), 2, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (10, N'Thùng nguyên đai ~ 5kg', 100, CAST(4995000.00 AS Decimal(18, 2)), CAST(3995000.00 AS Decimal(18, 2)), 2, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (11, N'Mini ~ 0.6kg', 100, CAST(599000.00 AS Decimal(18, 2)), CAST(479000.00 AS Decimal(18, 2)), 3, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (12, N'Nhỏ ~ 1kg', 100, CAST(999000.00 AS Decimal(18, 2)), CAST(799000.00 AS Decimal(18, 2)), 3, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (13, N'Trung ~ 2kg', 100, CAST(1998000.00 AS Decimal(18, 2)), CAST(1598000.00 AS Decimal(18, 2)), 3, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (14, N'Lớn ~ 3kg', 100, CAST(2997000.00 AS Decimal(18, 2)), CAST(2397000.00 AS Decimal(18, 2)), 3, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (15, N'Thùng nguyên đai ~ 5kg', 100, CAST(4995000.00 AS Decimal(18, 2)), CAST(3995000.00 AS Decimal(18, 2)), 3, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (16, N'Mini ~ 0.6kg', 100, CAST(599000.00 AS Decimal(18, 2)), CAST(479000.00 AS Decimal(18, 2)), 4, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (17, N'Nhỏ ~ 1kg', 100, CAST(999000.00 AS Decimal(18, 2)), CAST(799000.00 AS Decimal(18, 2)), 4, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (18, N'Trung ~ 2kg', 100, CAST(1998000.00 AS Decimal(18, 2)), CAST(1598000.00 AS Decimal(18, 2)), 4, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (19, N'Lớn ~ 3kg', 100, CAST(2997000.00 AS Decimal(18, 2)), CAST(2397000.00 AS Decimal(18, 2)), 4, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (20, N'Thùng nguyên đai ~ 5kg', 100, CAST(4995000.00 AS Decimal(18, 2)), CAST(3995000.00 AS Decimal(18, 2)), 4, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (21, N'Mini ~ 0.6kg', 97, CAST(599000.00 AS Decimal(18, 2)), CAST(2000.00 AS Decimal(18, 2)), 5, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (22, N'Nhỏ ~ 1kg', 100, CAST(999000.00 AS Decimal(18, 2)), CAST(799000.00 AS Decimal(18, 2)), 5, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (23, N'Trung ~ 2kg', 100, CAST(1998000.00 AS Decimal(18, 2)), CAST(1598000.00 AS Decimal(18, 2)), 5, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (24, N'Lớn ~ 3kg', 100, CAST(2997000.00 AS Decimal(18, 2)), CAST(2397000.00 AS Decimal(18, 2)), 5, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (25, N'Thùng nguyên đai ~ 5kg', 100, CAST(4995000.00 AS Decimal(18, 2)), CAST(3995000.00 AS Decimal(18, 2)), 5, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (26, N'Mini ~ 0.6kg', 5, CAST(599000.00 AS Decimal(18, 2)), CAST(479000.00 AS Decimal(18, 2)), 6, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (27, N'Nhỏ ~ 1kg', 100, CAST(999000.00 AS Decimal(18, 2)), CAST(799000.00 AS Decimal(18, 2)), 6, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (28, N'Trung ~ 2kg', 100, CAST(1998000.00 AS Decimal(18, 2)), CAST(1598000.00 AS Decimal(18, 2)), 6, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (29, N'Lớn ~ 3kg', 100, CAST(2997000.00 AS Decimal(18, 2)), CAST(2397000.00 AS Decimal(18, 2)), 6, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (30, N'Thùng nguyên đai ~ 5kg', 100, CAST(4995000.00 AS Decimal(18, 2)), CAST(3995000.00 AS Decimal(18, 2)), 6, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (31, N'Mini ~ 0.6kg', 100, CAST(599000.00 AS Decimal(18, 2)), CAST(479000.00 AS Decimal(18, 2)), 7, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (32, N'Nhỏ ~ 1kg', 100, CAST(999000.00 AS Decimal(18, 2)), CAST(799000.00 AS Decimal(18, 2)), 7, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (33, N'Trung ~ 2kg', 100, CAST(1998000.00 AS Decimal(18, 2)), CAST(1598000.00 AS Decimal(18, 2)), 7, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (34, N'Lớn ~ 3kg', 100, CAST(2997000.00 AS Decimal(18, 2)), CAST(2397000.00 AS Decimal(18, 2)), 7, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (35, N'Thùng nguyên đai ~ 5kg', 100, CAST(4995000.00 AS Decimal(18, 2)), CAST(3995000.00 AS Decimal(18, 2)), 7, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (36, N'Mini ~ 0.6kg', 100, CAST(599000.00 AS Decimal(18, 2)), CAST(479000.00 AS Decimal(18, 2)), 8, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (37, N'Nhỏ ~ 1kg', 100, CAST(999000.00 AS Decimal(18, 2)), CAST(799000.00 AS Decimal(18, 2)), 8, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (38, N'Trung ~ 2kg', 100, CAST(1998000.00 AS Decimal(18, 2)), CAST(1598000.00 AS Decimal(18, 2)), 8, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (39, N'Lớn ~ 3kg', 100, CAST(2997000.00 AS Decimal(18, 2)), CAST(2397000.00 AS Decimal(18, 2)), 8, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (40, N'Thùng nguyên đai ~ 5kg', 100, CAST(4995000.00 AS Decimal(18, 2)), CAST(3995000.00 AS Decimal(18, 2)), 8, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (41, N'Mini ~ 0.6kg', 100, CAST(599000.00 AS Decimal(18, 2)), CAST(479000.00 AS Decimal(18, 2)), 9, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (42, N'Nhỏ ~ 1kg', 100, CAST(999000.00 AS Decimal(18, 2)), CAST(799000.00 AS Decimal(18, 2)), 9, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (43, N'Trung ~ 2kg', 100, CAST(1998000.00 AS Decimal(18, 2)), CAST(1598000.00 AS Decimal(18, 2)), 9, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (44, N'Lớn ~ 3kg', 100, CAST(2997000.00 AS Decimal(18, 2)), CAST(2397000.00 AS Decimal(18, 2)), 9, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (45, N'Thùng nguyên đai ~ 5kg', 100, CAST(4995000.00 AS Decimal(18, 2)), CAST(3995000.00 AS Decimal(18, 2)), 9, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (46, N'Mini ~ 0.6kg', 100, CAST(599000.00 AS Decimal(18, 2)), CAST(479000.00 AS Decimal(18, 2)), 10, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (47, N'Nhỏ ~ 1kg', 100, CAST(999000.00 AS Decimal(18, 2)), CAST(799000.00 AS Decimal(18, 2)), 10, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (48, N'Trung ~ 2kg', 100, CAST(1998000.00 AS Decimal(18, 2)), CAST(1598000.00 AS Decimal(18, 2)), 10, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (49, N'Lớn ~ 3kg', 100, CAST(2997000.00 AS Decimal(18, 2)), CAST(2397000.00 AS Decimal(18, 2)), 10, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (50, N'Thùng nguyên đai ~ 5kg', 100, CAST(4995000.00 AS Decimal(18, 2)), CAST(3995000.00 AS Decimal(18, 2)), 10, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (51, N'Mini ~ 0.6kg', 100, CAST(599000.00 AS Decimal(18, 2)), CAST(479000.00 AS Decimal(18, 2)), 11, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (52, N'Nhỏ ~ 1kg', 100, CAST(999000.00 AS Decimal(18, 2)), CAST(799000.00 AS Decimal(18, 2)), 11, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (53, N'Trung ~ 2kg', 100, CAST(1998000.00 AS Decimal(18, 2)), CAST(1598000.00 AS Decimal(18, 2)), 11, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (54, N'Lớn ~ 3kg', 100, CAST(2997000.00 AS Decimal(18, 2)), CAST(2397000.00 AS Decimal(18, 2)), 11, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (55, N'Thùng nguyên đai ~ 5kg', 100, CAST(4995000.00 AS Decimal(18, 2)), CAST(3995000.00 AS Decimal(18, 2)), 11, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (56, N'Mini ~ 0.6kg', 100, CAST(599000.00 AS Decimal(18, 2)), CAST(479000.00 AS Decimal(18, 2)), 12, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (57, N'Nhỏ ~ 1kg', 100, CAST(999000.00 AS Decimal(18, 2)), CAST(799000.00 AS Decimal(18, 2)), 12, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (58, N'Trung ~ 2kg', 100, CAST(1998000.00 AS Decimal(18, 2)), CAST(1598000.00 AS Decimal(18, 2)), 12, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (59, N'Lớn ~ 3kg', 100, CAST(2997000.00 AS Decimal(18, 2)), CAST(2397000.00 AS Decimal(18, 2)), 12, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (60, N'Thùng nguyên đai ~ 5kg', 100, CAST(4995000.00 AS Decimal(18, 2)), CAST(3995000.00 AS Decimal(18, 2)), 12, 1)
INSERT [dbo].[ProductSpecs] ([ProductSpecId], [SpecName], [Quantity], [BasePrice], [SalePrice], [ProductId], [ProductStatus]) VALUES (62, N'bé', 1, CAST(2000.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(18, 2)), 15, 1)
SET IDENTITY_INSERT [dbo].[ProductSpecs] OFF
GO
SET IDENTITY_INSERT [dbo].[ReceiverInformations] ON 

INSERT [dbo].[ReceiverInformations] ([ReceiverId], [FullName], [PhoneNumber], [Email], [Address], [ProvinceId_GHN], [DistrictId_GHN], [WardId_GHN], [CustomerId], [IsDefault], [DistrictName_GHN], [ProvinceName_GHN], [WardName_GHN]) VALUES (9, N'Trương Nguyễn Việt Quang', N'0344338803', N'quanglequydon03@gmail.com', N'Thanh Hóa', N'260', N'2211', N'390507', N'94ed20f2-85b4-42c0-a0bf-46c4fc6ebd3b', 0, N'Huyện Sơn Hòa', N'Phú Yên', N'Xã Sơn Hà')
INSERT [dbo].[ReceiverInformations] ([ReceiverId], [FullName], [PhoneNumber], [Email], [Address], [ProvinceId_GHN], [DistrictId_GHN], [WardId_GHN], [CustomerId], [IsDefault], [DistrictName_GHN], [ProvinceName_GHN], [WardName_GHN]) VALUES (10, N'Viet Quang', N'0222333445', N'MichaelCharlotte@FUNewsManagement.org', N'Phố mới, Lào Cai', N'268', N'2046', N'220907', N'94ed20f2-85b4-42c0-a0bf-46c4fc6ebd3b', 0, N'Huyện Văn Lâm', N'Hưng Yên', N'Xã Lương Tài')
INSERT [dbo].[ReceiverInformations] ([ReceiverId], [FullName], [PhoneNumber], [Email], [Address], [ProvinceId_GHN], [DistrictId_GHN], [WardId_GHN], [CustomerId], [IsDefault], [DistrictName_GHN], [ProvinceName_GHN], [WardName_GHN]) VALUES (11, N'customer1', N'0344338803', N'admin@FUNewsManagementSystem.org', N'phuong trung van , thanh pho Ha Noi', N'252', N'1783', N'610803', N'94ed20f2-85b4-42c0-a0bf-46c4fc6ebd3b', 0, N'Huyện Năm Căn', N'Cà Mau', N'Xã Hàm Rồng')
INSERT [dbo].[ReceiverInformations] ([ReceiverId], [FullName], [PhoneNumber], [Email], [Address], [ProvinceId_GHN], [DistrictId_GHN], [WardId_GHN], [CustomerId], [IsDefault], [DistrictName_GHN], [ProvinceName_GHN], [WardName_GHN]) VALUES (12, N'quang', N'0222333445', N'Trunggptplus50@gmail.com', N'phuong trung van , thanh pho Ha Noi', N'252', N'1783', N'610801', N'94ed20f2-85b4-42c0-a0bf-46c4fc6ebd3b', 0, N'Huyện Năm Căn', N'Cà Mau', N'Thị Trấn Năm Căn')
INSERT [dbo].[ReceiverInformations] ([ReceiverId], [FullName], [PhoneNumber], [Email], [Address], [ProvinceId_GHN], [DistrictId_GHN], [WardId_GHN], [CustomerId], [IsDefault], [DistrictName_GHN], [ProvinceName_GHN], [WardName_GHN]) VALUES (13, N'quang', N'0344338803', N'quangtnvhe182422@fpt.edu.vn', N'Phố mới, Lào Cai', N'252', N'1901', N'610504', N'94ed20f2-85b4-42c0-a0bf-46c4fc6ebd3b', 1, N'Huyện Cái Nước', N'Cà Mau', N'Xã Hoà Mỹ')
INSERT [dbo].[ReceiverInformations] ([ReceiverId], [FullName], [PhoneNumber], [Email], [Address], [ProvinceId_GHN], [DistrictId_GHN], [WardId_GHN], [CustomerId], [IsDefault], [DistrictName_GHN], [ProvinceName_GHN], [WardName_GHN]) VALUES (14, N'Quang', N'0344338803', N'customer1@example.com', N'Phố mới, Lào Cai', N'258', N'1779', N'470705', N'cff91abc-ca4c-4309-be1d-aa763faaf4c4', 1, N'Huyện Đức Linh', N'Bình Thuận', N'Xã Đức Chính')
INSERT [dbo].[ReceiverInformations] ([ReceiverId], [FullName], [PhoneNumber], [Email], [Address], [ProvinceId_GHN], [DistrictId_GHN], [WardId_GHN], [CustomerId], [IsDefault], [DistrictName_GHN], [ProvinceName_GHN], [WardName_GHN]) VALUES (15, N'Quang', N'0344338803', N'quangtnvhe182422@fpt.edu.vn', N'số nhà 2', N'201', N'3440', N'13006', N'60de45a5-61da-4212-b11d-502d81d25d0d', 1, N'Quận Nam Từ Liêm', N'Hà Nội', N'Phường Phú Đô')
INSERT [dbo].[ReceiverInformations] ([ReceiverId], [FullName], [PhoneNumber], [Email], [Address], [ProvinceId_GHN], [DistrictId_GHN], [WardId_GHN], [CustomerId], [IsDefault], [DistrictName_GHN], [ProvinceName_GHN], [WardName_GHN]) VALUES (16, N'Quang', N'0222333445', N'quangtnvhe182422@fpt.edu.vn', N'số nhà 11, Chung ưu hòa bình', N'234', N'1876', N'280205', N'60de45a5-61da-4212-b11d-502d81d25d0d', 0, N'Thị xã Bỉm Sơn', N'Thanh Hóa', N'Phường Ngọc Trạo')
SET IDENTITY_INSERT [dbo].[ReceiverInformations] OFF
GO
ALTER TABLE [dbo].[ReceiverInformations] ADD  DEFAULT (N'') FOR [FullName]
GO
ALTER TABLE [dbo].[ReceiverInformations] ADD  DEFAULT (N'') FOR [PhoneNumber]
GO
ALTER TABLE [dbo].[ReceiverInformations] ADD  DEFAULT (N'') FOR [Email]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[CartItems]  WITH CHECK ADD  CONSTRAINT [FK_CartItems_Carts_CartId] FOREIGN KEY([CartId])
REFERENCES [dbo].[Carts] ([CartId])
GO
ALTER TABLE [dbo].[CartItems] CHECK CONSTRAINT [FK_CartItems_Carts_CartId]
GO
ALTER TABLE [dbo].[CartItems]  WITH CHECK ADD  CONSTRAINT [FK_CartItems_ProductSpecs_ProductSpecID] FOREIGN KEY([ProductSpecID])
REFERENCES [dbo].[ProductSpecs] ([ProductSpecId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartItems] CHECK CONSTRAINT [FK_CartItems_ProductSpecs_ProductSpecID]
GO
ALTER TABLE [dbo].[Carts]  WITH CHECK ADD  CONSTRAINT [FK_Carts_AspNetUsers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Carts] CHECK CONSTRAINT [FK_Carts_AspNetUsers_CustomerId]
GO
ALTER TABLE [dbo].[Feedbacks]  WITH CHECK ADD  CONSTRAINT [FK_Feedbacks_AspNetUsers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Feedbacks] CHECK CONSTRAINT [FK_Feedbacks_AspNetUsers_CustomerId]
GO
ALTER TABLE [dbo].[Feedbacks]  WITH CHECK ADD  CONSTRAINT [FK_Feedbacks_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Feedbacks] CHECK CONSTRAINT [FK_Feedbacks_Products_ProductId]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_CartItems_CartItemId] FOREIGN KEY([CartItemId])
REFERENCES [dbo].[CartItems] ([CartItemId])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_CartItems_CartItemId]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Feedbacks_FeedbackId] FOREIGN KEY([FeedbackId])
REFERENCES [dbo].[Feedbacks] ([FeedbackId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Feedbacks_FeedbackId]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Orders_OrderId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_AspNetUsers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_AspNetUsers_CustomerId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_ReceiverInformations_ReceiverId] FOREIGN KEY([ReceiverId])
REFERENCES [dbo].[ReceiverInformations] ([ReceiverId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_ReceiverInformations_ReceiverId]
GO
ALTER TABLE [dbo].[ProductCategories]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategories_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductCategories] CHECK CONSTRAINT [FK_ProductCategories_Categories_CategoryId]
GO
ALTER TABLE [dbo].[ProductCategories]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategories_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductCategories] CHECK CONSTRAINT [FK_ProductCategories_Products_ProductId]
GO
ALTER TABLE [dbo].[ProductImages]  WITH CHECK ADD  CONSTRAINT [FK_ProductImages_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductImages] CHECK CONSTRAINT [FK_ProductImages_Products_ProductId]
GO
ALTER TABLE [dbo].[ProductSpecs]  WITH CHECK ADD  CONSTRAINT [FK_ProductSpecs_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductSpecs] CHECK CONSTRAINT [FK_ProductSpecs_Products_ProductId]
GO
ALTER TABLE [dbo].[ReceiverInformations]  WITH CHECK ADD  CONSTRAINT [FK_ReceiverInformations_AspNetUsers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ReceiverInformations] CHECK CONSTRAINT [FK_ReceiverInformations_AspNetUsers_CustomerId]
GO
