using BookStore.Infra.Persistence.Extensions;
using BookStore.Core.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddApplication();

var app = builder.Build();

app.MapOpenApi();

app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Book Store Api"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(option => option.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

await app.ExecuteMigrationPersistenceAsync();

app.Run();
