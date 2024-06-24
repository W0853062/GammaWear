


# NuGet Console, Data Migration

## First Migration

```shell
Add-Migration InitialCreateUser -Context ApplicationDbContext
Update-Database  -Context ApplicationDbContext



Add-Migration InitialCreateSock -Context GammaWearContext
Update-Database  -Context GammaWearContext

```

## For Migration with Old Data

```shell
Remove-Migration  -Context ApplicationDbContext
Add-Migration InitialCreateUser -Context ApplicationDbContext
Update-Database  -Context ApplicationDbContext


-- change {database__server} to your database server name
-- change {database_db} to your database name
-- change {database_user} to your database user name
-- change {database_pwd} to your database password
Update-Database  -Context ApplicationDbContext -Connection "Server=tcp:{database__server}.database.windows.net,1433;Initial Catalog={database_db};Persist Security Info=False;User ID={database_user}@{database__server};Password={database_pwd};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"


Remove-Migration -Context GammaWearContext
Add-Migration InitialCreateSock -Context GammaWearContext
Update-Database  -Context GammaWearContext


-- change {database__server} to your database server name
-- change {database_db} to your database name
-- change {database_user} to your database user name
-- change {database_pwd} to your database password
Update-Database  -Context GammaWearContext -Connection "Server=tcp:{database__server}.database.windows.net,1433;Initial Catalog={database_db};Persist Security Info=False;User ID={database_user}@{database__server};Password={database_pwd};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

```


## For Table Delete

ApplicationDbContext

```sql
DROP TABLE AspNetUserRoles;
DROP TABLE AspNetUserClaims;
DROP TABLE AspNetUserLogins;
DROP TABLE AspNetUserTokens;
DROP TABLE AspNetRoleClaims;
DROP TABLE AspNetUsers;
DROP TABLE AspNetRoles;

```

GammaWearContext
```sql
DROP TABLE Socks;
DROP TABLE SockStyles;
DROP TABLE Seasons;
DROP TABLE OutdoorSports;
DROP TABLE Materials;
DROP TABLE Brands;

```