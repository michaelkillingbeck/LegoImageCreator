using Image_Colour_Swap.Interfaces;
using LegoImageCreator.Models;

namespace LegoImageCreator.Interfaces;

public interface IImagePixelator
{
    RgbPixelData[] GetPixelatedImage(
        IImageData originalImage, 
        ImageRatioModel ratio, 
        RgbPixelData[] originalPixels);
}