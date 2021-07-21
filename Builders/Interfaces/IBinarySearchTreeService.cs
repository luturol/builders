using System.Collections.Generic;
using System.Threading.Tasks;
using Builders.Models;

namespace Builders.Interfaces
{
    public interface IBinarySearchTreeService
    {
        Task<SimplifiedBinarySearchTree> AddNodesToTree(SimplifiedBinarySearchTree simplifiedBst, List<int> nodes);        
        Task<SimplifiedBinarySearchTree> AddSimplifiedBinarySearchTree(List<int> nodes);        
        Task<bool> DeleteSimplifiedBinaryTree(string id);
        Node FindNodeInsideBst(SimplifiedBinarySearchTree simplified, int value);
        Task<SimplifiedBinarySearchTree> GetSimplifiedBinarySearchTree(string id);
    }
}