namespace GamesCleaner;

internal sealed class CleanerCounters
{
	public long TotalGames;

	public long StrongGames;

	public long RejectedGames;

	public long Bullet;

	public long VeryFast;

	public long TooShort;

	public long MissingElo;

	public long LowElo;

	public long InvalidResult;

	public long Malformed;

	public long WithoutEventTag;

	public void Count(RejectReason reason, bool hasEventTag)
	{
		TotalGames++;
		if (!hasEventTag)
		{
			WithoutEventTag++;
		}
		if (reason == RejectReason.None)
		{
			StrongGames++;
			return;
		}
		RejectedGames++;
		switch (reason)
		{
		case RejectReason.Bullet:
			Bullet++;
			break;
		case RejectReason.VeryFast:
			VeryFast++;
			break;
		case RejectReason.TooShort:
			TooShort++;
			break;
		case RejectReason.MissingElo:
			MissingElo++;
			break;
		case RejectReason.LowElo:
			LowElo++;
			break;
		case RejectReason.InvalidResult:
			InvalidResult++;
			break;
		case RejectReason.Malformed:
			Malformed++;
			break;
		}
	}
}
