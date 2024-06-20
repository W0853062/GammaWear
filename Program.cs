using GammaWear.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GammaWear.Models;
using Microsoft.AspNetCore.Localization;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);


// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddDbContext<GammaWearContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GammaWearContext")
    ?? throw new InvalidOperationException("Connection string 'GammaWearContext' not found.")));

// Add services to the container..
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// Configure localization options
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("en-CA") };
    options.DefaultRequestCulture = new RequestCulture("en-CA");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    // add by vicky
    //app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// add by vicky
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Seed the database, by vicky
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    //SeedData.Initialize(services);
    

    try
    {
       // var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        //var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
        // Call your SeedUsers method here
        //SeedData.SeedUsers(userManager, roleManager).Wait();

        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();

        // Call the SeedData.Initialize method asynchronously
        await SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}
app.Run();
