using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DadJokesAPI.Helpers;
using DadJokesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DadJokesAPI.Controllers
{
    [Route("api/jokes")]
    [ApiController]
    public class JokesController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private static readonly ApiClient apiClient = new ApiClient();

        public JokesController(IConfiguration configuration)
        {
            Configuration = configuration;
		}

        [HttpGet]
        public async Task<ActionResult<DadJoke>> GetJoke()
        {
            return await apiClient.Get<DadJoke>();
        }
    }
}