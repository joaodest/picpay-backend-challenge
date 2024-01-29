
using Microsoft.EntityFrameworkCore;
using PicpayChallenge.Application.Interfaces;
using PicpayChallenge.Application.ObjectMapper;
using PicpayChallenge.Application.Services;
using PicpayChallenge.Domain.Entities;
using PicpayChallenge.Domain.Interfaces;
using PicpayChallenge.Domain.Services;
using PicpayChallenge.Infra;
using PicpayChallenge.Infra.Data.Operations;
using PicpayChallenge.Infra.Data.Users;
using PicpayChallenge.Presentation.Controllers;
using PicpayChallenge.Presentation.DTOs;
using PicpayChallenge.Presentation.ObjectMapper;
using System.Text.Json.Serialization;

namespace PicpayChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddHttpClient<UserController>();
            builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(TransactionProfile).Assembly);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<PicpayDbContext>(options =>
            {
                options.UseMySQL(connectionString);
            });


            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IBankingOperationsRepository, BankingOperationsRepository>();

            builder.Services.AddScoped<IBankingOperations, NormalUserBankingOperations>();
            builder.Services.AddScoped<IUserService, UserService>();

            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
