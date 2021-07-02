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
                        var result = files.GroupBy(file => file.CreationTime.Month);
                        foreach (var group in result)
                        {
                            if (group.Count() > 2)
                            {
                                var groupedFilesFolder = $"Файлы за {group.Key.ToMonth()}";
                                var newDirectory = Directory.CreateDirectory(Path.Combine(folderPath, groupedFilesFolder));
                                foreach (var file in group)
                                {
                                    Directory.Move(file.FullName, Path.Combine(newDirectory.FullName, file.Name));
                                }
                            }
                        }
                        break;
                    }
            }
        }

        private static string ToMonth(this int month) =>
            month switch
            {
                1 => "январь",
                2 => "февраль",
                3 => "март",
                4 => "апрель",
                5 => "май",
                6 => "июнь",
                7 => "июль",
                8 => "август",
                9 => "сентябрь",
                10 => "октябрь",
                11 => "ноябрь",
                12 => "декабрь",
                _ => "REDACTED",
            };
    }
}
