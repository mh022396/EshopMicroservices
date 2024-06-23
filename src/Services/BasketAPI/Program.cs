var builder = WebApplication.CreateBuilder(args);

//Services
var assembly = typeof(Program).Assembly;

builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LogginBehavior<,>));
});

var app = builder.Build();

//Configure HTTP pipeline

app.Run();
