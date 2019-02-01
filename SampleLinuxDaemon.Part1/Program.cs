using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SampleLinuxDaemon.Part1
{
    internal static class Program
    {
        internal static async Task Main(string[] args)
        {
            var hostedService = new HostBuilder().BuildApplication(args);

            await hostedService.RunConsoleAsync();
        }
    }
}
