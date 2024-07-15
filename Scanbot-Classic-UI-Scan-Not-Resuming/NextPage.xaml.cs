namespace Scanbot_Classic_UI_Scan_Not_Resuming;

public partial class NextPage : ContentPage
{
	public NextPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }
}