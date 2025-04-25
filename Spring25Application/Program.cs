//Wyatt Granlund
//COP 4870

using Library.eCommerce.Services;
using Spring2025_Samples.Models;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inventory = ProductServiceProxy.Current;
            var cart = new CartService();

            Console.WriteLine("Welcome to MillsMart!");

            while (true)
            {
                Console.WriteLine("\nMain Menu:");
                Console.WriteLine("(I) Manage Inventory");
                Console.WriteLine("(S) Shopping Cart");
                Console.WriteLine("(C) Checkout & Exit");
                Console.Write("Choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "I":
                    case "i":
                        ManageInventory(inventory);
                        break;

                    case "S":
                    case "s":
                        ManageCart(inventory, cart);
                        break;

                    case "C":
                    case "c":
                        Console.WriteLine(cart.Checkout());
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void ManageInventory(ProductServiceProxy inventory)
        {
            while (true)
            {
                Console.WriteLine("\nInventory Menu:");
                Console.WriteLine("(C) Create Product");
                Console.WriteLine("(V) View Products");
                Console.WriteLine("(U) Update Product");
                Console.WriteLine("(D) Delete Product");
                Console.WriteLine("(R) Return to Main Menu");
                Console.Write("Choice: ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "C":
                    case "c":
                        var p = new Product();

                        Console.Write("Product Name: ");
                        p.Name = Console.ReadLine();

                        Console.Write("Price: ");
                        decimal.TryParse(Console.ReadLine(), out decimal price);
                        p.Price = price;

                        Console.Write("Quantity: ");
                        int.TryParse(Console.ReadLine(), out int qty);
                        p.Quantity = qty;

                        inventory.AddOrUpdate(p);
                        break;

                    case "V":
                    case "v":
                        Console.WriteLine("\nInventory:");
                        inventory.Products.ForEach(p => Console.WriteLine(p));
                        break;

                    case "U":
                    case "u":
                        Console.Write("Enter Product ID to update: ");
                        int.TryParse(Console.ReadLine(), out int updateId);
                        var prod = inventory.GetById(updateId);

                        if (prod != null)
                        {
                            Console.Write("New Name: ");
                            prod.Name = Console.ReadLine();

                            Console.Write("New Price: ");
                            string? priceInput = Console.ReadLine();
                            if (decimal.TryParse(priceInput, out decimal newPrice))
                            {
                                prod.Price = newPrice;
                            }
                            else
                            {
                                Console.WriteLine("Invalid price entered. Keeping old value.");
                            }

                            Console.Write("New Quantity: ");
                            string? qtyInput = Console.ReadLine();
                            if (int.TryParse(qtyInput, out int newQty))
                            {
                                prod.Quantity = newQty;
                            }
                            else
                            {
                                Console.WriteLine("Invalid quantity entered. Keeping old value.");
                            }

                            inventory.AddOrUpdate(prod);
                        }
                        else
                        {
                            Console.WriteLine("Product not found.");
                        }

                        break;

                    case "D":
                    case "d":
                        Console.Write("Enter Product ID to delete: ");
                        int.TryParse(Console.ReadLine(), out int delId);
                        inventory.Delete(delId);
                        break;

                    case "R":
                    case "r":
                        return;

                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }
            }
        }

        static void ManageCart(ProductServiceProxy inventory, CartService cart)
        {
            while (true)
            {
                Console.WriteLine("\nCart Menu:");
                Console.WriteLine("(I) View Inventory");
                Console.WriteLine("(V) View Cart");
                Console.WriteLine("(A) Add to Cart");
                Console.WriteLine("(R) Remove from Cart");
                Console.WriteLine("(Q) Return to Main Menu");
                Console.Write("Choice: ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "I":
                    case "i":
                        inventory.Products.ForEach(p => Console.WriteLine(p));
                        break;

                    case "V":
                    case "v":
                        if (cart.Items.Count == 0)
                            Console.WriteLine("Cart is empty.");
                        else
                            cart.Items.ForEach(p => Console.WriteLine(p));
                        break;

                    case "A":
                    case "a":
                        Console.Write("Enter Product ID to add: ");
                        int.TryParse(Console.ReadLine(), out int addId);
                        var item = inventory.GetById(addId);
                        if (item == null)
                        {
                            Console.WriteLine("Product not found.");
                            break;
                        }

                        Console.Write("Enter Quantity: ");
                        int.TryParse(Console.ReadLine(), out int addQty);
                        if (addQty <= 0 || addQty > item.Quantity)
                        {
                            Console.WriteLine("Invalid quantity or not enough in stock.");
                            break;
                        }

                        item.Quantity -= addQty;
                        cart.AddToCart(item, addQty);
                        Console.WriteLine("Added to cart.");
                        break;

                    case "R":
                    case "r":
                        Console.Write("Enter Product ID to remove: ");
                        int.TryParse(Console.ReadLine(), out int remId);

                        Console.Write("Quantity to remove: ");
                        int.TryParse(Console.ReadLine(), out int remQty);

                        if (cart.RemoveFromCart(remId, remQty))
                        {
                            var returnItem = inventory.GetById(remId);
                            if (returnItem != null)
                            {
                                returnItem.Quantity += remQty;
                            }

                            Console.WriteLine("Item removed from cart.");
                        }
                        else
                        {
                            Console.WriteLine("Unable to remove item.");
                        }
                        break;

                    case "Q":
                    case "q":
                        return;

                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }
            }
        }
    }
}
