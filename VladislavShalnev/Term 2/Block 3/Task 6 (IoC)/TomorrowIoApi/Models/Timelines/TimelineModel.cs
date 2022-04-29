namespace TomorrowIoApi.Models.Timelines;

public record TimelineModel
{
	public string? Timestep { get; init; }
	public DateTime? EndTime { get; init; }
	public DateTime? StartTime { get; init; }
	public IntervalModel[]? Intervals { get; init; }
}
