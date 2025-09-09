using System.Numerics;

namespace GD12_1133_Lab2_Peskoff_Rob
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, it is currently " + DateTime.Now.ToString() + " and we are going to print some number");

            /*
                // :)
                int[] prints = { 0, 1, 2, 4, 8, 16, 32, 64, 100, 255 };
                for (int i = 0; i < prints.Length; i++) {
                    Console.WriteLine("Decimal: " + prints[i] + " -> Binary: " + Convert.ToString(prints[i], 2) + " -> Hex: " + Convert.ToString(prints[i], 16));
                }
            */

            Console.WriteLine("Decimal: " + 0 + " -> Binary: " + Convert.ToString(0, 2) + " -> Hex: " + Convert.ToString(0, 16));
            Console.WriteLine("Decimal: " + 1 + " -> Binary: " + Convert.ToString(1, 2) + " -> Hex: " + Convert.ToString(1, 16));
            Console.WriteLine("Decimal: " + 2 + " -> Binary: " + Convert.ToString(2, 2) + " -> Hex: " + Convert.ToString(2, 16));
            Console.WriteLine("Decimal: " + 4 + " -> Binary: " + Convert.ToString(4, 2) + " -> Hex: " + Convert.ToString(4, 16));
            Console.WriteLine("Decimal: " + 8 + " -> Binary: " + Convert.ToString(8, 2) + " -> Hex: " + Convert.ToString(8, 16));
            Console.WriteLine("Decimal: " + 16 + " -> Binary: " + Convert.ToString(16, 2) + " -> Hex: " + Convert.ToString(16, 16));
            Console.WriteLine("Decimal: " + 32 + " -> Binary: " + Convert.ToString(32, 2) + " -> Hex: " + Convert.ToString(32, 16));
            Console.WriteLine("Decimal: " + 64 + " -> Binary: " + Convert.ToString(64, 2) + " -> Hex: " + Convert.ToString(64, 16));
            Console.WriteLine("Decimal: " + 100 + " -> Binary: " + Convert.ToString(100, 2) + " -> Hex: " + Convert.ToString(100, 16));
            Console.WriteLine("Decimal: " + 255 + " -> Binary: " + Convert.ToString(255, 2) + " -> Hex: " + Convert.ToString(255, 16));

            Console.WriteLine("goodbye\n-rob peskoff");
        }
    }
}
