namespace WinForms_App
{
    partial class MainForm
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
            SubmitNameButton = new Button();
            greetingLabel = new Label();
            UserNameTextBox = new TextBox();
            SuspendLayout();
            // 
            // SubmitNameButton
            // 
            SubmitNameButton.Location = new Point(280, 244);
            SubmitNameButton.Name = "SubmitNameButton";
            SubmitNameButton.Size = new Size(203, 40);
            SubmitNameButton.TabIndex = 0;
            SubmitNameButton.Text = "Submit";
            SubmitNameButton.UseVisualStyleBackColor = true;
            SubmitNameButton.Click += SubmitNameButton_Click;
            // 
            // greetingLabel
            // 
            greetingLabel.AutoSize = true;
            greetingLabel.Location = new Point(237, 145);
            greetingLabel.Name = "greetingLabel";
            greetingLabel.Size = new Size(296, 30);
            greetingLabel.TabIndex = 1;
            greetingLabel.Text = "Hello! Please enter your name:";
            // 
            // UserNameTextBox
            // 
            UserNameTextBox.Location = new Point(237, 190);
            UserNameTextBox.Name = "UserNameTextBox";
            UserNameTextBox.Size = new Size(291, 35);
            UserNameTextBox.TabIndex = 2;
            UserNameTextBox.Enter += UserNameTextBox_Enter;
            UserNameTextBox.Leave += UserNameTextBox_Leave;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(UserNameTextBox);
            Controls.Add(greetingLabel);
            Controls.Add(SubmitNameButton);
            Name = "MainForm";
            Text = "Application";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SubmitNameButton;
        private Label greetingLabel;
        private TextBox UserNameTextBox;
    }
}