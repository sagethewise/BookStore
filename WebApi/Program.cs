using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using WebApi;
using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
});

// Eğer Entity Framework veya benzer bir ORM kullanıyorsan, burada BookContext gibi bir veritabanı bağlamı ekleyebilirsin.
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase("BookStore"));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
var app = builder.Build();

// Veritabanını başlatmak için servis sağlayıcı oluştur
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    DataGenerator.Initialize(serviceProvider); // DataGenerator sınıfını burada çağırıyoruz
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
