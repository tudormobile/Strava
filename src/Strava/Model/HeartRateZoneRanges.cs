namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a collection of heart rate zone ranges used to categorize heart rate measurements for analysis or fitness
/// tracking.
/// </summary>
/// <remarks>Use this class to define and manage multiple heart rate zones, such as resting, fat burn, cardio, and
/// peak, for applications that analyze heart rate data. Each zone range specifies a lower and upper heart rate
/// boundary, enabling classification of heart rate readings into meaningful categories.</remarks>
public class HeartRateZoneRanges : Zone<HeartRateZoneRange>
{
}
