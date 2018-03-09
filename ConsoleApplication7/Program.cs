using System;
using System.Linq;
using ExMatrix;

namespace ConsoleApplication7
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();



            Console.ReadKey();
        }

        private static void Test()
        {
            Matrix<int> matrix = new Matrix<int>(5, 5);
            Generate.RndMatrix(ref matrix, true);

            Console.WriteLine("Input Matrix:");
            Console.WriteLine(matrix);

            Console.WriteLine("Inversed Maatrix: ");
            Console.WriteLine(MathM.Inverse(matrix));
           
        }

    }
}
