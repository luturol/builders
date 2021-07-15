using System.Collections.Generic;
using System.Threading.Tasks;
using Builders.Models;
using Builders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Builders.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BinarySearchTreeController : ControllerBase
    {
        private readonly ILogger<BinarySearchTreeController> logger;
        private readonly IBinarySearchTreeRepository repository;
        private readonly IBinarySearchTreeService service;
        public BinarySearchTreeController(ILogger<BinarySearchTreeController> logger,
                                          IBinarySearchTreeService service,
                                          IBinarySearchTreeRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
            this.service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            try
            {
                var tree = await repository.GetSimplifiedBinarySearchTree(id);
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
            catch (Exception ex)
            {
                logger.LogInformation(ex, $"Error while trying to get tree by id { id } { ex.Message }");
                return StatusCode(500, new { message = "An internal error has happend. Try again later." });
            }
        }

        [HttpGet("{id}/{value}")]
        public async Task<ActionResult> FindNodeInsideTree(string id, int value)
        {
            var treeSimplified = await repository.GetSimplifiedBinarySearchTree(id);
            logger.LogInformation("Got the tree {treeSimplified}", treeSimplified);
            if (treeSimplified is null)
            {
                return NoContent();
            }
            else
            {
                var bst = new BinarySearchTree(treeSimplified.Nodes);

                return Ok(bst.FindWithValue(value));
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(List<int> values)
        {
            logger.LogInformation("Create a bst and insert into MongoDb");
            var bst = new BinarySearchTree(values);

            var simplifiedBst = new SimplifiedBinarySearchTree { Nodes = bst.GetSimplifiedBinarySearchTree() };
            await repository.AddSimplifiedBinarySearchTree(simplifiedBst);

            return Ok(simplifiedBst);
        }

        [HttpPatch]
        public async Task<ActionResult> Patch(string id, List<int> values)
        {
            var treeSimplified = await repository.GetSimplifiedBinarySearchTree(id);
            logger.LogInformation("Got the tree and will add another values to it with tree is not null {treeSimplified}", treeSimplified);
            if (treeSimplified is not null)
            {
                var bst = new BinarySearchTree(treeSimplified.Nodes);
                bst.AddNodes(values);

                treeSimplified.Nodes = bst.GetSimplifiedBinarySearchTree(); ;

                await repository.UpdateSimplifiedBinarySearchTree(treeSimplified);
                logger.LogInformation("Updated Tree {treeSimplified}", treeSimplified);

                return Ok(treeSimplified);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                if (await service.DeleteSimplifiedBinaryTree(id))
                {
                    return NoContent();
                }                    
                else
                {
                    return BadRequest("It was not possible to delete tree by giving id");
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex, $"Error while trying to delete tree by id { id } { ex.Message }");
                return StatusCode(500, new { message = "An internal error has happend. Try again later." });
            }
        }
    }
}