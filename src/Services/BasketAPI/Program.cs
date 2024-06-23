using Shared.Exceptions.Handler;

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

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//Configure HTTP pipeline

app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();
