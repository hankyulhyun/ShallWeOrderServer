using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ShallWeOrder.Models;

namespace ShallWeOrder.DBService
{
    public class UserDBService
    {
        private IConfiguration _configuration;
        private readonly IMongoCollection<User> _users;

        public UserDBService(IConfiguration configuration)
        {
            _configuration = configuration;

            var client = new MongoClient(_configuration["DB:ConnectionString"]);
            var database = client.GetDatabase(_configuration["DB:DatabaseName"]);
            _users = database.GetCollection<User>(_configuration["DB:UserCollection"]);
        }

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public User Get(string userId, string password) => 
            _users.Find<User>(user => user.UserId == userId && user.Password == password)
                .FirstOrDefault();

        public void Update(string id, User userIn) =>
            _users.ReplaceOne(user => user.Id == id, userIn);

    }
}