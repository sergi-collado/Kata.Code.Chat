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
        private readonly IMessageRepository messageRepository;

        public MessageController(ILogger<MessageController> logger, IRoom room, IMessageRepository messageRepository)
        {
            this.logger = logger;
            this.room = room;
            this.messageRepository = messageRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<V1.Message>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var messages = await messageRepository.GetMessages();
            room.AddMessages(messages.ToList());
            logger.LogInformation("This is the {number} log", "first");
            return Ok(room.Messages.ConvertAll(m => m.ToDto()));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] V1.Message message)
        {
            await messageRepository.SaveMessage(message.ToEntity());
            return Created("api/[controller]", message);
        }
    }
}