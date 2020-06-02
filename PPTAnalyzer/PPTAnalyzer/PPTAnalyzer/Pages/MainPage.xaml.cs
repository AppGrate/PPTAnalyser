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
        IGoogleDriveService _service;

        public MainPage()
        {
            InitializeComponent();
            _service = DependencyService.Get<IGoogleDriveService>();
            _service.SignedIn += Service_SignedIn;
        }

        private void Service_SignedIn()
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
            nPage.ToolbarItems.Add(new ToolbarItem("Logout", null, Logout));
            app.MainPage = nPage;
        }

        private void Logout()
        {
            var app = App.Current as App;
            app.MainPage = new MainPage();
            _service.SignOutGoogle();
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            _service.ConnectToGoogle();
        }
    }
}
