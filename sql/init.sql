IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'FoodDist')
BEGIN
    CREATE DATABASE FoodDist;
END
GO

USE FoodDist;
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Parcels' AND xtype='U')
BEGIN
CREATE TABLE Parcels (
                         Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                         BeneficiaryPhone NVARCHAR(50) NOT NULL,
                         Status NVARCHAR(50) NOT NULL,
                         CreatedAt DATETIME2 DEFAULT GETDATE()
);
END
GO
