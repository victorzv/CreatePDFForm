namespace WorkerService1.Forms;

public class DropdownListField : FormField
{
    public DropdownListField()
    {
        Type = "fieldDropdown";
    }

    public List<string> Options { get; set; }
    
}