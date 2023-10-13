namespace Advanced_C_
{
    partial class MainPage
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            treeView = new TreeView();
            listView = new ListView();
            filterTextBox = new TextBox();
            filterButton = new Button();
            SuspendLayout();
            // 
            // treeView
            // 
            treeView.Location = new Point(12, 12);
            treeView.Name = "treeView";
            treeView.Size = new Size(374, 833);
            treeView.TabIndex = 0;
            treeView.AfterSelect += TreeViewOnSelect;
            // 
            // listView
            // 
            listView.Location = new Point(407, 65);
            listView.Name = "listView";
            listView.Size = new Size(1358, 780);
            listView.TabIndex = 1;
            listView.UseCompatibleStateImageBehavior = false;
            listView.SelectedIndexChanged += ListViewSelectedIndexChanged;
            // 
            // filterTextBox
            // 
            filterTextBox.ForeColor = SystemColors.GrayText;
            filterTextBox.Location = new Point(1438, 17);
            filterTextBox.Name = "filterTextBox";
            filterTextBox.Size = new Size(220, 35);
            filterTextBox.TabIndex = 2;
            filterTextBox.Text = "Enter search term...";
            filterTextBox.Enter += SearchTextBoxEnter;
            filterTextBox.KeyDown += SearchTextBoxKeyDown;
            filterTextBox.Leave += SearchTextBoxLeave;
            // 
            // filterButton
            // 
            filterButton.Location = new Point(1664, 12);
            filterButton.Name = "filterButton";
            filterButton.Size = new Size(101, 40);
            filterButton.TabIndex = 4;
            filterButton.Text = "Filter";
            filterButton.UseVisualStyleBackColor = true;
            filterButton.Click += FilterButtonOnClick;
            // 
            // MainPage
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1777, 857);
            Controls.Add(filterButton);
            Controls.Add(filterTextBox);
            Controls.Add(listView);
            Controls.Add(treeView);
            Name = "MainPage";
            Text = "Global Commander";
            Click += FilterButtonOnClick;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView treeView;
        private ListView listView;
        private TextBox filterTextBox;
        private Button filterButton;
    }
}