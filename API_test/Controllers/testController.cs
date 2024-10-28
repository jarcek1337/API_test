using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

[ApiController]
[Route("test")]

public class StatsController2 : ControllerBase
{
    private readonly HttpClient _httpClient;

    // Inject HttpClient through dependency injection
    public StatsController2(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        // URL of the endpoint
        string url = "https://api.performfeeds.com/soccerdata/matchexpectedgoals/1vmmaetzoxkgg1qf6pkpfmku0k/7sasdn9v78std9dm86ajvnuok?_rt=b&_fmt=json";

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
