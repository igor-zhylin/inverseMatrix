using System;

namespace ExMatrix
{
    /// <summary>
    /// Математические операции с матрицами типа int, double.
    /// </summary>
    public class MathM
    {
       #region Determinant (вычисление определителя матрицы по Гауссу) - int, double
        /// <summary>
        /// Вычисление определителя матрицы методом Гаусса (Приводим матрицу к треугольному виду и перемножаем главную диагональ).
        /// </summary>
        public static double Determinant(Matrix<double> mA)
        {
            if (mA.Size.X != mA.Size.Y) throw new ArgumentException("Вычисление определителя возможно только для квадратных матриц.");
            Matrix<double> matrix = mA.Clone();
            double det = 1;
            int order = mA.Size.X;

            for (int i = 0; i < order - 1; i++)
            {
                double[] masterRow = matrix.GetRow(i);
                det *= masterRow[i];
                if (det == 0) return 0;
                for (int t = i + 1; t < order; t++)
                {
                    double[] slaveRow = matrix.GetRow(t);
                    double[] tmp = MulArrayConst(masterRow, slaveRow[i] / masterRow[i]);
                    double[] source = matrix.GetRow(t);
                    matrix.SetRow(SubArray(source, tmp), t);
                }
            }
            det *= matrix[order - 1, order - 1];

            return det;
        }
        #endregion

        #region Inverse (вычисление обратной матрицы) - int, double
        /// <summary>
        /// Возвращает матрицу обратную данной. Обратная матрица существует только для квадратных, невырожденных, матриц.
        /// </summary>
        public static Matrix<int> Inverse(Matrix<int> mA)
        {
            if (mA.Size.X != mA.Size.Y) throw new ArgumentException("Обратная матрица существует только для квадратных, невырожденных, матриц.");

            Matrix<double> convertMatrix = ConvertM.ToDouble(mA);
            convertMatrix = Inverse(convertMatrix);

            return ConvertM.ToInt32(convertMatrix);
        }

        /// <summary>
        /// Возвращает матрицу обратную данной. Обратная матрица существует только для квадратных, невырожденных, матриц.
        /// </summary>
        public static Matrix<double> Inverse(Matrix<double> mA, uint round = 0)
        {
            if (mA.Size.X != mA.Size.Y) throw new ArgumentException("Обратная матрица существует только для квадратных, невырожденных, матриц.");
            Matrix<double> matrix = new Matrix<double>(mA.Size.X);
            double determinant = Determinant(mA);

            if (determinant == 0) return matrix; //Если определитель == 0 - матрица вырожденная

            for (int i = 0; i < mA.Size.X; i++)
            {
                for (int t = 0; t < mA.Size.Y; t++)
                {
                    Matrix<double> tmp = mA.Exclude(i, t);
                    matrix[t, i] = round == 0 
                        ? (1 / determinant) * Math.Pow(-1, i + t) * Determinant(tmp) 
                        : Math.Round(((1 / determinant) * Math.Pow(-1, i + t) * Determinant(tmp)), (int)round, MidpointRounding.ToEven);

                }
            }
            return matrix;
        }
        #endregion

        #region SupportMethods
        /// <summary>
        /// поэлементное вычитание массивов.
        /// </summary>
        public static double[] SubArray(double[] A, double[] B)
        {
            double[] ret = (double[])A.Clone();
            for (int i = 0; i < (A.Length > B.Length ? A.Length : B.Length); i++)
                ret[i] -= B[i];
            return ret;
        }
        /// <summary>
        /// Поэлементное умножение массивов
        /// </summary>
        public static double[] MulArrayConst(double[] array, double number)
        {
            double[] ret = (double[])array.Clone();
            for (int i = 0; i < ret.Length; i++)
                ret[i] *= number;
            return ret;
        }
        #endregion
    }
}