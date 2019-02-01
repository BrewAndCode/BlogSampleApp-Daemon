using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SampleLinuxDaemon.Part1
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder BuildApplication(this IHostBuilder hostBuilder, string[] args)
        {

                return hostBuilder
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConsole();
                })
                .ConfigureServices((hostingContext, services) =>
                {
                    services.AddHostedService<SampleHostedService>();
                });
        }
    }
}
