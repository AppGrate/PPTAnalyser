using PPTAnalyzer.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PPTAnalyzer.Models
{
    public class HomeModel : BaseModel
    {
        public ObservableCollection<Models.GroupModel> Data { get; set; }

        public static HomeModel Current = new HomeModel();

        public ICommand CreateGroup { get; private set; }

        public HomeModel()
        {
            CreateGroup = new Command<string>(execute: async (string groupName) =>
             {
                 if (string.IsNullOrWhiteSpace(groupName))
                 {
                     await Application.Current.MainPage.DisplayAlert("Error", "Please enter a name for group", "Ok");
                 }
                 else
                 {
                     var groupId = Services.LocalStorageService.AddNewGroup(groupName);
                     DependencyService.Get<Services.IToastNotificationService>().DisplayToast("Group saved");
                     await Application.Current.MainPage.Navigation.PushAsync(new PersonPage()
                     {
                         BindingContext = new PersonModel
                         {
                             ParentId = groupId
                         }
                     });
                 }
             });
        }
    }
}
