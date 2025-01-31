using Library.eCommerce.Services;
using Spring25App.Models;

namespace Spring25App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Welcome to THE STORE!!");
            var lastKey = 1;

            Console.WriteLine("(C) Create new inventory item");
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
                        list.Add(new Product
                        {
                            Id = lastKey++,
                            Name = Console.ReadLine()
                        });
                        break;
                    case 'R':
                        list.ForEach(Console.WriteLine);
                        break;
                    case 'U':
                        Console.WriteLine("Which product would you like to update?");
                        int selection = int.Parse(Console.ReadLine() ?? "-1");
                        var selectedProd = list.FirstOrDefault(p => p.Id == selection);
                        if (selectedProd != null)
                        {
                            selectedProd.Name = Console.ReadLine() ?? "ERROR";
                        }
                        break;
                    case 'D':
                        Console.WriteLine("Which product would you like to delete?");
                        int selection2 = int.Parse(Console.ReadLine() ?? "-1");
                        var selectedProd2 = list.FirstOrDefault(p => p.Id == selection2);
                        list.Remove(selectedProd2);
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

}
