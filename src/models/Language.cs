// <copyright file="Genre.cs" company="Lavanya.">
// </copyright>

using System.Text.Json.Serialization;

namespace Microsoft.Movie.Hub.Processor.Models
{
    /// <summary>
    /// Language details of the movie.
    /// </summary>
    public class Language
    {
        /// <summary>
        /// Gets or sets the langugate identifier.
        /// </summary>
        [JsonPropertyName("english_name")]
        public string Name { get; set; }
    }
}
