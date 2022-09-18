using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Configurations
{
    public class PermissionConfigurations : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasMany(u => u.RolePermissions)
                .WithOne(o => o.Permission)
                .HasForeignKey(ol => ol.PermissionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
