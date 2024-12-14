using Entities;
using Microsoft.AspNetCore.Mvc;

namespace MessageApi.Response
{
    public class ResponseDataMessage
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<MessageEntity>? Data { get; set; }

    }
}
