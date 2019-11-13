using MongoDB.Driver;
using ShallWeOrder.Models;

namespace ShallWeOrder.DBService
{
    public class UserDBService
    {
        private readonly IMongoCollection<User> _users;

        public UserDBService()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("....");
            _users = database.GetCollection<User>("....");
        }

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

    }
}