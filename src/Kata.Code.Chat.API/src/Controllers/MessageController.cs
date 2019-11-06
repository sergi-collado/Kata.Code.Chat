using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kata.Code.Chat.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kata.Code.Chat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> logger;
        private readonly IRoom room;

        public MessageController(ILogger<MessageController> logger, IRoom room)
        {
            this.logger = logger;
            this.room = room;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<V1.Message>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            logger.LogInformation("This is the {number} log", "first");
            return Ok(room.Messages.Select(m => m.ToDto()));
        }
    }
}