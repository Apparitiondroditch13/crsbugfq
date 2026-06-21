using Microsoft.EntityFrameworkCore;
using CreditAPI.Data;
using CreditAPI.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Подключаем базу данных InMemory
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("CreditDB"));

var app = builder.Build();

// Настраиваем Swagger для тестирования API
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// ДОБАВЛЯЕМ ТЕСТОВЫЕ ДАННЫЕ
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    // Проверяем, есть ли данные
    if (!dbContext.Clients.Any())
    {
        // Добавляем тестовых клиентов
        dbContext.Clients.AddRange(
            new Client
            {
                FullName = "Иванов Иван Иванович",
                BirthDate = new DateTime(1990, 1, 15),
                AverageSalary = 45000,
                HasUnpaidFines = true,
                FineAmount = 1500
            },
            new Client
            {
                FullName = "Петров Петр Петрович",
                BirthDate = new DateTime(1985, 5, 20),
                AverageSalary = 60000,
                HasUnpaidFines = false,
                FineAmount = 0
            },
            new Client
            {
                FullName = "Сидорова Анна Сергеевна",
                BirthDate = new DateTime(1992, 11, 3),
                AverageSalary = 55000,
                HasUnpaidFines = true,
                FineAmount = 2500
            },
            new Client
            {
                FullName = "Козлов Дмитрий Алексеевич",
                BirthDate = new DateTime(1988, 7, 8),
                AverageSalary = 70000,
                HasUnpaidFines = false,
                FineAmount = 0
            }
        );
        
        dbContext.SaveChanges();
        Console.WriteLine("✅ Тестовые данные добавлены в базу!");
    }
}

app.Run();