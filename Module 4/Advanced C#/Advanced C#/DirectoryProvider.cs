namespace Advanced_C_;

// This class is necessary only to mock Directory.EnumerateDirectories and Directory.EnumerateFiles for test cases

public class DirectoryProvider : IDirectoryProvider
{
    public IEnumerable<string> EnumerateDirectories(string path)
    {
        return Directory.EnumerateDirectories(path);
    }

    public IEnumerable<string> EnumerateFiles(string path)
    {
        return Directory.EnumerateFiles(path);
    }
}
