using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PPTAnalyzer.Services;
using Xamarin.Essentials;

namespace PPTAnalyzer.Droid
{
    public class ToastNotificationService : IToastNotificationService
    {
        public void DisplayToast(string message)
        {
            var toast = Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long);
            //var view = toast.View;
            //var toastMsg = (TextView)view.FindViewById(Resource.Id.message);
            //toastMsg.SetTextColor(Android.Graphics.Color.White);
            //view.SetBackgroundColor(color.ToPlatformColor());
            toast.Show();
        }
    }
}