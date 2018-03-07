
namespace Matrix
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;

    /// <summary>
    /// Класс матрицы.
    /// </summary>
    public class Matrix<T> : IEnumerator<T>, IEnumerable<T>
    {
        /// <summary>
        /// Размерность матрицы. X - количество строк, Y - количество столбцов(полей)
        /// </summary>
        public MatrixSize Size { get; private set; }

        /// <summary>
        /// Массив представляющий матрицу. Использование не рекомендуется!!! (используйте индексаторы)
        /// </summary>
        public T[,] matrix = null;

        T current;
        int curIndex = -1;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public Matrix(int? x = null, int? y = null)
        {
            InitMatrix(new MatrixSize((int)x, (int)y));
        }

        public T this[int x, int y]
        {
            get { return matrix[x, y]; }
            set { matrix[x, y] = value; }
        }

        /// <summary>
        /// Инициализация матрицы. Вызывается при создании экземпляра класса. Ручной вызов нужен для изменения размера матрицы и заполнения значениями по-умолчанию.
        /// </summary>
        public void InitMatrix(MatrixSize size)
        {
            this.Size = size;

            matrix = new T[size.X, size.Y];

            for (int i = 0; i < size.X; i++)
                for (int t = 0; t < size.Y; t++)
                    matrix[i, t] = default(T);
        }

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            for (int i = 0; i < Size.X; i++)
            {
                for (int t = 0; t < Size.Y; t++)
                {
                    ret.Append(matrix[i, t]);
                    ret.Append("\t");
                }
                ret.Append("\n");
            }
            return ret.ToString();
        }

        /// <summary>
        /// Возвращает массив соответствующий указанной строке матрицы. Отсчет строк идет с 0.
        /// </summary>
        public T[] GetRow(int row)
        {
            if (row >= Size.X) throw new IndexOutOfRangeException("Индекс строки не принадлежит массиву.");
            T[] ret = new T[Size.Y];
            for (int i = 0; i < Size.Y; i++)
                ret[i] = (T)matrix[row, i];

            return ret;
        }

        /// <summary>
        /// Заполняет указанную строку матрицы значениями из массива. Если размер массива и размер строки не совпадают, то строка будет - либо заполнена не полностью, либо "лишние" значения массива будут проигнорированы.
        /// </summary>
        public void SetRow(T[] rowValues, int row)
        {
            if (row >= Size.X)
                throw new IndexOutOfRangeException("Индекс строки не принадлежит массиву.");
            for (int i = 0; i < (Size.Y > rowValues.Length ? rowValues.Length : Size.Y); i++)
                matrix[row, i] = rowValues[i];
        }

        /// <summary>
        /// Заполняет указанный столбец значениями из массива. Если размеры столбца и массива не совпадают - столбец либо будет заполнен не полностью, либо "лишние" значения массива будут проигнорированы.
        /// </summary>
        public void SetColumn(T[] columnValues, int column)
        {
            if (column >= Size.Y)
                throw new IndexOutOfRangeException("Индекс столбца(поля) не принадлежит массиву.");
            for (int i = 0; i < (Size.X > columnValues.Length ? columnValues.Length : Size.X); i++)
                matrix[i, column] = columnValues[i];
        }

        #region Enumerabling...
        void IDisposable.Dispose()
        {

        }

        object IEnumerator.Current
        {
            get { return current; }
        }

        bool System.Collections.IEnumerator.MoveNext()
        {
            if (++curIndex >= Size.X * Size.Y)
            {
                ((IEnumerator)this).Reset();
                return false;
            }
            else
            {
                int x = curIndex / Size.Y;
                int y = curIndex - Size.Y * x;
                current = matrix[x, y];
            }
            return true;
        }

        void System.Collections.IEnumerator.Reset()
        {
            curIndex = -1;
        }

        T IEnumerator<T>.Current
        {
            get { return current; }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
        #endregion

        /// <summary>
        /// Создает копию матрицы
        /// </summary>
        public Matrix<T> Clone()
        {
            Matrix<T> ret = new Matrix<T>();
            ret.Size = new MatrixSize(Size.X, Size.Y);
            ret.matrix = (T[,])matrix.Clone();
            return ret;
        }

        /// <summary>
        /// Возвращает матрицу без указанных строки и столбца. Исходная матрица не изменяется.
        /// </summary>
        public Matrix<T> Exclude(int row, int column)
        {
            if (row > Size.X || column > Size.Y)
                throw new IndexOutOfRangeException("Строка или столбец не принадлежат матрице.");
            Matrix<T> ret = new Matrix<T>();
            ret.InitMatrix(new MatrixSize(Size.X - 1, Size.Y - 1));
            int offsetX = 0;
            for (int i = 0; i < Size.X; i++)
            {
                int offsetY = 0;
                if (i == row) { offsetX++; continue; }
                for (int t = 0; t < Size.Y; t++)
                {
                    if (t == column) { offsetY++; continue; }
                    ret[i - offsetX, t - offsetY] = this[i, t];
                }
            }
            return ret;
        }

        /// <summary>
        /// Преобразует матрицу в одномерный массив.
        /// </summary>
        public T[] ToArray()
        {
            T[] ret = new T[Size.X * Size.Y];
            int i = -1;
            foreach (var item in this)
                ret[++i] = item;
            return ret;
        }

        /// <summary>
        /// Заполняет матрицу из одномерного массива.
        /// </summary>
        public void SetArray(T[] array)
        {
            int curIndex = 0;
            foreach (var item in array)
            {
                int x = curIndex / Size.Y;
                int y = curIndex - Size.Y * x;
                matrix[x, y] = array[curIndex++];
            }
        }
    }
}