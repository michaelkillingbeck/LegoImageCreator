using Image_Colour_Swap;
using Image_Colour_Swap.Interfaces;
using LegoImageCreator.Interfaces;
using LegoImageCreator.Services;

namespace LegoImageCreator.Bootstrapping;

internal class ServicesBootstrapping
{
    internal static void BootstrapServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IColourAverager<RgbPixelData>, RgbPixelDataAverager>();
        builder.Services.AddScoped<IImageLoader, ImageSharpImageLoader>();
        builder.Services.AddScoped<IImagePixelator, ImagePixelator>();
        builder.Services.AddScoped<IImageSaver, AWSS3ImageSaver>();
        builder.Services.AddScoped<IImageSizeCalculator, ImageSizeCalculator>();
        builder.Services.AddControllersWithViews();
    }
}