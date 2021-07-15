using System.Threading.Tasks;
using Builders.Models;

namespace Builders.Interfaces
{
    public interface IBinarySearchTreeRepository
    {
        Task AddSimplifiedBinarySearchTree(SimplifiedBinarySearchTree bst);
        Task<bool> DeleteSimplifiedBinaryTreeById(string id);
        Task<SimplifiedBinarySearchTree> GetSimplifiedBinarySearchTree(string id);
        Task UpdateSimplifiedBinarySearchTree(SimplifiedBinarySearchTree bst);
    }
}