// <copyright file="LongRunning.cs" company="Lavanya.">
// </copyright>

namespace Microsoft.Movie.Hub.Processor
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Movie.Hub.Processor.Workflow;

    /// <summary>
    /// Long running service.
    /// </summary>
    public class LongRunning : BackgroundService
    {
        private readonly IProcessorWorkflow processorWorkflow;

        /// <summary>
        /// Long Running workflow constructor.
        /// </summary>
        /// <param name="processorWorkflow">Workflow to fetch movies from TMDB.</param>
        public LongRunning(IProcessorWorkflow processorWorkflow)
        {
            this.processorWorkflow = processorWorkflow;
        }

        /// <summary>
        /// Run processing job in the background.
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns>Task.</returns>
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await this.processorWorkflow.ProcessAsync().ConfigureAwait(false);
        }
    }
}
