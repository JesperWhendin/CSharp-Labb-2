using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Labb2ProgTemplate
{
    public class Shop
    {
        private Customer CurrentCustomer { get; set; }
        public List<Customer> Customers { get; set; }
        private List<Product> Products { get; set; }

        public Shop()
        {
            Customers = new List<Customer>();
            Customers.AddRange(new[]
            {
                new Customer ("Knatte", "123" ),
                new Customer ("Fnatte", "321" ),
                new Customer ("Tjatte", "213" ),
                new Customer ("jw",     "qwe" )
            });

            Products = new List<Product>();
            Products.AddRange(new[]
            {
                new Product ( "Kaffe",        20,    1 ),
                new Product ( "Festis",       15,    2 ),
                new Product ( "Kanelbulle",   22.50, 3 ),
                new Product ( "Hallongrotta", 18.90, 4 )
            });
            Console.WriteLine("Välkommen till JW's Konditori.\n");
        }

        public void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("Skriv 1 för att logga in.\nSkriv 2 för att registrera ny kund.\nSkriv 9 för att stänga applikationen.");
                int.TryParse(Console.ReadLine(), out int mainMenuChoice);

                if (mainMenuChoice == 1)
                {
                    Console.Clear();
                    Login();
                }
                else if (mainMenuChoice == 2)
                {
                    Console.Clear();
                    Register();
                }
                else if (mainMenuChoice == 9)
                {
                    Console.Clear();
                    Console.WriteLine("Tack för besöket. Välkommen åter!");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Du skrev inte in ett giltigt val. Försök igen.\n");
                }
            }
        }

        private void Login()
        {
            Console.WriteLine("Skriv ditt användarnamn:");
            string loginNameInput = Console.ReadLine();
            foreach (var username in Customers)
            {
                if (loginNameInput == username.Name)
                {
                    Console.WriteLine("Skriv ditt lösenord:");
                    string loginPasswordInput = Console.ReadLine();

                    if (username.CheckPassword(loginPasswordInput))
                    {
                        CurrentCustomer = username;
                        ShopMenu();
                    }
                    MainMenu();
                }
            }
            Console.Clear();
            Console.WriteLine("Du har inte skrivit in ett godkänt användarnamn.\n");
        }

        private void Register()
        {
            Console.WriteLine("Skriv nytt användarnamn:");
            string registerNameInput = Console.ReadLine();
            foreach (var username in Customers)
            {
                if (registerNameInput == username.Name)
                {
                    Console.Clear();
                    Console.WriteLine("Användarnamnet är upptaget.\n");
                    MainMenu();
                }
            }
            Console.WriteLine("Skriv lösenord:");
            string registerPasswordInput = Console.ReadLine();
            Customer nyKund = new Customer(registerNameInput, registerPasswordInput);
            Customers.Add(nyKund);
            Console.Clear();
            Console.WriteLine($"{nyKund.Name} har registrerats.\n");
        }

        private void ShopMenu()
        {
            Console.WriteLine($"Välkommen {CurrentCustomer.Name}.\n");
            while (true)
            {
                Console.WriteLine("Skriv 1 för att handla.");
                Console.WriteLine("Skriv 2 för att ta bort produkter.");
                Console.WriteLine("Skriv 3 för att se kundvagn.");
                Console.WriteLine("Skriv 4 för att gå till kassan.");
                Console.WriteLine("Skriv 5 för att se kundinfo.");
                Console.WriteLine("Skriv 6 för att logga ut.");
                int.TryParse(Console.ReadLine(), out var shopMenuChoice);
                Console.Clear();
                switch (shopMenuChoice)
                {
                    case 1:
                        AddProduct();
                        break;
                    case 2:
                        if (CurrentCustomer.Cart.Count > 0)
                        {
                            RemoveProduct();
                            break;
                        }
                        Console.WriteLine("Du har för tillfället inga produkter i din kundvagn.\n");
                        break;
                    case 3:
                        ViewCart();
                        break;
                    case 4:
                        if (CurrentCustomer.Cart.Count > 0)
                        {
                            Checkout();
                            break;
                        }
                        Console.WriteLine("Du har för tillfället inga produkter i din kundvagn.\n");
                        break;
                    case 5:
                        CustomerInfo();
                        break;
                    case 6:
                        MainMenu();
                        break;
                    default:
                        Console.WriteLine("Du har inte gjort ett giltigt val.");
                        break;
                }
            }
        }

        private void AddProduct()
        {
            bool shopping = true;
            while (shopping)
            {
                foreach (var product in Products)
                {
                    Console.WriteLine($"Skriv {product.Id} för att lägga till {product.Name}.");
                }
                Console.WriteLine("\nTryck på valfri för att återgå till shopmenyn.");
                int.TryParse(Console.ReadLine(), out var shopProductChoice);

                switch (shopProductChoice)
                {
                    case 0:
                        shopping = false;
                        Console.Clear();
                        break;
                    case 1:
                        shopProductChoice -= 1;
                        CurrentCustomer.AddToCart(Products[shopProductChoice]);
                        break;
                    case 2:
                        shopProductChoice -= 1;
                        CurrentCustomer.AddToCart(Products[shopProductChoice]);
                        break;
                    case 3:
                        shopProductChoice -= 1;
                        CurrentCustomer.AddToCart(Products[shopProductChoice]);
                        break;
                    case 4:
                        shopProductChoice -= 1;
                        CurrentCustomer.AddToCart(Products[shopProductChoice]);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Du har inte gjort ett giltigt val.");
                        break;
                }
            }
        }

        private void RemoveProduct()
        {
            bool removing = true;
            while (removing)
            {
                var uniqueProducts = CurrentCustomer.Cart.DistinctBy(p => p.Name);
                foreach (var uniqueProduct in uniqueProducts)
                {
                    Console.WriteLine($"Skriv {uniqueProduct.Id} för att ta bort 1 {uniqueProduct.Name} av {CurrentCustomer.Cart.Count(p => p.Name == uniqueProduct.Name)}.");
                }
                Console.WriteLine("\nTryck på valfi tangent för att återgå till shopmenyn.");
                int.TryParse(Console.ReadLine(), out var removeProductChoice);
                switch (removeProductChoice)
                {
                    case 0:
                        removing = false;
                        Console.Clear();
                        break;
                    case 1:
                        removeProductChoice -= 1;
                        CurrentCustomer.RemoveFromCart(Products[removeProductChoice]);
                        break;
                        break;
                    case 2:
                        removeProductChoice -= 1;
                        CurrentCustomer.RemoveFromCart(Products[removeProductChoice]);
                        break;
                    case 3:
                        removeProductChoice -= 1;
                        CurrentCustomer.RemoveFromCart(Products[removeProductChoice]);
                        break;
                    case 4:
                        removeProductChoice -= 1;
                        CurrentCustomer.RemoveFromCart(Products[removeProductChoice]);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Du har inte gjort ett giltigt val.");
                        break;
                }
            }
        }

        private void ViewCart()
        {
            Console.WriteLine("Detta är din nuvarande kundvagn:\n");
            var uniqueProducts = CurrentCustomer.Cart.DistinctBy(p => p.Name);
            foreach (var uniqueProduct in uniqueProducts)
            {
                var count = CurrentCustomer.Cart.Count(p => p.Name == uniqueProduct.Name);
                var sum = count * uniqueProduct.Price;
                Console.WriteLine($"{uniqueProduct.Name} {count} st, {uniqueProduct.Price} kr/st, totalt: {sum} kr.");
            }
            Console.WriteLine($"\nTotalt: {CurrentCustomer.CartTotal()} kr.");
            Console.WriteLine("\nTryck på enter för att återgå.");
            Console.ReadLine();
            Console.Clear();
        }

        private void Checkout()
        {
            var uniqueProducts = CurrentCustomer.Cart.DistinctBy(p => p.Name);
            foreach (var uniqueProduct in uniqueProducts)
            {
                var count = CurrentCustomer.Cart.Count(p => p.Name == uniqueProduct.Name);
                var sum = count * uniqueProduct.Price;
                Console.WriteLine($"{uniqueProduct.Name} {count} st, {uniqueProduct.Price} kr/st, totalt: {sum} kr.");
            }
            Console.WriteLine($"\nTotalt: {CurrentCustomer.CartTotal()} kr.");
            CurrentCustomer.Cart.Clear();
            Console.WriteLine("Välkommen åter! Hoppas det smakar!");
            Console.ReadLine();
            Console.Clear();
            CurrentCustomer = null;
            MainMenu();
        }

        private void CustomerInfo()
        {
            Console.WriteLine(CurrentCustomer);
            Console.WriteLine("\nTryck på enter för att återgå.");
            Console.ReadLine();
            Console.Clear();
        }
    }
}