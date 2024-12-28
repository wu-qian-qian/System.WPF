using AutoMapper;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System.Api;
using System.Api.MiddleWare;
using System.Api.Module;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Initialize ();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//日志中间件添加
app.UseRequestResponseLogging();

app.UseAuthorization();

app.MapControllers();


app.Run();
