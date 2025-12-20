namespace Tudormobile.Strava.Model;

/// <summary>
/// Represents a user-generated comment associated with an activity.
/// </summary>
/// <remarks>The <see cref="Comment"/> class encapsulates the content and metadata for a comment, including its
/// unique identifier, the related activity, and the comment text. This class is typically used to store or transfer
/// comment information within applications that support activity-based discussions or feedback.</remarks>
public class Comment
{
    /// <summary>
    /// The unique identifier of this comment.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// The identifier of the activity this comment is related to.
    /// </summary>
    public long ActivityId { get; set; }

    /// <summary>
    /// The content of the comment.
    /// </summary>
    public string Text { get; set; } = String.Empty;

    /// <summary>
    /// The associated athlete.
    /// </summary>
    public SummaryAthlete? Athlete { get; set; }

    /// <summary>
    /// The time at which this comment was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
