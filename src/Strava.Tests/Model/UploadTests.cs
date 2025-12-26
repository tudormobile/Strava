using Tudormobile.Strava.Model;

namespace Strava.Tests.Model;

[TestClass]
public class UploadTests
{
    [TestMethod]
    public void Constructor_ShouldInitialize()
    {
        // Arrange & Act
        var upload = new Upload();

        // Assert
        Assert.IsNotNull(upload);
        Assert.AreEqual(0, upload.Id);
        Assert.AreEqual(0, upload.ActivityId);
    }

    [TestMethod]
    public void Id_CanBeSetAndRetrieved()
    {
        // Arrange
        var upload = new Upload
        {
            Id = 123456789
        };

        // Act
        var id = upload.Id;

        // Assert
        Assert.AreEqual(123456789, id);
    }

    [TestMethod]
    public void ActivityId_CanBeSetAndRetrieved()
    {
        // Arrange
        var upload = new Upload
        {
            ActivityId = 987654321
        };

        // Act
        var activityId = upload.ActivityId;

        // Assert
        Assert.AreEqual(987654321, activityId);
    }

    [TestMethod]
    public void IdStr_CanBeSetAndRetrieved()
    {
        // Arrange
        var upload = new Upload
        {
            IdStr = "123456789"
        };

        // Act
        var idStr = upload.IdStr;

        // Assert
        Assert.AreEqual("123456789", idStr);
    }

    [TestMethod]
    public void ExternalId_CanBeSetAndRetrieved()
    {
        // Arrange
        var upload = new Upload
        {
            ExternalId = "external_123.gpx"
        };

        // Act
        var externalId = upload.ExternalId;

        // Assert
        Assert.AreEqual("external_123.gpx", externalId);
    }

    [TestMethod]
    public void Error_CanBeSetAndRetrieved()
    {
        // Arrange
        var upload = new Upload
        {
            Error = "Invalid file format"
        };

        // Act
        var error = upload.Error;

        // Assert
        Assert.AreEqual("Invalid file format", error);
    }

    [TestMethod]
    public void Status_CanBeSetAndRetrieved()
    {
        // Arrange
        var upload = new Upload
        {
            Status = "Your activity is ready."
        };

        // Act
        var status = upload.Status;

        // Assert
        Assert.AreEqual("Your activity is ready.", status);
    }

    [TestMethod]
    public void Upload_WithAllProperties_ShouldRetainValues()
    {
        // Arrange
        var upload = new Upload
        {
            Id = 123456789,
            ActivityId = 987654321,
            IdStr = "123456789",
            ExternalId = "ride_2023.tcx",
            Error = null!,
            Status = "Your activity is ready."
        };

        // Act & Assert
        Assert.AreEqual(123456789, upload.Id);
        Assert.AreEqual(987654321, upload.ActivityId);
        Assert.AreEqual("123456789", upload.IdStr);
        Assert.AreEqual("ride_2023.tcx", upload.ExternalId);
        Assert.IsNull(upload.Error);
        Assert.AreEqual("Your activity is ready.", upload.Status);
    }

    [TestMethod]
    public void Status_WithProcessingStatus_ShouldStore()
    {
        // Arrange & Act
        var upload = new Upload
        {
            Status = "Your activity is still being processed."
        };

        // Assert
        Assert.AreEqual("Your activity is still being processed.", upload.Status);
    }

    [TestMethod]
    public void Status_WithErrorStatus_ShouldStoreWithError()
    {
        // Arrange & Act
        var upload = new Upload
        {
            Status = "There was an error processing your activity.",
            Error = "Duplicate activity detected"
        };

        // Assert
        Assert.AreEqual("There was an error processing your activity.", upload.Status);
        Assert.AreEqual("Duplicate activity detected", upload.Error);
    }

    [TestMethod]
    public void Upload_WithNullStringProperties_ShouldBeAllowed()
    {
        // Arrange & Act
        var upload = new Upload
        {
            Id = 123,
            IdStr = null!,
            ExternalId = null!,
            Error = null!,
            Status = null!
        };

        // Assert
        Assert.AreEqual(123, upload.Id);
        Assert.IsNull(upload.IdStr);
        Assert.IsNull(upload.ExternalId);
        Assert.IsNull(upload.Error);
        Assert.IsNull(upload.Status);
    }

    [TestMethod]
    public void Upload_WithZeroActivityId_ShouldBeValid()
    {
        // Arrange & Act
        var upload = new Upload
        {
            Id = 123456789,
            ActivityId = 0,
            Status = "Your activity is still being processed."
        };

        // Assert
        Assert.AreEqual(0, upload.ActivityId);
        Assert.AreEqual(123456789, upload.Id);
    }
}
