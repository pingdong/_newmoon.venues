IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190717104714_Init')
BEGIN
    CREATE TABLE [Requests] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Time] datetime2 NOT NULL,
        CONSTRAINT [PK_Requests] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190717104714_Init')
BEGIN
    CREATE TABLE [dbo].[Places] (
        [Id] uniqueidentifier NOT NULL,
        [TenantId] uniqueidentifier NOT NULL,
        [PlaceName] nvarchar(200) NOT NULL,
        [AddressNo] nvarchar(20) NOT NULL,
        [AddressStreet] nvarchar(100) NOT NULL,
        [AddressCity] nvarchar(40) NOT NULL,
        [AddressState] nvarchar(40) NOT NULL,
        [AddressCountry] nvarchar(40) NOT NULL,
        [AddressZipCode] nvarchar(10) NOT NULL,
        [StateId] int NOT NULL,
        CONSTRAINT [PK_Places] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190717104714_Init')
BEGIN
    CREATE INDEX [IX_Places_Id_TenantId] ON [dbo].[Places] ([Id], [TenantId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190717104714_Init')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190717104714_Init', N'2.2.6-servicing-10079');
END;

GO

