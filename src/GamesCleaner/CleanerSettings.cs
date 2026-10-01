using System;

namespace GamesCleaner;

internal sealed class CleanerSettings
{
	public int MinimumElo { get; init; } = 2400;

	public int MinimumFullMoves { get; init; } = 20;

	public bool RejectBullet { get; init; } = true;

	public bool RejectVeryFast { get; init; } = true;

	public int VeryFastBaseSeconds { get; init; } = 120;

	public int WorkerThreads { get; init; } = Math.Max(1, Environment.ProcessorCount / 2);
}
