using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SampleLinuxDaemon.Part2
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder BuildApplication(this IHostBuilder hostBuilder, string[] args)
        {
                return hostBuilder
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddLog4Net();
                    logging.SetMinimumLevel(LogLevel.Debug);
                })
                .ConfigureServices((hostingContext, services) =>
                {
                    services.AddHostedService<SampleHostedService>();
                });
        }
    }
}
