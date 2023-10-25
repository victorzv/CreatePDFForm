using System.Text.RegularExpressions;
using CreatePDFFom;
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
        //Console.WriteLine(yamlContent);
    }

    public List<FormField> ReadFormFields()
    {
        var listOfBlock = new List<FormField>();
        
        var deserializer = new DeserializerBuilder().Build();
        
        var list = deserializer.Deserialize<List<Dictionary<string, object>>>(yamlContent);

        foreach (var item in list)
        {
            FormField formField = FormElementFromDictionary.CreateFormField(item);
            listOfBlock.Add(formField);
        }
        
        return listOfBlock;
    }
    
}