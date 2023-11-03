using System.Text.RegularExpressions;
using CreatePDFFom;
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

    public Form ReadFormFields()
    {
        var listOfBlock = new List<FormItem>();
        
        var deserializer = new DeserializerBuilder().Build();
        
        var form = deserializer.Deserialize<Form>(yamlContent);
        
        return form;
    }
    
}