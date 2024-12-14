using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MessageDbContext : DbContext
    {
        public DbSet<MessageEntity> Users { get; set; }

        public MessageDbContext(DbContextOptions<MessageDbContext> options)
        : base(options) { }
    }
}
