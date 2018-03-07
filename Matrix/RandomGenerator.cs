using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    public class RandomGenerator
    {
        static Random random = new Random();

        /// <summary>
        /// Заполняет матрицу случайным набором значений типа int, если setTestValue = true значения принадлежат диапазону -10...10
        /// </summary>
        public static void MatrixFillRandom(ref Matrix<int> matrix)
        {
            for (int i = 0; i < matrix.Size.X; ++i)
                for (int j = 0; j < matrix.Size.Y; ++j)
                    matrix[i, j] = random.Next(0, 10);
        }

        public static void MatrixFillRandom(ref Matrix<double> matrix)
        {
            for (int i = 0; i < matrix.Size.X; ++i)
                for (int j = 0; j < matrix.Size.Y; ++j)
                    matrix[i, j] = random.Next(0, 10);
        }
    }
}
