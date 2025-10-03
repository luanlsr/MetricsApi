using MetricsApi.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Infrastructure.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .ValueGeneratedNever();

            builder.OwnsOne(u => u.Name, name =>
            {
                name.Property(n => n.FirstName).HasColumnName("FirstName").IsRequired();
                name.Property(n => n.MiddleName).HasColumnName("MiddleName");
                name.Property(n => n.LastName).HasColumnName("LastName").IsRequired();
            });

            builder.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Address).HasColumnName("Email").IsRequired();
            });

            builder.Property(u => u.Active).IsRequired();
            builder.Property(u => u.CreatedAt).IsRequired();
            builder.Property(u => u.UpdatedAt).IsRequired();
        }
    }
}
