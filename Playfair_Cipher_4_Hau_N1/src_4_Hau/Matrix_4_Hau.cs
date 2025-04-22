using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playfair_Cipher_4_Hau_N1.src_4_Hau
{
    public class Matrix_4_Hau
    {
        // Mảng 2 chiều kiểu char để lưu trữ bảng chữ cái (thường là bảng 5x5 trong Playfair)
        protected char[,] matrix_4_Hau;

        // Thuộc tính công khai: kích thước của ma trận (N_matrix_4_Hau x N_matrix_4_Hau)
        public int N_matrix_4_Hau; // size of matrix

        // Constructor: khởi tạo ma trận với kích thước n x n
        public Matrix_4_Hau(int n_4_Hau)
        {
            N_matrix_4_Hau = n_4_Hau; // Gán kích thước ma trận
            matrix_4_Hau = new char[N_matrix_4_Hau, N_matrix_4_Hau]; // Khởi tạo mảng 2 chiều với kích thước N_matrix_4_Hau x N_matrix_4_Hau
        }

        // Hàm lấy giá trị ký tự tại tọa độ (i, j) trong ma trận
        public char Get_4_Hau(int i_4_Hau, int j_4_Hau)
        {
            return matrix_4_Hau[i_4_Hau, j_4_Hau]; // Trả về ký tự tại vị trí hàng i, cột j
        }

        // Hàm lấy giá trị ký tự tại tọa độ được cung cấp bởi đối tượng Coordinate_4_Hau
        public char Get_4_Hau(Coordinate_4_Hau cor_4_Hau)
        {
            return matrix_4_Hau[cor_4_Hau.I_4_Hau, cor_4_Hau.J_4_Hau]; // Trả về ký tự tại tọa độ (I, J) từ đối tượng Coordinate_4_Hau
        }

        // Hàm đặt giá trị ký tự tại tọa độ (i, j) trong ma trận
        public void Set_4_Hau(int i_4_Hau, int j_4_Hau, char value_4_Hau)
        {
            // Kiểm tra xem giá trị có phải là chữ cái không
            if (!char.IsLetter(value_4_Hau))
            {
                // Ném ngoại lệ nếu giá trị không phải chữ cái
                throw new ArgumentException("Only alphabetic characters are allowed in the matrix.");
            }
            // Kiểm tra xem tọa độ (i, j) có nằm trong phạm vi ma trận không
            if (i_4_Hau < 0 || i_4_Hau >= N_matrix_4_Hau || j_4_Hau < 0 || j_4_Hau >= N_matrix_4_Hau)
            {
                // Ném ngoại lệ nếu tọa độ vượt quá kích thước ma trận
                throw new IndexOutOfRangeException($"Index ({i_4_Hau},{j_4_Hau}) is out of range for matrix of size {N_matrix_4_Hau}x{N_matrix_4_Hau}");
            }
            // Gán giá trị vào ma trận, chuyển thành chữ cái in hoa
            matrix_4_Hau[i_4_Hau, j_4_Hau] = char.ToUpper(value_4_Hau);
        }
    }
}

