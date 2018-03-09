using System;

namespace ExMatrix
{
    /// <summary>
    /// Класс  - генератор значений матриц.
    /// </summary>
    public static class Generate
    {
        static Random rnd = new Random();
        
        /// <summary>
        /// Заполняет матрицу случайным набором значений типа int, если setTestValue = true значения принадлежат диапазону -10...10
        /// </summary>
        public static void RndMatrix(ref Matrix<int> mA, bool setTestValue = false)
        {
            if (mA.matrix == null) mA.matrix = new int[mA.Size.X, mA.Size.Y];
            for (int i = 0; i < mA.Size.X; i++)
                for (int t = 0; t < mA.Size.Y; t++) 
                    if (!setTestValue) mA[i, t] = rnd.Next(int.MinValue, int.MaxValue);
                    else
                        mA[i, t] = rnd.Next(-10, 10);
        }
        
    }
}
