using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JWT.Web.Models;

namespace JWT.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _client;
    private readonly IHttpClientFactory _clientFactory;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _clientFactory = httpClientFactory;
        _client = _clientFactory.CreateClient("requestHandler");
    }

    public async Task<IActionResult> Index()
    {
        var endpoint = "/api/test/testrequest";
        var protectedResponse = await _client.GetAsync(endpoint);
        var response = new TestResponse();
        if (protectedResponse.IsSuccessStatusCode)
        {
            var protectedData = await protectedResponse.Content.ReadAsStringAsync();
            response.response = protectedData;
            _logger.LogInformation(protectedData);
        }
        else
        {
            _logger.LogWarning("Failed to access protected endpoint. Status code: " + protectedResponse.StatusCode);
        }
        
        return View(response);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}