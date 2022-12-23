using Image_Colour_Swap.Interfaces;
using LegoImageCreator.Interfaces;
using LegoImageCreator.Models;

namespace LegoImageCreator.Services;

public class ImagePixelator : IImagePixelator
{
    private readonly IColourAverager<RgbPixelData> _colourAverager;

    public ImagePixelator(IColourAverager<RgbPixelData> colourAverager)
    {
        _colourAverager = colourAverager;
    }

    public RgbPixelData[] GetPixelatedImage(
        IImageData originalImage, 
        ImageRatioModel ratio, 
        RgbPixelData[] originalPixels)
    {
        List<RgbPixelData>[] newData = new List<RgbPixelData>[(int)ratio.Height * (int)ratio.Width];
        int heightPixelLength = (int)(originalImage.Size.Height / ratio.Height);
        int widthPixelLength = (int)(originalImage.Size.Width / ratio.Width);    

        for(int i = 0; i < originalPixels.Count(); i++)
        {
            int row = 0;
            int column = 0;

            if(i > 0)
            {
                var originalImageRow = i / originalImage.Size.Width;
                row = originalImageRow / heightPixelLength;

                var originalImageColumn = i % originalImage.Size.Width;
                column = originalImageColumn / widthPixelLength;
            }

            var index = (row * ratio.Width) + column;
            if(newData[index] == null)
            {
                newData[index] = new List<RgbPixelData>();
            }

            newData[index].Add(originalPixels[i]);
        }

        RgbPixelData[] returnData = new RgbPixelData[newData.Length];

        for(int i = 0; i < newData.Length; i++)
        {
            returnData[i] = _colourAverager.GetAverageColour(newData[i]);
        }

        return returnData;
    }
}