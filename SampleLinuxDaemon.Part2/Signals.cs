using System;
using System.Threading;

namespace SampleLinuxDaemon.Part2
{
    internal static class Signals
    {
        internal static void AttachCtrlCSigtermShutdown(
            CancellationTokenSource cts, 
            ManualResetEventSlim resetEvent, 
            string shutdownMessage)
        {
            void ShutDown()
            {
                if (!cts.IsCancellationRequested)
                {
                    if (!string.IsNullOrWhiteSpace(shutdownMessage))
                        Program.Log.Warn(shutdownMessage);

                    try
                    {
                        cts.Cancel();
                    }
                    catch (ObjectDisposedException) { }
                }
                resetEvent.Wait();
            }

            AppDomain.CurrentDomain.ProcessExit += delegate { ShutDown(); };
          
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                ShutDown();
                eventArgs.Cancel = true;
            };
        }
    }
}