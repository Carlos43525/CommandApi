using CommandApi.App.Data;
using AutoMapper; 
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace CommandApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseNpgsql(builder.Configuration.GetConnectionString("LocalDb")));

            builder.Services.AddControllers().AddNewtonsoftJson(
                s => s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddScoped<ICommandRepo, CommandRepo>();

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
        }
    }
}