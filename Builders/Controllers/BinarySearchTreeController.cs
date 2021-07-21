using System.Collections.Generic;
using System.Threading.Tasks;
using Builders.Models;
using Builders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using MongoDB.Bson;
using Builders.Validations;
using Builders.Extensions;
using System.Net;

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
                var invalidObjectValidation = IsValidObjectId(id);
                if (invalidObjectValidation is not null)
                    return invalidObjectValidation;

                var tree = await service.GetSimplifiedBinarySearchTree(id);
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
            try
            {
                var invalidObjectValidation = IsValidObjectId(id);
                if (invalidObjectValidation is not null)
                    return invalidObjectValidation;

                var treeSimplified = await service.GetSimplifiedBinarySearchTree(id);
                if (treeSimplified is null)
                {
                    return NoContent();
                }
                else
                {
                    logger.LogInformation("Got the tree {treeSimplified}", treeSimplified);
                    return Ok(service.FindNodeInsideBst(treeSimplified, value));
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex, $"Error while trying to find node inside tree with id { id } and value { value } { ex.Message }");
                return StatusCode(500, new { message = "An internal error has happend. Try again later." });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(List<int> values)
        {
            try
            {
                logger.LogInformation("Create a bst and insert into MongoDb");
                var simplifiedBst = await service.AddSimplifiedBinarySearchTree(values);

                return CreatedAtAction(nameof(Get), new { id = simplifiedBst.Id }, simplifiedBst);
            }
            catch(Exception ex)
            {
                logger.LogInformation(ex, $"Error while trying to add node with values { values } { ex.Message }");
                return StatusCode(500, new { message = "An internal error has happend. Try again later." });
            }

        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(string id, List<int> values)
        {
            try
            {
                var invalidObjectValidation = IsValidObjectId(id);
                if (invalidObjectValidation is not null)
                    return invalidObjectValidation;

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
            catch (Exception ex)
            {
                logger.LogInformation(ex, $"Error while trying to patch tree with id { id } and add values { values } { ex.Message }");
                return StatusCode(500, new { message = "An internal error has happend. Try again later." });
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var invalidObjectValidation = IsValidObjectId(id);
                if (invalidObjectValidation is not null)
                    return invalidObjectValidation;

                await service.DeleteSimplifiedBinaryTree(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex, $"Error while trying to delete tree by id { id } { ex.Message }");
                return StatusCode(500, new { message = "An internal error has happend. Try again later." });
            }
        }

        private BadRequestObjectResult IsValidObjectId(string id)
        {
            var objectIdValidation = new ObjectIdValidation();
            var resultValidation = objectIdValidation.Validate(id);
            if (!resultValidation.IsValid)
            {
                resultValidation.Errors.ForEach(e => logger.LogInformation($"Invalid value for {e.PropertyName } error message { e.ErrorMessage }"));
                return BadRequest(resultValidation.ToProblemDetails(HttpStatusCode.BadRequest));
            }
            else
            {
                return null;
            }
        }
    }
}