using Domain.Entities;
using Infrastructure.DataBase.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataBase.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<QuestionItem> Questions { get; set; }
        public DbSet<Domain.Entities.Options> Options { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfigurations());
            modelBuilder.ApplyConfiguration(new ScoreConfigurations());
            modelBuilder.ApplyConfiguration(new OptionsConfigurations());
            modelBuilder.ApplyConfiguration(new QuizConfigurations());
            modelBuilder.ApplyConfiguration(new QuestionItemConfigurations());
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=D:\\ITVDN\\SmartTest\\QuizBot\\QuizBot_3.0\\bin\\Debug\\net7.0\\botData.db");
        //}
    }
}
