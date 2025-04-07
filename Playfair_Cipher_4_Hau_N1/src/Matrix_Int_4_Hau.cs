using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playfair_Cipher_4_Hau_N1.src
{
    public class Matrix_Int_4_Hau
    {
        protected int[,] matrix;  // Ma trận chính
        public int[,] matrix_inverse;  // Ma trận nghịch đảo
        public int N_matrix;  // Kích thước của ma trận (NxN)

        // Constructor: Khởi tạo ma trận vuông với kích thước n x n
        public Matrix_Int_4_Hau(int n)
        {
            N_matrix = n;
            matrix = new int[N_matrix, N_matrix];  // Khởi tạo ma trận chính
            matrix_inverse = new int[N_matrix, N_matrix];  // Khởi tạo ma trận nghịch đảo
        }

        // Lấy giá trị của ma trận tại vị trí (i, j)
        public int Get_4_Hau(int i, int j)
        {
            return matrix[i, j];
        }

        // Lấy giá trị của ma trận tại vị trí (i, j) từ đối tượng Coordinate_4_Hau
        public int Get_4_Hau(Coordinate_4_Hau cor)
        {
            return matrix[cor.I, cor.J];
        }

        // Cập nhật giá trị vào ma trận tại vị trí (i, j)
        public void Set_4_Hau(int i, int j, int value)
        {
            matrix[i, j] = value;
        }

        // Tính và lưu ma trận nghịch đảo
        public void InverseMatrix_4_Hau()
        {
            var adj_A = this.Adjugate();  // Tính ma trận phụ (adjugate)
            if (this.Determinant_4_Hau() == 0) throw new Exception("detA != 0");  // Kiểm tra định thức, nếu = 0 thì không thể tính nghịch đảo
            var det_inverse = this.Multiplicative_Inverse_4_Hau();  // Tính nghịch đảo định thức
            if (det_inverse < 0)
            {
                det_inverse = (det_inverse + (((Math.Abs(det_inverse) / 26) + 1) * 26));  // Chỉnh lại giá trị của nghịch đảo trong phạm vi 0-25
            }
            // Tính ma trận nghịch đảo
            for (int i = 0; i < N_matrix; i++)
            {
                for (int j = 0; j < N_matrix; j++)
                {
                    this.matrix_inverse[i, j] = (adj_A.matrix[i, j] * det_inverse) % 26;  // Áp dụng nghịch đảo vào ma trận
                }
            }
        }

        // Tính định thức của ma trận
        public int Determinant_4_Hau()
        {
            if (this.N_matrix == 1)
            {
                return this.matrix[0, 0];  // Định thức của ma trận 1x1 là chính phần tử duy nhất
            }
            if (this.N_matrix == 2)
            {
                // Định thức của ma trận 2x2 theo công thức: det(A) = a11 * a22 - a12 * a21
                return this.matrix[0, 0] * this.matrix[1, 1] - this.matrix[1, 0] * this.matrix[0, 1];
            }
            int flag = 1;  // Dấu cho các phần tử
            int det = 0;
            // Dùng phương pháp mở rộng theo hàng (cofactor expansion)
            for (int i = 0; i < this.N_matrix; i++)
            {
                Matrix_Int_4_Hau subMatrix = this.CopyMatrixExclude_4_Hau(0, i);  // Lấy ma trận con bỏ qua hàng 0 và cột i
                int sub_det = subMatrix.Determinant_4_Hau();  // Tính định thức của ma trận con
                det += this.matrix[0, i] * flag * sub_det;  // Cộng vào định thức tổng
                flag *= -1;  // Thay đổi dấu sau mỗi bước
            }
            return det;  // Trả về định thức của ma trận ban đầu
        }

#if TEST
        // Nếu symbol TEST đã được định nghĩa (bằng #define TEST trong mã nguồn hoặc cấu hình dự án), thì hàm này sẽ được biên dịch
        // Hàm này sẽ tạo ma trận con bằng cách loại bỏ hàng i và cột j
        public Matrix_Int CopyMatrixExclude(int i, int j)
#else
        // Nếu symbol TEST chưa được định nghĩa, hàm này sẽ được biên dịch thay thế hàm trên
        // Trong trường hợp này, hàm trả về kiểu Matrix_Int_4_Hau thay vì Matrix_Int
        protected Matrix_Int_4_Hau CopyMatrixExclude_4_Hau(int i, int j)
#endif
        {
            int n = N_matrix - 1;  // Kích thước của ma trận con (giảm 1)
            Matrix_Int_4_Hau matrix_new = new Matrix_Int_4_Hau(n);  // Tạo ma trận con với kích thước mới
            int a = 0;
            // Lặp qua các phần tử của ma trận và loại bỏ hàng i và cột j
            for (int t = 0; t < N_matrix; t++)
            {
                if (t == i) continue;  // Bỏ qua hàng i
                for (int u = 0; u < N_matrix; u++)
                {
                    if (u == j) continue;  // Bỏ qua cột j
                    matrix_new.matrix[a / n, a % n] = this.matrix[t, u];  // Sao chép phần tử vào ma trận con
                    a++;  // Tăng chỉ số để di chuyển qua các vị trí trong ma trận con
                }
            }
            return matrix_new;  // Trả về ma trận con đã được tạo
        }

        // Tạo ma trận chuyển vị của ma trận hiện tại
        public Matrix_Int_4_Hau Create_Matrix_Chuyen_Vi_4_Hau()
        {
            Matrix_Int_4_Hau new_matrix = new Matrix_Int_4_Hau(N_matrix);  // Tạo ma trận mới
            for (int i = 0; i < N_matrix; i++)
            {
                for (int j = i; j < N_matrix; j++)
                {
                    // Hoán đổi giá trị giữa phần tử tại (i, j) và (j, i) để tạo ma trận chuyển vị
                    new_matrix.matrix[i, j] = this.matrix[j, i];
                    new_matrix.matrix[j, i] = this.matrix[i, j];
                }
            }
            return new_matrix;  // Trả về ma trận chuyển vị
        }

        // Tính nghịch đảo số học của định thức (mod 26)
        public int Multiplicative_Inverse_4_Hau()
        {
            int detA = this.Determinant_4_Hau();  // Lấy định thức của ma trận
            if (detA < 0) detA = (detA + (((Math.Abs(detA) / 26) + 1) * 26)) % 26;  // Đảm bảo giá trị của định thức nằm trong phạm vi 0-25

            // Sử dụng thuật toán Euclid để tính nghịch đảo của định thức
            int a = detA, b = 26, x0 = 1, x1 = 0, y0 = 0, y1 = 1;
            int x = 0, y = 0;

            // Tìm nghịch đảo của a mod b
            while (b > 0)
            {
                int r = a % b;
                if (r == 0) break;  // Dừng nếu không còn phần dư
                int q = a / b;
                x = x0 - x1 * q;
                y = y0 - y1 * q;
                a = b;
                b = r;
                x0 = x1;
                x1 = x;
                y0 = y1;
                y1 = y;
            }
            if (x < 0) x = (x + 26) % 26;  // Đảm bảo giá trị x nằm trong phạm vi [0, 26)
            return x;  // Trả về nghịch đảo của định thức mod 26
        }

        // Tính ma trận phụ (adjugate matrix) của ma trận hiện tại
        public Matrix_Int_4_Hau Adjugate()
        {
            if (this.N_matrix == 1)
            {
                return new Matrix_Int_4_Hau(1)
                {
                    matrix = new int[1, 1]  // Ma trận 1x1 có adjugate là chính nó
                    {
                        {1}
                    }
                };
            }
            Matrix_Int_4_Hau return_matrix = new Matrix_Int_4_Hau(N_matrix);  // Tạo ma trận mới để lưu kết quả
            int flag_i = 1;
            for (int i = 0; i < N_matrix; i++)
            {
                int flag_j = 1;
                for (int j = 0; j < N_matrix; j++)
                {
                    // Tính các phần tử của ma trận phụ (adjugate) theo công thức cofactor expansion
                    Matrix_Int_4_Hau copy_matrix = this.CopyMatrixExclude_4_Hau(i, j);
                    var x = flag_i * flag_j * copy_matrix.Determinant_4_Hau();
                    if (x < 0)
                    {
                        x = (x + (((Math.Abs(x) / 26) + 1) * 26));  // Đảm bảo giá trị của x trong phạm vi [0, 26)
                    }
                    return_matrix.matrix[j, i] = x % 26;  // Lưu giá trị vào ma trận adjugate
                    flag_j *= -1;  // Thay đổi dấu sau mỗi bước
                }
                flag_i *= -1;  // Thay đổi dấu sau mỗi hàng
            }
            return return_matrix;  // Trả về ma trận phụ
        }

        // Lấy giá trị tại vị trí (i, j) trong ma trận nghịch đảo
        public int GetInverse_4_Hau(int i, int j)
        {
            return matrix_inverse[i, j];  // Trả về giá trị tại vị trí (i, j) của ma trận nghịch đảo
        }
    }
}
