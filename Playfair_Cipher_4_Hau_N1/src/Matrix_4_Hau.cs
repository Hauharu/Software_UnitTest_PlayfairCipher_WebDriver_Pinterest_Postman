using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playfair_Cipher_4_Hau_N1.src
{
    public class Matrix_4_Hau
    {
        // Mảng 2 chiều kiểu char để lưu trữ bảng chữ cái (thường là bảng 5x5 trong Playfair)
        protected char[,] matrix;

        // Thuộc tính công khai: kích thước của ma trận (N_matrix x N_matrix)
        public int N_matrix; // size of matrix

        // Constructor: khởi tạo ma trận với kích thước n x n
        public Matrix_4_Hau(int n)
        {
            N_matrix = n; // Gán kích thước ma trận
            matrix = new char[N_matrix, N_matrix]; // Khởi tạo mảng 2 chiều với kích thước N_matrix x N_matrix
        }

        // Hàm lấy giá trị ký tự tại tọa độ (i, j) trong ma trận
        public char Get_4_Hau(int i, int j)
        {
            return matrix[i, j]; // Trả về ký tự tại vị trí hàng i, cột j
        }

        // Hàm lấy giá trị ký tự tại tọa độ được cung cấp bởi đối tượng Coordinate_4_Hau
        public char Get_4_Hau(Coordinate_4_Hau cor)
        {
            return matrix[cor.I, cor.J]; // Trả về ký tự tại tọa độ (I, J) từ đối tượng Coordinate_4_Hau
        }

        // Hàm đặt giá trị ký tự tại tọa độ (i, j) trong ma trận
        public void Set_4_Hau(int i, int j, char value)
        {
            // Kiểm tra xem giá trị có phải là chữ cái không
            if (!char.IsLetter(value))
            {
                // Ném ngoại lệ nếu giá trị không phải chữ cái
                throw new ArgumentException("Only alphabetic characters are allowed in the matrix.");
            }
            // Kiểm tra xem tọa độ (i, j) có nằm trong phạm vi ma trận không
            if (i < 0 || i >= N_matrix || j < 0 || j >= N_matrix)
            {
                // Ném ngoại lệ nếu tọa độ vượt quá kích thước ma trận
                throw new IndexOutOfRangeException($"Index ({i},{j}) is out of range for matrix of size {N_matrix}x{N_matrix}");
            }
            // Gán giá trị vào ma trận, chuyển thành chữ cái in hoa
            matrix[i, j] = char.ToUpper(value);
        }
    }
}

