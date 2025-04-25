//Wyatt Granlund
//COP 4870

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring2025_Samples.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } 

        public string? Display => $"{Id}. {Name} - ${Price:F2} (Qty: {Quantity})";

        public override string ToString()
        {
            return Display ?? string.Empty;
        }

        public Product()
        {
            Name = string.Empty;
        }
    }
}
