using Infrastructure.Entities.Users.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DataSeed;

public static class IdentitySeeding
{
    private const string DefaultPassword = "P@ssw0rd";
    private const string SuperAdminRole = "SuperAdmin";
    private const string AdminRole = "Admin";
    private const string MemberRole = "Member";
    private const string TrainerRole = "Trainer";

    public static async Task<bool> SeedDataAsync(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        try
        {
            await SeedRolesAsync(roleManager);
            await SeedUsersAsync(userManager);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        var roles = new[] { SuperAdminRole, AdminRole, MemberRole, TrainerRole };

        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole { Name = roleName };
                await roleManager.CreateAsync(role);
            }
        }
    }

    private static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
    {
        if (await userManager.Users.AnyAsync())
        {
            return;
        }

        var usersToSeed = new[]
        {
            new UserSeedData
            {
                FirstName = "SuperAdmin",
                LastName = "SuperAdmin",
                UserName = "SuperAdmin",
                Email = "superadmin@vitagym.com",
                PhoneNumber = "1234567890",
                Role = SuperAdminRole
            },
            new UserSeedData
            {
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "Admin",
                Email = "admin@vitagym.com",
                PhoneNumber = "1234567891",
                Role = AdminRole
            }
        };

        foreach (var userData in usersToSeed)
        {
            await CreateUserAsync(userManager, userData);
        }
    }

    private static async Task CreateUserAsync(
        UserManager<ApplicationUser> userManager,
        UserSeedData userData)
    {
        var user = new ApplicationUser
        {
            FirstName = userData.FirstName,
            LastName = userData.LastName,
            UserName = userData.UserName,
            Email = userData.Email,
            PhoneNumber = userData.PhoneNumber,
            EmailConfirmed = true // Auto-confirm email for seeded users
        };

        var createResult = await userManager.CreateAsync(user, DefaultPassword);

        if (createResult.Succeeded)
        {
            await userManager.AddToRoleAsync(user, userData.Role);
        }
    }

    private class UserSeedData
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Role { get; set; }
    }
}
