CREATE DATABASE IMS

USE IMS

CREATE TABLE [dbo].[orders] (
    [id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [user] VARCHAR(100) NOT NULL,
    [details] VARCHAR(5000) NOT NULL,
    [price] VARCHAR(50) NOT NULL,
    [paid] VARCHAR(30) NOT NULL,
    [isDeleted] INT NULL,
    [isPaid] INT NULL
);

CREATE TABLE [dbo].[Products] (
    [id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [model] VARCHAR(100) NOT NULL,
    [part] VARCHAR(100) NOT NULL,
    [type] VARCHAR(50) NOT NULL,
    [price] VARCHAR(50) NOT NULL,
    [instock] INT NOT NULL,
    [isDeleted] INT NULL
);

CREATE TABLE [dbo].[users] (
    [id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [first] VARCHAR(100) NOT NULL,
    [last] VARCHAR(100) NOT NULL,
    [username] VARCHAR(100) NOT NULL UNIQUE,
    [phone] VARCHAR(20) NULL,
    [password] VARCHAR(50) NOT NULL,
    [usertype] VARCHAR(20) NOT NULL
);

INSERT INTO users ([first],[last],[username],[password],[usertype],[phone]) VALUES
	('admin','admin','admin','admin123','admin',LEFT('123456789456',15)),
	('manager','manager','manager','manager123','manager',LEFT('123456789456',15)),
	('cashier','cashier','counter','counter123','member',LEFT('987654231654',15))