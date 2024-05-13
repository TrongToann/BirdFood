using BirdFood.API.Middleware;
using BirdFood.Application.DependencyInjection.Extensions;
using BirdFood.Infrastructure.DependencyInjection.Extension;
using BirdFood.Persistence;
using Carter;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database Configure
builder.Services.ConfigurePersistenceServices(builder.Configuration);
//Mediat-Validators
builder.Services.AddConfigureMediatR();
//AutoMapper
builder.Services.AddConfigureAutoMapper();

builder.Services.AddInfrastructureServices();
//MiddleWare Exception Handling
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddCarter();
//Authen-Author
builder.Services.AddJwtAuthentication(builder.Configuration);
//Configure Masstransit RabbitMQ

builder.Services.AddApiVersioning(options => options.ReportApiVersions = true)
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

//app.MapControllers();
app.MapCarter();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
