using BikeShopDSD605.Data;
using BikeShopDSD605.Models;
using System.Text.Json;

namespace APIIntergrationTest.TestData
{
    static class AddTestData
    //import json array and add to context
    {
        public static void AddStockData(ApplicationDbContext context)
        {
            var jsonString = File.ReadAllText("TestData/TestStock.json");

            //need to stop it being case sensitive the model is capital case and the json is not
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var list = JsonSerializer.Deserialize<Stocks[]>(jsonString, options);
            {
                foreach (var item in list)
                {
                    context.Stocks.Add(item);
                }
                context.SaveChanges();
            }

        }


        public static void AddSingleStockData(ApplicationDbContext context)
        {
            Stocks stock = new Stocks();

            stock.StockId = Guid.NewGuid();
            stock.ProductName = "OnTrack Puncture Repair Kit";
            stock.ProductDescription = "Dont cut your ride short because of punctures!The OnTrack puncture repair kit has everything needed to get you up and running again.";
            stock.ProductType = "Patch";
            stock.Price = 3;
            stock.Quantity = 123;

            context.Stocks.Add(stock);
            //save to the in memory database
            context.SaveChanges();
        }




        public static void AddCastData(ApplicationDbContext context)
        {
            var jsonString = File.ReadAllText("TestData/TestCast.json");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var list = JsonSerializer.Deserialize<Cast[]>(jsonString, options);
            {
                foreach (var item in list)
                {
                    context.Cast.Add(item);
                }
                context.SaveChanges();
            }

        }

    }

}