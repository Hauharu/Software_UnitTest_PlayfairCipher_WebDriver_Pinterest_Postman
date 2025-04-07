using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playfair_Cipher_4_Hau_N1.src
{
    public class Coordinate_4_Hau
    {
        // Thuộc tính hàng
        public int I { get; set; }

        // Thuộc tính cột
        public int J { get; set; }

        // Constructor không tham số
        public Coordinate_4_Hau() { }

        // Constructor có tham số để khởi tạo tọa độ
        public Coordinate_4_Hau(int i, int j)
        {
            I = i;
            J = j;
        }

        // Ghi đè phương thức ToString để in ra định dạng (I, J)
        public override string ToString()
        {
            return $"({I}, {J})";
        }
    }
}
