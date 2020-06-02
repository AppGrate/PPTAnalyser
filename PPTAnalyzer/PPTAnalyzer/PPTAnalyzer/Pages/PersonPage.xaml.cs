using PPTAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PPTAnalyzer.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonPage : ContentPage
    {
        public PersonPage()
        {
            InitializeComponent();
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            if (Navigation.NavigationStack.LastOrDefault() is PersonPage && Navigation.NavigationStack.Reverse().Skip(1).FirstOrDefault() is PersonPage personPage)
            {
                Navigation.RemovePage(personPage);
            }
        }
    }
}