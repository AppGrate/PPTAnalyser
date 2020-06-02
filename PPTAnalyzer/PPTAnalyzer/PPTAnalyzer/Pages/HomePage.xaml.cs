using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PPTAnalyzer.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void lstView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && e.Item is Models.GroupModel groupModel)
            {
                await Navigation.PushAsync(new GroupPage()
                {
                    BindingContext = groupModel
                });
            }
        }
    }
}