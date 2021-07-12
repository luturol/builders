using System.Collections.Generic;
using System.Threading.Tasks;
using Builders.Models;

namespace Builders.Interfaces
{
    public interface IBinarySearchTreeRepository
    {
        Task<SimplifiedBinarySearchTree> GetBinarySearchTree(string id);
        Task AddBinarySearchTree(SimplifiedBinarySearchTree simplifiedBst);
        Task UpdateBinarySearchTree(SimplifiedBinarySearchTree bst);
    }
}