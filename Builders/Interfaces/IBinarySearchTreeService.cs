using System.Collections.Generic;
using System.Threading.Tasks;
using Builders.Models;

namespace Builders.Interfaces
{
    public interface IBinarySearchTreeService
    {
        Task<SimplifiedBinarySearchTree> GetSimplifiedBinarySearchTree(string id);
        Task AddSimplifiedBinarySearchTree(List<int> nodes);
        Task AddNodesToTree(SimplifiedBinarySearchTree simplifiedBst, List<int> nodes);
        Task<bool> DeleteSimplifiedBinaryTree(string id);
        
    }
}