using System.Threading.Tasks;
using Builders.Models;

namespace Builders.Interfaces
{
    public interface IBinarySearchTreeRepository
    {
        Task<BinarySearchTree> GetBinarySearchTree();
        Task AddBinarySearchTree(BinarySearchTree bst);
        Task UpdateBinarySearchTree(BinarySearchTree bst);
    }
}