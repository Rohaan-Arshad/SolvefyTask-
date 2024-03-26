using System;
using System.Collections.Generic;

namespace Solvefy_Task.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Price { get; set; }
        public string Description { get; set; } = null!;
        public string? Category { get; set; }
    }
}
