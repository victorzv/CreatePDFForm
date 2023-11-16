
using System.Net.Mime;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;
using WorkerService1;
using Color = System.Drawing.Color;

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
                if (item.newline != null)
                {
                    yPos -= 50;
                    xPos = 50;
                }
                else if (item.Text != null)
                {
                    TextFragment textFragment = new TextFragment(item.Text);
                    textFragment.Position = new Position(xPos, yPos);
                    page.Paragraphs.Add(textFragment);
                    textFragment.TextState.FontSize = 14;
                    textFragment.TextState.FontStyle = FontStyles.Bold;
                    textFragment.TextState.FontStyle = FontStyles.Italic;
                    xPos += textFragment.Rectangle.Width + 5;
                }
                else if (item.FieldSelect != null)
                {
                    ComboBoxField combo = new ComboBoxField(page, new Rectangle(xPos, yPos, xPos + 150, yPos + 30));
                    combo.Value = item.Label;
                    combo.DefaultAppearance = new DefaultAppearance("Arial", 14, Color.Black);
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
                    textBoxField.DefaultAppearance = new DefaultAppearance("Arial", 14, Color.Black);
                    pdf.Form.Add(textBoxField);
                    xPos += 150;
                }
            }
            
            pdf.Save("Letter.pdf");

            break;
        }
    }
}