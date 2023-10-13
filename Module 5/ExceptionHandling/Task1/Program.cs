using System;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("Please enter a string or 'exit' to quit:");
                var input = Console.ReadLine();

                if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                try
                {
                    if (string.IsNullOrEmpty(input))
                    {
                        throw new EmptyStringException();
                    }
                    
                    Console.WriteLine(input[0]);
                }
                catch (EmptyStringException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    public class EmptyStringException : Exception 
    {
        public EmptyStringException() : base("Provided string is empty.") { }
    }
}