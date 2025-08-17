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
    public class ScoreConfigurations : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> builder)
        {
            builder
                .HasKey(s => s.Id);
            builder
                .Property(s => s.Topic)
                .HasColumnType("nvarchar(100)");
        }
    }
}
