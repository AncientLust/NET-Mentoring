using ADONET.Repositories;

namespace ADO_NET_Library
{
    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Test line");

            var productRepository = new ProductRepository();
            productRepository.Insert();

        }
    }
}