using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PPTAnalyzer.Services
{
    public interface IGoogleSignInService
    {
        event Action<string> SignedIn;
        void SignIn();
        Task SignOut();
        void InvokeSignedIn(string email);
    }
}
