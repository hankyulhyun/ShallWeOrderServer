
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ShallWeOrder.Models;

namespace ShallWeOrder.DBService
{
    public class OrderDBService
    {
        private IConfiguration _configuration;
        private readonly IMongoCollection<Order> _orders;

        public OrderDBService(IConfiguration configuration)
        {
            _configuration = configuration;

            var client = new MongoClient(_configuration["DB:ConnectionString"]);
            var database = client.GetDatabase(_configuration["DB:DatabaseName"]);
            _orders = database.GetCollection<Order>(_configuration["DB:OrderCollection"]);
        }
    }
}