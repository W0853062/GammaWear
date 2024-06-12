using GammaWear.Data;
using GammaWear.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new GammaWearContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<GammaWearContext>>()))
        {
            await InitMaterial(context);
            await InitSockStyle(context);
            await InitOutdoorSports(context);
            await InitBrand(context);
            await InitSeason(context);
            await InitSock(context);

            context.SaveChanges();
        }


        // Seeding users and roles
        using (var scope = serviceProvider.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await SeedUsers(userManager, roleManager);
        }
    }

    private static async Task InitMaterial(GammaWearContext context)
    {
        if (context.Materials.Any())
        {
            return; // DB has been seeded
        }
        context.Materials.AddRange(
            new Material { Name = "Merino Wool" },
            new Material { Name = "Wool" },
            new Material { Name = "Bamboo" },
            new Material { Name = "Cotton" },
            new Material { Name = "Coolmax" },
            new Material { Name = "Cashmere" },
            new Material { Name = "Eco Friendly Fibres" }
        );

        await context.SaveChangesAsync();
    }

    private static async Task InitSockStyle(GammaWearContext context)
    {
        if (context.SockStyles.Any())
        {
            return; // DB has been seeded
        }

        context.SockStyles.AddRange(
            new SockStyle { Name = "Funky Socks" },
            new SockStyle { Name = "Ankle Socks" },
            new SockStyle { Name = "Casual" },
            new SockStyle { Name = "Dress" },
            new SockStyle { Name = "Toe Socks" },
            new SockStyle { Name = "Compression" },
            new SockStyle { Name = "Diabetic" },
            new SockStyle { Name = "Luxury" },
            new SockStyle { Name = "Crew Socks" },
            new SockStyle { Name = "Knee High Socks" }
        );

        await context.SaveChangesAsync();
    }

    private static async Task InitOutdoorSports(GammaWearContext context)
    {
        if (context.OutdoorSports.Any())
        {
            return; // DB has been seeded
        }

        context.OutdoorSports.AddRange(
            new OutdoorSport { Name = "Ski / Snowboard" },
            new OutdoorSport { Name = "Hiking / Camping" },
            new OutdoorSport { Name = "Running / Cycling" },
            new OutdoorSport { Name = "Athletic Sports" },
            new OutdoorSport { Name = "Work / Boot" },
            new OutdoorSport { Name = "Hunting / Fishing" },
            new OutdoorSport { Name = "Sock Liners" }
        );

        await context.SaveChangesAsync();
    }

    private static async Task InitBrand(GammaWearContext context)
    {
        if (context.Brands.Any())
        {
            return; // DB has been seeded
        }

        var brandsToAdd = new List<Brand>
        {
            new Brand { Name = "JB Fields" },
            new Brand { Name = "Arriva" },
            new Brand { Name = "Vagden" },
            new Brand { Name = "GABE" },
            new Brand { Name = "Runner Gear" },
            new Brand { Name = "Golf Pro" },
            new Brand { Name = "Trans Canada Trail" },
            new Brand { Name = "Muskoka Dock Socks" },
            new Brand { Name = "HikerPro" }
        };

        // Get the names of existing brands
        var existingBrandNames = context.Brands.Select(b => b.Name).ToHashSet();

        // Filter out brands that already exist
        var newBrands = brandsToAdd.Where(b => !existingBrandNames.Contains(b.Name)).ToList();

        if (newBrands.Any())
        {
            context.Brands.AddRange(newBrands);
            await context.SaveChangesAsync();
        }
    }

    private static async Task InitSeason(GammaWearContext context)
    {
        if (context.Seasons.Any())
        {
            return; // DB has been seeded
        }

        context.Seasons.AddRange(
            new Season { Name = "Winter Socks" },
            new Season { Name = "Fall Socks" },
            new Season { Name = "Summer Socks" }
        );

        await context.SaveChangesAsync();
    }

    private static async Task InitSock(GammaWearContext context)
    {
        if (context.Socks.Any())
        {
            return; // DB has been seeded
        }


        context.Socks.AddRange(
            new Sock
            {
                MaterialId = context.Materials.First(m => m.Name == "Wool").Id,

                SockSize = SockSize.Medium,
                SockStyleId = context.SockStyles.First(s => s.Name == "Casual").Id,
                    
                OutdoorSportId = context.OutdoorSports.First(o => o.Name == "Running / Cycling").Id,
                    
                ConsumerGroup = ConsumerGroup.Female,
                SeasonId = context.Seasons.First(s => s.Name == "Summer Socks").Id,
                BrandId = context.Brands.First(b => b.Name == "Runner Gear").Id,
                Price = 15.99m,
                Quantity = 120,
                ImageFile = "sock1.png",
                Description = "These lightweight Merino Wool ankle socks are perfect for summer hiking. They provide excellent breathability and comfort."
            },
            new Sock
            {
                MaterialId = context.Materials.First(m => m.Name == "Wool").Id,
                
                SockSize = SockSize.Large,
                SockStyleId = context.SockStyles.First(s => s.Name == "Knee High Socks").Id,
                OutdoorSportId = context.OutdoorSports.First(o => o.Name == "Ski / Snowboard").Id,
                
                ConsumerGroup = ConsumerGroup.Male,
               
                SeasonId = context.Seasons.First(s => s.Name == "Winter Socks").Id,
                BrandId = context.Brands.First(b => b.Name == "Trans Canada Trail").Id,
               
                Price = 18m,
                Quantity = 100,
                ImageFile = "sock2.png",
                Description = "High-performance knee-high socks made from Wool. Ideal for skiing and snowboarding in the winter."
            },
            new Sock
            {
                MaterialId = context.Materials.First(m => m.Name == "Bamboo").Id,
                
                SockSize = SockSize.Small,
                SockStyleId = context.SockStyles.First(s => s.Name == "Casual").Id,
                OutdoorSportId = context.OutdoorSports.First(o => o.Name == "Running / Cycling").Id,
                
                ConsumerGroup = ConsumerGroup.Female,
                SeasonId = context.Seasons.First(s => s.Name == "Summer Socks").Id,
                BrandId = context.Brands.First(b => b.Name == "Runner Gear").Id,
                Price = 12.5m,
                Quantity = 150,
                ImageFile = "sock3.png",
                Description = "Soft and eco-friendly Bamboo crew socks for spring running. Moisture-wicking and antibacterial."
            },
            new Sock
            {
                MaterialId = context.Materials.First(m => m.Name == "Cotton").Id,
                
                SockSize = SockSize.OneSizeFitsAll,
                SockStyleId = context.SockStyles.First(s => s.Name == "Casual").Id,
                OutdoorSportId = context.OutdoorSports.First(o => o.Name == "Athletic Sports").Id,
                
                ConsumerGroup = ConsumerGroup.Male,
                SeasonId = context.Seasons.First(s => s.Name == "Summer Socks").Id,
                BrandId = context.Brands.First(b => b.Name == "Golf Pro").Id,
                
                Price = 14m,
                Quantity = 200,
                ImageFile = "sock4.png",
                Description = "Classic Cotton crew socks for summer golf. Comfortable and stylish for a day on the green."
            },
            new Sock
            {
                MaterialId = context.Materials.First(m => m.Name == "Coolmax").Id,
                
                SockSize = SockSize.Large,
                SockStyleId = context.SockStyles.First(s => s.Name == "Ankle Socks").Id,
                
                OutdoorSportId = context.OutdoorSports.First(o => o.Name == "Running / Cycling").Id,
                ConsumerGroup = ConsumerGroup.Female,
                SeasonId = context.Seasons.First(s => s.Name == "Summer Socks").Id,
                BrandId = context.Brands.First(b => b.Name == "JB Fields").Id,

                
                Price = 16m,
                Quantity = 130,
                ImageFile = "sock5.png",
                Description = "High-performance Coolmax ankle socks designed for cycling. Keeps your feet cool and dry."
            },
            new Sock
            {
                MaterialId = context.Materials.First(m => m.Name == "Cashmere").Id,
               
                SockSize = SockSize.Medium,
                SockStyleId = context.SockStyles.First(s => s.Name == "Knee High Socks").Id,
                
                OutdoorSportId = context.OutdoorSports.First(o => o.Name == "Ski / Snowboard").Id,
                ConsumerGroup = ConsumerGroup.Female,
                SeasonId = context.Seasons.First(s => s.Name == "Winter Socks").Id,
                BrandId = context.Brands.First(b => b.Name == "Vagden").Id,
                
                Price = 25m,
                Quantity = 80,
                ImageFile = "sock6.png",
                Description = "Luxurious Cashmere knee-high socks for winter sports. Extra warmth and comfort."
            },
            new Sock
            {
                MaterialId = context.Materials.First(m => m.Name == "Eco Friendly Fibres").Id,
                
                SockSize = SockSize.Large,
                SockStyleId = context.SockStyles.First(s => s.Name == "Crew Socks").Id,
                
                OutdoorSportId = context.OutdoorSports.First(o => o.Name == "Hiking / Camping").Id,
                ConsumerGroup = ConsumerGroup.Unisex,
                SeasonId = context.Seasons.First(s => s.Name == "Fall Socks").Id,
                BrandId = context.Brands.First(b => b.Name == "Muskoka Dock Socks").Id,
               
                
                Price = 17m,
                Quantity = 110,
                ImageFile = "sock7.png",
                Description = "Eco-friendly crew socks perfect for fall camping. Sustainable and durable."
            },
            new Sock
            {
                MaterialId = context.Materials.First(m => m.Name == "Merino Wool").Id,
                
                SockSize = SockSize.Large,
                
                SockStyleId = context.SockStyles.First(s => s.Name == "Crew Socks").Id,
                
                OutdoorSportId = context.OutdoorSports.First(o => o.Name == "Hiking / Camping").Id,
                ConsumerGroup = ConsumerGroup.Male,

                SeasonId = context.Seasons.First(s => s.Name == "Winter Socks").Id,
                BrandId = context.Brands.First(b => b.Name == "HikerPro").Id,

               

                
                Price = 19m,
                Quantity = 90,
                ImageFile = "sock8.png",
                Description = "Premium Merino Wool crew socks for winter hiking. Warm and moisture-wicking."
            },
            new Sock
            {
                MaterialId = context.Materials.First(m => m.Name == "Cotton").Id,
               
                SockSize = SockSize.Small,
                
                SockStyleId = context.SockStyles.First(s => s.Name == "Ankle Socks").Id,
                
                OutdoorSportId = context.OutdoorSports.First(o => o.Name == "Running / Cycling").Id,
                ConsumerGroup = ConsumerGroup.Female,
                SeasonId = context.Seasons.First(s => s.Name == "Summer Socks").Id,
                BrandId = context.Brands.First(b => b.Name == "Runner Gear").Id,

                
                
                Price = 11m,
                Quantity = 140,
                ImageFile = "sock9.png",
                Description = "Comfortable Cotton ankle socks for spring running. Breathable and soft."
            },
            new Sock
            {
                MaterialId = context.Materials.First(m => m.Name == "Coolmax").Id,
                
                SockSize = SockSize.Medium,
               
                SockStyleId = context.SockStyles.First(s => s.Name == "Crew Socks").Id,
               
                OutdoorSportId = context.OutdoorSports.First(o => o.Name == "Running / Cycling").Id,
                ConsumerGroup = ConsumerGroup.Male,

                SeasonId = context.Seasons.First(s => s.Name == "Summer Socks").Id,
                BrandId = context.Brands.First(b => b.Name == "JB Fields").Id,

                
                Price = 16.5m,
                Quantity = 150,
                ImageFile = "sock10.png",
                Description = "Coolmax crew socks designed for summer cycling. Keep your feet cool and comfortable."
            }
        );
        await context.SaveChangesAsync();
    }
    private static async Task SeedUsers(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (!userManager.Users.Any())
        {
            var users = new[]
            {
            new { Email = "admin@admin.com", Password = "Admin@Gamma123", Role = "Admin", PhoneNumber = "1234567890" },
            new { Email = "user@user.com", Password = "User@12345", Role = "User", PhoneNumber = "0987654321" }
        };

            foreach (var userInfo in users)
            {
                var user = new IdentityUser
                {
                    UserName = userInfo.Email,
                    Email = userInfo.Email,
                    EmailConfirmed = true,
                    PhoneNumber = userInfo.PhoneNumber,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false
                };
                var result = await userManager.CreateAsync(user, userInfo.Password);

                if (result.Succeeded)
                {
                    if (!await roleManager.RoleExistsAsync(userInfo.Role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(userInfo.Role));
                    }
                    await userManager.AddToRoleAsync(user, userInfo.Role);
                }
                else
                {
                    // Log or handle the error here
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error creating user {userInfo.Email}: {error.Description}");
                    }
                }
            }
        }
    }

}
