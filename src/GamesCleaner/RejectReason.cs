namespace GamesCleaner;

internal enum RejectReason
{
	None,
	Bullet,
	VeryFast,
	TooShort,
	MissingElo,
	LowElo,
	InvalidResult,
	Malformed
}
