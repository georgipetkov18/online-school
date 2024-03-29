﻿using Microsoft.EntityFrameworkCore;
using OnlineSchoolBusinessLogic.Common;
using OnlineSchoolData.Entities;

namespace OnlineSchoolData.Seeders
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(this ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!await context.Roles.AnyAsync())
            {
                await context.Roles.AddRangeAsync(
                    new RoleEntity { Name = Roles.Student },
                    new RoleEntity { Name = Roles.Teacher },
                    new RoleEntity { Name = Roles.Administrator }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
