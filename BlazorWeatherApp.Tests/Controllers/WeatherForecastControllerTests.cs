using BlazorWeatherApp.Api.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BlazorWeatherApp.Tests.Controllers;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ReturnsCorrectNumberOfForecasts()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<WeatherForecastController>>();
        var controller = new WeatherForecastController(loggerMock.Object);

        // Act
        var result = controller.Get();

        // Assert
        Assert.Equal(5, result.Count());
    }

    [Fact]
    public void Get_ReturnsValidForecasts()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<WeatherForecastController>>();
        var controller = new WeatherForecastController(loggerMock.Object);

        // Act
        var result = controller.Get();

        // Assert
        foreach (var forecast in result)
        {
            Assert.NotNull(forecast.Summary);
            Assert.NotEqual(default, forecast.Date);
            Assert.Equal(32 + (int)(forecast.TemperatureC / 0.5556), forecast.TemperatureF);
        }
    }
}