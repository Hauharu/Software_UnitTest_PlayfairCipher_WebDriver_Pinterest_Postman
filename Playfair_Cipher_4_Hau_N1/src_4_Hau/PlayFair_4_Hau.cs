using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playfair_Cipher_4_Hau_N1.src_4_Hau
{
    // Lớp PlayFair_4_Hau sử dụng ma trận Playfair (MatrixPlayFair_4_Hau) để mã hóa và giải mã văn bản.
    public class PlayFair_4_Hau
    {
        private MatrixPlayFair_4_Hau matrix_4_Hau;  // Biến private lưu trữ đối tượng MatrixPlayFair_4_Hau

        // Constructor nhận vào một kích thước ma trận và khởi tạo đối tượng MatrixPlayFair_4_Hau với kích thước đó
        public PlayFair_4_Hau(int n_4_Hau)
        {
            matrix_4_Hau = new MatrixPlayFair_4_Hau(n_4_Hau);
        }

        // Phương thức khởi tạo ma trận Playfair với khóa (key)
        public MatrixPlayFair_4_Hau InitMatrix_4_Hau(string key_4_Hau)
        {
            matrix_4_Hau.CreateMatrix_4_Hau(key_4_Hau);  // Gọi phương thức CreateMatrix_4_Hau để tạo ma trận từ khóa
            return matrix_4_Hau;
        }

        // Phương thức loại bỏ khoảng trắng trong chuỗi văn bản
        public string RemoveSpaceInString_4_Hau(string text_4_Hau)
        {
            var temp_4_Hau = String.Copy(text_4_Hau);  // Tạo bản sao của chuỗi văn bản
            int i_4_Hau = 0;
            // Duyệt qua chuỗi và loại bỏ khoảng trắng
            while (i_4_Hau < temp_4_Hau.Length)
            {
                if (temp_4_Hau[i_4_Hau] == ' ')
                {
                    temp_4_Hau = temp_4_Hau.Remove(i_4_Hau, 1);  // Loại bỏ ký tự khoảng trắng tại vị trí i
                }
                else
                {
                    i_4_Hau++;  // Nếu không phải khoảng trắng, tăng chỉ số i
                }
            }
            return temp_4_Hau;  // Trả về chuỗi đã loại bỏ khoảng trắng
        }

        // Phương thức hoán đổi vị trí của hai ký tự trong ma trận Playfair
        public string HoanDoi_4_Hau(Coordinate_4_Hau dau_4_Hau, Coordinate_4_Hau cuoi_4_Hau, bool isEncrypt_4_Hau = true)
        {
            int value_4_Hau = isEncrypt_4_Hau ? 1 : -1;  // Nếu mã hóa, value = 1, nếu giải mã, value = -1

            // Kiểm tra xem các chỉ số tọa độ có hợp lệ không
            if (!(dau_4_Hau.I_4_Hau < matrix_4_Hau.N_matrix_4_Hau && dau_4_Hau.I_4_Hau >= 0 && dau_4_Hau.J_4_Hau < matrix_4_Hau.N_matrix_4_Hau && dau_4_Hau.J_4_Hau >= 0
                && cuoi_4_Hau.I_4_Hau < matrix_4_Hau.N_matrix_4_Hau && cuoi_4_Hau.I_4_Hau >= 0 && cuoi_4_Hau.J_4_Hau < matrix_4_Hau.N_matrix_4_Hau && cuoi_4_Hau.J_4_Hau >= 0))
                throw new IndexOutOfRangeException();  // Nếu không hợp lệ, ném lỗi

            var result_4_Hau = "";
            // Nếu ký tự cùng hàng
            if (dau_4_Hau.I_4_Hau == cuoi_4_Hau.I_4_Hau)
            {
                // Hoán đổi cột của hai ký tự
                result_4_Hau += matrix_4_Hau.Get_4_Hau(dau_4_Hau.I_4_Hau, (dau_4_Hau.J_4_Hau + value_4_Hau + matrix_4_Hau.N_matrix_4_Hau) % matrix_4_Hau.N_matrix_4_Hau);
                result_4_Hau += matrix_4_Hau.Get_4_Hau(cuoi_4_Hau.I_4_Hau, (cuoi_4_Hau.J_4_Hau + value_4_Hau + matrix_4_Hau.N_matrix_4_Hau) % matrix_4_Hau.N_matrix_4_Hau);
            }
            else if (dau_4_Hau.J_4_Hau == cuoi_4_Hau.J_4_Hau)  // Nếu ký tự cùng cột
            {
                // Hoán đổi hàng của hai ký tự
                result_4_Hau += matrix_4_Hau.Get_4_Hau((dau_4_Hau.I_4_Hau + value_4_Hau + matrix_4_Hau.N_matrix_4_Hau) % matrix_4_Hau.N_matrix_4_Hau, dau_4_Hau.J_4_Hau);
                result_4_Hau += matrix_4_Hau.Get_4_Hau((cuoi_4_Hau.I_4_Hau + value_4_Hau + matrix_4_Hau.N_matrix_4_Hau) % matrix_4_Hau.N_matrix_4_Hau, cuoi_4_Hau.J_4_Hau);
            }
            else  // Nếu ký tự khác hàng và khác cột
            {
                // Hoán đổi vị trí chéo của hai ký tự
                result_4_Hau += matrix_4_Hau.Get_4_Hau(dau_4_Hau.I_4_Hau, cuoi_4_Hau.J_4_Hau);
                result_4_Hau += matrix_4_Hau.Get_4_Hau(cuoi_4_Hau.I_4_Hau, dau_4_Hau.J_4_Hau);
            }
            return result_4_Hau;  // Trả về kết quả hoán đổi
        }

        // Phương thức mã hóa hai ký tự trong ma trận Playfair
        public string EncryptTwoCharacter_4_Hau(char one_4_Hau, char two_4_Hau)
        {
            if (one_4_Hau == ' ' || two_4_Hau == ' ')  // Nếu có ký tự khoảng trắng
                throw new ArgumentException();

            // Lấy tọa độ của hai ký tự
            Coordinate_4_Hau c_one_4_Hau, c_two_4_Hau;
            c_one_4_Hau = matrix_4_Hau.GetCoordinate_4_Hau(one_4_Hau);
            c_two_4_Hau = matrix_4_Hau.GetCoordinate_4_Hau(two_4_Hau);
            if (c_one_4_Hau == null || c_two_4_Hau == null)  // Nếu không tìm thấy tọa độ, ném lỗi
                throw new ArgumentException();

            // Mã hóa hai ký tự
            string result_4_Hau = HoanDoi_4_Hau(c_one_4_Hau, c_two_4_Hau);
            return result_4_Hau;
        }

        // Phương thức mã hóa văn bản với Playfair cipher
        public string Encrypt_4_Hau(string plainText_4_Hau)
        {
            plainText_4_Hau = RemoveSpaceInString_4_Hau(plainText_4_Hau);  // Loại bỏ khoảng trắng trong văn bản
            if (plainText_4_Hau.Length == 0)
                throw new ArgumentException("plaintext is empty");

            string modifiedText_4_Hau = "";
            // Duyệt qua văn bản và chèn "X" vào giữa các cặp ký tự trùng lặp
            for (int i_4_Hau = 0; i_4_Hau < plainText_4_Hau.Length; i_4_Hau++)
            {
                modifiedText_4_Hau += plainText_4_Hau[i_4_Hau];
                if (i_4_Hau % 2 == 0 && i_4_Hau + 1 < plainText_4_Hau.Length && plainText_4_Hau[i_4_Hau] == plainText_4_Hau[i_4_Hau + 1])
                {
                    modifiedText_4_Hau += "X";  // Chèn "X" nếu gặp cặp ký tự trùng
                }
            }
            if (modifiedText_4_Hau.Length % 2 != 0)
                modifiedText_4_Hau += "X";  // Nếu chiều dài văn bản lẻ, thêm "X"

            string result_4_Hau = "";
            // Mã hóa từng cặp ký tự trong văn bản
            for (int i_4_Hau = 0; i_4_Hau < modifiedText_4_Hau.Length; i_4_Hau += 2)
            {
                var temp_4_Hau = EncryptTwoCharacter_4_Hau(modifiedText_4_Hau[i_4_Hau], modifiedText_4_Hau[i_4_Hau + 1]);
                result_4_Hau += temp_4_Hau ?? throw new ArgumentException("plain text invalid");
            }
            return result_4_Hau;  // Trả về văn bản mã hóa
        }

        // Phương thức giải mã hai ký tự trong ma trận Playfair
        public string DecryptTwoCharacter_4_Hau(char one_4_Hau, char two_4_Hau)
        {
            if (one_4_Hau == ' ' || two_4_Hau == ' ')  // Nếu có ký tự khoảng trắng
                throw new ArgumentException();

            // Lấy tọa độ của hai ký tự
            Coordinate_4_Hau c_one_4_Hau, c_two_4_Hau;
            c_one_4_Hau = matrix_4_Hau.GetCoordinate_4_Hau(one_4_Hau);
            c_two_4_Hau = matrix_4_Hau.GetCoordinate_4_Hau(two_4_Hau);
            if (c_one_4_Hau == null || c_two_4_Hau == null)  // Nếu không tìm thấy tọa độ, ném lỗi
                throw new ArgumentException();

            // Giải mã hai ký tự
            string result_4_Hau = HoanDoi_4_Hau(c_one_4_Hau, c_two_4_Hau, false);
            return result_4_Hau;
        }

        // Phương thức giải mã văn bản với Playfair cipher
        public string Decrypt_4_Hau(string plainText_4_Hau)
        {
            // Loại bỏ khoảng trắng trong văn bản
            plainText_4_Hau = RemoveSpaceInString_4_Hau(plainText_4_Hau);
            if (plainText_4_Hau.Length == 0)
                throw new ArgumentException("plaintext is empty");

            if (plainText_4_Hau.Length % 2 != 0)
            {
                plainText_4_Hau += "X";  // Nếu chiều dài văn bản lẻ, thêm "X"
            }
            // Giải mã từng cặp ký tự trong văn bản
            int i_4_Hau = 0;
            string result_4_Hau = "";
            while (i_4_Hau < plainText_4_Hau.Length)
            {
                var temp_4_Hau = DecryptTwoCharacter_4_Hau(plainText_4_Hau[i_4_Hau], plainText_4_Hau[i_4_Hau + 1]);
                result_4_Hau += temp_4_Hau ?? throw new ArgumentException("plain text invalid");
                i_4_Hau += 2;
            }
            return result_4_Hau;  // Trả về văn bản giải mã
        }
    }
}