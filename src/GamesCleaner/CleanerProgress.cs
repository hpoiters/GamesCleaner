using System;

namespace GamesCleaner;

internal sealed class CleanerProgress
{
	public required string CurrentFile { get; init; }

	public required long ProcessedBytes { get; init; }

	public required long TotalBytes { get; init; }

	public required CleanerCounters Counters { get; init; }

	public required TimeSpan Elapsed { get; init; }

	public TimeSpan? Eta { get; init; }

	public double Percent
	{
		get
		{
			if (TotalBytes > 0)
			{
				return Math.Clamp((double)ProcessedBytes * 100.0 / (double)TotalBytes, 0.0, 100.0);
			}
			return 0.0;
		}
	}
}
