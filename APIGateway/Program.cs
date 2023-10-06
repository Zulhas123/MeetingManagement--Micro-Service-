using APIGateway.Wonder;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add configuration sources
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile("ocelot.json")
    .AddEnvironmentVariables();

// Add services
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", cors =>
            cors.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
// Add Ocelot
//builder.Services.AddOcelot(builder => builder.AddWonder());
builder.Services.AddOcelot(builder.Configuration).AddWonder();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Use Ocelot
app.UseOcelot().Wait();

// Configure the HTTP request pipeline.
app.UseCors("CorsPolicy");
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("API Gateway Service Is Running...");
    });
});
app.Run();
