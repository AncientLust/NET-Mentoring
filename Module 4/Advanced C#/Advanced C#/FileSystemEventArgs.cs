namespace Advanced_C_;

public class FileSystemEventArgs
{
    public string Path { get; }
    public bool CancelSearch { get; set; }
    public bool ExcludeFromResults { get; set; }

    public FileSystemEventArgs(string path)
    {
        Path = path;
    }
}
