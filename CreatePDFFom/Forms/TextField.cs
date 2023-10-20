namespace WorkerService1.Forms;

public class TextField : FormField
{
    public TextField()
    {
        Type = "fieldText";
    }

    // Методы для обработки текстовых полей
    public void Process()
    {
        Console.WriteLine($"Text field: {Label}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Font: {Font}");
        Console.WriteLine($"Font size: {FontSize}");
    }
}