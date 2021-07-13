using Builders.Models;

namespace Builders.Extensions
{
    public static class SimplifiedBinarySearchTreeExtension
    {
        public static BinarySearchTree ToBST(this SimplifiedBinarySearchTree simplified)
        {
            return new BinarySearchTree(simplified.Nodes);   
        }
    }
}