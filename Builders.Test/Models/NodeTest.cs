using Xunit;
using Builders.Models;

namespace Builders.Test.Models
{
    public class NodeTest
    {
        [Fact]
        public void ShouldBeAbleToFindNodeThatHasGivingValue()
        {
            #region Arrange
            Node n1 = new Node { Value = 1 };
            Node expected = new Node { Value = 3 };
            Node n2 = new Node { Value = 2, Left = n1, Right = expected };
            #endregion Arrange

            #region Act
            var actual = n2.FindWithValue(3);
            #endregion

            #region Assert
            Assert.Equal(expected, actual);
            #endregion Assert
        }

        [Fact]
        public void ShouldBeAbleToFindRootNodeThatHasGivingValue()
        {
            #region Arrange
            Node n1 = new Node { Value = 1 };
            Node n3 = new Node { Value = 3 };
            Node expected = new Node { Value = 2, Left = n1, Right = n3 };
            #endregion Arrange

            #region Act
            var actual = expected.FindWithValue(2);
            #endregion

            #region Assert
            Assert.Equal(expected, actual);
            #endregion Assert
        }

        [Fact]
        public void ShouldBeAbleToReturnNullWithDoesNotFoundNodeWithGivingValue()
        {
             #region Arrange
            Node n1 = new Node { Value = 1 };
            Node n3 = new Node { Value = 3 };
            Node n2 = new Node { Value = 2, Left = n1, Right = n3 };
            #endregion Arrange

            #region Act
            var actual = n2.FindWithValue(0);
            #endregion

            #region Assert
            Assert.Null(actual);
            #endregion Assert
        }
    }
}