
using SalesManagmentSystem.Models.Store;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ModelStore>(); // или другой подходящий период жизни, напр. Singleton, Scoped

// Добавление сервисов в контейнер.
builder.Services.AddControllers();

// Изучите, как настраивать Swagger/OpenAPI на https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Настройка конвейера HTTP-запросов.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
