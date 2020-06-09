using System;
using System.Collections.Generic;

namespace Capstone5GC.Models
{
    public partial class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int? ModelYear { get; set; }
        public string Color { get; set; }
    }
}
