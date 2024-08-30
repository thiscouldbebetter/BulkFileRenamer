namespace BulkFileRenamer
{
	internal partial class Program
	{
		public class BulkFileRenamer
		{
			public string DirectoryRootPath { get; set; }
			public string SearchPatternForFilesToRename { get; set; }
			public bool ShouldRecurseIntoSubdirectories { get; set; }
			public string RenamedFileFormatString { get; set; }

			public BulkFileRenamer
			(
				string directoryRootPath,
				bool shouldRecurseIntoSubdirectories,
				string searchPatternForFilesToRename,
				string renamedFileFormatString
			)
			{
				DirectoryRootPath = directoryRootPath;
				ShouldRecurseIntoSubdirectories = shouldRecurseIntoSubdirectories;
				SearchPatternForFilesToRename = searchPatternForFilesToRename;
				RenamedFileFormatString = renamedFileFormatString;
			}

			public void RenameFiles()
			{
				RenameFilesInDirectoryAtPath(DirectoryRootPath);
			}

			private void RenameFilesInDirectoryAtPath(string directoryPath)
			{
				if (ShouldRecurseIntoSubdirectories)
				{
					var subdirectoryPaths =
						Directory.GetDirectories(directoryPath);

					foreach (var subdirectoryPath in subdirectoryPaths)
					{
						RenameFilesInDirectoryAtPath(subdirectoryPath);
					}
				}

				var filesToRename =
					Directory.GetFiles(directoryPath, SearchPatternForFilesToRename);
				var fileCount = filesToRename.Length;

				var digitsInFileIndex = (int)Math.Ceiling(Math.Log10(fileCount) );

				for (var i = 0; i < filesToRename.Length; i++)
				{
					var fileToRenameFullName = filesToRename[i];

					var fileToRenameNameMinusExtension =
						Path.GetFileNameWithoutExtension(fileToRenameFullName);

					var fileToRenameExtension = Path.GetExtension(fileToRenameFullName);

					var fileIndexPadded =
						("" + i).PadLeft(digitsInFileIndex, '0');

					var fileRenamedName =
						RenamedFileFormatString
						.Replace("{stem}", fileToRenameNameMinusExtension)
						.Replace("{ext}", fileToRenameExtension)
						.Replace("{index}", fileIndexPadded);

					File.Move(fileToRenameFullName, fileRenamedName);
				}
			}
		}
	}
}
