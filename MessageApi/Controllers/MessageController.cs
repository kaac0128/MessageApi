using DataAccess;
using Entities;
using MessageApi.Response;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace MessageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _userService;
        private readonly ILogger<MessageController> _logger;

        public MessageController(IMessageService userService, ILogger<MessageController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessageUser([FromBody] MessageEntity user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                _logger.LogWarning("Datos del usuario inválidos recibidos.");
                return BadRequest(new ResponseDataMessage { Success = false, Message = "Datos del usuario inválidos." });
            }

            var result = await _userService.AddUserAsync(user);

            if (result.Success)
            {
                return Ok(new ResponseDataMessage { Success = true, Message = result.Message });
            }

            _logger.LogError("Error al crear el usuario {Name}", user.Name);
            return StatusCode(500, new ResponseDataMessage  { Success = false, Message = result.Message });
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersSentMessage()
        {
            var result = await _userService.GetUsersAsync();

            if (result.Success)
            {
                return Ok(new ResponseDataMessage { Success = true, Message = result.Message, Data = result.Users });
            }

            if (result.Users == null)
            {
                return StatusCode(500, new ResponseDataMessage { Success = false, Message = result.Message });
            }

            return NotFound(new ResponseDataMessage { Success = false, Message = result.Message });
        }
    }
}
