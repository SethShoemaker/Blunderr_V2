using System.Reflection;
using Amazon.S3;
using Blunderr.Core.Data.Files.FileItemService;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Data.Security.PasswordService;
using Blunderr.Core.Data.Security.TokenService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static void AddCoreDependencies(this IServiceCollection services, IConfiguration Configuration, bool isDevelopment)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddDbContext<AppDbContext>(options =>
            {
                string connection = Configuration.GetConnectionString("App");
                options.UseMySql(connection, ServerVersion.AutoDetect(connection));
            });

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();

            services.AddScoped<IFileItemService, S3FileItemService>();

            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}