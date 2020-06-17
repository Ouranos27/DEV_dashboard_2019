using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dashboard.ApiWrapper;
using dashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace dashboard.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TranslateController
    {
        private readonly ILogger<TranslateController> _logger;
        private TranslateWrrapper   Endpoint = new TranslateWrrapper();
        
        public TranslateController(ILogger<TranslateController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public TranslateModel Get([FromQuery] string from, [FromQuery] string to,[FromQuery] string text)
        {
            Task<TranslateModel> result = Endpoint.FetchTranslate(from, to, text);
            return result.Result;
        } 
    }
}