using InvoiceR.Presentation;
using InvoiceR.Infrastructure;
using InvoiceR.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPresentation();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UsePresentation();
app.UseInfrastructure();
app.UseApplication();

app.Run();