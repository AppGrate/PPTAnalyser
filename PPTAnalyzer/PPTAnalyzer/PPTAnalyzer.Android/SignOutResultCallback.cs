using Android.Gms.Common.Apis;
using Java.Lang;
using PPTAnalyzer.Services;
using Xamarin.Forms;

namespace PPTAnalyzer.Droid
{
    public class SignOutResultCallback : Object, IResultCallback
    {
        public MainActivity Activity { get; set; }

        public void OnResult(Object result)
        {
            DependencyService.Get<IGoogleSignInService>().InvokeSignedIn(null);
        }
    }
}