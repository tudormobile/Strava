using System.Text;
using System.Text.Json;
using Tudormobile.Strava;
using Tudormobile.Strava.Model;

namespace Strava.Tests;

[TestClass]
public class StravaSerializerTests
{
    [TestMethod]
    public void BadJsonStringTest()
    {
        var json = "this is not a json object";
        var actual = StravaSerializer.TryDeserialize<SummaryActivity>(json, out var _);
        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void BadJsonStreamTest()
    {
        var json = "this is not a json object";
        var s = new MemoryStream(Encoding.UTF8.GetBytes(json));
        var actual = StravaSerializer.TryDeserialize<SummaryActivity>(s, out var _);
        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void BadJsonWithExceptionTest()
    {
        var json = "this is not a json object";
        var s = new MemoryStream(Encoding.UTF8.GetBytes(json));
        var actual = StravaSerializer.TryDeserialize(s, out SummaryActivity? activity, out JsonException? ex);
        Assert.IsFalse(actual);
        Assert.IsNull(activity);
        Assert.IsNotNull(ex);
        Assert.IsInstanceOfType<JsonException>(ex);
    }

    [TestMethod]
    public void TryDeserializeWithValidJsonTest()
    {
        var expected = 12345;
        var json = $@"{{
""id"": {expected}
}}";

        Assert.IsTrue(StravaSerializer.TryDeserialize<AthleteId>(json, out var actual));
        Assert.IsNotNull(actual);
        Assert.AreEqual(expected, actual.Id);
    }

    [TestMethod]
    public void TryDeserializeWithValidJsonAndExceptionTest()
    {
        var expected = 12345;
        var json = $@"{{
""id"": {expected}
}}";

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
        Assert.IsTrue(StravaSerializer.TryDeserialize<AthleteId>(stream, out var actual, out var exception));
        Assert.IsNull(exception);
        Assert.IsNotNull(actual);
        Assert.AreEqual(expected, actual.Id);
    }


    [TestMethod]
    public void OptionsTests()
    {
        var options = StravaSerializer.Options;
        Assert.IsNotNull(options.PropertyNamingPolicy);
    }

    [TestMethod]
    public async Task SerializeDeserializeAsyncTest()
    {
        var metaActivityId = 123;
        var metaAthleteId = 456;
        var startDate = DateTime.UtcNow;
        var lap = new Lap()
        {
            Activity = new MetaActivity() { Id = metaActivityId },
            Athlete = new MetaAthlete() { Id = metaAthleteId },
            Distance = 5,
            StartDate = startDate,
            ElapsedTime = TimeSpan.FromSeconds(10)
        };

        using var stream = new MemoryStream();

        await StravaSerializer.SerializeAsync(stream, lap, CancellationToken.None);
        stream.Position = 0;
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync(TestContext.CancellationToken);

        Assert.Contains("elapsed_time", json);
        Assert.Contains("\"distance\":5", json);

        using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
        var actual = await StravaSerializer.DeserializeAsync<Lap>(jsonStream, CancellationToken.None);

        Assert.IsNotNull(actual);
        Assert.AreEqual(lap.Distance, actual.Distance);
        Assert.AreEqual(metaAthleteId, actual.Athlete.Id);
        Assert.AreEqual(metaActivityId, actual.Activity.Id);
    }

    [TestMethod]
    public void Read_WithNoStartObject_Fails()
    {
        var json = "[test";
        Assert.IsFalse(StravaSerializer.TryDeserialize<ActivityStats>(json, out var result));
        Assert.IsNull(result);
    }

    [TestMethod]
    public void DeserializeCommandTest()
    {
        var json = @"[
  {
    ""id"": 12345678987654320,
    ""activity_id"": 12345678987654320,
    ""post_id"": null,
    ""resource_state"": 2,
    ""text"": ""Good job and keep the cat pictures coming!"",
    ""mentions_metadata"": null,
    ""created_at"": ""2018-02-08T19:25:39Z"",
    ""athlete"": {
      ""firstname"": ""Peter"",
      ""lastname"": ""S""
    },
    ""cursor"": ""abc123%20""
  }
]";
        var success = StravaSerializer.TryDeserialize<List<Comment>>(json, out var comments);
        Assert.IsTrue(success);
        Assert.IsNotNull(comments);
        Assert.HasCount(1, comments);
        Assert.AreEqual(12345678987654320, comments[0].Id);
        Assert.AreEqual("Good job and keep the cat pictures coming!", comments[0].Text);
        Assert.AreEqual(DateTime.Parse("2018-02-08T19:25:39Z"), comments[0].CreatedAt.ToLocalTime());
        Assert.IsNotNull(comments[0].Athlete);
        Assert.AreEqual("Peter", comments[0].Athlete?.Firstname);
        Assert.AreEqual("S", comments[0].Athlete?.Lastname);
    }

    public TestContext TestContext { get; set; }
}
