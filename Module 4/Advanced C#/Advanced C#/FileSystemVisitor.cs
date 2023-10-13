using System.IO;

namespace Advanced_C_;

public class FileSystemVisitor
{
    public string CurrentDirectory { set; get; }
    public Func<string, bool>? Filter { set; get; }
    private IDirectoryProvider directoryProvider;

    public event Action? OnTraverseStart;
    public event Action? OnTraverseEnd;
    public event Action<FileSystemEventArgs>? OnDirectoryFound;
    public event Action<FileSystemEventArgs>? OnFileFound;
    public event Action<FileSystemEventArgs>? OnFilteredDirectoryFound;
    public event Action<FileSystemEventArgs>? OnFilteredFileFound;

    public FileSystemVisitor(string currentDirectory, Func<string, bool>? filter = null, IDirectoryProvider? directoryProvider = null)
    {
        this.directoryProvider = directoryProvider ?? new DirectoryProvider();
        CurrentDirectory = currentDirectory;
        Filter = filter;
    }

    public IEnumerable<string> Traverse()
    {
        OnTraverseStart?.Invoke();

        foreach (var directory in TraverseDirectories())
        {
            yield return directory;
        }

        foreach (var file in TraverseFiles())
        {
            yield return file;
        }

        OnTraverseEnd?.Invoke();
    }

    private IEnumerable<string> TraverseDirectories()
    {
        var directories = directoryProvider.EnumerateDirectories(CurrentDirectory);
        foreach (var directory in directories)
        {
            var args = new FileSystemEventArgs(directory);
            if (Filter is null || Filter(Path.GetFileName(directory)))
            {
                (Filter is null ? OnDirectoryFound : OnFilteredDirectoryFound)?.Invoke(args);
                {
                    if (!args.ExcludeFromResults)
                    {
                        yield return directory;
                    }   
                }
            }

            if (args.CancelSearch)
            {
                yield break;
            }
        }
    }

    private IEnumerable<string> TraverseFiles()
    {
        var files = directoryProvider.EnumerateFiles(CurrentDirectory);
        foreach (var file in files)
        {
            var args = new FileSystemEventArgs(file);
            if (Filter is null || Filter(Path.GetFileName(file)))
            {
                (Filter is null ? OnFileFound : OnFilteredFileFound)?.Invoke(args);
                {
                    yield return file;
                }
            }

            if (args.CancelSearch)
            {
                yield break;
            }
        }
    }
}
