using System.Collections.Generic;
using System.Threading.Tasks;
using Builders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Builders.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BinarySearchTreeController : ControllerBase
    {
        private ILogger<BinarySearchTreeController> logger;
        public BinarySearchTreeController(ILogger<BinarySearchTreeController> logger)
        {
            this.logger = logger;
        }

        [HttpGet("{value}")]
        public async Task<ActionResult> Get(int value)
        {
            logger.LogInformation(value.ToString());
            logger.LogInformation("Let's create this tree");
            Node n1 = new Node { Value = 1 };
            Node n3 = new Node { Value = 3 };
            Node n2 = new Node { Value = 2, Left = n1, Right = n3 };

            return Ok(n2.FindWithValue(2));
        }

        [HttpPost]
        public async Task<ActionResult> Post(List<int> values)
        {
            var bst = new BinarySearchTree();
            foreach(int value in values)
            {
                bst.AddNode(value);
            }

            return Ok(bst);
        }
    }
}