using CommandLine;
using CommandLine.Text;
using System;

namespace grep
{
	public class CommandLineArgs
	{  
		[Option('L', "SHOWLINES", HelpText = "Displays lines")]
		public bool SHOWLINES { get; set; }

		[Option('C',"CASE", HelpText = "Case-sentivensive")]
		public bool MATCHEXACT { get; set; }

		[Option('W',"WORD", HelpText = "Optional to use this, otherwise the first parameter is the search word.")]
		public String word { get; set; }

		[Option('S', "SUBFOLDERS", HelpText = "Look for files in all subfolders.")]
		public bool subfolders { get; set; }

		[Option('F', "FILE", HelpText = "This is optional if you want to search in a file.")]
		public String FILE { get; set; }

		[HelpOption("HELP",HelpText = "Display this help screen.")]
		public string GetUsage()
		{
			var usage = HelpText.AutoBuild(this);
			usage.AddPreOptionsLine("Developed in C# by IT-KiLLER (github.com/IT-KiLLER)");
			//usage.Copyright = "copyright text here";
			//usage.Heading = "Grep 1.0";
			return usage.ToString();
		}
	}
}
