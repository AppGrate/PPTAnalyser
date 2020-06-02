using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections;
using PPTAnalyzer.Models;

namespace PPTAnalyzer.Services
{
    public static class LocalStorageService
    {
        public static ObservableCollection<Models.GroupModel> AllData { get; set; } = new ObservableCollection<Models.GroupModel>();

        public static ObservableCollection<Models.GroupModel> GetAllData()
        {
            ObservableCollection<Models.GroupModel> allData = null;
            try
            {
                var collection = Application.Current.Properties.Values;
                allData = new ObservableCollection<Models.GroupModel>(from item in collection
                                                                      let json = item?.ToString()
                                                                      where json != null
                                                                      select Newtonsoft.Json.JsonConvert.DeserializeObject<Models.GroupModel>(json));
            }
            catch { }
            AllData = allData;
            return allData;
        }

        public static Models.GroupModel GetGroup(Guid groupId)
        {
            return AllData.FirstOrDefault(x => x.Id.Equals(groupId));
        }

        public static Guid AddNewGroup(string groupName)
        {
            var id = Guid.NewGuid();
            AllData.Add(new Models.GroupModel
            {
                Id = id,
                CreatedOn = DateTime.Now,
                GroupName = groupName,
                People = new ObservableCollection<Models.PersonModel>()
            });
            return id;
        }

        public async static Task SaveExistingGroup(GroupModel group)
        {
            var strGuid = group.Id.ToString();
            Application.Current.Properties[strGuid] = Newtonsoft.Json.JsonConvert.SerializeObject(group);
            await Application.Current.SavePropertiesAsync();
        }

        public async static Task<Guid> SavePerson(Models.PersonModel person)
        {
            var group = AllData.FirstOrDefault(x => x.Id.Equals(person.ParentId));
            if (group != null)
            {
                if (person.Id == default(Guid))
                {
                    person.Id = Guid.NewGuid();
                    group.People.Add(person);
                }
                else
                {
                    group.People.Remove(group.People.FirstOrDefault(x => x.Id == person.Id));
                    group.People.Add(person);
                }
            }
            var strGuid = group.Id.ToString();
            Application.Current.Properties[strGuid] = Newtonsoft.Json.JsonConvert.SerializeObject(group);
            await Application.Current.SavePropertiesAsync();
            return person.Id;
        }

        public async static Task DeletePerson(Models.PersonModel person)
        {
            var group = AllData.FirstOrDefault(x => x.Id.Equals(person.ParentId));
            if (group != null)
            {
                if (person.Id != default(Guid))
                {
                    group.People.Remove(group.People.FirstOrDefault(x => x.Id == person.Id));
                }
            }
            var strGuid = group.Id.ToString();
            Application.Current.Properties[strGuid] = Newtonsoft.Json.JsonConvert.SerializeObject(group);
            await Application.Current.SavePropertiesAsync();
        }

        public async static Task DeleteGroup(Guid groupId)
        {
            var group = AllData.FirstOrDefault(x => x.Id.Equals(groupId));
            if (group != null)
            {
                var strGuid = group.Id.ToString();
                AllData.Remove(group);
                Application.Current.Properties.Remove(strGuid);
                await Application.Current.SavePropertiesAsync();
            }
        }
    }
}
