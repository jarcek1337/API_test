using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

[ApiController]
[Route("matches")]

public class StatsController : ControllerBase
{
    private readonly HttpClient _httpClient;

    // Inject HttpClient through dependency injection
    public StatsController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        // URL of the endpoint
        string url = "https://api.performfeeds.com/soccerdata/match/1vmmaetzoxkgg1qf6pkpfmku0k?tmcl=408bfjw6uz5k19zk4am50ykmh&_fmt=json&_rt=b#";

        HttpResponseMessage response = null;

        try
        {
            // Fetch data from API
            response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Read the response as a string
            string responseData = await response.Content.ReadAsStringAsync();

            return Ok(responseData); // Return raw JSON string
        }
        catch (HttpRequestException ex)
        {
            // Handle null response case safely
            var statusCode = response != null ? (int)response.StatusCode : 500;
            return StatusCode(statusCode, new { error = ex.Message });
        }
    }
}
