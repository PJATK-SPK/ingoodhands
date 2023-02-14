﻿using Core.Database.Enums;
using Core.Database.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace Core.Database.Seeders
{
    public static class UserSeeder
    {
        public static readonly User ServierUser = new()
        {
            Id = 1,
            Status = DbEntityStatus.Active,
            FirstName = "Service",
            LastName = "Service",
            Email = DbConstants.ServiceUserEmail
        };

        public static void Execute(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(ServierUser);
        }
    }
}
