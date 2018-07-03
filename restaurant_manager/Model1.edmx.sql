
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/03/2018 00:19:37
-- Generated from EDMX file: C:\Users\Dyoma\source\repos\restaurant_manager\restaurant_manager\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [restaurant];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DishesDishes_categories]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DishesSet] DROP CONSTRAINT [FK_DishesDishes_categories];
GO
IF OBJECT_ID(N'[dbo].[FK_DishesFormula]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FormulaSet] DROP CONSTRAINT [FK_DishesFormula];
GO
IF OBJECT_ID(N'[dbo].[FK_Order_DishesDishes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order_DishesSet] DROP CONSTRAINT [FK_Order_DishesDishes];
GO
IF OBJECT_ID(N'[dbo].[FK_Order_DishesTables]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order_DishesSet] DROP CONSTRAINT [FK_Order_DishesTables];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_categoryProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductSet] DROP CONSTRAINT [FK_Product_categoryProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductDishes_Dishes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductDishes] DROP CONSTRAINT [FK_ProductDishes_Dishes];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductDishes_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductDishes] DROP CONSTRAINT [FK_ProductDishes_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductFormula]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FormulaSet] DROP CONSTRAINT [FK_ProductFormula];
GO
IF OBJECT_ID(N'[dbo].[FK_Ready_DishesOrder_Dishes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order_DishesSet] DROP CONSTRAINT [FK_Ready_DishesOrder_Dishes];
GO
IF OBJECT_ID(N'[dbo].[FK_ReservationGuests]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReservationSet] DROP CONSTRAINT [FK_ReservationGuests];
GO
IF OBJECT_ID(N'[dbo].[FK_ReservationTables]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReservationSet] DROP CONSTRAINT [FK_ReservationTables];
GO
IF OBJECT_ID(N'[dbo].[FK_Staff_PosStaff]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffSet] DROP CONSTRAINT [FK_Staff_PosStaff];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Dishes_categoriesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dishes_categoriesSet];
GO
IF OBJECT_ID(N'[dbo].[DishesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DishesSet];
GO
IF OBJECT_ID(N'[dbo].[FormulaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FormulaSet];
GO
IF OBJECT_ID(N'[dbo].[GuestsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GuestsSet];
GO
IF OBJECT_ID(N'[dbo].[Order_DishesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Order_DishesSet];
GO
IF OBJECT_ID(N'[dbo].[Product_categorySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product_categorySet];
GO
IF OBJECT_ID(N'[dbo].[ProductDishes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductDishes];
GO
IF OBJECT_ID(N'[dbo].[ProductSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductSet];
GO
IF OBJECT_ID(N'[dbo].[Ready_DishesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ready_DishesSet];
GO
IF OBJECT_ID(N'[dbo].[ReservationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReservationSet];
GO
IF OBJECT_ID(N'[dbo].[Staff_PosSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Staff_PosSet];
GO
IF OBJECT_ID(N'[dbo].[StaffSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StaffSet];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[TablesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TablesSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ProductSet'
CREATE TABLE [dbo].[ProductSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Product_categoryId] int  NOT NULL,
    [count] bigint  NOT NULL,
    [Unit] nvarchar(max)  NULL,
    [IsSelected] bit  NULL
);
GO

-- Creating table 'Product_categorySet'
CREATE TABLE [dbo].[Product_categorySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DishesSet'
CREATE TABLE [dbo].[DishesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] nvarchar(max)  NOT NULL,
    [Dishes_categoriesId] int  NOT NULL,
    [Weight] nvarchar(max)  NOT NULL,
    [IsSelected] bit  NULL
);
GO

-- Creating table 'GuestsSet'
CREATE TABLE [dbo].[GuestsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [FullName] nvarchar(max)  NULL
);
GO

-- Creating table 'ReservationSet'
CREATE TABLE [dbo].[ReservationSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Time] datetime  NOT NULL,
    [Guests_Id] int  NOT NULL,
    [Tables_Id] int  NOT NULL
);
GO

-- Creating table 'Staff_PosSet'
CREATE TABLE [dbo].[Staff_PosSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Position] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'StaffSet'
CREATE TABLE [dbo].[StaffSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [login] nvarchar(max)  NOT NULL,
    [password] nvarchar(max)  NOT NULL,
    [Staff_PosId] int  NOT NULL,
    [phone_number] nvarchar(max)  NULL,
    [secret_word] nvarchar(max)  NULL
);
GO

-- Creating table 'Order_DishesSet'
CREATE TABLE [dbo].[Order_DishesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Ready_Dishes_Id] int  NOT NULL,
    [Dishes_Id] int  NOT NULL,
    [Tables_Id] int  NOT NULL
);
GO

-- Creating table 'Ready_DishesSet'
CREATE TABLE [dbo].[Ready_DishesSet] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Dishes_categoriesSet'
CREATE TABLE [dbo].[Dishes_categoriesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FormulaSet'
CREATE TABLE [dbo].[FormulaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DishesId] int  NOT NULL,
    [ProductId] int  NOT NULL,
    [Count] float  NOT NULL
);
GO

-- Creating table 'TablesSet'
CREATE TABLE [dbo].[TablesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Num] int  NULL
);
GO

-- Creating table 'ProductDishes'
CREATE TABLE [dbo].[ProductDishes] (
    [DishesSet_Id] int  NOT NULL,
    [ProductSet_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [PK_ProductSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Product_categorySet'
ALTER TABLE [dbo].[Product_categorySet]
ADD CONSTRAINT [PK_Product_categorySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DishesSet'
ALTER TABLE [dbo].[DishesSet]
ADD CONSTRAINT [PK_DishesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GuestsSet'
ALTER TABLE [dbo].[GuestsSet]
ADD CONSTRAINT [PK_GuestsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ReservationSet'
ALTER TABLE [dbo].[ReservationSet]
ADD CONSTRAINT [PK_ReservationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Staff_PosSet'
ALTER TABLE [dbo].[Staff_PosSet]
ADD CONSTRAINT [PK_Staff_PosSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StaffSet'
ALTER TABLE [dbo].[StaffSet]
ADD CONSTRAINT [PK_StaffSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Order_DishesSet'
ALTER TABLE [dbo].[Order_DishesSet]
ADD CONSTRAINT [PK_Order_DishesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Ready_DishesSet'
ALTER TABLE [dbo].[Ready_DishesSet]
ADD CONSTRAINT [PK_Ready_DishesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Id] in table 'Dishes_categoriesSet'
ALTER TABLE [dbo].[Dishes_categoriesSet]
ADD CONSTRAINT [PK_Dishes_categoriesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FormulaSet'
ALTER TABLE [dbo].[FormulaSet]
ADD CONSTRAINT [PK_FormulaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TablesSet'
ALTER TABLE [dbo].[TablesSet]
ADD CONSTRAINT [PK_TablesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [DishesSet_Id], [ProductSet_Id] in table 'ProductDishes'
ALTER TABLE [dbo].[ProductDishes]
ADD CONSTRAINT [PK_ProductDishes]
    PRIMARY KEY CLUSTERED ([DishesSet_Id], [ProductSet_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Product_categoryId] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [FK_Product_categoryProduct]
    FOREIGN KEY ([Product_categoryId])
    REFERENCES [dbo].[Product_categorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_categoryProduct'
CREATE INDEX [IX_FK_Product_categoryProduct]
ON [dbo].[ProductSet]
    ([Product_categoryId]);
GO

-- Creating foreign key on [Ready_Dishes_Id] in table 'Order_DishesSet'
ALTER TABLE [dbo].[Order_DishesSet]
ADD CONSTRAINT [FK_Ready_DishesOrder_Dishes]
    FOREIGN KEY ([Ready_Dishes_Id])
    REFERENCES [dbo].[Ready_DishesSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Ready_DishesOrder_Dishes'
CREATE INDEX [IX_FK_Ready_DishesOrder_Dishes]
ON [dbo].[Order_DishesSet]
    ([Ready_Dishes_Id]);
GO

-- Creating foreign key on [Staff_PosId] in table 'StaffSet'
ALTER TABLE [dbo].[StaffSet]
ADD CONSTRAINT [FK_Staff_PosStaff]
    FOREIGN KEY ([Staff_PosId])
    REFERENCES [dbo].[Staff_PosSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Staff_PosStaff'
CREATE INDEX [IX_FK_Staff_PosStaff]
ON [dbo].[StaffSet]
    ([Staff_PosId]);
GO

-- Creating foreign key on [Dishes_categoriesId] in table 'DishesSet'
ALTER TABLE [dbo].[DishesSet]
ADD CONSTRAINT [FK_DishesDishes_categories]
    FOREIGN KEY ([Dishes_categoriesId])
    REFERENCES [dbo].[Dishes_categoriesSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DishesDishes_categories'
CREATE INDEX [IX_FK_DishesDishes_categories]
ON [dbo].[DishesSet]
    ([Dishes_categoriesId]);
GO

-- Creating foreign key on [DishesId] in table 'FormulaSet'
ALTER TABLE [dbo].[FormulaSet]
ADD CONSTRAINT [FK_DishesFormula]
    FOREIGN KEY ([DishesId])
    REFERENCES [dbo].[DishesSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DishesFormula'
CREATE INDEX [IX_FK_DishesFormula]
ON [dbo].[FormulaSet]
    ([DishesId]);
GO

-- Creating foreign key on [ProductId] in table 'FormulaSet'
ALTER TABLE [dbo].[FormulaSet]
ADD CONSTRAINT [FK_ProductFormula]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[ProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductFormula'
CREATE INDEX [IX_FK_ProductFormula]
ON [dbo].[FormulaSet]
    ([ProductId]);
GO

-- Creating foreign key on [Dishes_Id] in table 'Order_DishesSet'
ALTER TABLE [dbo].[Order_DishesSet]
ADD CONSTRAINT [FK_Order_DishesDishes]
    FOREIGN KEY ([Dishes_Id])
    REFERENCES [dbo].[DishesSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_DishesDishes'
CREATE INDEX [IX_FK_Order_DishesDishes]
ON [dbo].[Order_DishesSet]
    ([Dishes_Id]);
GO

-- Creating foreign key on [DishesSet_Id] in table 'ProductDishes'
ALTER TABLE [dbo].[ProductDishes]
ADD CONSTRAINT [FK_ProductDishes_Dishes]
    FOREIGN KEY ([DishesSet_Id])
    REFERENCES [dbo].[DishesSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProductSet_Id] in table 'ProductDishes'
ALTER TABLE [dbo].[ProductDishes]
ADD CONSTRAINT [FK_ProductDishes_Product]
    FOREIGN KEY ([ProductSet_Id])
    REFERENCES [dbo].[ProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductDishes_Product'
CREATE INDEX [IX_FK_ProductDishes_Product]
ON [dbo].[ProductDishes]
    ([ProductSet_Id]);
GO

-- Creating foreign key on [Tables_Id] in table 'Order_DishesSet'
ALTER TABLE [dbo].[Order_DishesSet]
ADD CONSTRAINT [FK_Order_DishesTables]
    FOREIGN KEY ([Tables_Id])
    REFERENCES [dbo].[TablesSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_DishesTables'
CREATE INDEX [IX_FK_Order_DishesTables]
ON [dbo].[Order_DishesSet]
    ([Tables_Id]);
GO

-- Creating foreign key on [Guests_Id] in table 'ReservationSet'
ALTER TABLE [dbo].[ReservationSet]
ADD CONSTRAINT [FK_ReservationGuests]
    FOREIGN KEY ([Guests_Id])
    REFERENCES [dbo].[GuestsSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReservationGuests'
CREATE INDEX [IX_FK_ReservationGuests]
ON [dbo].[ReservationSet]
    ([Guests_Id]);
GO

-- Creating foreign key on [Tables_Id] in table 'ReservationSet'
ALTER TABLE [dbo].[ReservationSet]
ADD CONSTRAINT [FK_ReservationTables]
    FOREIGN KEY ([Tables_Id])
    REFERENCES [dbo].[TablesSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReservationTables'
CREATE INDEX [IX_FK_ReservationTables]
ON [dbo].[ReservationSet]
    ([Tables_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------