using UtilsLibrary;

namespace WinForms_App
{
    public partial class MainForm : Form
    {
        private const string textBoxPlaceholderText = "Your name...";

        public MainForm()
        {
            InitializeComponent();
            InitNameTextBox();
        }

        private void InitNameTextBox()
        {
            UserNameTextBox.Text = textBoxPlaceholderText;
            UserNameTextBox.ForeColor = Color.Gray;
        }

        private void SubmitNameButton_Click(object sender, EventArgs e)
        {
            string userName = UserNameTextBox.Text;

            if (Utils.ValidateName(userName))
            {
                MessageBox.Show(Utils.GetGreeting(userName), "Greeting", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(Utils.ValidationFailMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UserNameTextBox_Enter(object sender, EventArgs e)
        {
            if (UserNameTextBox.Text == textBoxPlaceholderText)
            {
                UserNameTextBox.Text = "";
                UserNameTextBox.ForeColor = Color.Black;
            }
        }

        private void UserNameTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UserNameTextBox.Text))
            {
                UserNameTextBox.Text = textBoxPlaceholderText;
                UserNameTextBox.ForeColor = Color.Gray;
            }
        }
    }
}