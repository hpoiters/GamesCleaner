using System;

namespace GamesCleaner;

internal sealed class CleanerResult
{
	public required string OutputDirectory { get; init; }

	public required string StrongPath { get; init; }

	public required string RejectedPath { get; init; }

	public required string ReportPath { get; init; }

	public required CleanerCounters Counters { get; init; }

	public required TimeSpan Elapsed { get; init; }

	public required bool Cancelled { get; init; }
}
