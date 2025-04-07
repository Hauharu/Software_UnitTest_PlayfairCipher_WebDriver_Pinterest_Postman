using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playfair_Cipher_4_Hau_N1.src
{
    // Lớp PlayFair_4_Hau sử dụng ma trận Playfair (MatrixPlayFair_4_Hau) để mã hóa và giải mã văn bản.
    public class PlayFair_4_Hau
    {
        private MatrixPlayFair_4_Hau matrix;  // Biến private lưu trữ đối tượng MatrixPlayFair_4_Hau

        // Constructor nhận vào một kích thước ma trận và khởi tạo đối tượng MatrixPlayFair_4_Hau với kích thước đó
        public PlayFair_4_Hau(int n)
        {
            matrix = new MatrixPlayFair_4_Hau(n);
        }

        // Phương thức khởi tạo ma trận Playfair với khóa (key)
        public MatrixPlayFair_4_Hau InitMatrix_4_Hau(string key)
        {
            matrix.CreateMatrix_4_Hau(key);  // Gọi phương thức CreateMatrix_4_Hau để tạo ma trận từ khóa
            return matrix;
        }

        // Phương thức loại bỏ khoảng trắng trong chuỗi văn bản
        public string RemoveSpaceInString_4_Hau(string text)
        {
            var temp = String.Copy(text);  // Tạo bản sao của chuỗi văn bản
            int i = 0;
            // Duyệt qua chuỗi và loại bỏ khoảng trắng
            while (i < temp.Length)
            {
                if (temp[i] == ' ')
                {
                    temp = temp.Remove(i, 1);  // Loại bỏ ký tự khoảng trắng tại vị trí i
                }
                else
                {
                    i++;  // Nếu không phải khoảng trắng, tăng chỉ số i
                }
            }
            return temp;  // Trả về chuỗi đã loại bỏ khoảng trắng
        }

        // Phương thức hoán đổi vị trí của hai ký tự trong ma trận Playfair
        public string HoanDoi_4_Hau(Coordinate_4_Hau dau, Coordinate_4_Hau cuoi, bool isEncrypt = true)
        {
            int value = isEncrypt ? 1 : -1;  // Nếu mã hóa, value = 1, nếu giải mã, value = -1

            // Kiểm tra xem các chỉ số tọa độ có hợp lệ không
            if (!(dau.I < matrix.N_matrix && dau.I >= 0 && dau.J < matrix.N_matrix && dau.J >= 0
                && cuoi.I < matrix.N_matrix && cuoi.I >= 0 && cuoi.J < matrix.N_matrix && cuoi.J >= 0))
                throw new IndexOutOfRangeException();  // Nếu không hợp lệ, ném lỗi

            var result = "";
            // Nếu ký tự cùng hàng
            if (dau.I == cuoi.I)
            {
                // Hoán đổi cột của hai ký tự
                result += matrix.Get_4_Hau(dau.I, (dau.J + value + matrix.N_matrix) % matrix.N_matrix);
                result += matrix.Get_4_Hau(cuoi.I, (cuoi.J + value + matrix.N_matrix) % matrix.N_matrix);
            }
            else if (dau.J == cuoi.J)  // Nếu ký tự cùng cột
            {
                // Hoán đổi hàng của hai ký tự
                result += matrix.Get_4_Hau((dau.I + value + matrix.N_matrix) % matrix.N_matrix, dau.J);
                result += matrix.Get_4_Hau((cuoi.I + value + matrix.N_matrix) % matrix.N_matrix, cuoi.J);
            }
            else  // Nếu ký tự khác hàng và khác cột
            {
                // Hoán đổi vị trí chéo của hai ký tự
                result += matrix.Get_4_Hau(dau.I, cuoi.J);
                result += matrix.Get_4_Hau(cuoi.I, dau.J);
            }
            return result;  // Trả về kết quả hoán đổi
        }

        // Phương thức mã hóa hai ký tự trong ma trận Playfair
        public string EncryptTwoCharacter_4_Hau(char one, char two)
        {
            if (one == ' ' || two == ' ')  // Nếu có ký tự khoảng trắng
                throw new ArgumentException();

            // Lấy tọa độ của hai ký tự
            Coordinate_4_Hau c_one, c_two;
            c_one = matrix.GetCoordinate_4_Hau(one);
            c_two = matrix.GetCoordinate_4_Hau(two);
            if (c_one == null || c_two == null)  // Nếu không tìm thấy tọa độ, ném lỗi
                throw new ArgumentException();

            // Mã hóa hai ký tự
            string result = HoanDoi_4_Hau(c_one, c_two);
            return result;
        }

        // Phương thức mã hóa văn bản với Playfair cipher
        public string Encrypt_4_Hau(string plainText)
        {
            plainText = RemoveSpaceInString_4_Hau(plainText);  // Loại bỏ khoảng trắng trong văn bản
            if (plainText.Length == 0)
                throw new ArgumentException("plaintext is empty");

            string modifiedText = "";
            // Duyệt qua văn bản và chèn "X" vào giữa các cặp ký tự trùng lặp
            for (int i = 0; i < plainText.Length; i++)
            {
                modifiedText += plainText[i];
                if (i % 2 == 0 && i + 1 < plainText.Length && plainText[i] == plainText[i + 1])
                {
                    modifiedText += "X";  // Chèn "X" nếu gặp cặp ký tự trùng
                }
            }
            if (modifiedText.Length % 2 != 0)
                modifiedText += "X";  // Nếu chiều dài văn bản lẻ, thêm "X"

            string result = "";
            // Mã hóa từng cặp ký tự trong văn bản
            for (int i = 0; i < modifiedText.Length; i += 2)
            {
                var temp = EncryptTwoCharacter_4_Hau(modifiedText[i], modifiedText[i + 1]);
                result += temp ?? throw new ArgumentException("plain text invalid");
            }
            return result;  // Trả về văn bản mã hóa
        }

        // Phương thức giải mã hai ký tự trong ma trận Playfair
        public string DecryptTwoCharacter_4_Hau(char one, char two)
        {
            if (one == ' ' || two == ' ')  // Nếu có ký tự khoảng trắng
                throw new ArgumentException();

            // Lấy tọa độ của hai ký tự
            Coordinate_4_Hau c_one, c_two;
            c_one = matrix.GetCoordinate_4_Hau(one);
            c_two = matrix.GetCoordinate_4_Hau(two);
            if (c_one == null || c_two == null)  // Nếu không tìm thấy tọa độ, ném lỗi
                throw new ArgumentException();

            // Giải mã hai ký tự
            string result = HoanDoi_4_Hau(c_one, c_two, false);
            return result;
        }

        // Phương thức giải mã văn bản với Playfair cipher
        public string Decrypt_4_Hau(string plainText)
        {
            // Loại bỏ khoảng trắng trong văn bản
            plainText = RemoveSpaceInString_4_Hau(plainText);
            if (plainText.Length == 0)
                throw new ArgumentException("plaintext is empty");

            if (plainText.Length % 2 != 0)
            {
                plainText += "X";  // Nếu chiều dài văn bản lẻ, thêm "X"
            }
            // Giải mã từng cặp ký tự trong văn bản
            int i = 0;
            string result = "";
            while (i < plainText.Length)
            {
                var temp = DecryptTwoCharacter_4_Hau(plainText[i], plainText[i + 1]);
                result += temp ?? throw new ArgumentException("plain text invalid");
                i += 2;
            }
            return result;  // Trả về văn bản giải mã
        }
    }
}
