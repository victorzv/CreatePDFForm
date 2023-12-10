using YamlDotNet.Serialization;

namespace CreatePDFFom.Model;

public class FormItem
{
    [YamlMember(Alias = "break")]
    public string Break { get; set; }

    [YamlMember(Alias = "text")]
    public string Text { get; set; }

    [YamlMember(Alias = "input")]
    public string FieldText { get; set; }

    [YamlMember(Alias = "select")]
    public string FieldSelect { get; set; }

    public string label { get; set; }

    public List<string> options { get; set; }

    [YamlMember(Alias = "radio")]
    public string FieldRadio { get; set; }

    [YamlMember(Alias = "checkbox")]
    public string FieldCheckbox { get; set; }

    [YamlMember(Alias = "button")]
    public string FieldButton { get; set; }

    [YamlMember(Alias = "signature")]
    public string FieldSignature { get; set; }

    [YamlMember(Alias = "listbox")]
    public string FieldListBox { get; set; }
}
