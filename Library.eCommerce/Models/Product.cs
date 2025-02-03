using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25App.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Display
        {
            get
            {
                return $"{Id}. {Name}";
            }
        }
        public Product()
        {
            Name = string.Empty;
        }

        public override string ToString()
        {
            return Display ?? string.Empty;
        }
    }
}
