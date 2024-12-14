using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MessageService : IMessageService
    {
        private readonly MessageDbContext _context;
        private readonly ILogger<MessageService> _logger;

        public MessageService(MessageDbContext context, ILogger<MessageService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(bool Success, string Message)> AddUserAsync(MessageEntity user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Mensaje de bienvenida enviado a {Name} al {PhoneNumber}", user.Name, user.PhoneNumber);

                return (true, $"Mensaje de bienvenida enviado a {user.Name} al {user.PhoneNumber}.");
            }
            catch (Exception ex)
            {
  
                _logger.LogError(ex, "Error al agregar el usuario {Name}", user.Name);
                return (false, "Ocurrió un error al agregar el usuario.");
            }
        }

        public async Task<(bool Success, string Message, List<MessageEntity> Users)> GetUsersAsync()
        {
            try
            {
                var users = await Task.FromResult(_context.Users.ToList());

                if (users.Any())
                {
                    return (true, "Usuarios recuperados exitosamente.", users);
                }

                return (false, "No se encontraron usuarios.", new List<MessageEntity>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al recuperar los usuarios.");
                return (false, "Ocurrió un error al recuperar los usuarios.", null);
            }
        }
    }
}
