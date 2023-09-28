using Audit.Core;
using Audit.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
Audit.Core.Configuration.Setup()
    .UseMongoDB(config => config
        .ConnectionString("mongodb://localhost:27017")
        .Database("audit")
        .Collection("Event"));
 
app.UseAuditMiddleware(_ => _
    .WithEventType("{controller}/{action} ({verb})")
    .IncludeHeaders()
    .IncludeRequestBody()
    .IncludeResponseBody());
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