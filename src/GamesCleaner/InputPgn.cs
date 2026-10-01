using System.IO;

namespace GamesCleaner;

internal sealed record InputPgn(string Path, long SizeBytes)
{
	public string DisplaySize
	{
		get
		{
			if (SizeBytes >= 1073741824)
			{
				return $"{(double)SizeBytes / 1073741824.0:0.00} GB";
			}
			return $"{(double)SizeBytes / 1048576.0:0.0} MB";
		}
	}

	public string DisplayPath(string root)
	{
		return System.IO.Path.GetRelativePath(root, Path);
	}
}
