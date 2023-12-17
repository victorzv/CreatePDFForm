using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;
using CreatePDFFom.Service;
using CreatePDFFom.Service.Interface;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            
            new License().SetLicense("./test.lic");
            
            string yamlContent = File.ReadAllText("./W-4.yaml");
            IFormFieldReader reader = new YamlFormFieldReader(yamlContent);
            var form = reader.ReadFormFields();

            Document pdf = new Document();

            Page page = pdf.Pages.Add();

            int yPos = 800;
            double xPos = 50;
            foreach (var item in form.Items)
            {
                if (item.Break != null)
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
                    ComboBoxField combo = new ComboBoxField(page, new Rectangle(xPos, yPos, xPos + 150, yPos - 20));
                    combo.Value = item.label;
                    combo.DefaultAppearance = new DefaultAppearance("Arial", 14, Color.Black);
                    if (item.options != null)
                    {
                        foreach (var el in item.options)
                        {
                            combo.AddOption(el);    
                        }

                        xPos += 150;
                    }

                    pdf.Form.Add(combo);
                }
                else if (item.FieldText != null)
                {
                    TextBoxField textBoxField = new TextBoxField(page, new Aspose.Pdf.Rectangle(xPos, yPos, xPos + 150, yPos - 20));
                    textBoxField.PartialName = "textbox" + yPos;
                    textBoxField.Value = item.label;
                    textBoxField.DefaultAppearance = new DefaultAppearance("Arial", 14, Color.Black);
                    pdf.Form.Add(textBoxField);
                    xPos += 150;
                }
                else if (item.FieldRadio != null)
                {
                    RadioButtonField radio = new RadioButtonField(page);
                    pdf.Form.Add(radio, 1);
                    if (item.options != null)
                    {
                        foreach (var el in item.options)
                        {
                            radio.AddOption(el, new Rectangle(xPos, yPos, xPos + 20, yPos - 20));
                            var textf = new TextFragment(el);
                            textf.TextState.Font = FontRepository.FindFont("Arial");
                            textf.TextState.FontSize = 10;
                            textf.TextState.LineSpacing = 6.32f;
                            textf.Position = new Position(xPos + 25, yPos - 10);
                            page.Paragraphs.Add(textf);
                            yPos -= 20;
                            
                        }
                        xPos += 150;
                    }
                }
                else if (item.FieldCheckbox != null)
                {
                    //Console.WriteLine(item.FieldCheckbox);
                    var checkBox = new CheckboxField(page, new Rectangle(xPos, yPos, xPos + 30, yPos - 30));
                    checkBox.ExportValue = item.FieldCheckbox;
                    checkBox.Style = BoxStyle.Square;
                    checkBox.Color = Aspose.Pdf.Color.Black;
                    checkBox.Checked = true;
                    pdf.Form.Add(checkBox);
                    xPos += 35;
                    var textf = new TextFragment(item.FieldCheckbox);
                    textf.TextState.Font = FontRepository.FindFont("Arial");
                    textf.TextState.FontSize = 10;
                    textf.TextState.LineSpacing = 6.32f;
                    textf.Position = new Position(xPos, yPos - 10);
                    page.Paragraphs.Add(textf);
                    
                    xPos += 100;
                }
            }
            
            pdf.Save("Letter.pdf");
            
            Console.WriteLine("DONE");

            break;
        }
    }
}

