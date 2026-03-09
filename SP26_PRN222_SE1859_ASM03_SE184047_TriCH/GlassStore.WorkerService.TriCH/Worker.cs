using GlassStore.Services.TriCH;
using System.Text.Json;
using System.Text.Json.Schema;
using System.Text.Json.Serialization;

namespace GlassStore.WorkerService.TriCH
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //if (_logger.IsEnabled(LogLevel.Information))
                //{
                //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //}

                await Task.Delay(5000, stoppingToken);
            }
        }

        protected async Task WriteFile()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var productService = scope.ServiceProvider.GetRequiredService<IProductTriCHService>();
                var items = await productService.GetAllProductAsync();
                var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
                var content = JsonSerializer.Serialize(items, opt);

                var path = @"D:Datalog.SE184047.txt";

                using (var file = File.Open(path, FileMode.Append, FileAccess.Write))
                {
                    using (var writer = new StreamWriter(file))
                    {
                        await writer.WriteLineAsync(content);
                        await writer.FlushAsync();
                    }
                }
            }
        }
    }
}
