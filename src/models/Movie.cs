// <copyright file="Genre.cs" company="Lavanya.">
// </copyright>

namespace Microsoft.Movie.Hub.Processor.Models
{
    using System.Collections.ObjectModel;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Movie details.
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// Gets or sets the Movie identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the movie title.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Genres.
        /// </summary>
        [JsonPropertyName("genres")]
        public Collection<Genre> Genres { get; set; }

        /// <summary>
        /// Gets or sets the Languages.
        /// </summary>
        [JsonPropertyName("spoken_languages")]
        public Collection<Language> Languages { get; set; } 
    }
}
