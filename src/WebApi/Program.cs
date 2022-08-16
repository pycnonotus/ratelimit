using Application;
using FakeCompany.RateLimit;
using FakeCompany.RateLimit.Extensions;

const string OpenToAllCrossName = "open-to-all-cross";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRateLimiting<ErrorMessageFactory>();
builder.Services.AddIsPrimeService();
builder.Services.AddSingleton<ConfigurationManager>(builder.Configuration);

builder.Services.AddCors(p => p.AddPolicy(OpenToAllCrossName, builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


var app = builder.Build();
app.UseCors(OpenToAllCrossName);

app.UseRateLimitingMiddleware();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
