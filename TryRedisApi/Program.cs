
using TryRedisApi.Repository;
using TryRedisApi.Services;

namespace TryRedisApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //redis
            builder.Services.AddStackExchangeRedisCache(o =>
            {
                o.Configuration = builder.Configuration.GetConnectionString("redis-server");
                o.InstanceName = "test";
            });

            //session
            builder.Services.AddSession(o => o.IdleTimeout = TimeSpan.FromMinutes(3));

            // test data
            builder.Services.AddScoped<GamesRepository>();
            builder.Services.AddScoped<RedisService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            // session
            app.UseSession();
            app.MapControllers();

            app.Run();
        }
    }
}
