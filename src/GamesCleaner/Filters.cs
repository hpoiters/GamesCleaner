using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamesCleaner;

internal static partial class CleanerEngine
{
	private static RejectReason Classify(GameMeta meta, bool wholeFileBullet, CleanerSettings settings)
	{
		if (meta.ForceMalformed || !meta.HasAnyHeader || !meta.SawMovetext)
		{
			return RejectReason.Malformed;
		}
		if (settings.RejectBullet && (wholeFileBullet || meta.BulletInHeaders))
		{
			return RejectReason.Bullet;
		}
		if (!IsValidResult(meta.Result))
		{
			return RejectReason.InvalidResult;
		}
		if (!meta.WhiteElo.HasValue || !meta.BlackElo.HasValue)
		{
			return RejectReason.MissingElo;
		}
		if (meta.WhiteElo.Value < settings.MinimumElo || meta.BlackElo.Value < settings.MinimumElo)
		{
			return RejectReason.LowElo;
		}
		if (settings.RejectVeryFast && IsVeryFastTimeControl(meta.TimeControl, settings.VeryFastBaseSeconds))
		{
			return RejectReason.VeryFast;
		}
		if (meta.MaxMoveNumber < settings.MinimumFullMoves)
		{
			return RejectReason.TooShort;
		}
		return RejectReason.None;
	}

	private static bool IsValidResult(string? result)
	{
		switch (result)
		{
		case "1-0":
		case "0-1":
		case "1/2-1/2":
			return true;
		default:
			return false;
		}
	}

	private static bool IsVeryFastTimeControl(string? value, int maxBaseSeconds)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return false;
		}
		string text = value.Trim();
		if ((text == "-" || text == "?") ? true : false)
		{
			return false;
		}
		if (text.Contains('/') || text.Contains(':'))
		{
			return false;
		}
		int num = text.IndexOf('+');
		if (!int.TryParse((num >= 0) ? text.Substring(0, num) : text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result))
		{
			return false;
		}
		if (result >= 0)
		{
			return result <= maxBaseSeconds;
		}
		return false;
	}

	private static bool ContainsBulletClass(string value)
	{
		if (!ContainsWholeWord(value, "bullet"))
		{
			return ContainsWholeWord(value, "ultrabullet");
		}
		return true;
		static bool ContainsWholeWord(string text, string word)
		{
			int num = 0;
			while (num < text.Length)
			{
				int num2 = text.IndexOf(word, num, StringComparison.OrdinalIgnoreCase);
				if (num2 < 0)
				{
					return false;
				}
				int num3 = num2 + word.Length;
				bool num4 = num2 == 0 || !char.IsLetterOrDigit(text[num2 - 1]);
				bool flag = num3 == text.Length || !char.IsLetterOrDigit(text[num3]);
				if (num4 && flag)
				{
					return true;
				}
				num = num2 + 1;
			}
			return false;
		}
	}
}
