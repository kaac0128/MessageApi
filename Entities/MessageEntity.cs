using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class MessageEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "El numero de telefono es obligatorio.")]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
