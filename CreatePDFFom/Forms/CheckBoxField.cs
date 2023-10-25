namespace WorkerService1.Forms;

public class CheckBoxField : FormField
{
    public CheckBoxField()
    {
        Type = "fieldCheckBox";
    }
    
    public bool State { get; set; }
}