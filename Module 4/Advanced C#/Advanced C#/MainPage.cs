using System.Diagnostics;

namespace Advanced_C_;

public partial class MainPage : Form
{
    private FileSystemVisitor visitor;
    private int listViewColumnWidth = -2;
    private string currentDirectory;
    private TreeNode? currentNode;
    private readonly string filterTextBoxText = "Enter search term...";

    public MainPage()
    {
        InitializeComponent();
        currentDirectory = Directory.GetLogicalDrives().First().ToString();
        visitor = new FileSystemVisitor(currentDirectory);
        FillTreeView();
        InitListView();
        FillListView();
    }

    private void InitListView()
    {
        listView.View = View.Details;
        listView.Columns.Add("Name", listViewColumnWidth, HorizontalAlignment.Left);
        listView.Columns.Add("Path", listViewColumnWidth, HorizontalAlignment.Left);
    }

    private void FillListView()
    {
        listView.Items.Clear();
        foreach (var path in visitor.Traverse())
        {
            var listItem = new ListViewItem(Path.GetFileName(path));
            listItem.SubItems.Add(path);
            listView.Items.Add(listItem);
        }
    }

    private void FillTreeView()
    {
        foreach (string drive in Directory.GetLogicalDrives())
        {
            TreeNode driveNode = new TreeNode(drive);
            treeView.Nodes.Add(driveNode);
            PopulateTreeView(drive, driveNode);
        }
    }

    private void PopulateTreeView(string directory, TreeNode parentNode)
    {
        try
        {
            var directoryInfo = new DirectoryInfo(directory);

            foreach (DirectoryInfo dirInfo in directoryInfo.GetDirectories().Where(IsNotSystemDirectory))
            {
                TreeNode newDirectoryNode = parentNode.Nodes.Add(dirInfo.Name);
                PopulateTreeView(dirInfo.FullName, newDirectoryNode);
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            Debug.WriteLine($"Don't have access to directory {directory}. Error: {ex.Message}");
            Debug.WriteLine($"Tree view population contunued to the next folder.");
        }
        catch (IOException ex)
        {
            Debug.WriteLine($"Input-output error with directory {directory}. Error: {ex.Message}");
            Debug.WriteLine($"Tree view population contunued to the next folder.");
        }
    }

    private bool IsNotSystemDirectory(DirectoryInfo dirInfo)
    {
        return (dirInfo.Attributes & FileAttributes.System) != FileAttributes.System;
    }

    private void TreeViewOnSelect(object sender, TreeViewEventArgs e)
    {
        if (e.Node is not null)
        {
            currentDirectory = e.Node.FullPath;
            currentNode = treeView.SelectedNode;
            visitor.CurrentDirectory = currentDirectory;
            FillListView();
        }
    }

    private void FilterButtonOnClick(object sender, EventArgs e)
    {
        string filterString = filterTextBox.Text;

        Func<string, bool> filter = path => path.Contains(filterString);
        visitor.CurrentDirectory = currentDirectory;
        visitor.Filter = filter;
        FillListView();
        visitor.Filter = null;
    }

    private void SearchTextBoxEnter(object sender, EventArgs e)
    {
        if (filterTextBox.Text == filterTextBoxText)
        {
            filterTextBox.Text = "";
            filterTextBox.ForeColor = Color.Black;
        }
    }

    private void SearchTextBoxLeave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(filterTextBox.Text))
        {
            filterTextBox.Text = filterTextBoxText;
            filterTextBox.ForeColor = Color.Gray;
        }
    }

    private void SearchTextBoxKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            filterButton.PerformClick();
        }
    }

    private void ListViewSelectedIndexChanged(object sender, EventArgs e)
    {
        if (listView.SelectedItems.Count == 0)
        {
            return;
        }

        var selectedPath = listView.SelectedItems[0].SubItems[1].Text;
        if (Directory.Exists(selectedPath) && currentNode is not null)
        {
            SelectTreeNodeByPath(currentNode.Nodes, selectedPath);
        }
    }

    private void SelectTreeNodeByPath(TreeNodeCollection nodes, string path)
    {
        foreach (TreeNode node in nodes)
        {
            if (Path.GetFullPath(node.FullPath) == Path.GetFullPath(path))
            {
                treeView.SelectedNode = node;
                node.Expand();
                break;
            }
        }
    }
}