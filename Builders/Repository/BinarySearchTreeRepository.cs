using System.Collections.Generic;
using System.Threading.Tasks;
using Builders.Interfaces;
using Builders.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Builders.Repository
{
    public class BinarySearchTreeRepository : IBinarySearchTreeRepository
    {
        private IMongoCollection<SimplifiedBinarySearchTree> collection;

        public BinarySearchTreeRepository(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.Database);
            collection = database.GetCollection<SimplifiedBinarySearchTree>(settings.Collection);
        }

        public async Task AddSimplifiedBinarySearchTree(SimplifiedBinarySearchTree simplifiedBst)
        {
            await collection.InsertOneAsync(simplifiedBst);
        }

        public async Task<SimplifiedBinarySearchTree> GetSimplifiedBinarySearchTree(string id)
        {
            var filter = Builders<SimplifiedBinarySearchTree>.Filter.Eq(bst => bst.Id, id);
            var tree = await collection.Find(filter).FirstOrDefaultAsync();

            return tree;
        }

        public async Task UpdateSimplifiedBinarySearchTree(SimplifiedBinarySearchTree bst)
        {
            var filter = Builders<SimplifiedBinarySearchTree>.Filter.Eq(e => e.Id, bst.Id);

            await collection.ReplaceOneAsync(filter, bst);
        }

        public async Task<bool> DeleteSimplifiedBinaryTreeById(string id)
        {
            var filter = Builders<SimplifiedBinarySearchTree>.Filter.Eq(e => e.Id, id);

            var deleteResponse = await collection.DeleteOneAsync(filter);

            return deleteResponse.DeletedCount > 0;
        }
    }
}