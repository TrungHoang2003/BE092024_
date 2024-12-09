using DataAccess.Net.DAL;
using DataAccess.Net.DALImpl;
using DataAccess.Net.DataObject;
using System.Collections.Generic;
using System.Text;
using DataAccess.Net.Bussiness;
using DataAccess.Net.DBContext;
using DataAccess.Net.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BE092024_DbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionStr") ?? string.Empty));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = false,
        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
        ValidAudience = builder.Configuration["Jwt:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration["Jwt:Secret"] ?? string.Empty))
    };
    
    // Tùy chỉnh thông báo lỗi
    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.Response.Headers["WWW-Authenticate"] = "Bearer";
            context.Response.StatusCode = 401; // Unauthorized
            context.Response.WriteAsync("Vui lòng đăng nhập để sử dụng chức năng này");
            return Task.CompletedTask;
        }
    };
});

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
builder.Services.AddTransient<IAccountRepository, AccountRepository>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();