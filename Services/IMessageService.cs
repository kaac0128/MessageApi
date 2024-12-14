using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IMessageService
    {
        Task<(bool Success, string Message)> AddUserAsync(MessageEntity user);
        Task<(bool Success, string Message, List<MessageEntity> Users)> GetUsersAsync();
    }
}
