using System.Collections.Generic;
using System.Linq;
using Builders.Extensions;
using Builders.Models;
using Xunit;

namespace Builders.Test.Extensions
{
    public class SimplifiedBinarySearchTreeExtensionTest
    {
        [Fact]
        public void ShouldBeAbleToTransformSimplifiedInABinarySearchTree()
        {
            #region Arrange
            var nodes = new List<int> { 2, 3, 1};
            var bst = new BinarySearchTree(nodes);
            var expectedSimplified = bst.GetSimplifiedBinarySearchTree();            
            #endregion Arrange

            #region Act
            var actualSimplifiedBst = new SimplifiedBinarySearchTree { Nodes = nodes};
            var actualBst = actualSimplifiedBst.ToBST();
            var actualNodes = actualBst.GetSimplifiedBinarySearchTree();
            #endregion Act

            #region Assert
            Assert.NotNull(actualSimplifiedBst);
            Assert.NotNull(actualBst);
            Assert.NotNull(actualBst.Root);
            Assert.NotNull(actualNodes);
            Assert.True(actualNodes.SequenceEqual(expectedSimplified));
            Assert.True(actualBst.IsBst());
            #endregion Assert
        }
    }
}