// <copyright file="Startup.cs" company="Lavanya.">
// </copyright>

namespace Microsoft.Movie.Hub.Processor
{
    using Azure.Search.Documents;
    using Azure;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using RestSharp;
    using Microsoft.Movie.Hub.Processor.Workflow;
    using System.Text.Json.Serialization;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.DotNet.Scaffolding.Shared;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Movie.Hub.Processor.Models;
    using Azure.Identity;

    /// <summary>
    /// StartUp.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The hosting environment.</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.Configuration = configuration;
            this.Environment = environment;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        public IWebHostEnvironment Environment { get; }

        /// <summary>
        /// Configures the services. This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {

            string indexName = "movie-index";

            // Get the service endpoint and API key from the environment
            Uri endpoint = new Uri("https://movie-store.search.windows.net");

            // Create a client
            SearchClient searchClient = new SearchClient(endpoint, indexName, new DefaultAzureCredential());

            services.AddSingleton(searchClient);

            RestClientOptions options = new RestClientOptions("https://api.themoviedb.org/3/authentication");

#pragma warning disable CA2000 // Dispose objects before losing scope
            RestClient restClient = new RestClient(options);
#pragma warning restore CA2000 // Dispose objects before losing scope

            services.AddSingleton(restClient);
            services.AddHostedService<LongRunning>();
            services.AddSingleton<IProcessorWorkflow, PopulateMovieStoreWorkflow>();
        }

        /// <summary>
        /// Configures the specified application. This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime, RestClient restClient)
        {
            ArgumentNullException.ThrowIfNull(lifetime);

            _ = lifetime.ApplicationStopping.Register(() =>
            {
                restClient.Dispose();
            });
        }
    }
}
