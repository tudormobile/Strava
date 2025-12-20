using System.Text;
using System.Text.Json;
using Tudormobile.Strava;
using Tudormobile.Strava.Model;

namespace Strava.Tests.Converters;

[TestClass]
public class ResourceStatesConverterTests
{
    [TestMethod]
    public void Deserialize_WithNumber_ConvertsToEnum()
    {
        // Arrange
        var json = @"{""resource_state"": 2}";

        // Act
        var result = JsonSerializer.Deserialize<TestClass>(json, StravaSerializer.Options);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(ResourceStates.Summary, result.ResourceState);
    }

    [TestMethod]
    public void Serialize_WithEnum_ConvertsToNumber()
    {
        // Arrange
        var obj = new TestClass { ResourceState = ResourceStates.Detail };

        // Act
        var json = JsonSerializer.Serialize(obj, StravaSerializer.Options);

        // Assert
        Assert.Contains("\"resource_state\":3", json);
    }

    [TestMethod]
    public void Deserialize_DetailedAthlete_ConvertsResourceState()
    {
        // Arrange
        var json = @"{""resource_state"": 3}";

        // Act
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
        var result = JsonSerializer.Deserialize<DetailedAthlete>(stream, StravaSerializer.Options);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(ResourceStates.Detail, result.ResourceState);
    }

    private class TestClass
    {
        public ResourceStates ResourceState { get; set; }
    }
}