using PPTAnalyzer.Pages;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PPTAnalyzer.Models
{
    public class PersonModel : BaseModel
    {
        private string _name;
        private string _run;
        private string _chinUp;
        private string _sitUps;
        private string _sprint100m;
        private string _shuttle;

        public ICommand ViewAll { get; set; }
        public ICommand SavePerson { get; set; }
        public ICommand DeletePerson { get; set; }

        public PersonModel()
        {
            ViewAll = new Command(execute: async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new GroupPage
                {
                    BindingContext = Services.LocalStorageService.GetGroup(ParentId)
                });
            });

            SavePerson = new Command<string>(execute: async (name) =>
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Please enter name of the person", "Ok");
                }
                else
                {
                    await Services.LocalStorageService.SavePerson(this);
                    var groupId = ParentId;
                    await Application.Current.MainPage.Navigation.PushAsync(new PersonPage
                    {
                        BindingContext = new PersonModel
                        {
                            ParentId = ParentId
                        }
                    });
                }
            });

            DeletePerson = new Command(execute: async () =>
            {
                if (await Application.Current.MainPage.DisplayAlert("Delete", $"Are you sure you want to delete {Name}?", "Yes", "No"))
                {
                    await Services.LocalStorageService.DeletePerson(this);
                    DependencyService.Get<Services.IToastNotificationService>().DisplayToast("Person deleted");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            });
        }

        public Guid Id { get; set; }
        public Guid ParentId { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Run
        {
            get => _run;
            set
            {
                if (_run != value)
                {
                    _run = value;
                    OnPropertyChanged(nameof(Run));
                }
            }
        }

        public string ChinUp
        {
            get => _chinUp;
            set
            {
                if (_chinUp != value)
                {
                    _chinUp = value;
                    OnPropertyChanged(nameof(ChinUp));
                }
            }
        }

        public string SitUps
        {
            get => _sitUps;
            set
            {
                if (_sitUps != value)
                {
                    _sitUps = value;
                    OnPropertyChanged(nameof(SitUps));
                }
            }
        }

        public string Sprint100m
        {
            get => _sprint100m;
            set
            {
                if (_sprint100m != value)
                {
                    _sprint100m = value;
                    OnPropertyChanged(nameof(Sprint100m));
                }
            }
        }

        public string Shuttle
        {
            get => _shuttle;
            set
            {
                if (_shuttle != value)
                {
                    _shuttle = value;
                    OnPropertyChanged(nameof(Shuttle));
                }
            }
        }
    }
}
