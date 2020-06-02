using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PPTAnalyzer.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupPage : ContentPage
    {
        public GroupPage()
        {
            InitializeComponent();
        }

        private async void lstView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && e.Item is Models.PersonModel personModel)
            {
                await Navigation.PushAsync(new PersonPage
                {
                    BindingContext = personModel
                });
            }
        }

        private void ContentPage_Appearing(object sender, System.EventArgs e)
        {
            if (Navigation.NavigationStack.FirstOrDefault(x => x is GroupPage && x != this) is GroupPage groupPage)
            {
                Navigation.RemovePage(groupPage);
            }
        }
    }
}