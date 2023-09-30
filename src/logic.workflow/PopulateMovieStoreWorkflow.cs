// <copyright file="PopulateMovieStoreWorkflow.cs" company="Lavanya.">
// </copyright>

namespace Microsoft.Movie.Hub.Processor.Workflow
{
    using Azure.Search.Documents;
    using Microsoft.Movie.Hub.Processor.Models;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// Workflow for the Continous SLNM Kpi.
    /// </summary>
    public class PopulateMovieStoreWorkflow : IProcessorWorkflow
    {
        private readonly SearchClient searchClient;

        private readonly RestClient restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="PopulateMovieStoreWorkflow"/> class.
        /// </summary>
        public PopulateMovieStoreWorkflow(RestClient restClient, SearchClient searchClient)
        {
            this.restClient = restClient;
            this.searchClient = searchClient;
        }

        /// <summary>
        /// Task to populate the Continous Slnm Kpi Table.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task ProcessAsync()
        {
            int page = 1, totalPages = 1;
            bool isFirstTime = true;

            do
            {
                try
                {
                    var requestMovieChanges = new RestRequest($"https://api.themoviedb.org/3/movie/changes?page={page}&api_key=");
                    requestMovieChanges.AddHeader("accept", "application/json");
                    requestMovieChanges.AddHeader("Authorization", "Bearer 38f5b378e4d94e47e73889b59ff524b0");
                    RestResponse response = await this.restClient.GetAsync(requestMovieChanges).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        MovieIdsResponse movieIdsResponse = JsonSerializer.Deserialize<MovieIdsResponse>(response.Content);

                        if (isFirstTime)
                        {
                            totalPages = movieIdsResponse.TotalPages;
                            isFirstTime = false;
                        }

                        foreach (var movieId in movieIdsResponse.MovieIds)
                        {
                            List<MovieIndexRecord> movies = new List<MovieIndexRecord>();

                            var movieRequest = new RestRequest($"https://api.themoviedb.org/3/movie/{movieId.Id}?api_key=");
                            movieRequest.AddHeader("accept", "application/json");

                            RestResponse movieDetails = await this.restClient.GetAsync(movieRequest).ConfigureAwait(false);
                            Movie movie = JsonSerializer.Deserialize<Movie>(movieDetails.Content);

                            MovieIndexRecord indexRecord = new MovieIndexRecord();
                            indexRecord.Title = movie.Title;
                            indexRecord.Id = $"{movie.Id}";
                            indexRecord.Genre = movie.Genres.Count > 0 ? movie.Genres.First().Name : string.Empty;
                            indexRecord.Language = movie.Languages.Count > 0 ? movie.Languages.First().Name : string.Empty;

                            movies.Add(indexRecord);

                            try
                            {
                                await this.searchClient.MergeOrUploadDocumentsAsync(movies).ConfigureAwait(false);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                page++;

            } while (page < totalPages);
        }
    }
}