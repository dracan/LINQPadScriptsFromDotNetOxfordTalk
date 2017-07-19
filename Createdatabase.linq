<Query Kind="SQL">
  <Connection>
    <ID>8e1781eb-e464-4966-87b6-a99132c91883</ID>
    <Persist>true</Persist>
    <Server>DAN-MBP</Server>
    <NoCapitalization>true</NoCapitalization>
    <Database>LINQPadTalkOMS</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

if exists (select * from sys.tables where name = 'OrderItem')
    drop table [OrderItem]
go

if exists (select * from sys.tables where name = 'OrderItem')
    drop table [OrderItem]
go

if exists (select * from sys.tables where name = 'Product')
    drop table [Product]
go

if exists (select * from sys.tables where name = 'Order')
    drop table [Order]
go

if exists (select * from sys.tables where name = 'User')
    drop table [User]
go

create table [User]
(
    ID uniqueidentifier not null primary key,
    FirstName nvarchar(64) not null,
    LastName nvarchar(64) not null
)
go

create table [Order]
(
    ID uniqueidentifier not null primary key,
    TotalPrice decimal(19,2) not null,
    UserID uniqueidentifier not null CONSTRAINT FK_User_ID REFERENCES [User](ID),
)
go

create table [Product]
(
    ID uniqueidentifier not null primary key,
    Name nvarchar(max) not null,
    Description nvarchar(max) not null,
    Price decimal(19,2) not null,
    ImageUri nvarchar(max) not null,
)
go

create table [OrderItem]
(
    ID uniqueidentifier not null primary key,
    OrderID uniqueidentifier not null CONSTRAINT FK_Order_ID REFERENCES [Order](ID),
    ProductID uniqueidentifier not null CONSTRAINT FK_Product_ID REFERENCES [Product](ID),
    Quantity int not null,
    SalePrice decimal(19,2) not null,
    DiscountID uniqueidentifier null
)
go