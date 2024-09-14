using Microsoft.EntityFrameworkCore;
using GkoTradeService.Data;
using GkoTradeService.Services;
using GkoTradeService.Interfaces;
using GkoTradeService.Domain;
using GkoTradeService.Application.Factories;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseInMemoryDatabase("CryptoPricesInMemoryDb");
});

// Auto Mapper Configurations
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add typed clients for different APIs
builder.Services.AddHttpClient<IPriceProviders, BitStampPrice>();
builder.Services.AddHttpClient<IPriceProviders, BitFinexPrice>();

builder.Services.AddTransient<IBitcoinPriceService, BitcoinPriceService>();
builder.Services.AddTransient<BitcoinPriceFactory>();

builder.Services.AddScoped<ICryptoPriceRepo, CryptoPriceRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.MapControllers();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.Run();

