using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBWebAPI.Models;

namespace MongoDBWebAPI.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<User> _userCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            try
            {
                var client = new MongoClient(mongoDBSettings.Value.ConnectionString);
                var database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
                _userCollection = database.GetCollection<User>(mongoDBSettings.Value.UsersCollection);
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to MongoDB: {ex.Message}.");
                throw;
            }
        }

        public List<User> GetUsers => _userCollection.Find(user => true).ToList();

        public User GetUserById(string id) => _userCollection.Find(user => user.Id.ToString() == id).FirstOrDefault();

        public List<User> GetUsersAboveAge(int age) => _userCollection.Find(user => user.age > age).ToList();

        public User GetUserByIdAndMinAge(string id, int minAge)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<User>.Filter.And(
                Builders<User>.Filter.Eq(u => u.Id, objectId),
                Builders<User>.Filter.Gt(u => u.age, minAge)
                );
            return _userCollection.Find(filter).FirstOrDefault();
        }
    }
}