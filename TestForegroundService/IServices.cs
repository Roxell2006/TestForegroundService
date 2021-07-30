using System;
using System.Collections.Generic;
using System.Text;

namespace TestForegroundService
{
    public interface IServices
    {
        void StartService();
        void StopService();
    }
}
