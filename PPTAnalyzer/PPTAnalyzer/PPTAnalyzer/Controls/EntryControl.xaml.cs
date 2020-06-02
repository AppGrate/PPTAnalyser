using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PPTAnalyzer.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryControl : ContentView
    {
        public static readonly BindableProperty TextProperty =
             BindableProperty.Create("Text", typeof(string), typeof(EntryControl), "");

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty SelectedItemProperty =
             BindableProperty.Create("SelectedItem", typeof(string), typeof(EntryControl), "EX");

        public string SelectedItem
        {
            get { return (string)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly BindableProperty SelectedIndexProperty =
             BindableProperty.Create("SelectedIndex", typeof(int), typeof(EntryControl), 0);

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public EntryControl()
        {
            InitializeComponent();
        }
    }
}