using System;

namespace grep
{
	class Program
	{

		static int Main(string[] args)
		{
			CommandLineArgs cmd = new CommandLineArgs();
			string[] data = null;
			string[] filePaths = null;

			if (args.Length == 0)
			{
				ErrorMsg("[ERROR] Unable to identify parameters. Use --help to find more parameters.");
			}
			if(!CommandLine.Parser.Default.ParseArguments(args, cmd))
			{
				// HELP MENU OR ERROR
				return 1;
			}

			Input getData = new Input();

			// CHECK IF THE FILE PARAMETER EXISTS
			if (cmd.FILE == null)
			{
				// GETTING CONSOLE INPUT
				string temp = getData.stdin();
				if(temp != null)
				data = new string[] { temp };
			}
			else
			{
				// READING FILES
				data = getData.readfile(cmd.FILE, ref filePaths, cmd.subfolders);
		
				// IF NO FILES AR FOUND. 
				if (filePaths.Length == 0)
				{
					ErrorMsg("[ERROR] No files could be found.");
				}
			}

			if (data == null)
			{
				ErrorMsg("[ERROR] Failed to retrieve data from " + (cmd.FILE==null ? "console input" : "files"));
			}
			
			if (cmd.word == null)
			{
				cmd.word = args[0];
			}
			Find result = new Find(data, ref filePaths, cmd);

			//Console.Write($"\n==== Results: {SearchInstance.TotaltHits} hits in {result.FileHitCounter} =====");
			return 0;
		}

		static private void ErrorMsg(String s, bool linebreak = false)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			if(linebreak)
				Console.WriteLine(s);
			else
				Console.Write(s);
			Console.ResetColor();
			System.Environment.Exit(2);
		}



}
}