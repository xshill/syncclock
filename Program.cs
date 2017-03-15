using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;

namespace SyncClock
{
    class Program
    {
        static bool IsW32Time(ServiceController service)
        {
            return service.ServiceName.Equals("W32Time", StringComparison.OrdinalIgnoreCase);
        }

        static void Main(string[] args)
        {
            var services = ServiceController.GetServices();
            var w32time = services.FirstOrDefault(IsW32Time);
            
            if(w32time != null)
            {
                try
                {
                    var status = w32time.Status;
                    if(status != ServiceControllerStatus.Running &&
                       status != ServiceControllerStatus.StartPending &&
                       status != ServiceControllerStatus.ContinuePending)
                    {
                        w32time.Start();
                    }

                    w32time.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 10));

                    if(w32time.Status == ServiceControllerStatus.Running)
                    {
                        var psi = new ProcessStartInfo("w32tm", "/resync");
                        psi.UseShellExecute = false;
                        psi.CreateNoWindow = true;
                        psi.RedirectStandardError = true;
                        psi.RedirectStandardInput = true;
                        psi.RedirectStandardOutput = true;
                        Process.Start(psi);
                    }
                }
                catch(Exception)
                {
                }
            }
        }
    }
}
