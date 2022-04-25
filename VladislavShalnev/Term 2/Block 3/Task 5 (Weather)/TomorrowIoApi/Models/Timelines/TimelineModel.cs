namespace TomorrowIoApi.Models.Timelines;

public record TimelineModel
{
	public string? Timestep { get; set; }
	public DateTime? EndTime { get; set; }
	public DateTime? StartTime { get; set; }
	public IntervalModel[]? Intervals { get; set; }
}
