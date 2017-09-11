using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace grep
{
	class Input
	{
		int line;
		public string stdin(string stdin = null)
		{
			line = 1;
			if (Console.IsInputRedirected)
			{
				using (Stream stream = Console.OpenStandardInput())
				{
					byte[] buffer = new byte[1000];
					StringBuilder builder = new StringBuilder();
					int read = -1;
					while (true)
					{
						AutoResetEvent gotInput = new AutoResetEvent(false);
						Thread inputThread = new Thread(() =>
						{
							try
							{
								read = stream.Read(buffer, 0, buffer.Length);
								gotInput.Set();
							}
							catch (ThreadAbortException)
							{
								Console.ForegroundColor = ConsoleColor.Red;
								ErrorMsg("An error occurred in line " + line);
								Console.ResetColor();
								Thread.ResetAbort();
							}
						})
						{
							IsBackground = true
						};

						inputThread.Start();

						// Timeout expired ms
						if (!gotInput.WaitOne(100))
						{
							inputThread.Abort();
							break;
						}

						// End of stream
						if (read == 0)
						{
							stdin = builder.ToString();
							break;
						}

						// Got data
						builder.Append(Console.InputEncoding.GetString(buffer, 0, read));
						line++;
					}
				}
			}
			return stdin;
		}
		public string[] readfile(string filename, ref string[] filePaths, bool subfolders)
		{
			ArrayList filepaths = new ArrayList();
			try
			{
				filePaths = Directory.GetFiles(@Directory.GetCurrentDirectory(), @filename, subfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
			}
			catch (UnauthorizedAccessException)
			{
				ErrorMsg("[ERROR] Access to files could not be granted");
				return null;
			}
			catch (Exception)
			{
				ErrorMsg("[ERROR] EXECPTION");
				return null; 
			}

			if(filePaths.Length == 0)
			{
				return null;
			}

			foreach (string file in filePaths)
			{
				using (StreamReader read = File.OpenText(file))
				{
					filepaths.Add(read.ReadToEnd());
				}
			}
			return (string[]) filepaths.ToArray(typeof(string));
		}

		private void ErrorMsg(String s, bool linebreak = false)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			if (linebreak)
				Console.WriteLine(s);
			else
				Console.Write(s);
			Console.ResetColor();
			System.Environment.Exit(2);
		}
	}
}