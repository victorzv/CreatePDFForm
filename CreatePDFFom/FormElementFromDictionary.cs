using WorkerService1;
using WorkerService1.Forms;

namespace CreatePDFFom;

public class FormElementFromDictionary
{
    public static FormField CreateFormField(Dictionary<string, object> item)
    {
        string fieldType = item["Type"].ToString();

        switch (fieldType)
        {
            case "fieldCheckBox":
                return new CheckBoxField
                {
                    Type = fieldType,
                    Name = item["Name"].ToString(),
                    Label = item["Label"].ToString(),
                    State = Convert.ToBoolean(item["State"])
                };
            case "fieldDropDown":
                return new DropdownListField
                {
                    Type = fieldType,
                    Name = item["Name"].ToString(),
                    Label = item["Label"].ToString(),
                    Options = ((List<object>)item["options"]).ConvertAll(option => option.ToString())
                };
            case "fieldText":
                return new TextField()
                {
                    Type = fieldType,
                    Name = item["Name"].ToString(),
                    Label = item["Label"].ToString(),
                };
            case "Text":
                return new SimpleTextField
                {
                    Type = fieldType,
                    Name = item["Name"].ToString(),
                    Label = item["Label"].ToString(),
                };
            // Другие типы полей можно обработать аналогичным образом
            default:
                throw new NotSupportedException($"Неизвестный тип поля: {fieldType}");
        }
    }
}