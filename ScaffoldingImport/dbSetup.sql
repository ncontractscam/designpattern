CREATE TABLE [dbo].[Customer](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[address] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Item](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[UnitPrice] [money] NOT NULL
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[Order](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Total] [money] NOT NULL,
	[CustomerId] [bigint] NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OrderItem](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Subtotal] [money] NOT NULL,
	[ItemId] [bigint] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Item] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([Id])
GO

ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Item]
GO

ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO

ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Order]
GO




ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO

ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customers]
GO



