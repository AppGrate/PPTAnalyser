using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PPTAnalyzer.Services
{
    public interface IToastNotificationService
    {
        void DisplayToast(string message);
    }
}
