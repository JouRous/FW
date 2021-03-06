using System;
using System.Threading.Tasks;
using Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();

      using var scope = host.Services.CreateScope();
      var services = scope.ServiceProvider;
      var loggerFactory = services.GetRequiredService<ILoggerFactory>();

      try
      {
        var dbContext = services.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
      }
      catch (Exception ex)
      {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, ex.Message);
      }

      host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
