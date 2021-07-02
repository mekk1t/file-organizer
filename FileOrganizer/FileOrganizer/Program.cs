using static System.Console;

namespace FileOrganizer
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Организовать Downloads или Documents? (dwn/dcm)");
            var folder = ReadLine();
            WriteLine("Группировать по дате/расширению (date/ext)");
            var groupingInput = ReadLine();
            FilesGrouping grouping = FilesGrouping.Default;

            if (groupingInput == "date")
                grouping = FilesGrouping.ByCreatedDate;
            else if (groupingInput == "ext")
                grouping = FilesGrouping.ByExtension;

            if (folder == "dwn")
                Organizer.RestructureFolder(Folder.Downloads, grouping);
            else if (folder == "dcm")
                Organizer.RestructureFolder(Folder.Documents, grouping);
        }
    }
}
