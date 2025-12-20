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

    [TestMethod]
    public void Deserialize_WithZero_ReturnsUnknown()
    {
        // Arrange
        var json = @"{""resource_state"": 0}";

        // Act
        var result = JsonSerializer.Deserialize<TestClass>(json, StravaSerializer.Options);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(ResourceStates.Unknown, result.ResourceState);
    }

    [TestMethod]
    public void Deserialize_WithOne_ReturnsMeta()
    {
        // Arrange
        var json = @"{""resource_state"": 1}";

        // Act
        var result = JsonSerializer.Deserialize<TestClass>(json, StravaSerializer.Options);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(ResourceStates.Meta, result.ResourceState);
    }

    [TestMethod]
    public void Deserialize_WithStringMeta_ReturnsMeta()
    {
        // Arrange
        var json = @"{""resource_state"": ""Meta""}";

        // Act
        var result = JsonSerializer.Deserialize<TestClass>(json, StravaSerializer.Options);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(ResourceStates.Meta, result.ResourceState);
    }

    [TestMethod]
    public void Deserialize_WithStringLowercase_ReturnsCorrectEnum()
    {
        // Arrange
        var json = @"{""resource_state"": ""summary""}";

        // Act
        var result = JsonSerializer.Deserialize<TestClass>(json, StravaSerializer.Options);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(ResourceStates.Summary, result.ResourceState);
    }

    [TestMethod]
    public void Deserialize_WithStringDetail_ReturnsDetail()
    {
        // Arrange
        var json = @"{""resource_state"": ""Detail""}";

        // Act
        var result = JsonSerializer.Deserialize<TestClass>(json, StravaSerializer.Options);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(ResourceStates.Detail, result.ResourceState);
    }

    [TestMethod]
    public void Deserialize_WithInvalidString_ReturnsUnknown()
    {
        // Arrange
        var json = @"{""resource_state"": ""InvalidValue""}";

        // Act
        var result = JsonSerializer.Deserialize<TestClass>(json, StravaSerializer.Options);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(ResourceStates.Unknown, result.ResourceState);
    }

    [TestMethod]
    public void Deserialize_WithInvalidNumber_ReturnsUnknown()
    {
        // Arrange
        var json = @"{""resource_state"": 999}";

        // Act
        var result = JsonSerializer.Deserialize<TestClass>(json, StravaSerializer.Options);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(ResourceStates.Unknown, result.ResourceState);
    }

    [TestMethod]
    public void Deserialize_WithNegativeNumber_ReturnsUnknown()
    {
        // Arrange
        var json = @"{""resource_state"": -1}";

        // Act
        var result = JsonSerializer.Deserialize<TestClass>(json, StravaSerializer.Options);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(ResourceStates.Unknown, result.ResourceState);
    }

    [TestMethod]
    public void Deserialize_WithBoolean_ReturnsUnknown()
    {
        // Arrange
        var json = @"{""resource_state"": true}";

        // Act
        var result = JsonSerializer.Deserialize<TestClass>(json, StravaSerializer.Options);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(ResourceStates.Unknown, result.ResourceState);
    }

    [TestMethod]
    public void Deserialize_WithNull_ReturnsUnknown()
    {
        // Arrange
        var json = @"{""resource_state"": null}";

        // Act
        var result = JsonSerializer.Deserialize<TestClass>(json, StravaSerializer.Options);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(ResourceStates.Unknown, result.ResourceState);
    }

    [TestMethod]
    public void Deserialize_WithEmptyString_ReturnsUnknown()
    {
        // Arrange
        var json = @"{""resource_state"": """"}";

        // Act
        var result = JsonSerializer.Deserialize<TestClass>(json, StravaSerializer.Options);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(ResourceStates.Unknown, result.ResourceState);
    }

    [TestMethod]
    public void Serialize_AllEnumValues_ProducesCorrectNumbers()
    {
        // Test all enum values
        Assert.Contains("\"resource_state\":0", JsonSerializer.Serialize(new TestClass { ResourceState = ResourceStates.Unknown }, StravaSerializer.Options));
        Assert.Contains("\"resource_state\":1", JsonSerializer.Serialize(new TestClass { ResourceState = ResourceStates.Meta }, StravaSerializer.Options));
        Assert.Contains("\"resource_state\":2", JsonSerializer.Serialize(new TestClass { ResourceState = ResourceStates.Summary }, StravaSerializer.Options));
        Assert.Contains("\"resource_state\":3", JsonSerializer.Serialize(new TestClass { ResourceState = ResourceStates.Detail }, StravaSerializer.Options));
    }

    [TestMethod]
    public void RoundTrip_WithAllValidValues_PreservesData()
    {
        // Test round-trip for each valid value
        foreach (ResourceStates state in Enum.GetValues<ResourceStates>())
        {
            var original = new TestClass { ResourceState = state };
            var json = JsonSerializer.Serialize(original, StravaSerializer.Options);
            var result = JsonSerializer.Deserialize<TestClass>(json, StravaSerializer.Options);

            Assert.IsNotNull(result);
            Assert.AreEqual(state, result.ResourceState, $"Round-trip failed for {state}");
        }
    }

    private class TestClass
    {
        public ResourceStates ResourceState { get; set; }
    }
}