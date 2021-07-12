using System.Collections.Generic;
using System.Threading.Tasks;
using Builders.Models;
using Builders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace Builders.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BinarySearchTreeController : ControllerBase
    {
        private ILogger<BinarySearchTreeController> logger;
        private IBinarySearchTreeRepository repository;
        public BinarySearchTreeController(ILogger<BinarySearchTreeController> logger,
                                          IBinarySearchTreeRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var tree = await repository.GetBinarySearchTree(id);
            if (tree is null)
            {
                logger.LogInformation("No tree found with giving id {id}", id);
                return NoContent();
            }
            else
            {                
                return Ok(tree);
            }

        }

        [HttpGet("{id}/{value}")]
        public async Task<ActionResult> FindNodeInSideTree(string id, int value)
        {
            var treeSimplified = await repository.GetBinarySearchTree(id);
            logger.LogInformation("Got the first tree {treeSimplified}", treeSimplified);
            if (treeSimplified is null)
            {
                return NoContent();
            }
            else
            {
                var bst = new BinarySearchTree();
                bst.AddNode(treeSimplified.Nodes);

                return Ok(bst.FindWithValue(value));
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(List<int> values)
        {
            logger.LogInformation("Create a bst and insert into MongoDb");
            var bst = new BinarySearchTree();
            foreach (int value in values)
            {
                bst.AddNode(value);
            }

            await repository.AddBinarySearchTree(bst.GetSimplifiedBst());
            return Ok(bst);
        }

        [HttpPatch]
        public async Task<ActionResult> Patch(string id, List<int> values)
        {
            var treeSimplified = await repository.GetBinarySearchTree(id);
            logger.LogInformation("Got the tree and will add another values to it with tree is not null {treeSimplified}", treeSimplified);
            if (treeSimplified is not null)
            {
                var bst = new BinarySearchTree();
                bst.AddNode(treeSimplified.Nodes);
                bst.AddNode(values);

                var updatedBst = bst.GetSimplifiedBst();
                treeSimplified.Nodes = updatedBst.Nodes;
                
                await repository.UpdateBinarySearchTree(treeSimplified);
                logger.LogInformation("Updated Tree {treeSimplified}", treeSimplified);

                return Ok(treeSimplified);
            }

            return NoContent();
        }
    }
}