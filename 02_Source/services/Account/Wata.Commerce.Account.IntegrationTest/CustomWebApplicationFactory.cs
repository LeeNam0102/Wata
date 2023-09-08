using System.Diagnostics;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wata.Commerce.Common.Test;
using Wata.Commerce.Account.IntegrationTest.Context;

namespace Wata.Commerce.Account.IntegrationTest
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                IntegrationTestBase._configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

                services.AddDbContext<AppDbContext>(options => {
                    string? testConnectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__INTEGRATION_TESTS");
                    if (string.IsNullOrEmpty(testConnectionString))
                    {
                        options.UseSqlServer("name=IntegrationTest:ConnectionString");
                    }
                    else
                    {
                        options.UseSqlServer(testConnectionString);
                    }
                });
                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<AppDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    //db.Database.EnsureDeleted();
                    //db.Database.EnsureCreated();

                    try
                    {
                        Debug.WriteLine("Using Connection String " + db.Database.GetConnectionString() ?? "");

                        Debug.WriteLine("Migrating Database", "info");
                        db.Database.Migrate();
                        Debug.WriteLine("Finished Migrating Database", "info");

                        var sql = System.IO.File.ReadAllText("DBTestInit.sql");

                        Debug.WriteLine("Populating Database", "info");
                        db.Database.ExecuteSqlRaw(sql);
                        Debug.WriteLine("Finished Populating Database", "info");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }
    }
}