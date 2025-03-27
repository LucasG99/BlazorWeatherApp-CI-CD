using System.Net.Http.Json;
using BlazorWeatherApp.Web.Models;
using Microsoft.Extensions.Configuration;

namespace BlazorWeatherApp.Web.Services;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public WeatherService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<WeatherForecast[]?> GetForecastAsync()
    {
        var baseUrl = _configuration["ApiBaseUrl"];
        _httpClient.BaseAddress = new Uri(baseUrl ?? "");
        
        return await _httpClient.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
    }
}