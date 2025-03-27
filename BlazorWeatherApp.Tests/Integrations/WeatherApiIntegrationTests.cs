using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using BlazorWeatherApp.Api;
using System.Text.Json;
using BlazorWeatherApp.Api.Models;

namespace BlazorWeatherApp.Tests.Integration;

public class WeatherApiIntegrationTests
{
    private readonly WebApplicationFactory<Program> _factory;

    public WeatherApiIntegrationTests()
    {
        _factory = new WebApplicationFactory<Program>();
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsSuccessAndCorrectContentType()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/WeatherForecast");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8", 
            response.Content.Headers.ContentType?.ToString());
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsExpectedNumberOfForecasts()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/WeatherForecast");
        var content = await response.Content.ReadAsStringAsync();
        var forecasts = JsonSerializer.Deserialize<WeatherForecast[]>(content, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Assert
        Assert.NotNull(forecasts);
        Assert.Equal(5, forecasts.Length);
    }
}