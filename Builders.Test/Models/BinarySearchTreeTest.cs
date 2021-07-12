using Builders.Models;
using Xunit;

namespace Builders.Test.Models
{
    public class BinarySearchTreeTest
    {
        [Fact]
        public void ShouldBeAbleToAddInTheRightPositionOfTreeRootGivingValueBiggerThanTheRootValue()
        {
            #region Arrange
            var bst = new BinarySearchTree();
            bst.AddNode(1);
            bst.AddNode(2);

            var expectedValue = 2;
            #endregion Arrange

            #region Act
            var rightNode = bst.Root.Right;
            #endregion Act

            #region Assert
            Assert.NotNull(rightNode);
            Assert.Equal(rightNode.Value, expectedValue);
            #endregion Assert
        }

        [Fact]
        public void ShouldBeAbleToAddInTheLeftPositionOfTreeRootGivingValueLesThanTheRootValue()
        {
            #region Arrange
            var bst = new BinarySearchTree();
            bst.AddNode(3);
            bst.AddNode(2);

            var expectedValue = 2;
            #endregion Arrange

            #region Act
            var leftNode = bst.Root.Left;
            #endregion Act

            #region Assert
            Assert.NotNull(leftNode);
            Assert.Equal(leftNode.Value, expectedValue);
            #endregion Assert
        }

        public void ShouldBeAbleToAddInTheRootPositionGivingRootIsNull()
        {
            #region Arrange
            var bst = new BinarySearchTree();
            bst.AddNode(3);

            var expectedValue = 3;
            #endregion Arrange

            #region Act
            var rootNode = bst.Root;
            #endregion Act

            #region Assert
            Assert.NotNull(rootNode);
            Assert.Equal(rootNode.Value, expectedValue);
            #endregion Assert
        }
    }
}