using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Builders.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Builders.Test.Controllers
{
    public class BinarySearchTreeControllerTest : IClassFixture<WebApplicationFactory<Builders.Startup>>
    {
        private readonly WebApplicationFactory<Builders.Startup> factory;

        public BinarySearchTreeControllerTest(WebApplicationFactory<Builders.Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task ShouldBeAbleToGetBinarySearchTreeFromMongoDbWithGivingId()
        {
            #region Arrange
            var client = factory.CreateClient();

            #endregion Arrange

            #region Act
            var response = await client.GetAsync("BinarySearchTree/60ec7faf0f030a719662a8f9");
            
            var actualBst = JsonConvert.DeserializeObject<BinarySearchTree>(await response.Content.ReadAsStringAsync());
            #endregion Act

            #region Assert
            response.EnsureSuccessStatusCode();
            

            Assert.Equal(new BinarySearchTree(), actualBst);
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
            #endregion Assert
        }

        [Fact]
        public async Task ShouldBeAbleToAddBinarySearchTreeInMongoDb()
        {
            #region Arrange
            var client = factory.CreateClient();
            var nodes = new List<int> {
                8, 5, 6, 7, 9, 10
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(nodes), Encoding.UTF8, "application/json");

            var expectedBst = new BinarySearchTree();
            expectedBst.AddNode(nodes);

            var expectedSimplified = expectedBst.GetSimplifiedBst();
            #endregion Arrange

            #region Act
            var response = await client.PostAsync("BinarySearchTree/", httpContent);                    

            var json = await response.Content.ReadAsStringAsync();
            var actualBst = JsonConvert.DeserializeObject<BinarySearchTree>(json);
            var actualSimplified = actualBst.GetSimplifiedBst();
            #endregion Act

            #region Assert            
            response.EnsureSuccessStatusCode();

            Assert.Equal(expectedSimplified, actualSimplified);
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
            #endregion Assert
        }

    }
}