using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Builders.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PalindromeController : ControllerBase
    {
        private ILogger<PalindromeController> logger;

        public PalindromeController(ILogger<PalindromeController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        public ActionResult Post(string word)
        {
            word = word.ToLower();
            char[] wordChar = word.ToCharArray();
            Array.Reverse(wordChar);

            var reversedWord = new string(wordChar);

            bool isPalindrome = reversedWord == word;            

            return Ok(new { isPalindrome, word, reversedWord });                
        }
    }
}