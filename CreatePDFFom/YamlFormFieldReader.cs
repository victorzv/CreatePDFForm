using System.Text.RegularExpressions;

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
        var formObjects = new List<FormField>();

        var pattern = @"^- type: [a-zA-Z]+\n(?:  .+\n)*";
        var regex = new Regex(pattern);
        
        var matches = regex.Matches(yamlContent);  
        
        

        return formObjects;
    }
    
}