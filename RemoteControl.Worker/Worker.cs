using Grpc.Net.Client;
using System.Management.Automation;

namespace RemoteControl.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;

        public Worker(ILogger<Worker> logger)
        {
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);

                // Stop-Computer -ComputerName localhost -force true

                using var channel = GrpcChannel.ForAddress("https://localhost:7005");
                var client = new Greeter.GreeterClient(channel);
                var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
                Console.WriteLine("Greeting: " + reply.Message);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();

                await ExecuteCommandAsync();
            }
        }

        async Task ExecuteCommandAsync()
        {
            PowerShell ps = PowerShell.Create();

            ps.AddCommand("Stop-Computer");
            ps.AddParameter("ComputerName", "localhost");
            ps.AddParameter("force", true);

            if (!System.Diagnostics.Debugger.IsAttached)
                await ps.InvokeAsync();
        }
    }
}