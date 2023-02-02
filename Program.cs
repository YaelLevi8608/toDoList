using OrderManagement;
using myTasks.Interfaces;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using myTasks.Services;
using myTasks.Service;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOrders();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddAuthentication();

// builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(cfg =>
    {
        cfg.TokenValidationParameters = TokenService.GetTokenValidationParameters();
    });

builder.Services.AddAuthorization(cfg =>
{
    cfg.AddPolicy("Admin", policy => policy.RequireClaim("type", "Admin"));
    cfg.AddPolicy("User", policy => policy.RequireClaim("type", "User"));
}
);
// builder.Services.AddSwaggerGen();
   builder.Services.AddSwaggerGen(c =>
            {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
         {
            In = ParameterLocation.Header,
            Description = "Please enter JWT with Bearer into field",
             Name = "Authorization",
              Type = SecuritySchemeType.ApiKey
          });
          c.AddSecurityRequirement(new OpenApiSecurityRequirement {
          { new OpenApiSecurityScheme
                   {
                 Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                 },
             new string[] {}
             }
             });
         });



// singlton
builder.Services.AddSingleton<ITaskService, TaskService>();

var config = builder.Configuration;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// Extention- scoped
namespace OrderManagement
{
    static class Extention
    {
        public static IServiceCollection AddOrders(this IServiceCollection services)
        {
            services.AddScoped<ITaskService,TaskService>();
            services.AddScoped<IUserService,UserService>();

            return services;
        }
    }
}
