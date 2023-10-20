namespace WorkerService1.Forms;

public class DropdownListField : FormField
{
    public DropdownListField()
    {
        Type = "fieldDropdown";
    }

    public List<string> Options { get; set; }

    // Методы для обработки выпадающих списков
    public void Process()
    {
        Console.WriteLine($"DropDown list: {Label}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Font: {Font}");
        Console.WriteLine($"Font Size: {FontSize}");
        Console.WriteLine("Options:");
        foreach (var option in Options)
        {
            Console.WriteLine(option);
        }
    }
}