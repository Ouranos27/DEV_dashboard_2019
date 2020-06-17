using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using dashboard.Responses.Movies;
using Newtonsoft.Json;

namespace dashboard.ApiWrapper
{
    public class MoviesWrapper
    {
        public async Task<MoviesResponse> FetchMovies(string movie)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(
                    "https://api.themoviedb.org/3/search/company?api_key=319bb88e8f2caefe1a7e362f8e5d3445&query=" +
                    movie +"&page=1").ConfigureAwait(true))

                using (HttpContent content = response.Content)
                {
                    string result = await content.ReadAsStringAsync().ConfigureAwait(true);
                    MoviesResponse movies = JsonConvert.DeserializeObject<MoviesResponse>(result);
                    return movies;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }
    }
}
