using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playfair_Cipher_4_Hau_N1.src
{
    // Lớp MatrixPlayFair_4_Hau kế thừa lớp Matrix_4_Hau, dùng để tạo ma trận Playfair với kích thước tùy chọn.
    public class MatrixPlayFair_4_Hau : Matrix_4_Hau
    {
        // Constructor để khởi tạo ma trận với kích thước n_matrix
        public MatrixPlayFair_4_Hau(int n_matrix) : base(n_matrix)
        {
            // Gọi constructor của lớp cha (Matrix_4_Hau) để khởi tạo ma trận với kích thước đã cho
        }

        // Phương thức tạo ma trận Playfair từ khóa (key)
        public void CreateMatrix_4_Hau(string key)
        {
            string temp = key;
            // Lấy bảng chữ cái từ Alphabet_4_Hau (có thể là danh sách các ký tự hợp lệ)
            var alpha = Alphabet_4_Hau.Tolist_4_Hau();
            // Tạo chuỗi gồm các chữ cái trong alphabet và các ký tự số từ 0 đến 9
            var an = string.Concat(alpha) + "0123456789";

            // Loại bỏ khoảng trắng trong key
            temp = temp.Replace(" ", "");
            temp = temp.ToUpper();  // Đưa key về chữ hoa

            // Kiểm tra nếu key chứa ký tự không hợp lệ
            foreach (char c in temp)
            {
                if (!an.Contains<char>(c))  // Nếu ký tự không có trong bảng chữ cái và số
                    throw new ArgumentException("Key must not contain special characters");
            }

            // Thêm các ký tự từ bảng alphabet vào cuối key
            temp += alpha;

            // Kiểm tra kích thước ma trận: 5x5 (Playfair chuẩn) 
            if (N_matrix == 5)
            {
                // Thay thế chữ 'J' bằng 'I' theo quy tắc của mã Playfair
                temp = temp.Replace('J', 'I');
            }
     
            else
            {
                // Nếu kích thước ma trận không hợp lệ, ném lỗi
                throw new Exception("Size of matrix must be 5");
            }

            // Loại bỏ các ký tự trùng lặp trong key
            temp = RemoveSameCharacter_4_Hau(temp);

            // Kiểm tra xem chiều dài key sau khi xử lý có phù hợp với kích thước ma trận không
            int expectedLength = N_matrix * N_matrix; // 25 cho ma trận 5x5
            if (temp.Length != expectedLength)
            {
                // Nếu không khớp, ném lỗi với thông báo chi tiết
                throw new Exception($"Expected {expectedLength} characters, but got {temp.Length} characters after processing the key.");
            }

            // Điền các ký tự vào ma trận
            int k = 0;
            foreach (char x in temp.ToCharArray())
            {
                // Set giá trị cho từng ô trong ma trận tại vị trí (k / N_matrix, k % N_matrix)
                base.Set_4_Hau(k / N_matrix, k % N_matrix, x);
                k++;  // Tăng k để chuyển sang vị trí tiếp theo trong ma trận
            }
        }

        // Phương thức loại bỏ các ký tự trùng lặp trong chuỗi (dành cho key)
#if TEST
        public string RemoveSameCharacter(string key)
#else
        private string RemoveSameCharacter_4_Hau(string key)
#endif
        {
            HashSet<char> seen = new HashSet<char>();  // Sử dụng HashSet để lưu trữ các ký tự đã gặp
            StringBuilder result = new StringBuilder();  // Dùng StringBuilder để xây dựng chuỗi kết quả

            // Duyệt qua từng ký tự trong key
            foreach (char c in key)
            {
                if (!seen.Contains(c))  // Nếu ký tự chưa xuất hiện
                {
                    seen.Add(c);  // Thêm ký tự vào HashSet
                    result.Append(c);  // Thêm ký tự vào chuỗi kết quả
                }
            }
            return result.ToString();  // Trả về chuỗi không có ký tự trùng lặp
        }

        // Phương thức lấy tọa độ của một ký tự trong ma trận
        public Coordinate_4_Hau GetCoordinate_4_Hau(char key)
        {
            key = key.ToString().ToUpper()[0];  // Đảm bảo ký tự là chữ hoa

            // Duyệt qua từng ô trong ma trận
            for (int i = 0; i < N_matrix; i++)
            {
                for (int j = 0; j < N_matrix; j++)
                {
                    if (matrix[i, j] == key)  // Nếu tìm thấy ký tự trong ma trận
                    {
                        // Trả về tọa độ của ký tự
                        Coordinate_4_Hau temp = new Coordinate_4_Hau()
                        {
                            I = i,
                            J = j
                        };
                        return temp;
                    }
                }
            }
            return null;  // Nếu không tìm thấy ký tự, trả về null
        }
    }
}
