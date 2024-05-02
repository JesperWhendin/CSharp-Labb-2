using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Labb2ProgTemplate
{
    public record Product
    {
        public string Name
        {
            get { return _name; }
            set { value = _name; }
        }
        private string _name;
        public double Price
        {
            get { return _price; }
            set { value = _price; }
        }
        private double _price;
        public int Id
        {
            get { return _id; }
            set { value = _id; }
        }
        private int _id;
        public Product(string name, double price, int id)
        {
            _name = name;
            _price = price;
            _id = id;
        }
    }
}
