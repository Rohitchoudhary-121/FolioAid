using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FolioAid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [Authorize]
        public IEnumerable<WeatherForecast> Get()
        {
            //var jwtTokenHandler = new JwtSecurityTokenHandler();

            //try
            //{
            //    var jwtBearerOptions = new JwtBearerOptions()
            //    {
            //        TokenValidationParameters =  {
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("15aceb7f-5d3f-4050-bfb9-028c377596b6")),

            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidAudiences = new string[] { "FolioAidUI" },
            //        ValidIssuer = "https://localhost:44480",
            //        ClockSkew = TimeSpan.Zero,
            //        ValidateIssuerSigningKey = true,
            //        RequireSignedTokens = true,

            //    }
            //    };

            //    SecurityToken validatedToken = null;
            //    var principal = jwtTokenHandler.ValidateToken(accessToken, jwtBearerOptions.TokenValidationParameters, out validatedToken);
            //}
            //catch (Exception ex)
            //{
            //}

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}