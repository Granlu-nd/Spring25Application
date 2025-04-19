//using Library.eCommerce.Services;
//using Spring25App.Models;

/*namespace Spring25App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Welcome to THE STORE!!");

            Console.WriteLine("(C) Create new inventory item or add item to cart");
            Console.WriteLine("(R) Read all inventory items");
            Console.WriteLine("(U) Update an inventory item");
            Console.WriteLine("(D) Delete an inventory item");
            Console.WriteLine("(Q) Quit");

            List<Product?> list = ProductServiceProxy.Current.Products;

            char choice;
            do
            {
                string? input = Console.ReadLine();
                choice = input.ToUpper()[0];

                switch (choice)
                {
                    case 'C':
                        ProductServiceProxy.Current.AddOrUpdate(new Product
                        {
                            Name = Console.ReadLine()
                        });
                        break;
                    case 'R':
                        list.ForEach(Console.WriteLine);
                        break;
                    case 'U':
                        Console.WriteLine("Which product would you like to update?");
                        int selection = int.Parse(Console.ReadLine() ?? "-1");
                        var selectedProd = list.FirstOrDefault(p => p.Id == selection); //make copy constructor

                        if (selectedProd != null)
                        {
                            selectedProd.Name = Console.ReadLine() ?? "ERROR";

                            ProductServiceProxy.Current.AddOrUpdate(selectedProd);
                        }


                        break;
                    case 'D':
                        Console.WriteLine("Which product would you like to delete?");
                        int selection2 = int.Parse(Console.ReadLine() ?? "-1");
                        ProductServiceProxy.Current.Delete(selection2);
                        break;
                    case 'Q':
                        Console.WriteLine("Closing Application");
                        break;
                    default:
                        Console.WriteLine("ERROR: Invalid choice");
                        break;
                }
            } while (choice != 'Q' || choice != 'q');

            Console.ReadLine();

        }

    }

}*/

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
                Console.WriteLine("1. Create Product");
                Console.WriteLine("2. View Products");
                Console.WriteLine("3. Update Product");
                Console.WriteLine("4. Delete Product");
                Console.WriteLine("5. Return to Main Menu");
                Console.Write("Choice: ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
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

                    case "2":
                        Console.WriteLine("\nInventory:");
                        inventory.Products.ForEach(p => Console.WriteLine(p));
                        break;

                    case "3":
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

                    case "4":
                        Console.Write("Enter Product ID to delete: ");
                        int.TryParse(Console.ReadLine(), out int delId);
                        inventory.Delete(delId);
                        break;

                    case "5":
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
                Console.WriteLine("1. View Inventory");
                Console.WriteLine("2. View Cart");
                Console.WriteLine("3. Add to Cart");
                Console.WriteLine("4. Remove from Cart");
                Console.WriteLine("5. Return to Main Menu");
                Console.Write("Choice: ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        inventory.Products.ForEach(p => Console.WriteLine(p));
                        break;

                    case "2":
                        if (cart.Items.Count == 0)
                            Console.WriteLine("Cart is empty.");
                        else
                            cart.Items.ForEach(p => Console.WriteLine(p));
                        break;

                    case "3":
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

                    case "4":
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

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }
            }
        }
    }
}
