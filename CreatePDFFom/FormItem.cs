using YamlDotNet.Serialization;

namespace WorkerService1;

public class FormItem
{
    [YamlMember(Alias = "rn")]
    public string NewString { get; set; }

    [YamlMember(Alias = "text")]
    public string Text { get; set; }

    [YamlMember(Alias = "input")]
    public string FieldText { get; set; }

    [YamlMember(Alias = "select")]
    public string FieldSelect { get; set; }

    public string Label { get; set; }
    
    public List<string> Options { get; set; }
    
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

public class Form
{
    public List<FormItem> Items { get; set; }
}