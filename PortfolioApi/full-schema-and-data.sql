IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

CREATE TABLE [Projects] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Link] nvarchar(max) NULL,
    [Image] nvarchar(max) NULL,
    CONSTRAINT [PK_Projects] PRIMARY KEY ([Id])
);

CREATE TABLE [Sections] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [Image] nvarchar(max) NULL,
    CONSTRAINT [PK_Sections] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Educations] (
    [Id] int NOT NULL IDENTITY,
    [SectionId] int NOT NULL,
    [School] nvarchar(max) NOT NULL,
    [Course] nvarchar(max) NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NULL,
    CONSTRAINT [PK_Educations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Educations_Sections_SectionId] FOREIGN KEY ([SectionId]) REFERENCES [Sections] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Hobbies] (
    [Id] int NOT NULL IDENTITY,
    [SectionId] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Hobbies] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Hobbies_Sections_SectionId] FOREIGN KEY ([SectionId]) REFERENCES [Sections] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [SoftSkills] (
    [Id] int NOT NULL IDENTITY,
    [SectionId] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Level] nvarchar(max) NULL,
    CONSTRAINT [PK_SoftSkills] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SoftSkills_Sections_SectionId] FOREIGN KEY ([SectionId]) REFERENCES [Sections] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [TechnicalSkills] (
    [Id] int NOT NULL IDENTITY,
    [SectionId] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_TechnicalSkills] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TechnicalSkills_Sections_SectionId] FOREIGN KEY ([SectionId]) REFERENCES [Sections] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [WorkExperiences] (
    [Id] int NOT NULL IDENTITY,
    [SectionId] int NOT NULL,
    [Company] nvarchar(max) NOT NULL,
    [Position] nvarchar(max) NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NULL,
    CONSTRAINT [PK_WorkExperiences] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WorkExperiences_Sections_SectionId] FOREIGN KEY ([SectionId]) REFERENCES [Sections] ([Id]) ON DELETE CASCADE
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] ON;
INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
VALUES (N'admin-role-id', NULL, N'Admin', N'ADMIN'),
(N'reader-role-id', NULL, N'Reader', N'READER');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
    SET IDENTITY_INSERT [AspNetUsers] ON;
INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
VALUES (N'00000000-0000-0000-0000-000000000001', 0, N'7345e2b2-d5c3-4315-ad9f-73041a6bb4c9', NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, N'ADMIN', N'$2b$12$UHXqd4tw92f.Tl1Ybp1hAuxOMoDoG.SMmgSlW7bgxQxRL4HzTm4Ei', NULL, CAST(0 AS bit), N'00000000-0000-0000-0000-0000000000AA', CAST(0 AS bit), N'admin'),
(N'00000000-0000-0000-0000-000000000002', 0, N'14ef0f04-3943-44a9-9c8c-d0e5da9c19b5', NULL, CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, N'READER', N'$2b$12$CHlyLjz2jhDsSQ8knDmnMuEHtd4l3U8dGksLOeqepafHpJUuet9WO', NULL, CAST(0 AS bit), N'00000000-0000-0000-0000-0000000000BB', CAST(0 AS bit), N'reader');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
    SET IDENTITY_INSERT [AspNetUsers] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Image', N'Link', N'Name') AND [object_id] = OBJECT_ID(N'[Projects]'))
    SET IDENTITY_INSERT [Projects] ON;
INSERT INTO [Projects] ([Id], [Description], [Image], [Link], [Name])
VALUES (1, N'Vue client that consumes Starwars API', N'project1.jpg', N'https://www.github.com/cheungkinwong/javascript-framework-test', N'SWApi'),
(2, N'Game project using js and css', N'project2.jpg', N'https://www.github.com/cheungkinwong/memory-game', N'Memory game');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Image', N'Link', N'Name') AND [object_id] = OBJECT_ID(N'[Projects]'))
    SET IDENTITY_INSERT [Projects] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Image', N'Title') AND [object_id] = OBJECT_ID(N'[Sections]'))
    SET IDENTITY_INSERT [Sections] ON;
INSERT INTO [Sections] ([Id], [Description], [Image], [Title])
VALUES (1, N'Full-stack developer passionate about combining technology and design for seamless user experiences.', N'about.png', N'About Me'),
(2, N'Formal education and certifications in development and design.', N'education.png', N'Education'),
(3, N'Professional roles in front-end development, prepress, and lab work.', N'experience.png', N'Experience'),
(4, N'Tools and technologies used in development and deployment.', N'techskills.png', N'Technical Skills'),
(5, N'Key interpersonal and problem-solving strengths.', N'softskills.png', N'Soft Skills'),
(6, N'Interests outside of work.', N'hobbies.png', N'Hobbies');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Image', N'Title') AND [object_id] = OBJECT_ID(N'[Sections]'))
    SET IDENTITY_INSERT [Sections] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AspNetUserRoles] ON;
INSERT INTO [AspNetUserRoles] ([RoleId], [UserId])
VALUES (N'admin-role-id', N'00000000-0000-0000-0000-000000000001'),
(N'reader-role-id', N'00000000-0000-0000-0000-000000000002');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AspNetUserRoles] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'SectionId') AND [object_id] = OBJECT_ID(N'[Hobbies]'))
    SET IDENTITY_INSERT [Hobbies] ON;
INSERT INTO [Hobbies] ([Id], [Name], [SectionId])
VALUES (1, N'Bass', 6),
(2, N'TTRPG', 6),
(3, N'Games', 6),
(4, N'Reading', 6);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'SectionId') AND [object_id] = OBJECT_ID(N'[Hobbies]'))
    SET IDENTITY_INSERT [Hobbies] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Level', N'Name', N'SectionId') AND [object_id] = OBJECT_ID(N'[SoftSkills]'))
    SET IDENTITY_INSERT [SoftSkills] ON;
INSERT INTO [SoftSkills] ([Id], [Level], [Name], [SectionId])
VALUES (1, NULL, N'Communication', 5),
(2, NULL, N'Problem Solving', 5),
(3, NULL, N'Critical Thinking', 5),
(4, NULL, N'Growth Mindset', 5),
(5, NULL, N'Attention to Detail', 5);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Level', N'Name', N'SectionId') AND [object_id] = OBJECT_ID(N'[SoftSkills]'))
    SET IDENTITY_INSERT [SoftSkills] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'SectionId') AND [object_id] = OBJECT_ID(N'[TechnicalSkills]'))
    SET IDENTITY_INSERT [TechnicalSkills] ON;
INSERT INTO [TechnicalSkills] ([Id], [Name], [SectionId])
VALUES (1, N'Vue.js / Nuxt.js', 4),
(2, N'React', 4),
(3, N'C# / .NET', 4),
(4, N'Razor / Blazor', 4),
(5, N'HTML / CSS', 4),
(6, N'Figma / Adobe XD', 4),
(7, N'MSSQL', 4),
(8, N'Git / GitHub', 4),
(9, N'OAuth / SSO', 4),
(10, N'Stripe / Mollie / Paypal', 4);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'SectionId') AND [object_id] = OBJECT_ID(N'[TechnicalSkills]'))
    SET IDENTITY_INSERT [TechnicalSkills] OFF;

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

CREATE INDEX [IX_Educations_SectionId] ON [Educations] ([SectionId]);

CREATE INDEX [IX_Hobbies_SectionId] ON [Hobbies] ([SectionId]);

CREATE INDEX [IX_SoftSkills_SectionId] ON [SoftSkills] ([SectionId]);

CREATE INDEX [IX_TechnicalSkills_SectionId] ON [TechnicalSkills] ([SectionId]);

CREATE INDEX [IX_WorkExperiences_SectionId] ON [WorkExperiences] ([SectionId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250714131953_initialcreate', N'9.0.6');

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'42a68fc7-7ade-4de8-9600-021d24f1ed2c'
WHERE [Id] = N'00000000-0000-0000-0000-000000000001';
SELECT @@ROWCOUNT;


UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'6d3fd674-3e2f-49d1-a67f-f3c1a0afc9f9'
WHERE [Id] = N'00000000-0000-0000-0000-000000000002';
SELECT @@ROWCOUNT;


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Course', N'EndDate', N'School', N'SectionId', N'StartDate') AND [object_id] = OBJECT_ID(N'[Educations]'))
    SET IDENTITY_INSERT [Educations] ON;
INSERT INTO [Educations] ([Id], [Course], [EndDate], [School], [SectionId], [StartDate])
VALUES (1, N'Full-stack developer', NULL, N'VDAB', 2, '2024-01-01T00:00:00.0000000'),
(2, N'Front-end developer', '2020-01-01T00:00:00.0000000', N'BeCode', 2, '2019-01-01T00:00:00.0000000'),
(3, N'DTP-Prepress', '2011-01-01T00:00:00.0000000', N'VDAB', 2, '2010-01-01T00:00:00.0000000'),
(4, N'Chemistry', '2009-01-01T00:00:00.0000000', N'AP Hoge School', 2, '2007-01-01T00:00:00.0000000');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Course', N'EndDate', N'School', N'SectionId', N'StartDate') AND [object_id] = OBJECT_ID(N'[Educations]'))
    SET IDENTITY_INSERT [Educations] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Company', N'EndDate', N'Position', N'SectionId', N'StartDate') AND [object_id] = OBJECT_ID(N'[WorkExperiences]'))
    SET IDENTITY_INSERT [WorkExperiences] ON;
INSERT INTO [WorkExperiences] ([Id], [Company], [EndDate], [Position], [SectionId], [StartDate])
VALUES (1, N'ZAPFLOOR', '2024-01-01T00:00:00.0000000', N'Front-End Developer', 3, '2020-01-01T00:00:00.0000000'),
(2, N'Gazelle Printing House', '2019-01-01T00:00:00.0000000', N'Prepress Coordinator', 3, '2011-01-01T00:00:00.0000000'),
(3, N'Umicore', '2009-01-01T00:00:00.0000000', N'Lab Technician', 3, '2007-01-01T00:00:00.0000000');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Company', N'EndDate', N'Position', N'SectionId', N'StartDate') AND [object_id] = OBJECT_ID(N'[WorkExperiences]'))
    SET IDENTITY_INSERT [WorkExperiences] OFF;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250714132122_seed', N'9.0.6');

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'10e5c194-e911-4125-b924-264754278901', [PasswordHash] = N'AQAAAAEAACcQAAAAEHPfA2XinY6qHZPThr97lr0vrjBjXJbgO5XvvBbuS0Q++VU3FDR6NgQ4c7WpruOzNg=='
WHERE [Id] = N'00000000-0000-0000-0000-000000000001';
SELECT @@ROWCOUNT;


UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'6a339106-0641-421d-a7aa-5fefa470faa4', [PasswordHash] = N'AQAAAAEAACcQAAAAEOG+srRNrPTnR4y2hvBowdppe2k6JMzU3OmGQoPu/zYtPR7KQWIRFwIGc8i6uY/QGQ=='
WHERE [Id] = N'00000000-0000-0000-0000-000000000002';
SELECT @@ROWCOUNT;


INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250714133143_seedpasswordhash', N'9.0.6');

ALTER TABLE [Sections] ADD [AltText] nvarchar(max) NULL;

ALTER TABLE [Projects] ADD [AltText] nvarchar(max) NULL;

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'1fd8645c-4381-43f4-a319-879127a91671'
WHERE [Id] = N'00000000-0000-0000-0000-000000000001';
SELECT @@ROWCOUNT;


UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'b795b41d-8b1a-4686-9c0a-eab1f0ea25e8'
WHERE [Id] = N'00000000-0000-0000-0000-000000000002';
SELECT @@ROWCOUNT;


UPDATE [Projects] SET [AltText] = NULL
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


UPDATE [Projects] SET [AltText] = NULL
WHERE [Id] = 2;
SELECT @@ROWCOUNT;


UPDATE [Sections] SET [AltText] = NULL
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


UPDATE [Sections] SET [AltText] = NULL
WHERE [Id] = 2;
SELECT @@ROWCOUNT;


UPDATE [Sections] SET [AltText] = NULL
WHERE [Id] = 3;
SELECT @@ROWCOUNT;


UPDATE [Sections] SET [AltText] = NULL
WHERE [Id] = 4;
SELECT @@ROWCOUNT;


UPDATE [Sections] SET [AltText] = NULL
WHERE [Id] = 5;
SELECT @@ROWCOUNT;


UPDATE [Sections] SET [AltText] = NULL
WHERE [Id] = 6;
SELECT @@ROWCOUNT;


INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250729085637_AddSectionImageFields', N'9.0.6');

COMMIT;
GO

