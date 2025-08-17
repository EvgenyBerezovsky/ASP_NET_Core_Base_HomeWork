using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataBase.EntityConfigurations
{
    public class OptionsConfigurations : IEntityTypeConfiguration<Options>
    {
        public void Configure(EntityTypeBuilder<Options> builder)
        {
            builder
                .HasKey(o => o.Id);
            builder
                .Property(o => o.Option1)
                .HasColumnType("nvarchar(100)");
            builder
                .Property(o => o.Option2)
                .HasColumnType("nvarchar(100)");
            builder
                .Property(o => o.Option3)
                .HasColumnType("nvarchar(100)");
            builder
                .Property(o => o.Option4)
                .HasColumnType("nvarchar(100)");
        }
    }
}
