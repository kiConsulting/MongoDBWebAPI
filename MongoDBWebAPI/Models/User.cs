using MongoDB.Bson;

namespace MongoDBWebAPI.Models
{
    public class User
    {
        public ObjectId Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int age { get; set; }
    }
    
}