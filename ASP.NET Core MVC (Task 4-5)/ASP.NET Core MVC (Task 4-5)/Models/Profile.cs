using System;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_MVC__Task_4_5_.Models
{
    public class Profile
    {
        public Guid ProfileId { get; set; }
        [Required]
        public string Name { get; init; }
        [Required]
        public string Email { get; init; }
        [Required]
        public string Address { get; init; }
    }
}
