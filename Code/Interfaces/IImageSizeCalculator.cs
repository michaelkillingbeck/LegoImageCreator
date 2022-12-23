using LegoImageCreator.Models;

namespace LegoImageCreator.Interfaces;

public interface IImageSizeCalculator
{
    ImageRatioModel GetImageDimensions(ImageRatioModel originalImage, int numberOfPixelsInTotal);
}