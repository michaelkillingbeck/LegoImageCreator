using LegoImageCreator.Bootstrapping;

var builder = WebApplication.CreateBuilder(args);
ServicesBootstrapping.BootstrapServices(builder);

var app = builder.Build();
AppBootstrapper.BoostrapApplication(app);

app.Run();