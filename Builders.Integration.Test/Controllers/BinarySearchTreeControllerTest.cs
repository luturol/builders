using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Builders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using MongoDB.Bson;
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
        public async Task ShouldBeAbleToGetNoContentByGivingWrongId()
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

        [Fact]
        public async Task ShouldNotBeAbleToGetTreeByGivingInvalidIdFormat()
        {
            #region Arrange
            var client = factory.CreateClient();
            var fakeId = "asdasdasdas";
            var expectedStatusCode = (int)HttpStatusCode.BadRequest;
            #endregion Arrange

            #region Act
            var response = await client.GetAsync("BinarySearchTree/" + fakeId);
            var actualStatusCode = (int)response.StatusCode;

            var json = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<ProblemDetails>(json);
            #endregion Act

            #region Assert            
            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.NotNull(responseObject);
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

        [Fact]
        public async Task ShouldNotBeAbleToFindNodeInsideTreeByGivingInvalidId()
        {
            #region Arrange
            var client = factory.CreateClient();
            var expectedStatusCode = (int)HttpStatusCode.BadRequest;
            var invalidId = "asdasdas";
            var randomValue = 2;
            #endregion Arrange

            #region Act
            var response = await client.GetAsync("BinarySearchTree/" + invalidId + "/" + randomValue);

            var json = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<ProblemDetails>(json);
            var actualStatusCode = responseObject.Status;
            #endregion Act

            #region Assert
            Assert.Equal(expectedStatusCode, (int)response.StatusCode);
            Assert.NotNull(responseObject);
            Assert.Equal(expectedStatusCode, actualStatusCode);
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
            var expectedStatusCode = (int)HttpStatusCode.NoContent;
            #endregion Arrange

            #region Act
            var response = await client.DeleteAsync("BinarySearchTree/" + expectedSimplifiedBst.Id);
            var actualStatusCode = (int)response.StatusCode;

            var responseGet = await client.GetAsync("BinarySearchTree/" + expectedSimplifiedBst.Id);
            var actualStatusCodeGet = (int)responseGet.StatusCode;
            #endregion Act

            #region Assert
            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.Equal(expectedStatusCode, actualStatusCodeGet);
            #endregion Assert
        }
        #endregion Delete Tests

        #region Patch Tests
        [Fact]
        public async Task ShouldBeAbleToAddMoreNodesToTreeByGivingId()
        {
            #region Arrange
            var client = factory.CreateClient();
            var expectedStatusCode = (int)HttpStatusCode.OK;
            var expectedSimplifiedBst = await AddSimplifiedBst();

            var expectedNodeValue = 49;
            var nodes = new List<int> { 45, 46, 90, 89, expectedNodeValue };

            var httpContent = new StringContent(JsonConvert.SerializeObject(nodes), Encoding.UTF8, "application/json");
            #endregion Arrange

            #region Act
            var response = await client.PatchAsync("BinarySearchTree/" + expectedSimplifiedBst.Id, httpContent);
            var actualStatusCode = (int)response.StatusCode;

            var json = await response.Content.ReadAsStringAsync();
            var actualSimplifiedTree = JsonConvert.DeserializeObject<SimplifiedBinarySearchTree>(json);

            var bst = new BinarySearchTree(actualSimplifiedTree.Nodes);
            var actualNode = bst.FindWithValue(expectedNodeValue);
            #endregion Act

            #region Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.NotNull(actualNode);
            Assert.True(bst.IsBst());
            Assert.Equal(expectedNodeValue, actualNode.Value);
            #endregion Assert
        }

        [Fact]
        public async Task ShouldNotBeAbleToAddMoreNodesGivingWrongId()
        {
            #region Arrange
            var client = factory.CreateClient();
            var expectedStatusCode = (int)HttpStatusCode.NoContent;

            var newId = new ObjectId().ToString();
            var nodes = new List<int> { 45, 46, 90, 89, 49 };

            var httpContent = new StringContent(JsonConvert.SerializeObject(nodes), Encoding.UTF8, "application/json");
            #endregion Arrange

            #region Act
            var response = await client.PatchAsync("BinarySearchTree/" + newId, httpContent);
            var actualStatusCode = (int)response.StatusCode;
            #endregion Act

            #region Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(expectedStatusCode, actualStatusCode);
            #endregion Assert
        }

        [Fact]
        public async Task ShouldNotBeAbleToAddMoreNodesGivingInvalidId()
        {
            #region Arrange
            var client = factory.CreateClient();
            var expectedStatusCode = (int)HttpStatusCode.BadRequest;

            var invalidId = "asdasd";
            var nodes = new List<int> { 45, 46, 90, 89, 49 };

            var httpContent = new StringContent(JsonConvert.SerializeObject(nodes), Encoding.UTF8, "application/json");
            #endregion Arrange

            #region Act
            var response = await client.PatchAsync("BinarySearchTree/" + invalidId, httpContent);
            var actualStatusCode = (int)response.StatusCode;

            var json = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<ProblemDetails>(json);
            var actualProblemStatusCode = responseObject.Status;
            #endregion Act

            #region Assert            
            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.NotNull(responseObject);
            Assert.Equal(expectedStatusCode, actualProblemStatusCode);
            #endregion Assert
        }

        #endregion Patch Tests
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