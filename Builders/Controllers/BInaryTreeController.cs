using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Builders.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BinaryTreeController : ControllerBase
    {
        private ILogger<BinaryTreeController> logger;
        public BinaryTreeController(ILogger<BinaryTreeController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            logger.LogInformation("Let's create this tree");
            return Ok();
        }
    }
}