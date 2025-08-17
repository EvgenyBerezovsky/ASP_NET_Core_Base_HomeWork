using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Infrastructure.DataBase.Context;
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

namespace Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrustructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDataRepository, DataRepository>();
            services.AddScoped<IQuizService, QuizService>();
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
