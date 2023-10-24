using System;
namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome4664();
            Wellcome6478();
            Console.ReadKey();
        }

        static partial void Wellcome6478();
        private static void Welcome4664()
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            Console.ReadKey();  
            Console.WriteLine("{0}, welcome to my first console application!!!", name);
        }
    }
}