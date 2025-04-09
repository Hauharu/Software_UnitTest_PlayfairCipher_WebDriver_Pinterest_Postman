using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Namespace chứa class này nằm trong project Playfair_Cipher_4_Hau_N1 chứa thư mục src
namespace Playfair_Cipher_4_Hau_N1.src
{
    public static class Alphabet_4_Hau
    {
        // Phương thức lấy vị trí của một ký tự trong bảng chữ cái (A=1, B=2, ..., Z=26)
        public static int GetPosition_4_Hau(char Chu_cai)
        {
            // Chuyển ký tự thành chữ hoa nếu là chữ thường
            char upperChar_4_Hau = char.ToUpper(Chu_cai);
            // Chuyển ký tự thành mã ASCII (A=65, B=66, ...) và trừ 64 để lấy vị trí (A=1, B=2, ...), ép kiểu thành số nguyên
            return (int)(upperChar_4_Hau) - 64;
        }

        // Phương thức lấy ký tự từ một vị trí trong bảng chữ cái (1=A, 2=B, ..., 26=Z)
        public static char GetCharacter_4_Hau(int x)
        {
            // Cộng x (kí tự) với 64 (vì A=65 trong ASCII) và ép kiểu thành ký tự
            return (char)(x + 64);
        }

        // Phương thức dịch chuyển vòng một vị trí trong bảng chữ cái theo offset
        // Trả về: Ký tự mới sau khi dịch chuyển
        public static char Seed_4_Hau(int currentPos, int offset)
        {
            // Tính vị trí mới sau khi dịch chuyển, dùng modulo 26 để quay vòng (A theo sau Z)
            int c_4_Hau = (currentPos + offset) % 26;
            // Nếu c = 0 (vượt quá Z quay về 0)
            if (c_4_Hau == 0) c_4_Hau = 26;
            // Cộng với 64 và ép kiểu thành ký tự (1=A, 2=B, ..., 26=Z)
            return (char)(c_4_Hau + 64);
        }

        // Phương thức tạo chuỗi chứa toàn bộ bảng chữ cái từ A đến Z
        // Trả về: Chuỗi "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        public static string Tolist_4_Hau()
        {
            // Sử dụng StringBuilder để xây dựng chuỗi 
            StringBuilder str_4_Hau = new StringBuilder();
            // Duyệt từ 0 đến 25 (26 ký tự), mỗi lần thêm một ký tự từ A (65) đến Z (90)
            for (int i = 0; i < 26; i++)
            {
                str_4_Hau.Append((char)(65 + i));
            }
            // Chuyển StringBuilder thành chuỗi và trả về
            return str_4_Hau.ToString();
        }
    }
}