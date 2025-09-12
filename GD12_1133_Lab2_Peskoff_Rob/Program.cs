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

            Console.WriteLine("Decimal: " + 0 + "\t->\tBinary: " + Convert.ToString(0, 2) + "\t\t->\tHex: " + Convert.ToString(0, 16));
            Console.WriteLine("Decimal: " + 1 + "\t->\tBinary: " + Convert.ToString(1, 2) + "\t\t->\tHex: " + Convert.ToString(1, 16));
            Console.WriteLine("Decimal: " + 2 + "\t->\tBinary: " + Convert.ToString(2, 2) + "\t\t->\tHex: " + Convert.ToString(2, 16));
            Console.WriteLine("Decimal: " + 4 + "\t->\tBinary: " + Convert.ToString(4, 2) + "\t\t->\tHex: " + Convert.ToString(4, 16));
            Console.WriteLine("Decimal: " + 8 + "\t->\tBinary: " + Convert.ToString(8, 2) + "\t\t->\tHex: " + Convert.ToString(8, 16));
            Console.WriteLine("Decimal: " + 16 + "\t->\tBinary: " + Convert.ToString(16, 2) + "\t\t->\tHex: " + Convert.ToString(16, 16));
            Console.WriteLine("Decimal: " + 32 + "\t->\tBinary: " + Convert.ToString(32, 2) + "\t\t->\tHex: " + Convert.ToString(32, 16));
            Console.WriteLine("Decimal: " + 64 + "\t->\tBinary: " + Convert.ToString(64, 2) + "\t\t->\tHex: " + Convert.ToString(64, 16));
            Console.WriteLine("Decimal: " + 100 + "\t->\tBinary: " + Convert.ToString(100, 2) + "\t\t->\tHex: " + Convert.ToString(100, 16));
            Console.WriteLine("Decimal: " + 255 + "\t->\tBinary: " + Convert.ToString(255, 2) + "\t->\tHex: " + Convert.ToString(255, 16));

            Console.WriteLine("goodbye\n-rob peskoff");
        }
    }
}
