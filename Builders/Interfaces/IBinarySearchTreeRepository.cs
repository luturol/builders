using System.Collections.Generic;
using System.Threading.Tasks;
using Builders.Models;

namespace Builders.Interfaces
{
    public interface IBinarySearchTreeRepository
    {
        Task<SimplifiedBinarySearchTree> GetSimplifiedBinarySearchTree(string id);
        Task AddSimplifiedBinarySearchTree(SimplifiedBinarySearchTree bst);
        Task UpdateSimplifiedBinarySearchTree(SimplifiedBinarySearchTree bst);
    }
}