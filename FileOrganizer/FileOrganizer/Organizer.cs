using System;
using System.IO;
using System.Linq;

namespace FileOrganizer
{
    public static class Organizer
    {
        public static void RestructureFolder(string folderPath, FilesGrouping grouping = default)
        {
            if (string.IsNullOrEmpty(folderPath))
                throw new ArgumentNullException(nameof(folderPath));

            var files = Directory.GetFiles(folderPath).Select(path => new FileInfo(path)).ToList();

            switch (grouping)
            {
                case FilesGrouping.ByExtension:
                    {
                        var result = files.GroupBy(file => file.Extension);
                        foreach (var group in result)
                        {
                            if (group.Count() > 2)
                            {
                                var groupedFilesFolder = $"Файлы с расширением {group.Key}";
                                var newDirectory = Directory.CreateDirectory(Path.Combine(folderPath, groupedFilesFolder));
                                foreach (var file in group)
                                {
                                    Directory.Move(file.FullName, Path.Combine(newDirectory.FullName, file.Name));
                                }
                            }
                        }
                        break;
                    }
                case FilesGrouping.ByCreatedDate:
                    {
                        var result = files.GroupBy(file => file.CreationTime.ToShortDateString());
                        foreach (var group in result)
                        {
                            if (group.Count() > 2)
                            {
                                var groupedFilesFolder = $"Файлы за день {group.Key}";
                                var newDirectory = Directory.CreateDirectory(Path.Combine(folderPath, groupedFilesFolder));
                                foreach (var file in group)
                                {
                                    Directory.Move(file.FullName, $"{newDirectory.FullName}/{file.Name}");
                                }
                            }
                        }
                        break;
                    }
            }
        }
    }
}
