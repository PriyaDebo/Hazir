using BL.Operations;
using DAL.Repositories;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);
var primaryKey = builder.Configuration["CosmosDbPrimaryKey"];
var endpoint = builder.Configuration["CosmosDbEndpoint"];

builder.Services.AddScoped<StudentOperations>();
builder.Services.AddScoped<AttendanceOperations>();
builder.Services.AddScoped<ClassOperations>();
builder.Services.AddScoped<AttendanceRepository>();
builder.Services.AddScoped<ClassRepository>();
builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped(sp => new CosmosClient(endpoint, primaryKey));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();