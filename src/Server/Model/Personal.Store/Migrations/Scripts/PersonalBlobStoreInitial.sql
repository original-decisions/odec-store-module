begin tran
-- create default blob store migration

IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[Categories] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](255) NOT NULL,
    [IsApproved] [bit] NOT NULL,
    [Code] [nvarchar](128) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [SortOrder] [int] NOT NULL,
    [DateUpdated] [datetime] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.Categories] PRIMARY KEY ([Id])
)
CREATE INDEX [ix_Categories_Name] ON [dbo].[Categories]([Name], [IsApproved])
CREATE INDEX [ix_Categories_IsApproved] ON [dbo].[Categories]([IsApproved])
end

IF schema_id('store') IS NULL
    EXECUTE('CREATE SCHEMA [store]')
IF schema_id('AspNet') IS NULL
    EXECUTE('CREATE SCHEMA [AspNet]')
IF schema_id('attach') IS NULL
    EXECUTE('CREATE SCHEMA [attach]')
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[store].[Colors]') AND type in (N'U'))
begin
CREATE TABLE [store].[Colors] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](50) NOT NULL,
    [R] [int] NOT NULL,
    [G] [int] NOT NULL,
    [B] [int] NOT NULL,
    [A] [int] NOT NULL,
    [Code] [nvarchar](128) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [SortOrder] [int] NOT NULL,
    [DateUpdated] [datetime] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    CONSTRAINT [PK_store.Colors] PRIMARY KEY ([Id])
)
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[store].[Designers]') AND type in (N'U'))
begin
CREATE TABLE [store].[Designers] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max) NOT NULL,
    [Code] [nvarchar](128) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [SortOrder] [int] NOT NULL,
    [DateUpdated] [datetime] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    CONSTRAINT [PK_store.Designers] PRIMARY KEY ([Id])
)
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[store].[GoodCategories]') AND type in (N'U'))
begin
CREATE TABLE [store].[GoodCategories] (
    [GoodId] [int] NOT NULL,
    [CategoryId] [int] NOT NULL,
    CONSTRAINT [PK_store.GoodCategories] PRIMARY KEY ([GoodId], [CategoryId])
)
CREATE INDEX [IX_GoodId] ON [store].[GoodCategories]([GoodId])
CREATE INDEX [IX_CategoryId] ON [store].[GoodCategories]([CategoryId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[store].[Goods]') AND type in (N'U'))
begin
CREATE TABLE [store].[Goods] (
    [Id] [int] NOT NULL IDENTITY,
    [Articul] [nvarchar](200) NOT NULL,
    [SerialNumber] [nvarchar](200) NOT NULL,
    [Name] [nvarchar](200) NOT NULL,
    [BasePrice] [decimal](18, 2) NOT NULL,
    [MarkUp] [decimal](18, 2) NOT NULL,
    [Description] [nvarchar](max) NOT NULL,
    [ShortDescription] [nvarchar](400) NOT NULL,
    [SizeId] [int] NOT NULL,
    [Height] [decimal](18, 2),
    [Width] [decimal](18, 2),
    [Depth] [decimal](18, 2),
    [Code] [nvarchar](128) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [SortOrder] [int] NOT NULL,
    [DateUpdated] [datetime] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    CONSTRAINT [PK_store.Goods] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_SizeId] ON [store].[Goods]([SizeId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[store].[Sizes]') AND type in (N'U'))
begin
CREATE TABLE [store].[Sizes] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](15) NOT NULL,
    [ScaleId] [int] NOT NULL,
    [Code] [nvarchar](128) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [SortOrder] [int] NOT NULL,
    [DateUpdated] [datetime] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    CONSTRAINT [PK_store.Sizes] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ScaleId] ON [store].[Sizes]([ScaleId])
end 
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[store].[Scales]') AND type in (N'U'))
begin
CREATE TABLE [store].[Scales] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](30) NOT NULL,
    [Code] [nvarchar](128) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [SortOrder] [int] NOT NULL,
    [DateUpdated] [datetime] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    CONSTRAINT [PK_store.Scales] PRIMARY KEY ([Id])
)
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[store].[GoodColors]') AND type in (N'U'))
begin
CREATE TABLE [store].[GoodColors] (
    [GoodId] [int] NOT NULL,
    [ColorId] [int] NOT NULL,
    CONSTRAINT [PK_store.GoodColors] PRIMARY KEY ([GoodId], [ColorId])
)
CREATE INDEX [IX_GoodId] ON [store].[GoodColors]([GoodId])
CREATE INDEX [IX_ColorId] ON [store].[GoodColors]([ColorId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[store].[GoodDesigners]') AND type in (N'U'))
begin
CREATE TABLE [store].[GoodDesigners] (
    [GoodId] [int] NOT NULL,
    [DesignerId] [int] NOT NULL,
    CONSTRAINT [PK_store.GoodDesigners] PRIMARY KEY ([GoodId], [DesignerId])
)
CREATE INDEX [IX_GoodId] ON [store].[GoodDesigners]([GoodId])
CREATE INDEX [IX_DesignerId] ON [store].[GoodDesigners]([DesignerId])
end 
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[store].[GoodsGallery]') AND type in (N'U'))
begin
CREATE TABLE [store].[GoodsGallery] (
    [GoodId] [int] NOT NULL,
    [ImageId] [int] NOT NULL,
    [IsMain] [bit] NOT NULL,
    CONSTRAINT [PK_store.GoodsGallery] PRIMARY KEY ([GoodId], [ImageId])
)
CREATE UNIQUE INDEX [ix_goodId_IsMain_ImageId] ON [store].[GoodsGallery]([GoodId], [IsMain], [ImageId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[attach].[Attachments]') AND type in (N'U'))
begin
CREATE TABLE [attach].[Attachments] (
    [Id] [int] NOT NULL IDENTITY,
    [AttachmentTypeId] [int] NOT NULL,
    [ExtensionId] [int] NOT NULL,
    [Content] [varbinary](max),
    [IsShared] [bit] NOT NULL,
    [PublicUri] [nvarchar](max),
    [Name] [nvarchar](max) NOT NULL,
    [Code] [nvarchar](128) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [SortOrder] [int] NOT NULL,
    [DateUpdated] [datetime] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    CONSTRAINT [PK_attach.Attachments] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttachmentTypeId] ON [attach].[Attachments]([AttachmentTypeId])
CREATE INDEX [IX_ExtensionId] ON [attach].[Attachments]([ExtensionId])
end 
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[attach].[AttachmentTypes]') AND type in (N'U'))
begin
CREATE TABLE [attach].[AttachmentTypes] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max) NOT NULL,
    [Code] [nvarchar](128) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [SortOrder] [int] NOT NULL,
    [DateUpdated] [datetime] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    CONSTRAINT [PK_attach.AttachmentTypes] PRIMARY KEY ([Id])
)
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[attach].[Extensions]') AND type in (N'U'))
begin
CREATE TABLE [attach].[Extensions] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max) NOT NULL,
    [Code] [nvarchar](128) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [SortOrder] [int] NOT NULL,
    [DateUpdated] [datetime] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    CONSTRAINT [PK_attach.Extensions] PRIMARY KEY ([Id])
)
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[store].[GoodMaterials]') AND type in (N'U'))
begin
CREATE TABLE [store].[GoodMaterials] (
    [GoodId] [int] NOT NULL,
    [MaterialId] [int] NOT NULL,
    [Value] [int],
    CONSTRAINT [PK_store.GoodMaterials] PRIMARY KEY ([GoodId], [MaterialId])
)
CREATE INDEX [IX_GoodId] ON [store].[GoodMaterials]([GoodId])
CREATE INDEX [IX_MaterialId] ON [store].[GoodMaterials]([MaterialId])
end 
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[store].[Materials]') AND type in (N'U'))
begin
CREATE TABLE [store].[Materials] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max) NOT NULL,
    [Code] [nvarchar](128) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [SortOrder] [int] NOT NULL,
    [DateUpdated] [datetime] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    CONSTRAINT [PK_store.Materials] PRIMARY KEY ([Id])
)
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[store].[GoodPriceHistory]') AND type in (N'U'))
begin
CREATE TABLE [store].[GoodPriceHistory] (
    [GoodId] [int] NOT NULL,
    [DateChange] [datetime] NOT NULL,
    [Price] [decimal](18, 2) NOT NULL,
    CONSTRAINT [PK_store.GoodPriceHistory] PRIMARY KEY ([GoodId], [DateChange])
)
CREATE INDEX [IX_GoodId] ON [store].[GoodPriceHistory]([GoodId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[store].[GoodTypes]') AND type in (N'U'))
begin
CREATE TABLE [store].[GoodTypes] (
    [GoodId] [int] NOT NULL,
    [TypeId] [int] NOT NULL,
    CONSTRAINT [PK_store.GoodTypes] PRIMARY KEY ([GoodId], [TypeId])
)

CREATE INDEX [IX_GoodId] ON [store].[GoodTypes]([GoodId])
CREATE INDEX [IX_TypeId] ON [store].[GoodTypes]([TypeId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[store].[Types]') AND type in (N'U'))
begin
CREATE TABLE [store].[Types] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](200) NOT NULL,
    [Code] [nvarchar](128) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [SortOrder] [int] NOT NULL,
    [DateUpdated] [datetime] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    CONSTRAINT [PK_store.Types] PRIMARY KEY ([Id])
)
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[store].[RelativeGoods]') AND type in (N'U'))
begin
CREATE TABLE [store].[RelativeGoods] (
    [GoodId] [int] NOT NULL,
    [RelativeGoodId] [int] NOT NULL,
    CONSTRAINT [PK_store.RelativeGoods] PRIMARY KEY ([GoodId], [RelativeGoodId])
)
CREATE INDEX [IX_GoodId] ON [store].[RelativeGoods]([GoodId])
CREATE INDEX [IX_RelativeGoodId] ON [store].[RelativeGoods]([RelativeGoodId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[store].[StoreGoods]') AND type in (N'U'))
begin
CREATE TABLE [store].[StoreGoods] (
    [StoreId] [int] NOT NULL,
    [GoodId] [int] NOT NULL,
    [ReservedCounter] [int] NOT NULL,
    [StoreQuantity] [int] NOT NULL,
    [Articul] [nvarchar](255) NOT NULL,
    CONSTRAINT [PK_store.StoreGoods] PRIMARY KEY ([StoreId], [GoodId])
)
CREATE UNIQUE INDEX [ix_Articul_StoreId] ON [store].[StoreGoods]([StoreId], [Articul])
CREATE INDEX [IX_GoodId] ON [store].[StoreGoods]([GoodId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[store].[Stores]') AND type in (N'U'))
begin
CREATE TABLE [store].[Stores] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max) NOT NULL,
    [Code] [nvarchar](128) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [SortOrder] [int] NOT NULL,
    [DateUpdated] [datetime] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    CONSTRAINT [PK_store.Stores] PRIMARY KEY ([Id])
)
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.GoodCategories_dbo.Categories_CategoryId')
		begin
ALTER TABLE [store].[GoodCategories] ADD CONSTRAINT [FK_store.GoodCategories_dbo.Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.GoodCategories_store.Goods_GoodId')
		begin
ALTER TABLE [store].[GoodCategories] ADD CONSTRAINT [FK_store.GoodCategories_store.Goods_GoodId] FOREIGN KEY ([GoodId]) REFERENCES [store].[Goods] ([Id])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.Goods_store.Sizes_SizeId')
		begin
ALTER TABLE [store].[Goods] ADD CONSTRAINT [FK_store.Goods_store.Sizes_SizeId] FOREIGN KEY ([SizeId]) REFERENCES [store].[Sizes] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.Sizes_store.Scales_ScaleId')
		begin
ALTER TABLE [store].[Sizes] ADD CONSTRAINT [FK_store.Sizes_store.Scales_ScaleId] FOREIGN KEY ([ScaleId]) REFERENCES [store].[Scales] ([Id])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.GoodColors_store.Colors_ColorId')
		begin
ALTER TABLE [store].[GoodColors] ADD CONSTRAINT [FK_store.GoodColors_store.Colors_ColorId] FOREIGN KEY ([ColorId]) REFERENCES [store].[Colors] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.GoodColors_store.Goods_GoodId')
		begin
ALTER TABLE [store].[GoodColors] ADD CONSTRAINT [FK_store.GoodColors_store.Goods_GoodId] FOREIGN KEY ([GoodId]) REFERENCES [store].[Goods] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.GoodDesigners_store.Designers_DesignerId')
		begin
ALTER TABLE [store].[GoodDesigners] ADD CONSTRAINT [FK_store.GoodDesigners_store.Designers_DesignerId] FOREIGN KEY ([DesignerId]) REFERENCES [store].[Designers] ([Id])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.GoodDesigners_store.Goods_GoodId')
		begin
ALTER TABLE [store].[GoodDesigners] ADD CONSTRAINT [FK_store.GoodDesigners_store.Goods_GoodId] FOREIGN KEY ([GoodId]) REFERENCES [store].[Goods] ([Id])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.GoodsGallery_store.Goods_GoodId')
		begin
ALTER TABLE [store].[GoodsGallery] ADD CONSTRAINT [FK_store.GoodsGallery_store.Goods_GoodId] FOREIGN KEY ([GoodId]) REFERENCES [store].[Goods] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.GoodsGallery_attach.Attachments_ImageId')
		begin
ALTER TABLE [store].[GoodsGallery] ADD CONSTRAINT [FK_store.GoodsGallery_attach.Attachments_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [attach].[Attachments] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_attach.Attachments_attach.AttachmentTypes_AttachmentTypeId')
		begin
ALTER TABLE [attach].[Attachments] ADD CONSTRAINT [FK_attach.Attachments_attach.AttachmentTypes_AttachmentTypeId] FOREIGN KEY ([AttachmentTypeId]) REFERENCES [attach].[AttachmentTypes] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_attach.Attachments_attach.Extensions_ExtensionId')
		begin
ALTER TABLE [attach].[Attachments] ADD CONSTRAINT [FK_attach.Attachments_attach.Extensions_ExtensionId] FOREIGN KEY ([ExtensionId]) REFERENCES [attach].[Extensions] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.GoodMaterials_store.Goods_GoodId')
		begin
ALTER TABLE [store].[GoodMaterials] ADD CONSTRAINT [FK_store.GoodMaterials_store.Goods_GoodId] FOREIGN KEY ([GoodId]) REFERENCES [store].[Goods] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.GoodMaterials_store.Materials_MaterialId')
		begin
ALTER TABLE [store].[GoodMaterials] ADD CONSTRAINT [FK_store.GoodMaterials_store.Materials_MaterialId] FOREIGN KEY ([MaterialId]) REFERENCES [store].[Materials] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.GoodPriceHistory_store.Goods_GoodId')
		begin
ALTER TABLE [store].[GoodPriceHistory] ADD CONSTRAINT [FK_store.GoodPriceHistory_store.Goods_GoodId] FOREIGN KEY ([GoodId]) REFERENCES [store].[Goods] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.GoodTypes_store.Goods_GoodId')
		begin
ALTER TABLE [store].[GoodTypes] ADD CONSTRAINT [FK_store.GoodTypes_store.Goods_GoodId] FOREIGN KEY ([GoodId]) REFERENCES [store].[Goods] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.GoodTypes_store.Types_TypeId')
		begin
ALTER TABLE [store].[GoodTypes] ADD CONSTRAINT [FK_store.GoodTypes_store.Types_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [store].[Types] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.RelativeGoods_store.Goods_GoodId')
		begin
ALTER TABLE [store].[RelativeGoods] ADD CONSTRAINT [FK_store.RelativeGoods_store.Goods_GoodId] FOREIGN KEY ([GoodId]) REFERENCES [store].[Goods] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.RelativeGoods_store.Goods_RelativeGoodId')
		begin
ALTER TABLE [store].[RelativeGoods] ADD CONSTRAINT [FK_store.RelativeGoods_store.Goods_RelativeGoodId] FOREIGN KEY ([RelativeGoodId]) REFERENCES [store].[Goods] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.StoreGoods_store.Goods_GoodId')
		begin
ALTER TABLE [store].[StoreGoods] ADD CONSTRAINT [FK_store.StoreGoods_store.Goods_GoodId] FOREIGN KEY ([GoodId]) REFERENCES [store].[Goods] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.StoreGoods_store.Stores_StoreId')
		begin
ALTER TABLE [store].[StoreGoods] ADD CONSTRAINT [FK_store.StoreGoods_store.Stores_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [store].[Stores] ([Id])
end 

-- ====================================== --
-- ====================================== --
-- ====================================== --

IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[AspNet].[Roles]') AND type in (N'U'))
begin
CREATE TABLE [AspNet].[Roles] (
    [Id] [int] NOT NULL IDENTITY,
    [InRoleId] [int],
    [Scope] [nvarchar](max),
    [Name] [nvarchar](256) NOT NULL,
    CONSTRAINT [PK_AspNet.Roles] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_InRoleId] ON [AspNet].[Roles]([InRoleId])
CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNet].[Roles]([Name])
end 
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[AspNet].[UserRoles]') AND type in (N'U'))
begin
CREATE TABLE [AspNet].[UserRoles] (
    [UserId] [int] NOT NULL,
    [RoleId] [int] NOT NULL,
    CONSTRAINT [PK_AspNet.UserRoles] PRIMARY KEY ([UserId], [RoleId])
)
CREATE INDEX [IX_UserId] ON [AspNet].[UserRoles]([UserId])
CREATE INDEX [IX_RoleId] ON [AspNet].[UserRoles]([RoleId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[AspNet].[UserClaims]') AND type in (N'U'))
begin
CREATE TABLE [AspNet].[UserClaims] (
    [Id] [int] NOT NULL IDENTITY,
    [UserId] [int] NOT NULL,
    [ClaimType] [nvarchar](max),
    [ClaimValue] [nvarchar](max),
    CONSTRAINT [PK_AspNet.UserClaims] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_UserId] ON [AspNet].[UserClaims]([UserId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[AspNet].[UserLogins]') AND type in (N'U'))
begin
CREATE TABLE [AspNet].[UserLogins] (
    [LoginProvider] [nvarchar](128) NOT NULL,
    [ProviderKey] [nvarchar](128) NOT NULL,
    [UserId] [int] NOT NULL,
    CONSTRAINT [PK_AspNet.UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey], [UserId])
)
CREATE INDEX [IX_UserId] ON [AspNet].[UserLogins]([UserId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[AspNet].[Users]') AND type in (N'U'))
begin
CREATE TABLE [AspNet].[Users] (
    [Id] [int] NOT NULL IDENTITY,
    [Rating] [decimal](18, 2) NOT NULL,
    [ProfilePicturePath] [nvarchar](max),
    [FirstName] [nvarchar](max),
    [LastName] [nvarchar](max),
    [Patronymic] [nvarchar](max),
    [DateUpdated] [datetime],
    [LastActivityDate] [datetime],
    [LastLogin] [datetime],
    [RemindInDays] [int] NOT NULL,
    [DateRegistration] [datetime] NOT NULL,
    [Description] [nvarchar](max),
    [Email] [nvarchar](256),
    [EmailConfirmed] [bit] NOT NULL,
    [PasswordHash] [nvarchar](max),
    [SecurityStamp] [nvarchar](max),
    [PhoneNumber] [nvarchar](max),
    [PhoneNumberConfirmed] [bit] NOT NULL,
    [TwoFactorEnabled] [bit] NOT NULL,
    [LockoutEndDateUtc] [datetime],
    [LockoutEnabled] [bit] NOT NULL,
    [AccessFailedCount] [int] NOT NULL,
    [UserName] [nvarchar](256) NOT NULL,
    CONSTRAINT [PK_AspNet.Users] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [UserNameIndex] ON [AspNet].[Users]([UserName])
end 
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[store].[UserStores]') AND type in (N'U'))
begin
CREATE TABLE [store].[UserStores] (
    [UserId] [int] NOT NULL,
    [StoreId] [int] NOT NULL,
    [IsActive] [bit] NOT NULL,
    CONSTRAINT [PK_store.UserStores] PRIMARY KEY ([UserId], [StoreId])
)
CREATE INDEX [IX_UserId] ON [store].[UserStores]([UserId])
CREATE INDEX [IX_StoreId] ON [store].[UserStores]([StoreId])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_AspNet.Roles_AspNet.Roles_InRoleId')
		begin
ALTER TABLE [AspNet].[Roles] ADD CONSTRAINT [FK_AspNet.Roles_AspNet.Roles_InRoleId] FOREIGN KEY ([InRoleId]) REFERENCES [AspNet].[Roles] ([Id])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_AspNet.UserRoles_AspNet.Roles_RoleId')
		begin
ALTER TABLE [AspNet].[UserRoles] ADD CONSTRAINT [FK_AspNet.UserRoles_AspNet.Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNet].[Roles] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_AspNet.UserRoles_AspNet.Users_UserId')
		begin
ALTER TABLE [AspNet].[UserRoles] ADD CONSTRAINT [FK_AspNet.UserRoles_AspNet.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNet].[Users] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_AspNet.UserClaims_AspNet.Users_UserId')
		begin
ALTER TABLE [AspNet].[UserClaims] ADD CONSTRAINT [FK_AspNet.UserClaims_AspNet.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNet].[Users] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_AspNet.UserLogins_AspNet.Users_UserId')
		begin
ALTER TABLE [AspNet].[UserLogins] ADD CONSTRAINT [FK_AspNet.UserLogins_AspNet.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNet].[Users] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.UserStores_store.Stores_StoreId')
		begin
ALTER TABLE [store].[UserStores] ADD CONSTRAINT [FK_store.UserStores_store.Stores_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [store].[Stores] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_store.UserStores_AspNet.Users_UserId')
		begin
ALTER TABLE [store].[UserStores] ADD CONSTRAINT [FK_store.UserStores_AspNet.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNet].[Users] ([Id])
end 

commit tran
