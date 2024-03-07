using CLN.Application.Interfaces;
using CLN.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Persistence
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            //var connectionString = configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

            //services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped(typeof(IGenericRepository), typeof(GenericRepository));
            //services.AddScoped<ICommonRepository, CommonRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtUtilsRepository, JwtUtilsRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            //services.AddTransient<IProductRepositoryAsync, ProductRepositoryAsync>();
        }
    }
}
