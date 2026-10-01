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
	private sealed class GameMeta
	{
		public int? WhiteElo;
		public int? BlackElo;
		public string? Result;
		public string? TimeControl;
		public bool BulletInHeaders;
		public bool HasAnyHeader;
		public int MaxMoveNumber;
		public bool SawEvent;
		public bool SawMovetext;
		public bool ForceMalformed;
		private bool _inHeaders = true;
		private int _braceDepth;
		private int _parenDepth;

		public void Reset()
		{
			WhiteElo = null;
			BlackElo = null;
			Result = null;
			TimeControl = null;
			BulletInHeaders = false;
			HasAnyHeader = false;
			MaxMoveNumber = 0;
			SawEvent = false;
			SawMovetext = false;
			ForceMalformed = false;
			_inHeaders = true;
			_braceDepth = 0;
			_parenDepth = 0;
		}

		public void ObserveLine(string line)
		{
			string text = line;
			if (text.StartsWith("ï»¿", StringComparison.Ordinal))
			{
				text = text.Substring(3);
			}
			if (_inHeaders)
			{
				if (string.IsNullOrWhiteSpace(text))
				{
					if (HasAnyHeader) _inHeaders = false;
					return;
				}
				if (text.StartsWith("[", StringComparison.Ordinal))
				{
					HasAnyHeader = true;
					if (text.StartsWith("[Event ", StringComparison.Ordinal)) SawEvent = true;
					if ((TryReadTag(text, "Event", out string value) || TryReadTag(text, "Site", out value) || TryReadTag(text, "Speed", out value) || TryReadTag(text, "TimeClass", out value)) && ContainsBulletClass(value)) BulletInHeaders = true;
					string value3; string value4; string value5;
					if (TryReadTag(text, "WhiteElo", out string value2)) WhiteElo = ParseElo(value2);
					else if (TryReadTag(text, "BlackElo", out value3)) BlackElo = ParseElo(value3);
					else if (TryReadTag(text, "Result", out value4)) Result = value4;
					else if (TryReadTag(text, "TimeControl", out value5)) TimeControl = value5;
					return;
				}
				_inHeaders = false;
			}
			if (text.Length > 0)
			{
				SawMovetext = true;
				ScanMoveNumbers(text);
			}
		}

		private void ScanMoveNumbers(string line)
		{
			bool flag = false;
			int num = 0;
			while (num < line.Length)
			{
				char c = line[num];
				if (flag) break;
				if (_braceDepth > 0)
				{
					if (c == '}') _braceDepth--;
					num++;
					continue;
				}
				switch (c)
				{
				case '{': _braceDepth++; num++; continue;
				case ';': flag = true; return;
				case '(': _parenDepth++; num++; continue;
				case ')': if (_parenDepth > 0) _parenDepth--; num++; continue;
				}
				if (_parenDepth == 0 && char.IsDigit(c) && (num == 0 || !char.IsLetterOrDigit(line[num - 1])))
				{
					int num2 = num;
					int num3 = 0;
					while (num2 < line.Length && char.IsDigit(line[num2]))
					{
						num3 = num3 * 10 + (line[num2] - 48);
						num2++;
						if (num3 > 10000) break;
					}
					if (num2 < line.Length && line[num2] == '.' && num3 > MaxMoveNumber) MaxMoveNumber = num3;
					num = Math.Max(num2, num + 1);
				}
				else num++;
			}
		}

		private static int? ParseElo(string value)
		{
			if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result) && result > 0 && result < 10000) return result;
			return null;
		}

		private static bool TryReadTag(string line, string tag, out string value)
		{
			value = string.Empty;
			if (!line.StartsWith("[" + tag + " ", StringComparison.Ordinal)) return false;
			int num = line.IndexOf('"');
			if (num < 0) return false;
			int num2 = line.LastIndexOf('"');
			if (num2 <= num) return false;
			value = line.Substring(num + 1, num2 - num - 1);
			return true;
		}
	}
}
