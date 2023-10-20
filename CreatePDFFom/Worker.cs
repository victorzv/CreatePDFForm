using WorkerService1;

namespace CreatePDFFom;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            string yamlContent = File.ReadAllText("./form_description.yaml");
            IFormFieldReader reader = new YamlFormFieldReader(yamlContent);
            var list = reader.ReadFormFields();
            foreach (var item in list)
            {
                Console.WriteLine($"type {item.Type} name {item.Name} label {item.Label}");
            }

            break;
        }
    }
}