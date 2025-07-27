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
                new Feature() {Id=new Guid(Constants.GetAllRolesFeature) ,FeatureName = FeatureCode.GetAllRoles.ToString(),CreatedAt=null },
                new Feature() {Id=new Guid(Constants.DeleteRoleFeature) ,FeatureName = FeatureCode.DeleteRole.ToString(),CreatedAt=null },
                new Feature() {Id=new Guid(Constants.UpdateRoleFeature) ,FeatureName = FeatureCode.UpdateRole.ToString(),CreatedAt = null },
                new Feature() {Id=new Guid(Constants.CreateRoleFeature) ,FeatureName = FeatureCode.CreateRole.ToString(),CreatedAt = null }
            );
        }

        public static void SeedNoneRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Models.Role>().HasData(
                new Domain.Models.Role() { Id=new Guid(Constants.NoneRole) ,Name="None",CreatedAt=null}
            );
        }
    }
}
