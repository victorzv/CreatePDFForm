using System.Text.RegularExpressions;
using WorkerService1.Forms;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace WorkerService1;

public class YamlFormFieldReader : IFormFieldReader
{
    private string yamlContent;

    public YamlFormFieldReader(string content)
    {
        yamlContent = content;
    }

    public List<FormField> ReadFormFields()
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        var formObjects = new List<FormField>();

        //var pattern = @"^- Type: [a-zA-Z]+\n(?:  .+\n)*";
        var pattern = @"^- Type: ""[a-zA-Z]+""\n(?:  .+\n)*";
        var regex = new Regex(pattern);
        
        var matches = regex.Matches(yamlContent);

        foreach (Match match in matches)
        {
            var section = match.Value;
            Console.WriteLine(section);
            var formField = deserializer.Deserialize<FormField>(section);
            if (formField.Type == "fieldText")
            {
                var textField = deserializer.Deserialize<TextField>(section);
                formObjects.Add(textField);
            }
            else if (formField.Type == "fieldButton")
            {
                var buttonField = deserializer.Deserialize<ButtonField>(section);
                formObjects.Add(buttonField);
            }
            else if (formField.Type == "fieldCheckBox")
            {
                var checkBoxField = deserializer.Deserialize<CheckBoxField>(section);
                formObjects.Add(checkBoxField);
            }
            else if (formField.Type == "fieldDate")
            {
                var dateField = deserializer.Deserialize<DateField>(section);
                formObjects.Add(dateField);
            }
            else if (formField.Type == "fieldDropdown")
            {
                var dropDownField = deserializer.Deserialize<DropdownListField>(section);
                formObjects.Add(dropDownField);
            }
            else if (formField.Type == "fieldFileUpload")
            {
                var fileUploadField = deserializer.Deserialize<FileUploadField>(section);
                formObjects.Add(fileUploadField);
            }
            else if (formField.Type == "fieldImage")
            {
                var imageField = deserializer.Deserialize<ImageField>(section);
                formObjects.Add(imageField);
            }
            else if (formField.Type == "fieldRadio")
            {
                var radioField = deserializer.Deserialize<RadioButtonField>(section);
                formObjects.Add(radioField);
            }
            else if (formField.Type == "fieldSignature")
            {
                var signatureField = deserializer.Deserialize<SignatureField>(section);
                formObjects.Add(signatureField);
            }
            else if (formField.Type == "fieldSlider")
            {
                var sliderField = deserializer.Deserialize<SignatureField>(section);
                formObjects.Add(sliderField);
            }
            else if (formField.Type == "fieldTextarea")
            {
                var textAreaField = deserializer.Deserialize<TextareaField>(section);
                formObjects.Add(textAreaField);
            }
            else if (formField.Type == "fieldTime")
            {
                var timeFiled = deserializer.Deserialize<TimeField>(section);
                formObjects.Add(timeFiled);
            }
        }

        return formObjects;
    }
    
}