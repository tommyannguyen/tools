﻿using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nca.Library.Interfaces.Reporting;
using Nca.Library.Models;
using Nca.Library.Models.Reporting;
using Nca.Library.Models.Repositories;
using Nca.Library.Repositories.Database;
using System.Security.Principal;

namespace Nca.Library.Bootrap
{
    public static class Bootstrap
    {
        public static string SqlConnectionString = "Server=PC102;Database=TestDB;Trusted_Connection=True;";
        //private static string SqlConnectionString = "Data Source=App_Data\\DB.sqlite;";
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((builder) =>
            {
                builder.UseSqlServer(SqlConnectionString, option =>
                {
                    option.MigrationsAssembly("Nca.Web.Spa");
                });
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<EmailSettings>((emailSetting) =>
            {
                emailSetting.MailPort = int.Parse(configuration["EmailSettings:MailPort"]);
                emailSetting.MailServer = configuration["EmailSettings:MailServer"];
                emailSetting.SenderName = configuration["EmailSettings:SenderName"];
                emailSetting.Sender = configuration["EmailSettings:Sender"];
                emailSetting.Password = configuration["EmailSettings:Password"];
            });
            services.AddSingleton<IReportingSettings>(new ReportingSettings {
                HeaderTemplate = "header.html",
                FooterTemplate = "footer.html",
                OutPutDirectory = "Data\\Temp",
                TempPathDirectory = "Data\\Templates"
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
                configuration.UseSqlServerStorage(SqlConnectionString);
            });

            services.AddTransient<EmailSenderJob>();

            //Regiser current IPrincipal
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(
                provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
        }
    }
}
