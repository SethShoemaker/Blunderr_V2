using System.Reflection;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Services.PasswordService;
using Blunderr.Core.Services.TokenService;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static void AddCoreDependencies(this IServiceCollection services, string connection, bool isDevelopment)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(connection, ServerVersion.AutoDetect(connection));
            });
        }
    }
}