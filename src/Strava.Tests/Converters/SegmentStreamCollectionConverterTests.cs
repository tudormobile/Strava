using Tudormobile.Strava;
using Tudormobile.Strava.Model;

namespace Strava.Tests.Converters;

[TestClass]
public class SegmentStreamCollectionConverterTests
{
    [TestMethod]
    public void SegmentStreamConverter_CanConvertFromJson()
    {
        var json = @"[
  {
    ""type"": ""latlng"",
    ""data"": [
      [
        37.833112,
        -122.483436
      ],
      [
        37.832964,
        -122.483406
      ]
    ],
    ""series_type"": ""distance"",
    ""original_size"": 2,
    ""resolution"": ""high""
  },
  {
    ""type"": ""distance"",
    ""data"": [
      0,
      16.8
    ],
    ""series_type"": ""distance"",
    ""original_size"": 2,
    ""resolution"": ""high""
  },
  {
    ""type"": ""altitude"",
    ""data"": [
      92.4,
      93.4
    ],
    ""series_type"": ""distance"",
    ""original_size"": 2,
    ""resolution"": ""high""
  }
]";
        var success = StravaSerializer.TryDeserialize<SegmentStreamCollection>(json, out var result);

        Assert.IsTrue(success);
        Assert.IsNotNull(result);
        Assert.HasCount(3, result);
        Assert.IsInstanceOfType<SegmentStream>(result[0]);
        Assert.IsInstanceOfType<SegmentEffortStream>(result[1]);
        Assert.IsInstanceOfType<SegmentEffortStream>(result[2]);

    }
}
