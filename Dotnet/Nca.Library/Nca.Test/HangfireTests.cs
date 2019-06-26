using Hangfire;
using Hangfire.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nca.Test.Jobs;
using NUnit.Framework;
using ServiceStack.Messaging.Redis;
using ServiceStack.Redis;
using StackExchange.Redis;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Tests
{
    public class HangfireTests : IDisposable
    {
        private ConnectionMultiplexer _redis;
        private IWebHost _host;
        [SetUp]
        public void Setup()
        {
            _redis = ConnectionMultiplexer.Connect("localhost:6379");

            var hostBuilder = new WebHostBuilder();
            hostBuilder.ConfigureServices(services => ConfigureServices(services));
            hostBuilder.Configure(app => Configure(app));
            hostBuilder.UseContentRoot(Directory.GetCurrentDirectory());
            hostBuilder.UseKestrel();
            hostBuilder.UseUrls("http://localhost:5051");
            _host = hostBuilder.Build();
            _host.Start();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfire(configuration =>
            {
                configuration.UseRedisStorage(_redis, new RedisStorageOptions { Prefix = "{hangfire-1}:" });
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
            var jobId = BackgroundJob.Enqueue<MyHangfireJobs>(jobs => 
                jobs.SendGetRequest()
                );
            Trace.WriteLine("ten ten ten");
            Assert.True(true);

            RecurringJob.AddOrUpdate(
                    () =>

                    Trace.WriteLine("Recurring!"),
                    Cron.Minutely());


            var succeeded = BackgroundJob.Requeue(jobId);
            Assert.True(succeeded);
        }

        public void Dispose()
        {
            _host.Dispose();
            _redis.Dispose();
        }

        [Test]
        public void TestRedisPubSub()
        {
            var channelName = "channelName";

            var database = _redis.GetDatabase();
            var subscriber = _redis.GetSubscriber();
            subscriber.Subscribe(channelName, (channel, message) =>
            {
                Trace.WriteLine($"1: {DateTime.Now.ToString("yyyyMMdd HH:mm:ss")}<Normal - {channel}><{message}>.");
            });

            var subscriber2 = _redis.GetSubscriber();
            subscriber2.Subscribe(channelName, (channel, message) =>
            {
                Trace.WriteLine($"2: {DateTime.Now.ToString("yyyyMMdd HH:mm:ss")}<Normal - {channel}><{message}>.");
            });


            var publisher = _redis.GetSubscriber();

            // publish message to one channel
            var results = publisher.Publish(channelName, $"Publish a message to literal channel: {channelName}", CommandFlags.FireAndForget);


            Assert.True(results > 0);
        }

        public class Hello { public string Name { get; set; } }
        public class HelloResponse { public string Result { get; set; } }
        [Test]
        public void TestMQRedis()
        {
            var redisFactory = new PooledRedisClientManager("localhost:6379");
            var mqHost = new RedisMqServer(redisFactory, retryCount: 2);

            //Server - MQ Service Impl:
            mqHost.RegisterHandler<Hello>(m =>
            {
                Trace.WriteLine("Received 1: " + m.GetBody());
                return new HelloResponse() { Result = "ten ten ten"};
            });
            //Client - Process Response:
            mqHost.RegisterHandler<HelloResponse>(m =>
            {
                Trace.WriteLine($"Received 2: {m.GetBody().Result}: {DateTime.Now.ToString()}");
                return null;
            });
            mqHost.Start();

            //Producer - Start publishing messages:
            var mqClient = mqHost.CreateMessageQueueClient();
            mqClient.Publish(new Hello { Name = "ServiceStack" });

            Thread.Sleep(10000);
            Assert.True(true);
        }

    }
}