using System;

namespace Task1
{
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Product otherProduct) 
            {
                return Name == otherProduct.Name && Price == otherProduct.Price;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Price);
        }
    }
}
