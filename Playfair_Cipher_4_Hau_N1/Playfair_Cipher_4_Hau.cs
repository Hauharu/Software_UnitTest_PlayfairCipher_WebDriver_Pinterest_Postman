using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using Playfair_Cipher_4_Hau_N1.src_4_Hau;

namespace Playfair_Cipher_4_Hau_N1
{
    public partial class Playfair_Cipher_4_Hau : Form
    {
        // Biến private để lưu đối tượng PlayFair_4_Hau dùng cho việc mã hóa/giải mã
        private PlayFair_4_Hau playfair_4_Hau;

        // Constructor của form
        public Playfair_Cipher_4_Hau()
        {
            // Khởi tạo các thành phần giao diện của form (được tự động tạo bởi Windows Forms Designer)
            InitializeComponent();

            // Tự động click vào radio button "rdbSo_4_Hau" để đặt chế độ mặc định
            rdbSo_4_Hau.PerformClick();

            // Tự động click vào radio button "rdbMaHoa_4_Hau" để đặt hành động mặc định là mã hóa
            rdbMaHoa_4_Hau.PerformClick();

            // Khởi tạo đối tượng PlayFair_4_Hau với ma trận kích thước 5x5
            playfair_4_Hau = new PlayFair_4_Hau(5);
        }

        // Xử lý sự kiện khi click nút "Kết Thúc"
        private void btnKetthuc_4_Hau_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận xem người dùng có muốn thoát không
            DialogResult Cancel_4_Hau = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát không?", // Thông điệp
                "Xác nhận thoát", // Tiêu đề
                MessageBoxButtons.YesNo, // Các nút lựa chọn: Có/Không
                MessageBoxIcon.Question // Biểu tượng câu hỏi
            );

            // Nếu người dùng chọn "Có", thoát ứng dụng
            if (Cancel_4_Hau == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // Xử lý sự kiện khi click nút "Reset"
        private void btnReset_4_Hau_Click(object sender, EventArgs e)
        {
            // Xóa nội dung ô nhập văn bản
            txtInput_4_Hau.Clear();

            // Xóa nội dung ô nhập khóa
            txtKhoa_4_Hau.Clear();

            // Xóa nội dung ô kết quả
            txtKetqua_4_Hau.Clear();

            // Đưa con trỏ về ô nhập văn bản
            txtInput_4_Hau.Focus();

            // Bỏ chọn tất cả radio button
            rdbMaHoa_4_Hau.Checked = false;
            rdbGiaiMa_4_Hau.Checked = false;
            rdbSo_4_Hau.Checked = false;

            // Xóa toàn bộ các điều khiển (controls) trong groupBox1_4_Hau (nơi hiển thị ma trận)
            groupBox1_4_Hau.Controls.Clear();
        }

        // Xử lý sự kiện khi thay đổi trạng thái của radio button chọn phương thức (Mã hóa/Giải mã)
        private void radioButtonMethod_CheckedChanged(object sender, EventArgs e)
        {
            // Lấy tên của radio button được chọn
            var control = ((System.Windows.Forms.RadioButton)sender).Name;

            // Nếu chọn radio button "Giải Mã"
            if (control == "rdbGiaiMa_4_Hau")
            {
                // Đổi nhãn của ô nhập thành "Cypher Text" (Văn bản mã hóa)
                lbChuoi_4_Hau.Text = "Cypher Text";

                // Đổi nhãn của radio button "Giải Mã" thành "Plain Text" (Văn bản gốc)
                rdbGiaiMa_4_Hau.Text = "Plain Text";

                // Đổi chữ trên nút "Thực Hiện" thành "Giải Mã"
                btnThuchien_4_Hau.Text = "Giải Mã";
            }
            else // Nếu chọn radio button "Mã Hóa"
            {
                // Đổi nhãn của radio button "Giải Mã" thành "Cypher Text" (Văn bản mã hóa)
                rdbGiaiMa_4_Hau.Text = "Cypher Text";

                // Đổi nhãn của ô nhập thành "Plain Text" (Văn bản gốc)
                lbChuoi_4_Hau.Text = "Plain Text";

                // Đổi chữ trên nút "Thực Hiện" thành "Mã Hóa"
                btnThuchien_4_Hau.Text = "Mã Hóa";
            }
        }

        // Xử lý sự kiện khi thay đổi trạng thái của radio button "Số" (chọn chế độ sử dụng ma trận 5x5)
        private void rdbSo_4_Hau_CheckedChanged(object sender, EventArgs e)
        {
            // Xóa các điều khiển con trong radio button "Số" (nếu có)
            this.rdbSo_4_Hau.Controls.Clear();

            // Khởi tạo lại đối tượng PlayFair_4_Hau với ma trận 5x5
            playfair_4_Hau = new PlayFair_4_Hau(5);
        }

        // Hàm vẽ ma trận Playfair lên giao diện trong groupBox1_4_Hau
        private void DrawMatrix_4_Hau(Matrix_4_Hau matrix, bool allowModifier)
        {
            // Xóa tất cả các điều khiển cũ trong groupBox1_4_Hau
            this.groupBox1_4_Hau.Controls.Clear();

            // Lấy kích thước của groupBox1_4_Hau
            int boxWidth_4_Hau = this.groupBox1_4_Hau.Width - 10;  // Trừ bớt 10 để tránh tràn viền
            int boxHeight_4_Hau = this.groupBox1_4_Hau.Height - 30; // Trừ bớt 30 để không đụng tiêu đề groupBox

            // Tính kích thước của mỗi ô trong ma trận (dựa trên kích thước groupBox)
            int cellWidth_4_Hau = boxWidth_4_Hau / matrix.N_matrix_4_Hau;
            int cellHeight_4_Hau = boxHeight_4_Hau / matrix.N_matrix_4_Hau;

            // Đặt khoảng cách giữa các ô
            int padding_4_Hau = 2;  // Khoảng trống giữa các ô để tránh dính nhau

            // Đặt vị trí bắt đầu vẽ ma trận
            int X_begin_4_Hau = 5;  // Canh lề trái một chút
            int Y_begin_4_Hau = 35; // Đẩy xuống thấp hơn để tránh đè lên tiêu đề groupBox

            // Duyệt qua từng hàng và cột của ma trận
            for (int i = 0; i < matrix.N_matrix_4_Hau; i++)
            {
                for (int j = 0; j < matrix.N_matrix_4_Hau; j++)
                {
                    // Tạo một ô TextBox để hiển thị ký tự trong ma trận
                    TextBox txt = new TextBox();
                    txt.Location = new System.Drawing.Point(X_begin_4_Hau, Y_begin_4_Hau); // Đặt vị trí ô
                    txt.Name = "txt" + i.ToString() + j.ToString(); // Đặt tên ô (ví dụ: txt00, txt01,...)
                    txt.Size = new System.Drawing.Size(cellWidth_4_Hau - padding_4_Hau, cellHeight_4_Hau - padding_4_Hau); // Đặt kích thước ô
                    txt.Enabled = allowModifier; // Cho phép chỉnh sửa nếu allowModifier = true
                    txt.TextAlign = HorizontalAlignment.Center; // Căn giữa chữ trong ô

                    // Nếu ma trận không rỗng, điền ký tự tại vị trí (i, j) vào ô
                    if (matrix != null)
                    {
                        txt.Text = matrix.Get_4_Hau(i, j).ToString();
                    }

                    // Thêm ô TextBox vào groupBox1_4_Hau
                    this.groupBox1_4_Hau.Controls.Add(txt);

                    // Dịch sang phải để vẽ ô tiếp theo trong cùng hàng
                    X_begin_4_Hau += cellWidth_4_Hau;
                }
                // Reset lại vị trí X để xuống hàng mới
                X_begin_4_Hau = 5;

                // Dịch xuống dưới để vẽ hàng tiếp theo
                Y_begin_4_Hau += cellHeight_4_Hau;
            }
        }

        // Xử lý sự kiện khi click nút "Tạo Ma Trận"
        private void btnTaoMaTran_4_Hau_Click(object sender, EventArgs e)
        {
            try
            {
                // Gọi phương thức InitMatrix_4_Hau để tạo ma trận Playfair từ khóa trong txtKhoa_4_Hau
                var matrix_4_Hau = playfair_4_Hau.InitMatrix_4_Hau(txtKhoa_4_Hau.Text);

                // Vẽ ma trận lên giao diện, không cho phép chỉnh sửa (false)
                DrawMatrix_4_Hau(matrix_4_Hau, false);
            }
            catch (Exception ex)
            {
                // Nếu có lỗi (ví dụ: khóa không hợp lệ), hiển thị thông báo lỗi
                MessageBox.Show($"Lỗi khi khởi tạo ma trận với khóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK);
            }
        }

        // Xử lý sự kiện khi click nút "Thực Hiện" (Mã hóa hoặc Giải mã)
        private void btnThuchien_4_Hau_Click(object sender, EventArgs e)
        {
            // Nếu chọn "Mã Hóa"
            if (rdbMaHoa_4_Hau.Checked)
            {
                try
                {
                    // Gọi phương thức Encrypt_4_Hau để mã hóa văn bản trong txtInput_4_Hau
                    string result_4_Hau = playfair_4_Hau.Encrypt_4_Hau(txtInput_4_Hau.Text);

                    // Hiển thị kết quả mã hóa trong txtKetqua_4_Hau
                    txtKetqua_4_Hau.Text = result_4_Hau;
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi (ví dụ: văn bản không hợp lệ), hiển thị thông báo lỗi
                    MessageBox.Show($"Lỗi khi mã hóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK);
                }
            }
            else // Nếu chọn "Giải Mã"
            {
                try
                {
                    // Gọi phương thức Decrypt_4_Hau để giải mã văn bản trong txtInput_4_Hau
                    string result_4_Hau = playfair_4_Hau.Decrypt_4_Hau(txtInput_4_Hau.Text);

                    // Hiển thị kết quả giải mã trong txtKetqua_4_Hau
                    txtKetqua_4_Hau.Text = result_4_Hau;
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi (ví dụ: văn bản không hợp lệ), hiển thị thông báo lỗi
                    MessageBox.Show($"Lỗi khi giải mã: {ex.Message}", "Lỗi", MessageBoxButtons.OK);
                }
            }
        }
    }
}