using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playfair_Cipher_4_Hau_N1.src
{
    public class MatrixPlayFair_4_Hau : Matrix_4_Hau
    {
        public MatrixPlayFair_4_Hau(int n_matrix) : base(n_matrix)
        {

        }

        /// <summary>
        /// Init matrix key
        /// </summary>
        /// <param name="key"></param>
        public void CreateMatrix_4_Hau(string key)
        {
            string temp = key;
            var alpha = Alphabet_4_Hau.Tolist_4_Hau();
            var an = string.Concat(alpha) + "0123456789";
            // Remove space
            temp = temp.Replace(" ", "");
            temp = temp.ToUpper();
            // Key must not contain special characters
            foreach (char c in temp)
            {
                if (!an.Contains<char>(c))
                    throw new ArgumentException("Key must not contain special characters");
            }

            temp += alpha;
            if (N_matrix == 5)
            {
                temp = temp.Replace('J', 'I');
            }
            else if (N_matrix == 6)
            {
                temp += "0123456789";
            }
            else
            {
                throw new Exception("Size of matrix must be 5 or 6");
            }

            temp = RemoveSameCharacter_4_Hau(temp);

            // Check if the number of characters matches the matrix size
            int expectedLength = N_matrix * N_matrix; // 25 for 5x5, 36 for 6x6
            if (temp.Length != expectedLength)
            {
                throw new Exception($"Expected {expectedLength} characters, but got {temp.Length} characters after processing the key.");
            }

            // Fill the matrix
            int k = 0;
            foreach (char x in temp.ToCharArray())
            {
                base.Set_4_Hau(k / N_matrix, k % N_matrix, x);
                k++;
            }
        }
        /// <summary>
        /// Delete same character string
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
#if TEST
        public string RemoveSameCharacter(string key)
#else
        private string RemoveSameCharacter_4_Hau(string key)
#endif
        {
            HashSet<char> seen = new HashSet<char>();
            StringBuilder result = new StringBuilder();

            foreach (char c in key)
            {
                if (!seen.Contains(c))
                {
                    seen.Add(c);
                    result.Append(c);
                }
            }
            return result.ToString();
        }


        /// <summary>
        /// Find Coordinate of a character in matrix
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Coordinate_4_Hau GetCoordinate_4_Hau(char key)
        {
            key = key.ToString().ToUpper()[0];
            for (int i = 0; i < N_matrix; i++)
            {
                for (int j = 0; j < N_matrix; j++)
                {
                    if (matrix[i, j] == key)
                    {
                        Coordinate_4_Hau temp = new Coordinate_4_Hau()
                        {
                            I = i,
                            J = j
                        };
                        return temp;
                    }
                }
            }
            return null;
        }
    }
}
