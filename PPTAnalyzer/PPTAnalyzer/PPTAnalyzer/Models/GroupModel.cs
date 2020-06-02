using PPTAnalyzer.Pages;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace PPTAnalyzer.Models
{
    public class GroupModel : BaseModel
    {
        private string _groupName;
        private DateTime _createdOn;

        public ICommand AddPerson { get; private set; }
        public ICommand DeleteGroup { get; private set; }
        public ICommand RenameGroup { get; private set; }

        public GroupModel()
        {
            AddPerson = new Command(execute: async () =>
               {
                   await Application.Current.MainPage.Navigation.PushAsync(new PersonPage()
                   {
                       BindingContext = new Models.PersonModel()
                       {
                           ParentId = Id
                       }
                   });
               });

            DeleteGroup = new Command(execute: async () =>
              {
                  if (await Application.Current.MainPage.DisplayAlert("Delete", "Are you sure you want to delete this group?", "Yes", "No"))
                  {
                      await Services.LocalStorageService.DeleteGroup(Id);
                      await Application.Current.MainPage.Navigation.PopToRootAsync();
                      DependencyService.Get<Services.IToastNotificationService>().DisplayToast("Group deleted");
                  }
              });
            RenameGroup = new Command(execute: async () =>
             {
                 var nGropName = await Application.Current.MainPage.DisplayPromptAsync("Rename", "Enter new name for Group", initialValue: GroupName);
                 GroupName = nGropName;
                 await Services.LocalStorageService.SaveExistingGroup(this);
             });
        }

        public Guid Id { get; set; }

        public string GroupName
        {
            get => _groupName;
            set
            {
                if (_groupName != value)
                {
                    _groupName = value;
                    OnPropertyChanged(nameof(GroupName));
                }
            }
        }

        public DateTime CreatedOn
        {
            get => _createdOn;
            set
            {
                if (_createdOn != value)
                {
                    _createdOn = value;
                    OnPropertyChanged(nameof(CreatedOn));
                }
            }
        }

        public ObservableCollection<PersonModel> People { get; set; }
    }
}
