// ReSharper disable ClassNeverInstantiated.Global

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SampleLinuxDaemon.Part2
{
    public class SampleHostedService : IHostedService 
    {
        private readonly ILogger<SampleHostedService> _logger;
        private Task _asyncTask;

        public SampleHostedService(ILogger<SampleHostedService> logger)
        {
            _logger = logger;
        }

        private async Task DoSomethingCool(CancellationToken cancellationToken)
        {
            while (cancellationToken.IsCancellationRequested == false)
            {
                await Task.Run(() =>
                {
                    _logger.LogInformation($"{DateTime.Now.ToShortTimeString()}");
                }, cancellationToken);

                await Task.Delay(2000, cancellationToken);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _asyncTask = DoSomethingCool(cancellationToken);
            return _asyncTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _asyncTask;
        }
    }
}
