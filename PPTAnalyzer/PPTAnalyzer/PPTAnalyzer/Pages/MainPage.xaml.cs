using PPTAnalyzer.Services;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace PPTAnalyzer.Pages
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        IGoogleSignInService _signInService;

        public MainPage()
        {
            InitializeComponent();
            _signInService = DependencyService.Get<IGoogleSignInService>();
            _signInService.SignedIn += _signInService_SignedIn;
        }

        private async void _signInService_SignedIn(string email)
        {
            if (email == null)
            {
                await DisplayAlert("Error", "Unable to login. Please try again after sometime", "Ok");
            }
            else
            {
                var app = App.Current as App;
                var nPage = new NavigationPage(new HomePage()
                {
                    BindingContext = new Models.HomeModel
                    {
                        Data = Services.LocalStorageService.GetAllData()
                    }
                })
                {
                    BarBackgroundColor = Color.FromHex("2296F3"),
                    BarTextColor = Color.White,
                    Title = "PPT Analyser"
                };
                app.MainPage = nPage;
            }
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            _signInService.SignIn();
        }
    }
}
