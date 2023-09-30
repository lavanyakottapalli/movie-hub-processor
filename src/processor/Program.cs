// <copyright file="Program.cs" company="Lavanya.">
// </copyright>

namespace Microsoft.Movie.Hub.Processor
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Program.
    /// </summary>
#pragma warning disable CA1052
    public class Program
#pragma warning restore CA1052
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Configure host builder properties.
        /// </summary>
        /// <param name="args">args.</param>
        /// <returns>IWebHost Builder.</returns>
        public static IWebHostBuilder CreateHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(loggingBuilder =>
                {
                    _ = loggingBuilder.AddConsole();
                })
            .ConfigureAppConfiguration((context, config) =>
            {
                if (context.HostingEnvironment.IsDevelopment())
                {
                    _ = config.AddUserSecrets<Program>();
                }
            })
            .UseUrls(urls: "http://localhost:5008")
            .UseStartup<Startup>();
        }
    }
}