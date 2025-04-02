using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playfair_Cipher_4_Hau_N1.src
{
    public class PlayFair_4_Hau
    {
        private MatrixPlayFair_4_Hau matrix;
        public PlayFair_4_Hau(int n)
        {
            matrix = new MatrixPlayFair_4_Hau(n);
        }

        public MatrixPlayFair_4_Hau InitMatrix_4_Hau(string key)
        {
            matrix.CreateMatrix_4_Hau(key);
            return matrix;
        }

        public string RemoveSpaceInString_4_Hau(string text)
        {
            var temp = String.Copy(text);
            int i = 0;
            while (i < temp.Length)
            {
                if (temp[i] == ' ')
                {
                    temp = temp.Remove(i, 1);
                }
                else
                {
                    i++;
                }
            }
            return temp;
        }

        public string HoanDoi_4_Hau(Coordinate_4_Hau dau, Coordinate_4_Hau cuoi, bool isEncrypt = true)
        {
            int value = isEncrypt ? 1 : -1;
            if (!(dau.I < matrix.N_matrix && dau.I >= 0 && dau.J < matrix.N_matrix && dau.J >= 0
                && cuoi.I < matrix.N_matrix && cuoi.I >= 0 && cuoi.J < matrix.N_matrix && cuoi.J >= 0))
                throw new IndexOutOfRangeException();

            var result = "";
            if (dau.I == cuoi.I) // Cùng hàng
            {
                result += matrix.Get_4_Hau(dau.I, (dau.J + value + matrix.N_matrix) % matrix.N_matrix);
                result += matrix.Get_4_Hau(cuoi.I, (cuoi.J + value + matrix.N_matrix) % matrix.N_matrix);
            }
            else if (dau.J == cuoi.J) // Cùng cột
            {
                result += matrix.Get_4_Hau((dau.I + value + matrix.N_matrix) % matrix.N_matrix, dau.J);
                result += matrix.Get_4_Hau((cuoi.I + value + matrix.N_matrix) % matrix.N_matrix, cuoi.J);
            }
            else // Khác hàng, khác cột
            {
                result += matrix.Get_4_Hau(dau.I, cuoi.J);
                result += matrix.Get_4_Hau(cuoi.I, dau.J);
            }
            return result;
        }

        public string EncryptTwoCharacter_4_Hau(char one, char two)
        {
            if (one == ' ' || two == ' ')
                throw new ArgumentException();
            //get coordinate of one and two
            Coordinate_4_Hau c_one, c_two;
            c_one = matrix.GetCoordinate_4_Hau(one);
            c_two = matrix.GetCoordinate_4_Hau(two);
            if (c_one == null || c_two == null)
                throw new ArgumentException();

            //encrypt two character
            string result = HoanDoi_4_Hau(c_one, c_two);
            return result;
        }

        public string Encrypt_4_Hau(string plainText)
        {
            plainText = RemoveSpaceInString_4_Hau(plainText);
            if (plainText.Length == 0)
                throw new ArgumentException("plaintext is empty");

            string modifiedText = "";
            for (int i = 0; i < plainText.Length; i++)
            {
                modifiedText += plainText[i];
                if (i % 2 == 0 && i + 1 < plainText.Length && plainText[i] == plainText[i + 1])
                {
                    modifiedText += "X"; // Chèn "X" giữa cặp ký tự trùng
                }
            }
            if (modifiedText.Length % 2 != 0)
                modifiedText += "X";

            string result = "";
            for (int i = 0; i < modifiedText.Length; i += 2)
            {
                var temp = EncryptTwoCharacter_4_Hau(modifiedText[i], modifiedText[i + 1]);
                result += temp ?? throw new ArgumentException("plain text invalid");
            }
            return result;
        }

        public string DecryptTwoCharacter_4_Hau(char one, char two)
        {
            if (one == ' ' || two == ' ')
                throw new ArgumentException();
            //get coordinate of one and two
            Coordinate_4_Hau c_one, c_two;
            c_one = matrix.GetCoordinate_4_Hau(one);
            c_two = matrix.GetCoordinate_4_Hau(two);
            if (c_one == null || c_two == null)
                throw new ArgumentException();

            //encrypt two character
            string result = HoanDoi_4_Hau(c_one, c_two, false);
            return result;
        }

        public string Decrypt_4_Hau(string plainText)
        {
            //remove space 
            plainText = RemoveSpaceInString_4_Hau(plainText);
            if (plainText.Length == 0)
                throw new ArgumentException("plaintext is empty");

            if (plainText.Length % 2 != 0)
            {
                plainText += "X";
            }
            //split pair two character to encrypt
            int i = 0;
            string result = "";
            while (i < plainText.Length)
            {
                var temp = DecryptTwoCharacter_4_Hau(plainText[i], plainText[i + 1]);
                result += temp ?? throw new ArgumentException("plain text invalid");
                i += 2;
            }
            return result;
        }
    }
}