using Microsoft.Extensions.Options;
using System.Text.Json;
using TesodevAPI.Ads.Configs;
using MongoDB.Driver;
using MongoDB.Bson;
using TesodevAPI.Ads.Repositories;
using TesodevAPI.Ads.Models;


namespace TesodevAPI.Ads.Repositories
{
    public class Repository:IRepository
    {

        /* private List<Models.Merchant>merchants { get; set; }
     public Repository()
         {
             merchants = new List<Models.Merchant>();
         }
         public Models.Merchant GetById(int Id)
         {
             return merchants.FirstOrDefault(m => m.Id == Id);       
         }
     }*/
        private readonly IMongoCollection<Merchant> merchantCollection;
        public Repository(IOptions<MongoDataBaseSettings> MongoDataBaseSetting)
        {
            const string connectionUri = "mongodb+srv://demrcioebrar:ebrar2003.@merchant.2uebqxs.mongodb.net/AdsAPI?retryWrites=true&w=majority";

            var settings = MongoClientSettings.FromConnectionString(connectionUri);

            // Set the ServerApi field of the settings object to Stable API version 1
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            // Create a new client and connect to the server
            var client = new MongoClient(settings);
            var database = client.GetDatabase("AdsAPI");
            merchantCollection = database.GetCollection<Merchant>("Merchant");
            // Send a ping to confirm a successful connection
            try
            {
                var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public List<Models.Merchant> GetAll()
        {
            var merchantList = merchantCollection.Find(Builders<Models.Merchant>.Filter.Empty).ToList();
            return merchantList;
        }
        public void Create(Models.Merchant merchant)
        {
         merchantCollection.InsertOne(merchant);      
        }
    }
}
