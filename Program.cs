
using Cwiczenia5.Data;
using Cwiczenia5.Services;
using Microsoft.EntityFrameworkCore;

namespace cwiczenie5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

           
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IPcService, PcService>();
            builder.Services.AddControllers();


            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                try
                {
                    dbContext.Database.Migrate();
                    Console.WriteLine($"Migracja wykonana pomyślnie!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd podczas migracji: {ex.Message}");
                    throw;
                }
            }


            app.MapControllers();

            app.Run();
        }
    }
}


