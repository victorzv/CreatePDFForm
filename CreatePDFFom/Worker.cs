
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;
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

            Document pdf = new Aspose.Pdf.Document();

            Page page = pdf.Pages.Add();

            int yPos = 500;
            
            foreach (var el in list)
            {
                if (el.Type == "fieldText")
                {
                    TextBoxField textBoxField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, yPos, 300, yPos+40));
                    textBoxField.PartialName = "textbox" + yPos/100;
                    textBoxField.Value = el.Name;
                    

                    Border border = new Border(textBoxField);
                    border.Width = 5;
                    border.Style = BorderStyle.Solid;
                    textBoxField.Border = border;

                    textBoxField.Color = Aspose.Pdf.Color.Black;
                    
                    pdf.Form.Add(textBoxField, 1);                    
                }
                yPos -= 100;
            }
            
            TextFragment textFragment = new TextFragment("Enter here name");
            textFragment.Position = new Position(100, yPos - 50);
            page.Paragraphs.Add(textFragment);
            
            pdf.Save("письмо.pdf");

            break;
        }
    }
}