﻿using APIIntergrationTest.TestData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BikeShopDSD605.Data
{
    //The WebApplicationFactory class is a factory that we can use to bootstrap an application in memory for functional end-to-end tests. 
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType ==
                        typeof(DbContextOptions<ApplicationDbContext>));
                // we remove the ApplicationDbContext registration from the Program class
                if (descriptor != null)
                    services.Remove(descriptor);

                //we add the database context to the service container and instruct it to use the in-memory database instead of the real database
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryStockTest");
                });


                //Finally, we ensure that we seed the data from the ApplicationDbContext class (The same data you inserted into a real SQL Server database).
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())

                using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    try
                    {
                        appContext.Database.EnsureCreated();

                        // AddTestData.AddSingleStockData(appContext);
                        AddTestData.AddStockData(appContext);
                        AddTestData.AddCastData(appContext);

                        //context.Database.EnsureCreated() ensures that the database for the context exists. If it exists, no action is taken. If it does not exist then the database and all its schema are created and also it ensures it is compatible with the model for this context.
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            });
        }
    }
}
