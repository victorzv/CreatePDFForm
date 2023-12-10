using CreatePDFFom.Model;
using CreatePDFFom.Service.Interface;
using YamlDotNet.Serialization;

namespace CreatePDFFom.Service;

public class YamlFormFieldReader : IFormFieldReader
{
    private string yamlContent;

    public YamlFormFieldReader(string content)
    {
        yamlContent = content;
    }

    public Form ReadFormFields()
    {
        var deserializer = new DeserializerBuilder().Build();
        var form = deserializer.Deserialize<Form>(yamlContent);
        return form;
    }

}