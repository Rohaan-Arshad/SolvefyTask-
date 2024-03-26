using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Solvefy_Task.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int? Age { get; set; }
        [Required]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; } = null!;
    }
}
