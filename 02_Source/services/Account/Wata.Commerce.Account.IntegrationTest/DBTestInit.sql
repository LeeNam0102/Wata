delete from dbo.AspNetUsers;
insert into dbo.AspNetUsers
(
	[Id]
    ,[AccessFailedCount]
    ,[ConcurrencyStamp]
    ,[Email]
    ,[EmailConfirmed]
    ,[LockoutEnabled]
	,[LockoutEnd]
	,[NormalizedEmail]
	,[NormalizedUserName]
	,[PasswordHash]
	,[PhoneNumber]
	,[PhoneNumberConfirmed]
	,[SecurityStamp]
	,[TwoFactorEnabled]
	,[UserName]
)

values
('1', 1, 'ConcurrencyStamp', 'Email', 0, 0,0,'NormalizedEmail','NormalizedUserName','PasswordHash','PhoneNumber',0,'SecurityStamp',0,'Username'),
('2', 1, 'ConcurrencyStamp', 'Email', 0, 0,0,'NormalizedEmail','NormalizedUserName','PasswordHash','PhoneNumber',0,'SecurityStamp',0,'Username')


DELETE FROM dbo.AspNetRoles;
insert into dbo.AspNetRoles
(
	[Id]
    ,[ConcurrencyStamp]
    ,[Name]
    ,[NormalizedName]
 
)
values
('1', 'ConcurrencyStamp','Name','NormalizedName'),
('2', 'ConcurrencyStamp','Name','NormalizedName')


