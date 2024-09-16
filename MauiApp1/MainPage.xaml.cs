namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnGoToAboutPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }

        private async void OnGoToBooksPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BooksPage());
        }

        private void OnUpdateLabelClicked(object sender, EventArgs e)
        {
            labelText.Text = entryField.Text;
        }

        private async void OnChangeLabelClicked(object sender, EventArgs e)
        {
            labelText.TextColor = Colors.Red;

            await Task.Delay(2000);

            labelText.TextColor = Colors.Black;
        }
    }

}
