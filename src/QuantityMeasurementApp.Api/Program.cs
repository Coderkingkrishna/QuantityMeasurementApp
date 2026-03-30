using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Business;
using QuantityMeasurementApp.Repository;

var builder = WebApplication.CreateBuilder(args);

// Configure web/API services and enum JSON handling.
builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter(allowIntegerValues: false)
        );
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();
// Repository layer handles SQL persistence and optional Redis caching.
builder.Services.AddQuantityMeasurementRepository(builder.Configuration);

var app = builder.Build();

// Apply pending EF migrations at startup so schema stays aligned with code.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<QuantityMeasurementDbContext>();
    dbContext.Database.Migrate();
}

// Configure middleware pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // app.UseSwaggerUI(options =>
    // {
    //     options.DefaultModelsExpandDepth(-1);
    // });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
