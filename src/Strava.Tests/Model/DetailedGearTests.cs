using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class DetailedGearTests
{
    [TestMethod]
    public void Constructor_WithDefaultValues_ShouldInitializeValues()
    {
        // Arrange & Act
        var gear = new DetailedGear();

        // Assert
        Assert.AreEqual(string.Empty, gear.Id);
        Assert.AreEqual(string.Empty, gear.Description);
        Assert.AreEqual(default, gear.ResourceState);
        Assert.IsFalse(gear.Primary);
        Assert.AreEqual(0, gear.Distance);
        Assert.IsNull(gear.BrandName);
        Assert.IsNull(gear.ModelName);
        Assert.AreEqual(FrameTypes.None, gear.FrameType);
        // Assert
        // When both BrandName and ModelName are null, string.Join returns "-"
        Assert.AreEqual("", gear.Name);
    }

    [TestMethod]
    public void Name_WithNameExplictlySet_ShouldReturnName()
    {
        //Arrange
        var gear = new DetailedGear
        {
            Name = "CustomName",
            ModelName = "ModelX",
            BrandName = "BrandY"
        };
        // Act
        var name = gear.Name;
        // Assert
        Assert.AreEqual("CustomName", name);
        Assert.AreEqual("ModelX", gear.ModelName);
        Assert.AreEqual("BrandY", gear.BrandName);
    }

    [TestMethod]
    public void Name_WithOnlyModelName_ShouldReturnModelName()
    {
        // Arrange
        var gear = new DetailedGear
        {
            ModelName = "ModelX"
        };
        // Act
        var name = gear.Name;
        // Assert
        Assert.AreEqual("ModelX", name);
        Assert.AreEqual("ModelX", gear.ModelName);
    }

    [TestMethod]
    public void Name_WithOnlyBrandName_ShouldReturnBrandName()
    {
        // Arrange
        var gear = new DetailedGear
        {
            BrandName = "BrandY"
        };
        // Act
        var name = gear.Name;
        // Assert
        Assert.AreEqual("BrandY", name);
        Assert.AreEqual("BrandY", gear.BrandName);
    }

    [TestMethod]
    public void Name_WithModelName_AndBrandName_ShouldReturnBrandAndModelWithSeparator()
    {
        // Arrange
        var gear = new DetailedGear
        {
            BrandName = "BrandY",
            ModelName = "ModelX"
        };
        // Act
        var name = gear.Name;
        // Assert
        Assert.AreEqual("BrandY-ModelX", name);
        Assert.AreEqual("BrandY", gear.BrandName);
        Assert.AreEqual("ModelX", gear.ModelName);
    }

}
