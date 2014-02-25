Insert into dbo.[Order] Values(1,'Yang Yu',GETDATE());
Insert into dbo.[Order] Values(2,'Yang Yu1',GETDATE());
Insert into dbo.[Order] Values(3,'Yang Yu2',GETDATE());
Insert into dbo.[Order] Values(4,'Yang Yu3',GETDATE());
Insert into dbo.[Order] Values(5,'Yang Yu4',GETDATE());

Insert into dbo.Product values(0,'Macbook Pro Retina Display 2014',3000,800);
Insert into dbo.Product values(1,'Macbook Pro 2014',2000,500);
Insert into dbo.Product values(2,'Mac Mini',500,300);
Insert into dbo.Product values(3,'Macbook Air',2000,500);

Insert into dbo.OrderDetail values(0,1,0,0);
Insert into dbo.OrderDetail values(1,2,0,1);
Insert into dbo.OrderDetail values(2,2,0,2);
Insert into dbo.OrderDetail values(3,3,1,0);

Update dbo.[Order]
Set [Date] = GETDATE()