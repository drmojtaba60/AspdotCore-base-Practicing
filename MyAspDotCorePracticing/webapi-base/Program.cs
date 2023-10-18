using MyServices.FakedServices;
using MyServices.Tools.IPChecker;
using webapi_base.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//the dependency injection service container.
builder.Services.Configure<Developer>(config.GetSection(nameof(Developer)));

#region register services
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IIPCsisCheckerService, IPCsisCheckerService>();

builder.Services.AddScoped<FakeServicePractice0>();
builder.Services.AddScoped<IFakeServicePracticeByInterface, FakeServicePracticeByInterface>();
builder.Services.AddScoped<IFakeServicePracticeByInterface2>(_=>new FakeServicePracticeByInterface2("alireza"));
builder.Services.AddScoped<IFakeServicePracticeByServiceProvider, FakeServicePracticeByServiceProvider>();


#endregion
var app = builder.Build();

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
