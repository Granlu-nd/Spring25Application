//Wyatt Granlund
//COP 4870

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Spring2025_Samples.Models;

namespace Library.eCommerce.Services
{
    public class CartService
    {
        private List<Product> cartItems;
        private const decimal TaxRate = 0.07m;

        public CartService()
        {
            cartItems = new List<Product>();
        }

        public List<Product> Items => cartItems;

        public void AddToCart(Product product, int quantity)
        {
            var existing = cartItems.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                cartItems.Add(new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = quantity
                });
            }
        }

        public bool RemoveFromCart(int id, int quantity)
        {
            var item = cartItems.FirstOrDefault(p => p.Id == id);
            if (item == null || item.Quantity < quantity) return false;

            item.Quantity -= quantity;
            if (item.Quantity == 0) cartItems.Remove(item);
            return true;
        }

        public string Checkout()
        {
            decimal subtotal = cartItems.Sum(i => i.Price * i.Quantity);
            decimal tax = subtotal * TaxRate;
            decimal total = subtotal + tax;

            string receipt = "\n--- MillsMart Receipt ---\n";
            foreach (var item in cartItems)
            {
                receipt += $"{item.Quantity}x {item.Name} @ ${item.Price:F2} = ${item.Price * item.Quantity:F2}\n";
            }

            receipt += $"\nSubtotal: ${subtotal:F2}\nTax (7%): ${tax:F2}\nTotal: ${total:F2}\n";
            receipt += "\nThanks for shopping with MillsMart!\n";

            cartItems.Clear();
            return receipt;
        }
    }
}
