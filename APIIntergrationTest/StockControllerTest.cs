using BikeShopDSD605.Data;

namespace APIIntegrationTest
{
    //The first thing we have to do is to implement the previously created TestingWebAppFactory class:
    public class StockControllerTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        //passing in the class using Injection and across to _client in the constructor
        public StockControllerTest(TestingWebAppFactory<Program> factory)
            => _client = factory.CreateClient();

        // GET: api/StockAPI
        [Fact]
        public async Task IndexReturnsStock()
        {
            var response = await _client.GetAsync("api/StocksApi");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("OnTrack Puncture Repair Kit", responseString);
        }

        // GET: api/Casts
        [Fact]
        public async Task IndexReturnsCast()
        {
            var response = await _client.GetAsync("api/CastsAPI");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Sigourney", responseString);
        }
    }
}