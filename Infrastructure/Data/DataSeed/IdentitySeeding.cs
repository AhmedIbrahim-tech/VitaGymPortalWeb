namespace Infrastructure.Data.DataSeed;

public static class IdentitySeeding
{
    public static async Task<bool> SeedData(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
    {
      
        try
        {
            if (!roleManager.Roles.Any())
            {
                var roles = new List<IdentityRole>
            {
                new() {Name ="SuperAdmin"},
                new() {Name ="Admin"}
            };

                foreach (var role in roles)
                {
                    if (!roleManager.RoleExistsAsync(role.Name ?? string.Empty).Result)
                    {
                        roleManager.CreateAsync(role).Wait();
                    }
                }

            }

            if (!userManager.Users.Any())
            {

                var superAdmin = new ApplicationUser
                {
                    FirstName = "SuperAdmin",
                    LastName = "SuperAdmin",
                    UserName = "SuperAdmin",
                    Email = "SuperAdmin@gmail.com",
                    PhoneNumber = "1234567890"
                };
                await userManager.CreateAsync(superAdmin, "P@ssw0rd");
                await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");

                var Admin = new ApplicationUser
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "1234567890"
                };
                await userManager.CreateAsync(Admin, "P@ssw0rd");
                await userManager.AddToRoleAsync(Admin, "Admin");
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed To Seed Data" + ex); 
            return false;
        }

    }
}
