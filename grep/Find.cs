using System;
using System.IO;
using System.Text.RegularExpressions;

namespace grep
{
	class Find
	{
		public Find(string[] filecollection, ref string[] @filePaths, CommandLineArgs cmd)
		{
			string[] buffer;
			string trimmed;
			int position = 0, lines = 0, index = 0;
            bool found;
            ConsoleColor textcolor;

            if (Console.ForegroundColor == ConsoleColor.Green || Console.ForegroundColor == ConsoleColor.DarkGreen) // select the perfect color
				textcolor = ConsoleColor.Red;
			else
				textcolor = ConsoleColor.Green;

			foreach(string file in filecollection)
			{
                found = false;
                index++;
                lines = 0;
                if (file == null) continue;
				buffer = Regex.Split(file, "\r\n");

				if (index-1 > 0) Console.WriteLine();

				if (filePaths!=null)
				{
					string filePathInDir = filePaths[index - 1].Substring(Directory.GetCurrentDirectory().Length + 1);
                    Console.WriteLine($@"============ File: {filePathInDir}, {index} of {filePaths.Length} ============");
				}
				for (int i = 0; i < buffer.Length-1; i++)
				{
                    if (buffer[i] == null) continue;
                    lines++;
                    int count = Regex.Matches(Regex.Escape(buffer[i]), cmd.word, cmd.MATCHEXACT ? RegexOptions.None : RegexOptions.IgnoreCase).Count;

                    if (count == 0 ) continue;

                    TotaltHits += count;

                    if (cmd.SHOWLINES) Console.Write($"{lines}. "); // Lines
					trimmed = (string) buffer[i];

                    for (int x = 0; x < count; x++)
					{
						position = trimmed.IndexOf(cmd.word, cmd.MATCHEXACT ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase); 
                        if (position ==-1) continue;
						Console.Write(trimmed.Substring(0, position)); // left text
						Console.ForegroundColor = textcolor;
						Console.Write(trimmed.Substring(position, cmd.word.Length)); // target text
						Console.ResetColor();
						trimmed = trimmed.Substring(position + cmd.word.Length); // right text
                        found = true;
                    }
				   Console.WriteLine(trimmed);
				}
                if (found)
                {
                    FileHitCounter++;
                }
            }
		}

        public int TotaltHits { get; set; }
        public int FileHitCounter { get; set; }

    }
}