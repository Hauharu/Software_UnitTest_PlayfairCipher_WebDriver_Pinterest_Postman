using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playfair_Cipher_4_Hau_N1.src_4_Hau
{
    // Lớp MatrixPlayFair_4_Hau kế thừa lớp Matrix_4_Hau, dùng để tạo ma trận Playfair với kích thước tùy chọn.
    public class MatrixPlayFair_4_Hau : Matrix_4_Hau
    {
        // Constructor để khởi tạo ma trận với kích thước n_matrix
        public MatrixPlayFair_4_Hau(int n_matrix_4_Hau) : base(n_matrix_4_Hau)
        {
            // Gọi constructor của lớp cha (Matrix_4_Hau) để khởi tạo ma trận với kích thước đã cho
        }

        // Phương thức tạo ma trận Playfair từ khóa (key)
        public void CreateMatrix_4_Hau(string key_4_Hau)
        {
            string temp_4_Hau = key_4_Hau;
            // Lấy bảng chữ cái từ Alphabet_4_Hau (có thể là danh sách các ký tự hợp lệ)
            var alpha_4_Hau = Alphabet_4_Hau.Tolist_4_Hau();
            // Tạo chuỗi gồm các chữ cái trong alphabet và các ký tự số từ 0 đến 9
            var an_4_Hau = string.Concat(alpha_4_Hau) + "0123456789";

            // Loại bỏ khoảng trắng trong key
            temp_4_Hau = temp_4_Hau.Replace(" ", "");
            temp_4_Hau = temp_4_Hau.ToUpper();  // Đưa key về chữ hoa

            // Kiểm tra nếu key chứa ký tự không hợp lệ
            foreach (char c_4_Hau in temp_4_Hau)
            {
                if (!an_4_Hau.Contains<char>(c_4_Hau))  // Nếu ký tự không có trong bảng chữ cái và số
                    throw new ArgumentException("Key must not contain special characters");
            }

            // Thêm các ký tự từ bảng alphabet vào cuối key
            temp_4_Hau += alpha_4_Hau;

            // Kiểm tra kích thước ma trận: 5x5 (Playfair chuẩn) 
            if (N_matrix_4_Hau == 5)
            {
                // Thay thế chữ 'J' bằng 'I' theo quy tắc của mã Playfair
                temp_4_Hau = temp_4_Hau.Replace('J', 'I');
            }

            else
            {
                // Nếu kích thước ma trận không hợp lệ, ném lỗi
                throw new Exception("Size of matrix must be 5");
            }

            // Loại bỏ các ký tự trùng lặp trong key
            temp_4_Hau = RemoveSameCharacter_4_Hau(temp_4_Hau);

            // Kiểm tra xem chiều dài key sau khi xử lý có phù hợp với kích thước ma trận không
            int expectedLength_4_Hau = N_matrix_4_Hau * N_matrix_4_Hau; // 25 cho ma trận 5x5
            if (temp_4_Hau.Length != expectedLength_4_Hau)
            {
                // Nếu không khớp, ném lỗi với thông báo chi tiết
                throw new Exception($"Expected {expectedLength_4_Hau} characters, but got {temp_4_Hau.Length} characters after processing the key.");
            }

            // Điền các ký tự vào ma trận
            int k_4_Hau = 0;
            foreach (char x_4_Hau in temp_4_Hau.ToCharArray())
            {
                // Set giá trị cho từng ô trong ma trận tại vị trí (k / N_matrix, k % N_matrix)
                base.Set_4_Hau(k_4_Hau / N_matrix_4_Hau, k_4_Hau % N_matrix_4_Hau, x_4_Hau);
                k_4_Hau++;  // Tăng k để chuyển sang vị trí tiếp theo trong ma trận
            }
        }

        // Phương thức loại bỏ các ký tự trùng lặp trong chuỗi (dành cho key)
        public string RemoveSameCharacter_4_Hau(string key_4_Hau)
        {
            HashSet<char> seen_4_Hau = new HashSet<char>();  // Sử dụng HashSet để lưu trữ các ký tự đã gặp
            StringBuilder result_4_Hau = new StringBuilder();  // Dùng StringBuilder để xây dựng chuỗi kết quả

            // Duyệt qua từng ký tự trong key
            foreach (char c_4_Hau in key_4_Hau)
            {
                if (!seen_4_Hau.Contains(c_4_Hau))  // Nếu ký tự chưa xuất hiện
                {
                    seen_4_Hau.Add(c_4_Hau);  // Thêm ký tự vào HashSet
                    result_4_Hau.Append(c_4_Hau);  // Thêm ký tự vào chuỗi kết quả
                }
            }
            return result_4_Hau.ToString();  // Trả về chuỗi không có ký tự trùng lặp
        }

        // Phương thức lấy tọa độ của một ký tự trong ma trận
        public Coordinate_4_Hau GetCoordinate_4_Hau(char key_4_Hau)
        {
            key_4_Hau = key_4_Hau.ToString().ToUpper()[0];  // Đảm bảo ký tự là chữ hoa

            // Duyệt qua từng ô trong ma trận
            for (int i_4_Hau = 0; i_4_Hau < N_matrix_4_Hau; i_4_Hau++)
            {
                for (int j_4_Hau = 0; j_4_Hau < N_matrix_4_Hau; j_4_Hau++)
                {
                    if (matrix_4_Hau[i_4_Hau, j_4_Hau] == key_4_Hau)  // Nếu tìm thấy ký tự trong ma trận
                    {
                        // Trả về tọa độ của ký tự
                        Coordinate_4_Hau temp_4_Hau = new Coordinate_4_Hau()
                        {
                            I_4_Hau = i_4_Hau,
                            J_4_Hau = j_4_Hau
                        };
                        return temp_4_Hau;
                    }
                }
            }
            return null;  // Nếu không tìm thấy ký tự, trả về null
        }
    }
}