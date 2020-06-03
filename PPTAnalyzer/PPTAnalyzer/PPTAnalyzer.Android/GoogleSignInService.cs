using PPTAnalyzer.Services;
using System;
using System.Threading.Tasks;

namespace PPTAnalyzer.Droid
{
    public class GoogleSignInService : IGoogleSignInService
    {
        static MainActivity _mainActivity;

        public event Action<string> SignedIn;

        public static void Initialize(MainActivity mainActivity)
        {
            _mainActivity = mainActivity;
        }

        public void SignIn()
        {
            _mainActivity.InitiateSignIn();
        }

        public async Task SignOut()
        {
            await _mainActivity.InitiateSignOut();
        }

        public void InvokeSignedIn(string email)
        {
            SignedIn?.Invoke(email);
        }
    }
}