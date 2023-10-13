namespace Advanced_C_;

// This interface is necessary only to mock Directory.EnumerateDirectories and Directory.EnumerateFiles for test cases

public interface IDirectoryProvider
{
    IEnumerable<string> EnumerateDirectories(string path);
    IEnumerable<string> EnumerateFiles(string path);
}
