// <copyright file="Genre.cs" company="Lavanya.">
// </copyright>

namespace Microsoft.Movie.Hub.Processor.Models
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Genre.
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// Gets or sets the name of genre.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
