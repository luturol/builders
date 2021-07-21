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
    }
}