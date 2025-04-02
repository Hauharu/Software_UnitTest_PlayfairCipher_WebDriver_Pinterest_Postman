using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playfair_Cipher_4_Hau_N1.src
{
    public class Matrix_4_Hau
    {
        protected char[,] matrix;
        public int N_matrix; //size of matrix

        public Matrix_4_Hau(int n)
        {
            N_matrix = n;
            matrix = new char[N_matrix, N_matrix];
        }

        public char Get_4_Hau(int i, int j)
        {
            return matrix[i, j];
        }

        public char Get_4_Hau(Coordinate_4_Hau cor)
        {
            return matrix[cor.I, cor.J];
        }

        public void Set_4_Hau(int i, int j, char value)
        {
            if (!char.IsLetter(value))
            {
                throw new ArgumentException("Only alphabetic characters are allowed in the matrix.");
            }
            if (i < 0 || i >= N_matrix || j < 0 || j >= N_matrix)
            {
                throw new IndexOutOfRangeException($"Index ({i},{j}) is out of range for matrix of size {N_matrix}x{N_matrix}");
            }
            matrix[i, j] = char.ToUpper(value);
        }
    }
}
