using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Builders.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Builders.Integration.Test.Controllers
{
    public class BinarySearchTreeControllerTest : IClassFixture<WebApplicationFactory<Builders.Startup>>
    {
        private readonly WebApplicationFactory<Builders.Startup> factory;

        public BinarySearchTreeControllerTest(WebApplicationFactory<Builders.Startup> factory)
        {
            this.factory = factory;
        }

        #region Get Test
        [Fact]
        public async Task ShouldBeAbleToGetBinarySearchTreeFromMongoDbWithGivingValidId()
        {
            #region Arrange
            var client = factory.CreateClient();
            var expectedSimplifiedBst = await AddSimplifiedBst();
            #endregion Arrange

            #region Act
            var response = await client.GetAsync("BinarySearchTree/" + expectedSimplifiedBst.Id);

            var actualBst = JsonConvert.DeserializeObject<SimplifiedBinarySearchTree>(await response.Content.ReadAsStringAsync());
            #endregion Act

            #region Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(expectedSimplifiedBst.Id, actualBst.Id);
            Assert.Equal(expectedSimplifiedBst.Nodes.Count, actualBst.Nodes.Count);
            Assert.True(actualBst.Nodes.SequenceEqual(expectedSimplifiedBst.Nodes));
            #endregion Assert
        }

        [Fact]
        public async Task ShouldBeAbleToGetNoContentByGivingInvalidId()
        {
            #region Arrange
            var client = factory.CreateClient();
            var fakeId = "60edef20784d79c6f17e2a9e";
            var expectedStatusCode = (int)HttpStatusCode.NoContent;
            #endregion Arrange

            #region Act
            var response = await client.GetAsync("BinarySearchTree/" + fakeId);
            var actualStatusCode = (int)response.StatusCode;
            #endregion Act

            #region Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(expectedStatusCode, actualStatusCode);
            #endregion Assert
        }
        #endregion Get Simplified Binary Search Tree Test

        #region Post
        [Fact]
        public async Task ShouldBeAbleToAddBinarySearchTreeInMongoDb()
        {
            #region Arrange
            var client = factory.CreateClient();
            var nodes = new List<int> {
                8, 5, 6, 7, 9, 10
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(nodes), Encoding.UTF8, "application/json");

            var expectedBst = new BinarySearchTree(nodes);

            var expectedSimplified = expectedBst.GetSimplifiedBinarySearchTree();
            #endregion Arrange

            #region Act
            var response = await client.PostAsync("BinarySearchTree/", httpContent);

            var json = await response.Content.ReadAsStringAsync();
            var actualSimplified = JsonConvert.DeserializeObject<SimplifiedBinarySearchTree>(json);
            var actualBst = new BinarySearchTree(actualSimplified.Nodes);
            #endregion Act

            #region Assert            
            response.EnsureSuccessStatusCode();
            Assert.True(actualSimplified.Nodes.SequenceEqual(expectedSimplified));
            Assert.True(actualBst.IsBst());
            #endregion Assert
        }
        #endregion Post

        #region Get Find Node by Value 
        [Fact]
        public async Task ShouldBeAbleToFindNodeByGivingValue()
        {
            #region Arrange
            var client = factory.CreateClient();
            var expectedSimplifiedBst = await AddSimplifiedBst();
            var bst = new BinarySearchTree(expectedSimplifiedBst.Nodes);
            var expectedNodeValue = expectedSimplifiedBst.Nodes.First();
            var expectedNode = bst.FindWithValue(expectedNodeValue);

            #endregion Arrange

            #region Act
            var response = await client.GetAsync("BinarySearchTree/" + expectedSimplifiedBst.Id + "/" + expectedNodeValue);

            var actualNode = JsonConvert.DeserializeObject<Node>(await response.Content.ReadAsStringAsync());
            #endregion Act

            #region Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(actualNode);
            Assert.Equal(expectedNodeValue, actualNode.Value);
            #endregion Assert
        }
        #endregion

        #region Delete Tests
        [Fact]
        public async Task ShouldBeAbleToDeleteSimplifiedBstByGivingId()
        {
            #region Arrange
            var client = factory.CreateClient();
            var expectedSimplifiedBst = await AddSimplifiedBst();
            var expectedStatusCode = (int) HttpStatusCode.NoContent;     
            #endregion Arrange

            #region Act
            var response = await client.DeleteAsync("BinarySearchTree/" + expectedSimplifiedBst.Id);
            var actualStatusCode = (int) response.StatusCode;

            var responseGet = await client.GetAsync("BinarySearchTree/" + expectedSimplifiedBst.Id);
            var actualStatusCodeGet = (int) responseGet.StatusCode;
            #endregion Act

            #region Assert
            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.Equal(expectedStatusCode, actualStatusCodeGet);
            #endregion Assert
        }

        #endregion
        private async Task<SimplifiedBinarySearchTree> AddSimplifiedBst()
        {
            var nodes = new List<int> { 3, 2, 4, 5, 6, 7, 9, 44, 10, 1, 0 };

            var client = factory.CreateClient();

            var httpContent = new StringContent(JsonConvert.SerializeObject(nodes), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("BinarySearchTree/", httpContent);
            var json = await response.Content.ReadAsStringAsync();

            var createdBst = JsonConvert.DeserializeObject<SimplifiedBinarySearchTree>(json);

            return createdBst;
        }
    }
}