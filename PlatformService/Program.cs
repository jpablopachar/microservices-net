using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Data.Platforms;
using PlatformService.Profiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information).EnableSensitiveDataLogging();
    options.UseInMemoryDatabase("InMem");
});

builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new PlatformProfile());
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors("corsapp");

app.UseAuthorization();

app.MapControllers();

using (var environment = app.Services.CreateScope())
{
    var services = environment.ServiceProvider;

    try {
        var context = environment.ServiceProvider.GetRequiredService<AppDbContext>();

        await LoadDatabase.InsertData(context);
    } catch (Exception exception) {
        var logger = services.GetRequiredService<ILogger<Program>>();

        logger.LogError(exception, "Se produjo un error al realizar la migración de la base de datos.");
    }
}

app.Run();
