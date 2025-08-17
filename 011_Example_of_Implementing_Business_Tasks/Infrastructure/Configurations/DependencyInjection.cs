using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Domain.Entities;
using Infrastructure.DataBase;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IDataRepository, DataRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAnaliticsService, AnaliticsService>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddDbContext<AppDbContext>(options =>
            {
                var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "UserData.db");
                var connectionString = $"Data Source={dbPath}";
                Console.WriteLine(dbPath);
                options.UseSqlite(connectionString);
            });
            services.Configure<JwtConfigurationOptions>(configuration.GetSection("JwtConfiguration"));

            return services;
        }
    }
}
