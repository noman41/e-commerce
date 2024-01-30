using Microsoft.AspNetCore.Identity;
using ECommerce.Server.Models.DataModels;

namespace ECommerce.Server.DAL
{
    public static class InitializeDatabase
    {
        public static void SeedSuperAdmin(this POSDbContext posDbContext)
        {
            if (!posDbContext.Users.Any((POSUser x) => x.Email == "noman.pugc41@gmail.com"))
            {
                // User
                var user = new POSUser
                {
                    UserName = "admin",
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "noman.pugc41@gmail.com",
                    IsSuperAdmin = true,
                    IsActive = true,
                    CreatedBy = Guid.Empty,
                    CreatedOn = DateTime.UtcNow,
                    EmailConfirmed = false
                };
                var passwordHasher = new PasswordHasher<POSUser>();
                var hashedPassword = passwordHasher.HashPassword(user, "NomiNomi@1");
                user.PasswordHash = hashedPassword;
                posDbContext.Users.Add(user);

                // Role
                var role = new IdentityRole
                {
                    Name = "admin"
                };
                posDbContext.Roles.Add(role);

                // User Role
                posDbContext.UserRoles.Add(new IdentityUserRole<string>
                {
                    UserId = user.Id,
                    RoleId = role.Id
                });

                // Save
                posDbContext.SaveChanges();
            }
        }
    }
}
