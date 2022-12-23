using LegoImageCreator.Interfaces;

namespace LegoImageCreator.Services;

public class RgbPixelDataAverager : IColourAverager<RgbPixelData>
{
    public RgbPixelData GetAverageColour(IEnumerable<RgbPixelData> source)
    {
        int blue = 0;
        int green = 0;
        int red = 0;

        foreach(RgbPixelData pixelData in source)
        {
            blue += pixelData.B * pixelData.B;
            green += pixelData.G * pixelData.G;
            red += pixelData.R * pixelData.R;
        }

        return new RgbPixelData
        {
            B = Convert.ToByte(Math.Sqrt(blue / source.Count())),
            G = Convert.ToByte(Math.Sqrt(green / source.Count())),
            R = Convert.ToByte(Math.Sqrt(red / source.Count()))
        };
    }
}