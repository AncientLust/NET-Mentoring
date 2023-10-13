using Moq;
using Advanced_C_;

namespace Advanced_C__Tests;

public class FileSystemVisitorTests
{
    private Mock<IDirectoryProvider> mockDirectoryProvider = new Mock<IDirectoryProvider>();

    [Fact]
    public void FilterTestWithContains()
    {
        var mockFolders = new string[] { "dir1", "dir2" };
        var mockFiles = new string[] { "file1.txt", "file2.txt" };
        var mockDirectoryProvider = SetUpMockDirectoryProvider(mockFolders, mockFiles);

        var visitor = new FileSystemVisitor("someDirectory", fileName => fileName.Contains("2"), mockDirectoryProvider);
        var result = visitor.Traverse().ToList();

        Assert.Contains("dir2", result);
        Assert.Contains("file2.txt", result);
        Assert.DoesNotContain("dir1", result);
        Assert.DoesNotContain("file1.txt", result);
    }

    [Fact]
    public void FilterTestWithStartsWith()
    {
        var mockFolders = new string[] { "dir1", "dir2" };
        var mockFiles = new string[] { "file1.txt", "file2.txt" };
        var mockDirectoryProvider = SetUpMockDirectoryProvider(mockFolders, mockFiles);

        var visitor = new FileSystemVisitor("someDirectory", fileName => fileName.StartsWith("dir"), mockDirectoryProvider);
        var result = visitor.Traverse().ToList();

        Assert.Contains("dir1", result);
        Assert.Contains("dir2", result);
        Assert.DoesNotContain("file2.txt", result);
        Assert.DoesNotContain("file1.txt", result);
    }

    [Fact]
    public void FilterTestWithEndsWith()
    {
        var mockFolders = new string[] { "dir1", "dir2" };
        var mockFiles = new string[] { "file1.txt", "file2.txt" };
        var mockDirectoryProvider = SetUpMockDirectoryProvider(mockFolders, mockFiles);

        var visitor = new FileSystemVisitor("someDirectory", fileName => fileName.EndsWith(".txt"), mockDirectoryProvider);
        var result = visitor.Traverse().ToList();

        Assert.DoesNotContain("dir1", result);
        Assert.DoesNotContain("dir2", result);
        Assert.Contains("file2.txt", result);
        Assert.Contains("file1.txt", result);
    }

    [Fact]
    public void FilterTestWithNoFiles()
    {
        var mockFolders = new string[] { "dir1", "dir2" };
        var mockFiles = new string[] { };
        var mockDirectoryProvider = SetUpMockDirectoryProvider(mockFolders, mockFiles);

        var visitor = new FileSystemVisitor("someDirectory", fileName => fileName.EndsWith(".txt"), mockDirectoryProvider);
        var result = visitor.Traverse().ToList();

        Assert.Empty(result);
    }

    [Fact]
    public void FilterTestWithNoFolders()
    {
        var mockFolders = new string[] { };
        var mockFiles = new string[] { "file1.txt", "file2.txt" };
        var mockDirectoryProvider = SetUpMockDirectoryProvider(mockFolders, mockFiles);

        var visitor = new FileSystemVisitor("someDirectory", fileName => fileName.StartsWith("dir"), mockDirectoryProvider);
        var result = visitor.Traverse().ToList();

        Assert.Empty(result);
    }

    private IDirectoryProvider SetUpMockDirectoryProvider(string[] folders, string[] files)
    {
        mockDirectoryProvider.Setup(dp => dp.EnumerateDirectories(It.IsAny<string>())).Returns(folders);
        mockDirectoryProvider.Setup(dp => dp.EnumerateFiles(It.IsAny<string>())).Returns(files);
        return mockDirectoryProvider.Object;
    }
}