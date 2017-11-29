namespace Vidly.Migrations
{
	using System;
	using System.Data.Entity.Migrations;

	public partial class SeedUsers : DbMigration
	{
		public override void Up()
		{
			Sql(@"

INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'39b5a06b-b205-468d-965e-dc683ad2bb62', N'admin@vidly.com', 0, N'AOFMPcOAoZoxxOw39j7MysJAV+MA49Z67Ep3jVS6iybq6kkH3YYmd4IaXiRUSQNL1Q==', N'05112e5e-6cb6-4efd-9c19-3bb26ff0d635', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c25441c4-8382-4d25-965f-ffc148cf3121', N'guest@vidly.com', 0, N'ABY8K/L3WZIkspJVKNSswYS/KFOmv4Ne34ZzN22PCd5Jh3IPi1fMQypzcscj0AKJ1g==', N'e76047cf-a651-4f6f-a6b2-373005020b80', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'60a45308-c1e5-49ad-ba17-22aacb7782d0', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'39b5a06b-b205-468d-965e-dc683ad2bb62', N'60a45308-c1e5-49ad-ba17-22aacb7782d0')
			");
		}

		public override void Down()
		{
		}
	}
}
