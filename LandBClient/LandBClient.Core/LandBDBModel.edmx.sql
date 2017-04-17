
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/17/2017 14:25:13
-- Generated from EDMX file: D:\GitHub\LandBClient\LandBClient\LandBClient.Core\LandBDBModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LandBDB_Test];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [CustomerID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [EmailID] nvarchar(max)  NULL,
    [IsVisited] nvarchar(5)  NULL,
    [Status] nvarchar(max)  NULL,
    [LeadSource] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [ContactDate] datetime  NULL,
    [OtherReason] nvarchar(255)  NULL
);
GO

-- Creating table 'DeletedCustomers'
CREATE TABLE [dbo].[DeletedCustomers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [EmailID] nvarchar(max)  NOT NULL,
    [IsVisited] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LeadSource] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [DeletedBy] nvarchar(max)  NULL,
    [OtherReason] nvarchar(255)  NULL
);
GO

-- Creating table 'TUsers'
CREATE TABLE [dbo].[TUsers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [UserName] nvarchar(max)  NULL,
    [Password] nvarchar(max)  NULL,
    [Role] nvarchar(max)  NULL,
    [EmailAddress] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [Gender] nvarchar(10)  NULL,
    [CreatedDate] datetime  NULL
);
GO

-- Creating table 'VisitedCustomers'
CREATE TABLE [dbo].[VisitedCustomers] (
    [CustomerID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [EmailID] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LeadSource] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [ContactDate] datetime  NULL,
    [OtherReason] nvarchar(255)  NULL,
    [IsVisited] nvarchar(5)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CustomerID] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([CustomerID] ASC);
GO

-- Creating primary key on [ID] in table 'DeletedCustomers'
ALTER TABLE [dbo].[DeletedCustomers]
ADD CONSTRAINT [PK_DeletedCustomers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'TUsers'
ALTER TABLE [dbo].[TUsers]
ADD CONSTRAINT [PK_TUsers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [CustomerID] in table 'VisitedCustomers'
ALTER TABLE [dbo].[VisitedCustomers]
ADD CONSTRAINT [PK_VisitedCustomers]
    PRIMARY KEY CLUSTERED ([CustomerID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------