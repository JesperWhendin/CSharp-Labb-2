using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Labb2ProgTemplate
{
    public class Customer
    {
        public string Name { get; private set; }

        private string Password { get; set; }

        private List<Product> _cart;
        public List<Product> Cart { get { return _cart; } }

        public Customer(string name, string password)
        {
            Name = name;
            Password = password;
            _cart = new List<Product>();
        }

        public bool CheckPassword(string password)
        {
            if (password == Password)
            {
                Console.Clear();
                Console.WriteLine("Du har lyckats logga in.");
                return true;
            }
            Console.Clear();
            Console.WriteLine("Du har inte skrivit in ett giltigt lösenord. Försök igen.\n");
            return false;
        }

        public void AddToCart(Product product)
        {
            Console.Clear();
            Cart.Add(product);
            Console.WriteLine($"1 {product.Name} har lagts till i kundvagnen.\n");
        }

        public void RemoveFromCart(Product product)
        {
            Console.Clear();
            if (Cart.Contains(product))
            {
                Cart.Remove(product);
                Console.WriteLine($"1 {product.Name} har tagits bort från kundvagnen.\n");
                return;
            }
            Console.WriteLine($"{product.Name} finns inte kundvagnen.\n");
        }

        public double CartTotal()
        {
            double cartSum = 0;
            foreach (Product product in Cart)
            {
                cartSum += product.Price;
            }
            return cartSum;
        }

        public override string ToString()
        {
            var customerInfo = string.Empty;
            customerInfo += $"Kundnamn: {Name}\n";
            customerInfo += $"Lösenord: {Password }\n";
            customerInfo += "Kundvagn: \n";
            var uniqueProducts = Cart.DistinctBy(p => p.Name);
            foreach (var uniqueProduct in uniqueProducts)
            {
                var count = Cart.Count(p => p.Name == uniqueProduct.Name);
                var sum = count * uniqueProduct.Price;
                customerInfo += $"\n{uniqueProduct.Name} {count} st, {uniqueProduct.Price} kr/st, totalt: {sum} kr.";
            }
            return customerInfo;
        }
    }
}