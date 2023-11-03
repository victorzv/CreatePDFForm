
using System.Net.Mime;
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
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            
            new License().SetLicense("/home/tigra/test.lic");
            
            string yamlContent = File.ReadAllText("./form_description.yaml");
            IFormFieldReader reader = new YamlFormFieldReader(yamlContent);
            var form = reader.ReadFormFields();

            Document pdf = new Aspose.Pdf.Document();

            Page page = pdf.Pages.Add();

            int yPos = 800;
            double xPos = 50;
            foreach (var item in form.Items)
            {
                if (item.NewString != null)
                {
                    yPos -= 50;
                    xPos = 50;
                }
                else if (item.Text != null)
                {
                    TextFragment textFragment = new TextFragment(item.Text);
                    textFragment.Position = new Position(xPos, yPos);
                    page.Paragraphs.Add(textFragment);
                    xPos += 150;
                }
                else if (item.FieldSelect != null)
                {
                    ComboBoxField combo = new ComboBoxField(page, new Rectangle(xPos, yPos, xPos + 150, yPos + 50));
                    
                    if (item.Options != null)
                    {
                        foreach (var el in item.Options)
                        {
                            combo.AddOption(el);    
                        }

                        xPos += 150;
                    }

                    pdf.Form.Add(combo);
                }
                else if (item.FieldText != null)
                {
                    TextBoxField textBoxField = new TextBoxField(page, new Aspose.Pdf.Rectangle(xPos, yPos, xPos + 150, yPos + 30));
                    textBoxField.PartialName = "textbox" + yPos;
                    textBoxField.Value = item.Label;
                    pdf.Form.Add(textBoxField);
                    xPos += 150;
                }
            }
            
            pdf.Save("Letter.pdf");

            break;
        }
    }
}