using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public static class DatabaseSeeders
    {
        public static void SeedFeatureData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feature>().HasData(
                new Feature() { Id = (int)FeatureCode.GetAllRoles, FeatureName = "GetAllRoles" },
                new Feature() { Id = (int)FeatureCode.DeleteRole, FeatureName = "DeleteRole" },
                new Feature() { Id = (int)FeatureCode.UpdateRole, FeatureName = "UpdateRole" },
                new Feature() { Id = (int)FeatureCode.CreateRole, FeatureName = "CreateRole" }
            );
        }
    }
}
