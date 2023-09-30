// <copyright file="IProcessWorkflow.cs" company="Lavanya.">
// </copyright>

namespace Microsoft.Movie.Hub.Processor.Workflow
{
    using System.Threading.Tasks;

    /// <summary>
    /// Workflow Processor.
    /// </summary>
    public interface IProcessorWorkflow
    {
        /// <summary>
        /// Processor that can be used to build business logic
        /// that can execute in the background.
        /// </summary>
        /// <returns>Task.</returns>
        public Task ProcessAsync();
    }
}
