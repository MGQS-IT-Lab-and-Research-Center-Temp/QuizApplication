using QuizApplication.Entities;
using QuizApplication.Helper;

namespace QuizApplication.Context
{
    internal class DbInitializer
    {
        internal static void Initialize(QuizApplicationContext context)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));

            context.Database.EnsureCreated();

            if (context.Roles.Any())
            {
                return;
            }

            var roles = new Role[]
            {
                new Role()
                {
                    ClassName = "Admin",
                    Description = "Role for admin",
                    CreatedBy = "System",
                    DateCreated = DateTime.Now,
                    IsDeleted = false,
                    ModifiedBy = "",
                    LastModified = new DateTime() //0001-01-01 00:00:00:00
                },
                new Role()
                {
                    ClassName = "AppUser",
                    Description = "Role for nominal user",
                    CreatedBy = "System",
                    DateCreated = DateTime.Now,
                    IsDeleted = false,
                    ModifiedBy = "",
                    LastModified = new DateTime()
                }
            };

            foreach (var r in roles)
            {
                context.Roles.Add(r);
            }

            context.SaveChanges();

            var password = "Haseeb123";
            var salt = HashingHelper.GenerateSalt();
            var admin = context.Roles.Where(r => r.ClassName == "Admin").SingleOrDefault();

            var users = new User[]
            {
                new User()
                {
                    UserName = "Haseeb",
                    HashSalt = salt,
                    PasswordHash = HashingHelper.HashPassword(password, salt),
                    Email = "Haseeb@gmail.com",
                    RoleId = admin.Id,
                    CreatedBy = "System",
                    DateCreated = DateTime.Now,
                    IsDeleted = false,
                    ModifiedBy = "",
                    LastModified = new DateTime()
                }
            };

            foreach (var u in users)
            {
                context.Users.Add(u);
            }

            context.SaveChanges();
        }
    }
}
