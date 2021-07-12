using System.Collections.Generic;
using Builders.Models;
using Xunit;

namespace Builders.Test.Models
{
    public class BinarySearchTreeTest
    {
        /*
            #region Arrange
            #endregion Arrange

            #region Act

            #endregion Act

            #region Assert
            #endregion Assert
        */

        #region Add Node Test Methods
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

        [Fact]
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
        #endregion Add Node Test Methods

        #region FindWithValue Test Methods
        [Fact]
        public void ShouldBeAbleToFindNodeThatHasGivingValue()
        {
            #region Arrange
            BinarySearchTree bst = new BinarySearchTree();
            bst.AddNode(1);
            bst.AddNode(3);
            bst.AddNode(2);

            var expectedValue = 3;
            #endregion Arrange

            #region Act
            var actualNode = bst.FindWithValue(expectedValue);
            var actualValue = actualNode.Value;
            #endregion

            #region Assert            
            Assert.Equal(expectedValue, actualValue);
            #endregion Assert
        }

        [Fact]
        public void ShouldBeAbleToFindRootNodeThatHasGivingValue()
        {
            #region Arrange
            BinarySearchTree bst = new BinarySearchTree();
            bst.AddNode(1);
            bst.AddNode(3);
            bst.AddNode(2);

            var expectedValue = 2;
            #endregion Arrange

            #region Act
            var actualNode = bst.FindWithValue(2);
            var actualValue = actualNode.Value;
            #endregion

            #region Assert
            Assert.Equal(expectedValue, actualValue);
            #endregion Assert
        }

        [Fact]
        public void ShouldBeAbleToReturnNullWithDoesNotFoundNodeWithGivingValue()
        {
            #region Arrange
            BinarySearchTree bst = new BinarySearchTree();
            bst.AddNode(1);
            bst.AddNode(3);
            bst.AddNode(2);
            #endregion Arrange

            #region Act
            var actual = bst.FindWithValue(0);
            #endregion

            #region Assert
            Assert.Null(actual);
            #endregion Assert
        }
        #endregion FindWithValue Test Methods

        #region Get Simplified Bst Test Methods
        [Fact]
        public void ShouldBeAbleToGetSimplifiedBstGivingBstWithOneNode()
        {
            #region Arrange
            var bst = new BinarySearchTree();
            bst.AddNode(1);
            var expected = new List<int> { 1 };
            #endregion Arrange

            #region Act
            var actual = bst.GetSimplifiedBst();
            #endregion Act

            #region Assert
            Assert.Equal(expected, actual);
            #endregion Assert
        }

        [Fact]
        public void ShouldBeAbleToGetSimplifiedBstGivingBstWithMoreThanOneChildNode()
        {
            #region Arrange
            var expected = new List<int> { 1, 3, 2, 5, 4, 6, 7, 8 };
            var bst = new BinarySearchTree();
            bst.AddNode(expected);

            #endregion Arrange

            #region Act
            var actual = bst.GetSimplifiedBst();
            #endregion Act

            #region Assert
            Assert.Equal(expected, actual);
            #endregion Assert
        }

        [Fact]
        public void ShouldBeAbletToGetSimplifiedBstGivingEmptyBst()
        {
            #region Arrange
            var expected = new List<int>();
            var bst = new BinarySearchTree();
            bst.AddNode(expected);

            #endregion Arrange

            #region Act
            var actual = bst.GetSimplifiedBst();
            #endregion Act

            #region Assert
            Assert.Equal(expected, actual);
            #endregion Assert
        }
        #endregion Get Simplified Bst Test Methods
    }
}