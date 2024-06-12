


NuGet Console, Data Migration

```shell
Add-Migration InitialCreateUser -Context ApplicationDbContext
Update-Database  -Context ApplicationDbContext

Add-Migration InitialCreateSock -Context GammaWearContext
Update-Database  -Context GammaWearContext
```