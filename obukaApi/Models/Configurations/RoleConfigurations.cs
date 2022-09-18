using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Configurations
{
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(u => u.UserRoles)
                .WithOne(o => o.Role)
                .HasForeignKey(ol => ol.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.RolePermissions)
                .WithOne(o => o.Role)
                .HasForeignKey(ol => ol.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
