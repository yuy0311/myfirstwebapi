CREATE TABLE [dbo].[OrderDetail] (
    [Id]       INT NOT NULL,
    [Quantity] INT NOT NULL,
    [OrderId] INT NULL, 
    [ProductId] INT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY ([OrderId]) REFERENCES [Order]([Id]), 
    CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id])
);

