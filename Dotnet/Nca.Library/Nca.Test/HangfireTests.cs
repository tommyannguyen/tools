using Hangfire;
using Hangfire.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nca.Test.Jobs;
using NUnit.Framework;
using StackExchange.Redis;
using System;
using System.Diagnostics;
using System.IO;

namespace Tests
{
    public class HangfireTests : IDisposable
    {
        public ConnectionMultiplexer Redis;
        private IWebHost host;
        [SetUp]
        public void Setup()
        {
            Redis = ConnectionMultiplexer.Connect("localhost:6379");
            var hostBuilder = new WebHostBuilder();
            hostBuilder.ConfigureServices(services => ConfigureServices(services));
            hostBuilder.Configure(app => Configure(app));
            hostBuilder.UseContentRoot(Directory.GetCurrentDirectory());
            hostBuilder.UseKestrel();
            host = hostBuilder.Build();

            host.Start();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfire(configuration =>
            {
                configuration.UseRedisStorage(Redis, new RedisStorageOptions { Prefix = "{hangfire-1}:" });
            });

            services.AddTransient<MyHangfireJobs>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHangfireServer();
        }

        [Test]
        public void Test1()
        {
            var jobId = BackgroundJob.Enqueue<MyHangfireJobs>(jobs => jobs.SendGetRequest());
            Trace.WriteLine("ten ten ten");
            Assert.True(true);

            RecurringJob.AddOrUpdate(
                    () => Console.WriteLine("Recurring!"),
                    Cron.Minutely());


            var succeeded = BackgroundJob.Requeue(jobId);
            Assert.True(succeeded);
        }

        public void Dispose()
        {
            this.host.Dispose();
        }
    }
}