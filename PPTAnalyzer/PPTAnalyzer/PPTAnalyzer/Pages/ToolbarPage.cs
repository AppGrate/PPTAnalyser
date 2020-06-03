using Xamarin.Forms;

namespace PPTAnalyzer.Pages
{
    public class ToolbarPage : ContentPage
    {
        public ToolbarPage()
        {
            ToolbarItems.Add(new ToolbarItem("Logout", "", async () =>
            {

                if (await DisplayAlert("Logout", "Are you sure you want to logout", "Yes", "No"))
                {
                    await DependencyService.Get<Services.IGoogleSignInService>().SignOut();
                    var app = App.Current as App;
                    app.MainPage = new MainPage();
                }

            }, ToolbarItemOrder.Default, 0));
        }
    }
}