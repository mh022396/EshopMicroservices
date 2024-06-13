using Shared.Behaviors;
using Shared.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

//Add Services
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//Build
var app = builder.Build();

//Configure HTTP request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
