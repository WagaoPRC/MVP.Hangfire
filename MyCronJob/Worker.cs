using System.Text;

namespace MyCronJob;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //while (!stoppingToken.IsCancellationRequested)
        //{
        //    if (_logger.IsEnabled(LogLevel.Information))
        //    {
        //        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        //        await Console.Out.WriteLineAsync($"Worker running at: {DateTimeOffset.Now}");

        //    }
        //    await Task.Delay(1000, stoppingToken);
        //}

        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        await Console.Out.WriteLineAsync($"=====> Worker running at: {DateTimeOffset.Now}");
        await Task.Delay(1000, stoppingToken);
    }

    public async Task MyJobFireAndForget()
    {
        await Task.Run(() =>
        {
            Console.WriteLine(value: "My First JobFireAndForget using Hangfire");
        });
    }
    public async Task MyExceptionJob()
    {
        await Task.Run(() =>
        {
            throw new NotImplementedException("Omg, this jog has a exception!!!");
        });
    }
    public async Task MyRecurringJob()
    {
        await Task.Run(() =>
        {
            throw new NotImplementedException("This is a recurring job!!!");
        });
    }
    public async Task MyDelayedjob() => await Task.Run(() =>
    {
        Console.WriteLine("This is a Delayed Job");
    });

    public async Task MyFatherJob() => await Task.Run(() =>
    {
        Console.WriteLine("This is a Father Job");
    });

    public async Task MySunJob() => await Task.Run(() =>
    {
        Console.WriteLine("This is a Sun Job");
    });
}
