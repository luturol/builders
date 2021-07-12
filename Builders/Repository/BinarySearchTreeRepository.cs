using System.Threading.Tasks;
using Builders.Interfaces;
using Builders.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Builders.Repository
{
    public class BinarySearchTreeRepository : IBinarySearchTreeRepository
    {
        private IMongoCollection<BinarySearchTree> collection;

        public BinarySearchTreeRepository(IMongoDbSettings settings)
        {            
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.Database);
            collection = database.GetCollection<BinarySearchTree>(settings.Collection);
        }

        public async Task AddBinarySearchTree(BinarySearchTree bst)
        {
            await collection.InsertOneAsync(bst);
        }

        public async Task<BinarySearchTree> GetBinarySearchTree(string id)
        {
            var filter = Builders<BinarySearchTree>.Filter.Eq(bst => bst.Id, id);
            var tree = await collection.Find(filter).FirstOrDefaultAsync();

            return tree;
        }

        public async Task UpdateBinarySearchTree(BinarySearchTree bst)
        {
            var filter = Builders<BinarySearchTree>.Filter.Eq(e => e.Id, bst.Id);

            await collection.ReplaceOneAsync(filter, bst);            
        }
    }
}