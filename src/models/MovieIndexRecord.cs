// <copyright file="MovieIndexRecord.cs" company="Lavanya.">
// </copyright>

namespace Microsoft.Movie.Hub.Processor.Models
{
    using System.Collections.ObjectModel;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Movie details.
    /// </summary>
    public class MovieIndexRecord
    {
        /// <summary>
        /// Gets or sets the Movie identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the movie title.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Genres.
        /// </summary>
        [JsonPropertyName("genre")]
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets the Languages.
        /// </summary>
        [JsonPropertyName("language")]
        public string Language { get; set; }
    }
}