using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Builders.Interfaces;
using Builders.Models;
using Builders.Services;
using MongoDB.Bson;
using Moq;
using Xunit;

namespace Builders.Test.Services
{
    public class BinarySearchTreeServiceTest
    {

        private readonly BinarySearchTreeService service;
        private readonly Mock<IBinarySearchTreeRepository> repository;
        public BinarySearchTreeServiceTest()
        {
            repository = new Mock<IBinarySearchTreeRepository>();
            service = new BinarySearchTreeService(repository.Object);
        }

        [Fact]
        public async Task ShouldBeAbleToGetBinarySearchTreeByGivingId()
        {
            #region Arrange
            repository.Setup(e => e.GetSimplifiedBinarySearchTree(It.IsAny<string>()))
                .Returns(Task.FromResult(new SimplifiedBinarySearchTree()));
            var validId = new ObjectId().ToString();

            #endregion Arrange

            #region Act
            var actual = await service.GetSimplifiedBinarySearchTree(validId);
            #endregion Act

            #region Assert
            Assert.NotNull(actual);
            #endregion Assert
        }

        [Fact]
        public async Task ShouldBeAbleToAddNodesToASimplifiedBstByGivingId()
        {
            #region Arrange
            repository.Setup(e => e.UpdateSimplifiedBinarySearchTree(It.IsAny<SimplifiedBinarySearchTree>()))
                .Returns(Task.FromResult(true));

            var nodes = new List<int> { 6, 5, 4, 8, 7, 9, 99, 88, 10, 3, 2 };
            var simplfiedNodes = new BinarySearchTree(nodes).GetSimplifiedBinarySearchTree();
            var bst = new SimplifiedBinarySearchTree { Nodes = new List<int>() };

            #endregion Arrange

            #region Act
            var actual = await service.AddNodesToTree(bst, nodes);

            #endregion Act

            #region Assert
            Assert.NotNull(actual);
            Assert.True(actual.Nodes.SequenceEqual(simplfiedNodes));
            #endregion Assert
        }

        [Fact]
        public async Task ShouldNotBeAbleToAddMoreNodesGivingDuplicateNodes()
        {
            #region Arrange
            repository.Setup(e => e.UpdateSimplifiedBinarySearchTree(It.IsAny<SimplifiedBinarySearchTree>()))
                .Returns(Task.FromResult(true));

            var nodes = new List<int> { 6, 5, 4, 8, 7, 9, 99, 88, 10, 3, 2 };
            var simplfiedNodes = new BinarySearchTree(nodes).GetSimplifiedBinarySearchTree();
            var bst = new SimplifiedBinarySearchTree { Nodes = simplfiedNodes };

            #endregion Arrange

            #region Act
            var actual = await service.AddNodesToTree(bst, nodes);

            #endregion Act

            #region Assert
            Assert.NotNull(actual);
            Assert.True(actual.Nodes.SequenceEqual(simplfiedNodes));
            #endregion Assert
        }

        [Fact]
        public void ShouldBeAbleToFindNodeGivingValueIsInsideTree()
        {
            #region Arrange
            var nodes = new List<int> { 6, 5, 4, 8, 7, 9, 99, 88, 10, 3, 2 };
            var bst = new BinarySearchTree(nodes);
            var expectedNode = bst.FindWithValue(6);

            var simplfiedNodes = bst.GetSimplifiedBinarySearchTree();
            var simplifiedBst = new SimplifiedBinarySearchTree { Nodes = simplfiedNodes };
            #endregion Arrange

            #region Act
            var actual = service.FindNodeInsideBst(simplifiedBst, 6);

            #endregion Act

            #region Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedNode.Value, actual.Value);
            Assert.Equal(expectedNode.Left.Value, actual.Left.Value);
            Assert.Equal(expectedNode.Right.Value, actual.Right.Value);
            #endregion Assert
        }
    }
}