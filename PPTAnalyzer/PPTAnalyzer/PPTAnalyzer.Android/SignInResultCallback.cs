using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common.Apis;
using Java.Lang;
using PPTAnalyzer.Services;
using Xamarin.Forms;

namespace PPTAnalyzer.Droid
{
    public class SignInResultCallback : Object, IResultCallback
    {
        public MainActivity Activity { get; set; }

        public void OnResult(Object result)
        {
            var googleSignInResult = result as GoogleSignInResult;
            var signInAcount = googleSignInResult?.SignInAccount;
            if (signInAcount != null)
                DependencyService.Get<IGoogleSignInService>().InvokeSignedIn(googleSignInResult?.SignInAccount?.Email);
        }
    }
}