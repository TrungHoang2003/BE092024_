using DataAccess.Net.DAL;
using DataAccess.Net.DALImpl;
using DataAccess.Net.DataObject;
using System.Collections.Generic;
using DataAccess.Net.Bussiness;
using DataAccess.Net.DBContext;
using DataAccess.Net.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BE092024_DbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionStr")));
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dang ky DI
//builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IOrderService, OrderService>();

builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
});


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