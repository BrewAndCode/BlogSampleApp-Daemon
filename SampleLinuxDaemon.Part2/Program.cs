using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SampleLinuxDaemon.Part2
{
    internal static class Program
    {
        internal static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(Program));

        internal static async Task Main(string[] args)
        {
            var cancellationToken = default(CancellationToken);
            var hostedService = new HostBuilder().BuildApplication(args);

            var done = new ManualResetEventSlim(false);
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                Signals.AttachCtrlCSigtermShutdown(cts, done, shutdownMessage: "SIGTERM received...");
                try
                {
                    await hostedService.RunConsoleAsync(cts.Token);
                }
                catch (TaskCanceledException)
                {
                    //This is  the expected Exception when a task is cancelled.  Do any cleanup here
                }
                catch (Exception ex)
                {
                    Log.Error($"Error captured from service process", ex);
                }
                finally
                {
                    done.Set();
                }
            }
        }
    }
}
