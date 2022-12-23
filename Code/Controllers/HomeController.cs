using Image_Colour_Swap;
using Image_Colour_Swap.Interfaces;
using LegoImageCreator.Interfaces;
using LegoImageCreator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LegoImageCreator.Controllers;

public class HomeController : Controller
{
    private readonly IImageLoader _imageLoader;
    private readonly IImagePixelator _imagePixelator;
    private readonly IImageSaver _imageSaver;
    private readonly IImageSizeCalculator _imageSizeCalculator;
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        IImageLoader imageLoader,
        IImagePixelator imagePixelator,
        IImageSaver imageSaver,
        IImageSizeCalculator imageSizeCalculator,
        ILogger<HomeController> logger)
    {
        _imageLoader = imageLoader;
        _imagePixelator = imagePixelator;
        _imageSaver = imageSaver;
        _imageSizeCalculator = imageSizeCalculator;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [RequestFormLimits(ValueCountLimit = int.MaxValue)]
    public async Task<IActionResult> Process(string sourceFile)
    {
        var image = _imageLoader.LoadImageFromBase64EncodedString(sourceFile);
        var imageRatio = new ImageRatioModel
        {
            Height = image.Size.Height,
            Width = image.Size.Width
        };

        var newImageDimensions = _imageSizeCalculator.GetImageDimensions(imageRatio, 2500);
        image = _imageLoader.Resize(
            image, 
            new InMemoryImageData(
                "", 
                Convert.ToInt32(Math.Ceiling((double)image.Size.Width / newImageDimensions.Width) * newImageDimensions.Width), 
                Convert.ToInt32(Math.Ceiling((double)image.Size.Height / newImageDimensions.Height) * newImageDimensions.Height),
                new byte[0]));

        var pixelData = _imageLoader.CreatePixelData(image);

        var pixelatedData = _imagePixelator.GetPixelatedImage(
            image, 
            newImageDimensions, 
            pixelData);

        using(MemoryStream stream = (MemoryStream)_imageLoader.GenerateStream(
            pixelatedData,
            new InMemoryImageData(
                "", 
                newImageDimensions.Width, 
                newImageDimensions.Height,
                new byte[0])))
        {
            var id = Guid.NewGuid().ToString();
            await _imageSaver.SaveAsync($"lego-{id}.jpg", stream);
        }

        return RedirectToAction("Index");
    }
}
