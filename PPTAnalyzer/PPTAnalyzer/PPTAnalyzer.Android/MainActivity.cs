using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using PPTAnalyzer.Services;
using Android.Content;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Auth.Api;
using Android.Gms.Common.Apis;
using System.Threading.Tasks;
using Android.Gms.Common;

namespace PPTAnalyzer.Droid
{
    [Activity(Label = "PPTAnalyzer", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, GoogleApiClient.IOnConnectionFailedListener
    {
        const int RC_SIGN_IN = 9001;
        GoogleApiClient mGoogleApiClient;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            DependencyService.Register<IToastNotificationService, ToastNotificationService>();

            DependencyService.Register<IGoogleSignInService, GoogleSignInService>();
            GoogleSignInService.Initialize(this);
            GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn).RequestEmail().Build();

            mGoogleApiClient = new GoogleApiClient.Builder(this)
                    .EnableAutoManage(this, this)
                    .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                    .Build();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }


        protected override void OnStart()
        {
            base.OnStart();
            var opr = Auth.GoogleSignInApi.SilentSignIn(mGoogleApiClient);
            if (opr.IsDone)
            {
                var result = opr.Get() as GoogleSignInResult;
                DependencyService.Get<IGoogleSignInService>().InvokeSignedIn(result?.SignInAccount?.Email);
            }
            else
            {
                opr.SetResultCallback(new SignInResultCallback { Activity = this });
            }
        }

        public void InitiateSignIn()
        {
            var signInIntent = Auth.GoogleSignInApi.GetSignInIntent(mGoogleApiClient);
            StartActivityForResult(signInIntent, RC_SIGN_IN);
        }

        public async Task InitiateSignOut()
        {
            try
            {
                await Auth.GoogleSignInApi.SignOut(mGoogleApiClient);
                await Auth.GoogleSignInApi.RevokeAccess(mGoogleApiClient);
            }
            catch (Exception ex)
            {
                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Error");
                alert.SetMessage(ex.Message);
                alert.Show();
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == RC_SIGN_IN)
            {
                var service = DependencyService.Get<IGoogleSignInService>();
                try
                {
                    var account = Auth.GoogleSignInApi.GetSignInResultFromIntent(data)?.SignInAccount;
                    if (account != null)
                        service.InvokeSignedIn(account.Email);
                }
                catch { }
            }
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Unhandled");
            alert.SetMessage(result.ErrorMessage);
            alert.Show();
        }
    }
}