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
    public class QuestionItemConfigurations : IEntityTypeConfiguration<QuestionItem>
    {
        public void Configure(EntityTypeBuilder<QuestionItem> builder)
        {
            builder
                .HasKey(q => q.Id);
            builder
                .Property(q => q.Answer)
                .HasColumnType("nvarchar(100)");
            builder
                .Property(q => q.Question)
                .HasColumnType("nvarchar(250)");
            builder
                .HasOne(q => q.Options)
                .WithOne(o => o.Question)
                .HasForeignKey<Options>(o => o.QuestionId);
        }
    }
}
