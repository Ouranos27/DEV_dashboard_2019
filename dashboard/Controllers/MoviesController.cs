using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dashboard.ApiWrapper;
using dashboard.Models;
using dashboard.Responses.Movies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Result = dashboard.Models.Result;

namespace dashboard.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly MoviesWrapper Endpoint = new MoviesWrapper();

        public MoviesController(ILogger<MoviesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Movies Get([FromBody] string movie)
        {
            Task<MoviesResponse> response = Endpoint.FetchMovies(movie);
            Movies  movies = new Movies();
            movies.Results = new List<Result>();

            foreach (var result in response.Result.results)
            {
                Result res = new Result();
                res.Tittle = result.title;
                res.OriginalLanguage = result.original_language;
                res.Overview = result.overview;
                res.ReleaseDate = result.release_date;
                movies.Results.Add(res);
            }
            return movies;
        }
    }
}