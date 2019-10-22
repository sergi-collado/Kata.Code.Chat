using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kata.Code.Chat.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kata.Code.Chat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IRoom room;

        public MessageController(IRoom room)
        {
            this.room = room;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<V1.Message>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return Ok(room.Messages.Select(m => m.ToDto()));
        }
    }
}