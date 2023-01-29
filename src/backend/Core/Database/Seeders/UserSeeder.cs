using Core.Database.Enums;
using Core.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Database.Seeders
{
    public static class UserSeeder
    {
        public static void Execute(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(new User
            {
                Id = 1,
                Status = DbEntityStatus.Active,
                FirstName = "Service",
                LastName = "Service",
                Email = DbConstants.ServiceUserEmail
            });
        }
    }
}
