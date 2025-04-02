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
using Playfair_Cipher_4_Hau_N1.src;

namespace Playfair_Cipher_4_Hau_N1
{
    public partial class Playfair_Cipher_4_Hau : Form
    {
        private PlayFair_4_Hau playfair_4_Hau;
        public Playfair_Cipher_4_Hau()
        {
            InitializeComponent();
            rdbSo_4_Hau.PerformClick();
            rdbMaHoa_4_Hau.PerformClick();
            playfair_4_Hau = new PlayFair_4_Hau(5);

        }

        private void btnKetthuc_4_Hau_Click(object sender, EventArgs e)
        {
            DialogResult Cancel_4_Hau = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát",
                                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Cancel_4_Hau == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnReset_4_Hau_Click(object sender, EventArgs e)
        {
            txtInput_4_Hau.Clear();
            txtKhoa_4_Hau.Clear();
            txtKetqua_4_Hau.Clear();
            txtInput_4_Hau.Focus();

            rdbMaHoa_4_Hau.Checked = false;
            rdbGiaiMa_4_Hau.Checked = false;
            rdbSo_4_Hau.Checked = false;

            groupBox1_4_Hau.Controls.Clear();
        }

        private void radioButtonMethod_CheckedChanged(object sender, EventArgs e)
        {
            var control = ((System.Windows.Forms.RadioButton)sender).Name;
            if (control == "rdbGiaiMa_4_Hau")
            {
                lbChuoi_4_Hau.Text = "Cypher Text";
                rdbGiaiMa_4_Hau.Text = "Plain Text";
                btnThuchien_4_Hau.Text = "Giải Mã";
            }
            else
            {
                rdbGiaiMa_4_Hau.Text = "Cypher Text";
                lbChuoi_4_Hau.Text = "Plain Text";
                btnThuchien_4_Hau.Text = "Mã Hóa";
            }
        }

        private void rdbSo_4_Hau_CheckedChanged(object sender, EventArgs e)
        {
            this.rdbSo_4_Hau.Controls.Clear();
            playfair_4_Hau = new PlayFair_4_Hau(5); // Luôn dùng ma trận 5x5
        }

        private void DrawMatrix_4_Hau(Matrix_4_Hau matrix, bool allowModifier)
        {
            // Xóa các control cũ trong GroupBox
            this.groupBox1_4_Hau.Controls.Clear();

            // Lấy kích thước của GroupBox
            int boxWidth_4_Hau = this.groupBox1_4_Hau.Width - 10;  // Trừ bớt để tránh tràn
            int boxHeight_4_Hau = this.groupBox1_4_Hau.Height - 30; // Giảm bớt để không đụng tiêu đề

            // Tính kích thước của từng ô
            int cellWidth_4_Hau = boxWidth_4_Hau / matrix.N_matrix;
            int cellHeight_4_Hau = boxHeight_4_Hau / matrix.N_matrix;

            // Điều chỉnh khoảng cách
            int padding_4_Hau = 2;  // Khoảng trống giữa các ô

            int X_begin_4_Hau = 5;  // Canh lề trái một chút
            int Y_begin_4_Hau = 35; // Đẩy xuống thấp hơn để tránh tiêu đề GroupBox

            for (int i = 0; i < matrix.N_matrix; i++)
            {
                for (int j = 0; j < matrix.N_matrix; j++)
                {
                    TextBox txt = new TextBox();
                    txt.Location = new System.Drawing.Point(X_begin_4_Hau, Y_begin_4_Hau);
                    txt.Name = "txt" + i.ToString() + j.ToString();
                    txt.Size = new System.Drawing.Size(cellWidth_4_Hau - padding_4_Hau, cellHeight_4_Hau - padding_4_Hau);
                    txt.Enabled = allowModifier;
                    txt.TextAlign = HorizontalAlignment.Center; // Căn giữa chữ

                    if (matrix != null)
                    {
                        txt.Text = matrix.Get_4_Hau(i, j).ToString();
                    }

                    this.groupBox1_4_Hau.Controls.Add(txt);
                    X_begin_4_Hau += cellWidth_4_Hau; // Dịch sang phải
                }
                X_begin_4_Hau = 5;  // Reset lại X để xuống dòng
                Y_begin_4_Hau += cellHeight_4_Hau; // Xuống dòng
            }
        }

        private void btnTaoMaTran_4_Hau_Click(object sender, EventArgs e)
        {
            try
            {
                var matrix_4_Hau = playfair_4_Hau.InitMatrix_4_Hau(txtKhoa_4_Hau.Text);
                DrawMatrix_4_Hau(matrix_4_Hau, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while init matrix with key: {ex.Message}", "Error", MessageBoxButtons.OK);
            }

        }

        private void btnThuchien_4_Hau_Click(object sender, EventArgs e)
        {
            if (rdbMaHoa_4_Hau.Checked)
            {
                try
                {
                    string result_4_Hau = playfair_4_Hau.Encrypt_4_Hau(txtInput_4_Hau.Text);
                    txtKetqua_4_Hau.Text = result_4_Hau;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error while init matrix with key: {ex.Message}", "Error", MessageBoxButtons.OK);
                }
            }
            else //giai ma
            {
                try
                {
                    string result_4_Hau = playfair_4_Hau.Decrypt_4_Hau(txtInput_4_Hau.Text);
                    txtKetqua_4_Hau.Text = result_4_Hau;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error while init matrix with key: {ex.Message}", "Error", MessageBoxButtons.OK);
                }
            }
        }
    }
}
