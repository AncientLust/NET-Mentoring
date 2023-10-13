using UtilsLibrary;

namespace Maui_App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            HandleNameSubmission();
        }

        private void OnEntryCompleted(object sender, EventArgs e)
        {
            HandleNameSubmission();
        }

        private async void HandleNameSubmission()
        {
            var userName = NameEntry.Text;
            if (Utils.ValidateName(userName))
            {
                await DisplayAlert("Greeting", Utils.GetGreeting(userName), "OK");
            }
            else
            {
                await DisplayAlert("Wrong name", Utils.ValidationFailMessage, "OK");
            }
        }
    }
}