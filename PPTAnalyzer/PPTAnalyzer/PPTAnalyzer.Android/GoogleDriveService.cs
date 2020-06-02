using Android.Gms.Auth.Api.SignIn;
using PPTAnalyzer.Services;
using System;

namespace PPTAnalyzer.Droid
{
    public class GoogleDriveService : IGoogleDriveService
    {
        static GoogleSignInClient _googleSignInClient;
        static GoogleSignInAccount _googleSignInAccount;
        static MainActivity _activity;

        public event Action SignedIn;

        public static void Initialize(MainActivity mainActivity, GoogleSignInAccount googleSignInAccount)
        {
            _activity = mainActivity;
            _googleSignInAccount = googleSignInAccount;
        }

        public void ConnectToGoogle()
        {
            try
            {
                if (_googleSignInAccount == null)
                {
                    var options = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn).Build();
                    _googleSignInClient = GoogleSignIn.GetClient(_activity, options);

                    var intent = _googleSignInClient.SignInIntent;
                    _activity.StartActivityForResult(intent, 1);
                }
                else
                {
                    AfterSignIn();
                }
            }
            catch { }
        }

        public  void SetSignInAccount()=> AfterSignIn();
        
        public void SignOutGoogle()
        {
            _googleSignInClient?.SignOut();
            _googleSignInAccount = null;
        }

        private void AfterSignIn()=> SignedIn?.Invoke();
    }
}