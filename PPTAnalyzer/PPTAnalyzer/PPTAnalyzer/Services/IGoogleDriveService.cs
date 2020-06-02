using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTAnalyzer.Services
{
    public interface IGoogleDriveService
    {
        event Action SignedIn;
        void ConnectToGoogle();
        void SignOutGoogle();
        void SetSignInAccount();
    }
}