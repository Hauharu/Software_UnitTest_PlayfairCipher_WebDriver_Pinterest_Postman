using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playfair_Cipher_4_Hau_N1.src_4_Hau
{
    public class Coordinate_4_Hau
    {
        // Thuộc tính hàng, lưu trữ giá trị hàng (row) trong ma trận 5x5.
        public int I_4_Hau { get; set; }
        // Thuộc tính cột, lưu trữ giá trị cột (column) trong ma trận 5x5.
        public int J_4_Hau { get; set; }

        // Constructor không tham số
        public Coordinate_4_Hau() { }

        // Constructor có tham số để khởi tạo tọa độ
        public Coordinate_4_Hau(int i_4_Hau, int j_4_Hau)
        {
            I_4_Hau = i_4_Hau;
            J_4_Hau = j_4_Hau;
        }

        // Ghi đè phương thức ToString để in ra định dạng (I, J)
        public override string ToString()
        {
            return $"({I_4_Hau}, {J_4_Hau})";
        }
    }
}
