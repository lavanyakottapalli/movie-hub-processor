// <copyright file="MovieIdsResponse.cs" company="Lavanya.">
// </copyright>

namespace Microsoft.Movie.Hub.Processor.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Class to represent response from TMDB api.
    /// </summary>
    public class MovieIdsResponse
    {
        /// <summary>
        /// Gets or sets list of movie ids.
        /// </summary>
        [JsonPropertyName("results")]
        public Collection<MovieId> MovieIds { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        [JsonPropertyName("page")]
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the total pages.
        /// </summary>
        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the total results.
        /// </summary>
        [JsonPropertyName("total_results")] 
        public int TotalResults { get; set; }
    }
}
