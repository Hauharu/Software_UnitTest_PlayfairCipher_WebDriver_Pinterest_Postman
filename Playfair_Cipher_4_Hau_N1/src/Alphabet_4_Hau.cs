using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playfair_Cipher_4_Hau_N1.src
{
    public static class Alphabet_4_Hau
    {
        public static int GetPosition_4_Hau(char Chu_cai)
        {
            return (int)(Chu_cai) - 64;
        }
        public static char GetCharacter_4_Hau(int x)
        {
            return (char)(x + 64);
        }
        public static char Seed_4_Hau(int currentPos, int offset)   
        {
            int c = (currentPos + offset) % 26;
            if (c == 0) c = 26;  // Tránh lỗi ký tự '@'
            return (char)(c + 64);
        }

        public static string Tolist_4_Hau()
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < 26; i++)
            {
                str.Append((char)(65 + i));
            }
            return str.ToString();
        }
    }
}
