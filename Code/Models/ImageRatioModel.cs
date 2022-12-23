namespace LegoImageCreator.Models;

public class ImageRatioModel
{
    public int Height { get; set; }
    public int Width { get; set; }

    public double Ratio()
    {
        if(Height > Width)
        {
            return (double)Height / (double)Width;
        }

        return (double)Width / (double)Height;
    }
}