using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class StarStateTests
{
    [TestMethod]
    public void Constructor_WithTrueValue_ShouldSetStarredToTrue()
    {
        // Arrange & Act
        var starState = new StarState(true);

        // Assert
        Assert.IsTrue(starState.Starred);
    }

    [TestMethod]
    public void Constructor_WithFalseValue_ShouldSetStarredToFalse()
    {
        // Arrange & Act
        var starState = new StarState(false);

        // Assert
        Assert.IsFalse(starState.Starred);
    }

    [TestMethod]
    public void Starred_CanBeSetToTrue()
    {
        // Arrange
        var starState = new StarState(false)
        {
            // Act
            Starred = true
        };

        // Assert
        Assert.IsTrue(starState.Starred);
    }

    [TestMethod]
    public void Starred_CanBeSetToFalse()
    {
        // Arrange
        var starState = new StarState(true)
        {
            // Act
            Starred = false
        };

        // Assert
        Assert.IsFalse(starState.Starred);
    }

    [TestMethod]
    public void Equality_TwoInstancesWithSameStarredValue_ShouldBeEqual()
    {
        // Arrange
        var starState1 = new StarState(true);
        var starState2 = new StarState(true);

        // Act & Assert
        Assert.AreEqual(starState1, starState2);
        Assert.IsTrue(starState1 == starState2);
    }

    [TestMethod]
    public void Equality_TwoInstancesWithDifferentStarredValue_ShouldNotBeEqual()
    {
        // Arrange
        var starState1 = new StarState(true);
        var starState2 = new StarState(false);

        // Act & Assert
        Assert.AreNotEqual(starState1, starState2);
        Assert.IsTrue(starState1 != starState2);
    }

    [TestMethod]
    public void GetHashCode_TwoInstancesWithSameValue_ShouldHaveSameHashCode()
    {
        // Arrange
        var starState1 = new StarState(true);
        var starState2 = new StarState(true);

        // Act
        var hash1 = starState1.GetHashCode();
        var hash2 = starState2.GetHashCode();

        // Assert
        Assert.AreEqual(hash1, hash2);
    }

    [TestMethod]
    public void GetHashCode_TwoInstancesWithDifferentValues_ShouldHaveDifferentHashCodes()
    {
        // Arrange
        var starState1 = new StarState(true);
        var starState2 = new StarState(false);

        // Act
        var hash1 = starState1.GetHashCode();
        var hash2 = starState2.GetHashCode();

        // Assert
        Assert.AreNotEqual(hash1, hash2);
    }

    [TestMethod]
    public void ToString_WithTrueValue_ShouldReturnExpectedFormat()
    {
        // Arrange
        var starState = new StarState(true);

        // Act
        var result = starState.ToString();

        // Assert
        Assert.IsNotNull(result);
        Assert.Contains("True", result);
    }

    [TestMethod]
    public void ToString_WithFalseValue_ShouldReturnExpectedFormat()
    {
        // Arrange
        var starState = new StarState(false);

        // Act
        var result = starState.ToString();

        // Assert
        Assert.IsNotNull(result);
        Assert.Contains("False", result);
    }

    [TestMethod]
    public void Record_Equality_WithModifiedProperty_ShouldNotBeEqual()
    {
        // Arrange
        var starState1 = new StarState(true);
        var starState2 = new StarState(true)
        {
            // Act
            Starred = false
        };

        // Assert
        Assert.AreNotEqual(starState1, starState2);
    }

    [TestMethod]
    public void With_Expression_ShouldCreateNewInstanceWithModifiedValue()
    {
        // Arrange
        var original = new StarState(true);

        // Act
        var modified = original with { Starred = false };

        // Assert
        Assert.IsTrue(original.Starred);
        Assert.IsFalse(modified.Starred);
        Assert.AreNotEqual(original, modified);
    }
}
