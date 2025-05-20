using DevFreela.API.ExceptionHandler;
using DevFreela.Application.Services;
using DevFreela.Application.Models;
using DevFreela.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using DevFreela.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<FreelanceTotalCostConfig>(builder.Configuration.GetSection("FreelanceTotalCostConfig"));//exemplo config option

builder.Services.AddSingleton<IConfigService, ConfigService>();//exemplo injecao dependencia

//builder.Services.AddDbContext<DevFreelaDbContext>(options =>
//    options.UseInMemoryDatabase("DevFreelaDb"));//exemplo banco de dados em memoria

var connectionString = builder.Configuration.GetConnectionString("DevFreelaCS");
builder.Services.AddDbContext<DevFreelaDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddApplication();//padrao result camada de aplicação

builder.Services.AddExceptionHandler<ApiExceptionHandler>();//exemplo exception handler***
builder.Services.AddProblemDetails();//em caso de erro (exception), adicionar detalhes***

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();//exemplo exception handler***
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
