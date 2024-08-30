using System.Diagnostics;

namespace BulkFileRenamer
{
	internal partial class Program
	{
		static void Main(string[] args)
		{
			new Program().Run();
		}

		public void Run()
		{
			Console.WriteLine("Bulk File Renamer");

			Console.WriteLine("Program begins: " + DateTime.Now.ToString("o") );

			Console.Write("Path of root-level directory containing files to rename [current dir]: ");
			var directoryRootPath = Console.ReadLine() ?? ".";

			Console.Write("Recurse into subdirectories? (Y/N) [N]: ");
			var shouldRecurseIntoSubdirectoriesAsString = Console.ReadLine() ?? "N";
			var shouldRecurseIntoSubdirectories =
				(shouldRecurseIntoSubdirectoriesAsString == "Y");

			Console.Write("Search pattern (wildcard expression) for files to rename: ");
			var searchPatternForFilesToRename = Console.ReadLine();

			Console.Write("Format string for renamed files (e.g. 'Prefix_{stem}_{index}_Suffix.{ext}'): ");
			var renamedFileFormatString = Console.ReadLine();

			var doesSpecifiedDirectoryExist = Directory.Exists(directoryRootPath);
			if (doesSpecifiedDirectoryExist == false)
			{
				Console.WriteLine
				(
					"The specified root directory does not exist: "
					+ directoryRootPath
					+ ".  Defaulting to current directory."
				);

				directoryRootPath = ".";
			}

			var bulkFileRenamer = new BulkFileRenamer
			(
				directoryRootPath,
				shouldRecurseIntoSubdirectories,
				searchPatternForFilesToRename,
				renamedFileFormatString
			);
			bulkFileRenamer.RenameFiles();

			Console.WriteLine("Program ends: " + DateTime.Now.ToString("o"));

			if (Debugger.IsAttached)
			{
				Console.Write("Press the Enter key to quit.");
				Console.ReadLine();
			}

		}
	}
}
