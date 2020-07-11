using Xamarin.Forms;

namespace PPTAnalyzer.Pages
{
    public class ToolbarPage : ContentPage
    {
        public ToolbarPage()
        {
            ToolbarItems.Add(new ToolbarItem("PPT Analyzer", "", () => { }, ToolbarItemOrder.Default, 0));
        }
    }
}