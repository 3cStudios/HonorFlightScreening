CREATE TABLE [dbo].[AspNetRoleClaims] (
  [Id] [int] IDENTITY,
  [RoleId] [nvarchar](450) NOT NULL,
  [ClaimType] [nvarchar](max) NULL,
  [ClaimValue] [nvarchar](max) NULL,
  CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED ([Id])
)
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId]
  ON [dbo].[AspNetRoleClaims] ([RoleId])
GO

ALTER TABLE [dbo].[AspNetRoleClaims]
  ADD CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE
GO