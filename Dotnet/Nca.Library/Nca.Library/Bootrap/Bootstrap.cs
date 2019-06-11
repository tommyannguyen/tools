using Hangfire;
using Hangfire.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nca.Library.Models;
using Nca.Library.Models.Repositories;
using Nca.Library.Repositories.Database;

namespace Nca.Library.Bootrap
{
    public static class Bootstrap
    {
        private static string SqlConnectionString = "Data Source=App_Data\\DB.sqlite;";
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((builder) => {
                builder.UseSqlite(SqlConnectionString, option => {
                    option.MigrationsAssembly("Nca.Web.Spa");
                });
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<EmailSettings>((emailSetting) => {
                emailSetting.MailPort = int.Parse(configuration["EmailSettings:MailPort"]);
                emailSetting.MailServer = configuration["EmailSettings:MailServer"];
                emailSetting.SenderName = configuration["EmailSettings:SenderName"];
                emailSetting.Sender = configuration["EmailSettings:Sender"];
                emailSetting.Password = configuration["EmailSettings:Password"];
            });
            RegisterJobs(services);
        }
        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

        }

        private static void RegisterJobs(IServiceCollection services)
        {
            services.AddHangfire(configuration =>
            {
                configuration.UseSQLiteStorage(SqlConnectionString);
            });

            services.AddTransient<EmailSenderJob>();
        }
    }
}
