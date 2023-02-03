using Microsoft.EntityFrameworkCore;
using SIMinfo.API.CustomMiddlewares;
using SIMinfo.API.DataAccessLayer;
using SIMinfo.API.Services.Class;
using SIMinfo.API.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SimInfoDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("SimInfoConnectionString")));

builder.Services.AddCors((Setup) =>
{
    Setup.AddPolicy("default", (options) =>
    {
        options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});//this is used for allowing request from any service,method and any header

builder.Services.AddScoped<ISimInfoService, SimInfoService>();
builder.Services.AddScoped<IMobileCountryCodeService, MobileCountryCodeService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("default");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
