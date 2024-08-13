
<<<<<<< HEAD
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mock_Project.Models;
using System.Text;
=======
using Microsoft.EntityFrameworkCore;
using Mock_Project.Models;
>>>>>>> 7ad36da62a560858fa321f07c8c98f0a5ec07642

namespace Mock_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
<<<<<<< HEAD
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "yourdomain.com",
                    ValidAudience = "yourdomain.com",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKey"))
                };
            });

            builder.Services.AddAuthorization();
=======
>>>>>>> 7ad36da62a560858fa321f07c8c98f0a5ec07642

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<db_xemphimContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnect"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");
<<<<<<< HEAD
            app.UseAuthentication();
=======
>>>>>>> 7ad36da62a560858fa321f07c8c98f0a5ec07642
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
