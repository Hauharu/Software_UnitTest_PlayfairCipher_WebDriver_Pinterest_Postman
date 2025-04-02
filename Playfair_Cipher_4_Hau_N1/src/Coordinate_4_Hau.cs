using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playfair_Cipher_4_Hau_N1.src
{
    public class Coordinate_4_Hau
    {
        public int I { get; set; }
        public int J { get; set; }

        // Constructor không tham số (bổ sung)
        public Coordinate_4_Hau() { }

        // Constructor có tham số
        public Coordinate_4_Hau(int i, int j)
        {
            I = i;
            J = j;
        }

        public override string ToString()
        {
            return $"({I}, {J})";
        }
    }
}
