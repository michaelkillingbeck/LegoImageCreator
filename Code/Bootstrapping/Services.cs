namespace LegoImageCreator.Bootstrapping;

internal class ServicesBootstrapping
{
    internal static void BootstrapServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews();
    }
}