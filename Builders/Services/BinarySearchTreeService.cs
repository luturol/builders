using System.Collections.Generic;
using System.Threading.Tasks;
using Builders.Extensions;
using Builders.Interfaces;
using Builders.Models;
using MongoDB.Driver;

namespace Builders.Services
{
    public class BinarySearchTreeService : IBinarySearchTreeService
    {
        private readonly IBinarySearchTreeRepository repository;

        public BinarySearchTreeService(IBinarySearchTreeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<SimplifiedBinarySearchTree> GetSimplifiedBinarySearchTree(string id)
        {
            return await repository.GetSimplifiedBinarySearchTree(id);
        }

        public async Task AddSimplifiedBinarySearchTree(List<int> nodes)
        {
            var bst = new BinarySearchTree(nodes);

            var simplifiedBst = new SimplifiedBinarySearchTree { Nodes = bst.GetSimplifiedBinarySearchTree() };
            await repository.AddSimplifiedBinarySearchTree(simplifiedBst);
        }

        public async Task AddNodesToTree(SimplifiedBinarySearchTree simplifiedBst, List<int> nodes)
        {
            var bst = simplifiedBst.ToBST();
            bst.AddNodes(nodes);

            simplifiedBst.Nodes = bst.GetSimplifiedBinarySearchTree();

            await repository.UpdateSimplifiedBinarySearchTree(simplifiedBst);
        }

        public async Task<bool> DeleteSimplifiedBinaryTree(string id)
        {
            return await repository.DeleteSimplifiedBinaryTreeById(id);
        }
    }
}