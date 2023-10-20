namespace WorkerService1.Forms;

public class ImageField : FormField
{
    public ImageField()
    {
        Type = "fieldImage";
    }

    public string ImageSource { get; set; }
}