using System;
using System.Linq;
using System.Windows.Forms;

namespace GamesCleaner;

internal static class Program
{
	[STAThread]
	private static int Main(string[] args)
	{
		if (args.Any((string a) => a.Equals("--selftest", StringComparison.OrdinalIgnoreCase)))
		{
			return SelfTest.Run();
		}
		ApplicationConfiguration.Initialize();
		Application.Run(new MainForm());
		return 0;
	}
}
