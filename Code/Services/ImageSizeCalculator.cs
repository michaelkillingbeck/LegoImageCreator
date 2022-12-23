using LegoImageCreator.Interfaces;
using LegoImageCreator.Models;

namespace LegoImageCreator.Services;

public class ImageSizeCalculator : IImageSizeCalculator
{
    public ImageRatioModel GetImageDimensions(ImageRatioModel originalImage, int numberOfPixelsInTotal)
    {
        double ratio = originalImage.Ratio();
        var xForNewImage = Math.Sqrt(((double)numberOfPixelsInTotal / ratio));
        int newHeight = 0;
        int newWidth = 0;

        if(originalImage.Height > originalImage.Width)
        {
            newWidth = Convert.ToInt32(Math.Ceiling((double)xForNewImage));
            newHeight = Convert.ToInt32(Math.Ceiling(newWidth * originalImage.Ratio()));
        }
        else
        {
            newHeight = Convert.ToInt32(Math.Ceiling((double)xForNewImage));
            newWidth = Convert.ToInt32(Math.Ceiling(newHeight * originalImage.Ratio()));
        }

        return new ImageRatioModel
        {
            Height = newHeight,
            Width = newWidth
        };
    }
}