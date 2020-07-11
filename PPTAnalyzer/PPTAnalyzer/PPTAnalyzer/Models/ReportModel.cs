using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PPTAnalyzer.Models
{
    public class ReportModel : BaseModel
    {
        private string _csv;
        private bool _showExportBox = false;
        private GroupModel _defaultSelected;
        private string _displayHTML;

        public ICommand Export { get; set; }
        public ICommand CloseExportBox { get; set; }
        public ICommand CopyToClipboard { get; set; }


        public ObservableCollection<GroupModel> Groups { get; set; }

        public string DisplayHTML
        {
            get => _displayHTML;
            set
            {
                _displayHTML = value;
            }
        }

        public GroupModel DefaultSelected
        {
            get => _defaultSelected ?? Groups?.FirstOrDefault();
            set
            {
                _defaultSelected = value;
                var first = "<!DOCTYPEhtml><html><head><style>table{font-family:arial,sans-serif;border-collapse:collapse;width:100%;}td,th{border:1pxsolid#dddddd;text-align:center;padding:8px;}tr:nth-child(even){background-color:#dddddd;}</style></head><body><table><tr><th>Name</th><th>Run</th><th>Chinup</th><th>Situps</th><th>Sprint(100m)</th><th>Shuttle</th></tr>";
                var middleArr = _defaultSelected.People.Select(x => $"<tr><td>{x.Name}</td><td>{x.Run ?? "-"}</td><td>{x.ChinUp ?? "-"}</td><td>{x.SitUps ?? "-"}</td><td>{x.Sprint100m ?? "-"}</td><td>{x.Shuttle ?? "-"}</td></tr>");
                var last = "</table></body></html>";
                DisplayHTML = first + string.Join("", middleArr) + last;
                OnPropertyChanged(nameof(DefaultSelected));
                OnPropertyChanged(nameof(DisplayHTML));
            }
        }

        public string CSV
        {
            get => _csv;
            set
            {
                _csv = value;
                OnPropertyChanged(nameof(CSV));
            }
        }

        public bool ShowExportBox
        {
            get => _showExportBox;
            set
            {
                _showExportBox = value;
                OnPropertyChanged(nameof(ShowExportBox));
            }
        }

        public ReportModel()
        {
            Export = new Command<GroupModel>(execute: (groupModel) =>
            {
                CSV = "\"Name\",\"Run\",\"Chinup\",\"Situp\",\"Sprint(100m)\",\"Shuttle\""
                + Environment.NewLine
                + string.Join(Environment.NewLine, groupModel.People.Select(x => $"\"{x.Name}\",{x.Run},{x.ChinUp},{x.SitUps},{x.Sprint100m},{x.Shuttle}"));
                ShowExportBox = true;
            });

            CloseExportBox = new Command(execute: () =>
               {
                   ShowExportBox = false;
               });

            CopyToClipboard = new Command<string>(execute: async text =>
              {
                  await Clipboard.SetTextAsync(text);
                  DependencyService.Get<Services.IToastNotificationService>().DisplayToast("Copied");
              });
        }
    }
}
