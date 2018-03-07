namespace Matrix
{
    /// <summary>
    /// Размер матрицы.
    /// </summary>
    public class MatrixSize
    {
        /// <summary>
        /// Количество строк
        /// </summary>
        public int X { get; private set; }
        /// <summary>
        /// Количество столбцов
        /// </summary>
        public int Y { get; private set; }
        /// <summary>
        /// Нулевая матрица
        /// </summary>
        public static MatrixSize Zero
        {
            get
            {
                return new MatrixSize(0, 0);
            }
        }

        /// <summary>
        /// Инициализация размера. 
        /// </summary>
        /// <param name="X">Длина матрицы</param>
        /// <param name="Y">Высота матрицы</param>
        public MatrixSize(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public static bool operator ==(MatrixSize s1, MatrixSize s2)
        {
            return (s1.X != s2.X) || (s1.Y != s2.Y) ? false : true;
        }

        public static bool operator !=(MatrixSize s1, MatrixSize s2)
        {
            return (s1.X != s2.X) || (s1.Y != s2.Y) ? false : true;
        }

        public override int GetHashCode()
        {
            return ((X * Y) + X) - X % Y;
        }

        public override bool Equals(object obj)
        {
            MatrixSize core = obj as MatrixSize;
            return (this.X != core.X) || (this.Y != core.Y) ? false : true;
        }
    }
}
