
// File: Playfair_Test_4_Hau.cs
// Mục đích: File này chứa các test case (unit test) cho thuật toán mã hóa Playfair.
// Các test case tập trung vào kiểm tra các chức năng mã hóa (Encrypt_4_Hau) và giải mã (Decrypt_4_Hau)
// với các trường hợp thông thường, cùng với tích hợp dữ liệu từ file CSV/Excel.

using Microsoft.VisualStudio.TestTools.UnitTesting; 
using System; 
using Playfair_Cipher_4_Hau_N1.src_4_Hau; // Namespace chứa lớp PlayFair_4_Hau triển khai thuật toán Playfair.
using System.Data; 
using System.Linq; 
using ClosedXML.Excel; // Thư viện đọc/ghi file Excel cho test case Excel.
using System.Collections.Generic; 
using System.IO; 

namespace Playfair_Cipher_Tester_4_Hau_N1 // Namespace chứa lớp kiểm thử Playfair.
{
    [TestClass] // Đánh dấu lớp này chứa các test case sẽ được chạy bởi MSTest.
    public class Playfair_Test_4_Hau
    {
        // Biến instance của lớp PlayFair_4_Hau, dùng chung cho tất cả test case.
        private PlayFair_4_Hau playfair;

        // Thuộc tính TestContext để truy cập dữ liệu từ file CSV trong test case.
        public TestContext TestContext { get; set; }

        // Phương thức khởi tạo chạy trước mỗi test case để thiết lập môi trường kiểm thử.
        [TestInitialize]
        public void Setup_4_Hau()
        {
            // Khởi tạo đối tượng PlayFair_4_Hau với ma trận 5x5 (chuẩn cho Playfair Cipher).
            // Đảm bảo mỗi test case bắt đầu với trạng thái mới, tránh ảnh hưởng từ test trước.
            playfair = new PlayFair_4_Hau(5);
        }

        // TC1: Kiểm tra mã hóa với văn bản có độ dài lẻ.
        [TestMethod]
        public void TC1_Encrypt_Test_Odd_Length_Plaintext_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa dài "daihoccongnghethongtin".
            // Khóa này sẽ được xử lý để loại bỏ ký tự trùng lặp và tạo bảng mã hóa 5x5.
            playfair.InitMatrix_4_Hau("daihoccongnghethongtin");

            // Văn bản đầu vào: "xinchaocacban" (độ dài sau xử lý là lẻ).
            // Thuật toán sẽ xóa khoảng trắng, chuyển thành "XINCHAOCACBAN" (13 ký tự),
            // sau đó thêm 'X' thành "XINCHAOCACBANX" (14 ký tự) trước khi mã hóa.
            string input_4_hau = "xinchaocacban";

            // Kết quả mong đợi sau khi mã hóa: "IGGNOIDTDNFDGW".
            // Đã được tính toán thủ công dựa trên ma trận Playfair với khóa trên.
            string expected_4_hau = "IGGNOIDTDNFDGW";

            // Gọi phương thức Encrypt_4_Hau để mã hóa văn bản đầu vào.
            string actual_4_hau = playfair.Encrypt_4_Hau(input_4_hau);

            // So sánh kết quả thực tế với kết quả mong đợi.
            // Nếu không khớp, hiển thị thông báo lỗi để dễ debug.
            Assert.AreEqual(expected_4_hau, actual_4_hau, "Mã hóa thất bại với văn bản có độ dài lẻ.");
        }

        // TC2: Kiểm tra mã hóa với văn bản có khoảng trắng.
        [TestMethod]
        public void TC2_Encrypt_Test_Spaces_Plaintext_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "playfair".
            playfair.InitMatrix_4_Hau("dai hoc cong nghe thong tin");

            // Văn bản đầu vào: "HI" (ngắn, không cần thêm ký tự đệm).
            string input_4_hau = "xin chao cac ban";

            // Kết quả mong đợi sau mã hóa: "BM".
            // Đã được tính toán dựa trên ma trận với khóa "playfair".
            string expected_4_hau = "IGGNOIDTDNFDGW";

            // Gọi phương thức Encrypt_4_Hau để mã hóa văn bản.
            string actual_4_hau = playfair.Encrypt_4_Hau(input_4_hau);

            // So sánh kết quả thực tế với mong đợi.
            Assert.AreEqual(expected_4_hau, actual_4_hau, "Mã hóa thất bại với văn bản ngắn.");
        }

        // TC3: Kiểm tra mã hóa với văn bản chứa cặp ký tự giống nhau.
        [TestMethod]
        public void TC3_Encrypt_Test_Same_Letters_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "keyword".
            playfair.InitMatrix_4_Hau("keyword");

            // Văn bản đầu vào: "BALLOON", chứa cặp "LL" và "OO".
            // Thuật toán sẽ chèn 'X' giữa các cặp giống nhau, thành "BALXLOON".
            string input_4_hau = "BALLOON";

            // Kết quả mong đợi sau mã hóa: "GBTTXVVR".
            // Đã được tính toán dựa trên ma trận với khóa "keyword".
            string expected_4_hau = "CBIZSCZWQU";

            // Gọi phương thức Encrypt_4_Hau để mã hóa văn bản.
            string actual_4_hau = playfair.Encrypt_4_Hau(input_4_hau);

            // So sánh kết quả thực tế với mong đợi.
            Assert.AreEqual(expected_4_hau, actual_4_hau, "Mã hóa thất bại với văn bản có cặp ký tự giống nhau.");
        }

        // TC4: Kiểm tra mã hóa với văn bản có ký tự không hợp lệ
        [TestMethod]
        public void TC4_Encrypt_Test_NonAlphabetic_Plaintext_ThrowsException_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "phi"
            playfair.InitMatrix_4_Hau("phi");

            // Văn bản đầu vào: "this1is" (chứa ký tự không hợp lệ là số "1")
            string input_4_hau = "this1is";

            // Kiểm tra xem phương thức Encrypt_4_Hau có ném ngoại lệ ArgumentException không
            Assert.ThrowsException<ArgumentException>(() => playfair.Encrypt_4_Hau(input_4_hau),
                "Phương thức ném ngoại lệ khi văn bản đầu vào chứa ký tự không hợp lệ.");
        }

        // TC5: Kiểm tra mã hóa với văn bản chứa cặp ký tự hoa/thường.
        [TestMethod]
        public void TC5_Encrypt_Test_Mixed_Case_Ciphertext_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "keyword".
            playfair.InitMatrix_4_Hau("kEyWoRd");

            // Văn bản đầu vào: "BALLOON", chứa cặp "LL" và "OO".
            // Thuật toán sẽ chèn 'X' giữa các cặp giống nhau, thành "BALXLOON".
            string input_4_hau = "BaLlOoN";

            // Kết quả mong đợi sau mã hóa: "CBFFKKQU".
            // Đã được tính toán dựa trên ma trận với khóa "keyword".
            string expected_4_hau = "CBFFKKQU";

            // Gọi phương thức Encrypt_4_Hau để mã hóa văn bản.
            string actual_4_hau = playfair.Encrypt_4_Hau(input_4_hau);

            // So sánh kết quả thực tế với mong đợi.
            Assert.AreEqual(expected_4_hau, actual_4_hau, "Mã hóa thất bại với văn bản có cặp ký tự hoa với thường.");
        }


        // TC6: Kiểm tra giải mã với văn bản mã hóa chứa chữ thường/hoa
        [TestMethod]
        public void TC6_Decrypt_Test_Mixed_Case_Ciphertext_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "kEyWoRd"
            // Khóa này tạo ma trận 5x5 với các ký tự từ "kEyWoRd" (sẽ được chuẩn hóa thành "KEYWORD") và phần còn lại của bảng chữ cái
            playfair.InitMatrix_4_Hau("kEyWoRd");

            // Văn bản mã hóa đầu vào: "BaLlOoN" (hỗn hợp chữ thường và hoa)
            // Thuật toán sẽ chuyển tất cả thành chữ hoa trước khi giải mã, thành "BALLOON"
            string input_4_hau = "BaLlOoN";

            // Kết quả mong đợi sau giải mã: "ADIIWWQU"
            // Đã được tính toán thủ công dựa trên ma trận với khóa "kEyWoRd"
            string expected_4_hau = "ADIIWWQU";

            // Gọi phương thức Decrypt_4_Hau để giải mã văn bản đầu vào
            string actual_4_hau = playfair.Decrypt_4_Hau(input_4_hau);

            // So sánh kết quả thực tế với mong đợi, đảm bảo giải mã đúng
            Assert.AreEqual(expected_4_hau, actual_4_hau, "Giải mã thất bại với văn bản mã hóa chữ thường/hoa.");
        }

        // TC7: Kiểm tra giải mã với văn bản mã hóa có độ dài lẻ
        [TestMethod]
        public void TC7_Decrypt_Test_Odd_Length_Ciphertext_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "daihoccongnghethongtin"
            playfair.InitMatrix_4_Hau("daihoccongnghethongtin");

            // Văn bản mã hóa đầu vào: "IGGNOIDTDNFDGW" (14 ký tự, độ dài chẵn, nhưng là kết quả mã hóa của văn bản gốc lẻ 13 ký tự)
            // Đây là kết quả mã hóa của "xinchaocacban" (13 ký tự, thêm "X" thành 14 ký tự)
            string input_4_hau = "IGGNOIDTDNFDGW";

            // Kết quả mong đợi sau giải mã: "XINCHAOCACBANX"
            // Vì đây là văn bản đã được chuẩn hóa khi mã hóa (thêm "X" ở cuối)
            string expected_4_hau = "XINCHAOCACBANX";

            // Gọi phương thức Decrypt_4_Hau để giải mã văn bản đầu vào
            string actual_4_hau = playfair.Decrypt_4_Hau(input_4_hau);

            // So sánh kết quả thực tế với kết quả mong đợi
            Assert.AreEqual(expected_4_hau, actual_4_hau, "Giải mã thất bại với văn bản mã hóa có độ dài lẻ.");
        }

        // TC8: Kiểm tra giải mã với văn bản mã hóa có khoảng trắng
        [TestMethod]
        public void TC8_Decrypt_Test_Spaces_Ciphertext_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "dai hoc cong nghe thong tin"
            playfair.InitMatrix_4_Hau("dai hoc cong nghe thong tin");

            // Văn bản mã hóa đầu vào: "IG GN OI DT DN FD GW" (có khoảng trắng, 14 ký tự sau khi xóa khoảng trắng)
            // Thuật toán sẽ xóa khoảng trắng, thành "IGGNOIDTDNFDGW"
            string input_4_hau = "IG GN OI DT DN FD GW";

            // Kết quả mong đợi sau giải mã: "XINCHAOCACBANX"
            // Vì đây là văn bản đã được chuẩn hóa khi mã hóa (thêm "X" ở cuối)
            string expected_4_hau = "XINCHAOCACBANX";

            // Gọi phương thức Decrypt_4_Hau để giải mã văn bản đầu vào
            string actual_4_hau = playfair.Decrypt_4_Hau(input_4_hau);

            // So sánh kết quả thực tế với kết quả mong đợi
            Assert.AreEqual(expected_4_hau, actual_4_hau, "Giải mã thất bại với văn bản mã hóa có khoảng trắng.");
        }

        // TC9: Kiểm tra giải mã với văn bản mã hóa chứa cặp ký tự giống nhau
        [TestMethod]
        public void TC9_Decrypt_Test_Same_Letters_Ciphertext_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "keyword"
            playfair.InitMatrix_4_Hau("keyword");

            // Văn bản mã hóa đầu vào: "CBIZSCZWQU"
            // Đây là kết quả mã hóa của "BALLOON" (sau khi chèn "X" thành "BALXLOON")
            string input_4_hau = "CBIZSCZWQU";

            // Kết quả mong đợi sau giải mã: "BALXLOON"
            // Vì đây là văn bản đã được chuẩn hóa khi mã hóa (chèn "X" giữa "LL")
            string expected_4_hau = "BALXLOXONX";

            // Gọi phương thức Decrypt_4_Hau để giải mã văn bản đầu vào
            string actual_4_hau = playfair.Decrypt_4_Hau(input_4_hau);

            // So sánh kết quả thực tế với kết quả mong đợi
            Assert.AreEqual(expected_4_hau, actual_4_hau, "Giải mã thất bại với văn bản mã hóa chứa cặp ký tự giống nhau.");
        }

        // TC10: Kiểm tra giải mã với văn bản mã hóa có ký tự không hợp lệ
        [TestMethod]
        public void TC10_Decrypt_Test_NonAlphabetic_Ciphertext_ThrowsException_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "phi"
            playfair.InitMatrix_4_Hau("phi");

            // Văn bản mã hóa đầu vào: "IEKF1BMZ" (chứa ký tự không hợp lệ là số "1")
            string input_4_hau = "IEKF1BMZ";

            // Kiểm tra xem phương thức Decrypt_4_Hau có ném ngoại lệ ArgumentException không
            Assert.ThrowsException<ArgumentException>(() => playfair.Decrypt_4_Hau(input_4_hau),
                "Phương thức Decrypt_4_Hau phải ném ngoại lệ khi văn bản mã hóa chứa ký tự không hợp lệ.");
        }

        // TC11: Kiểm tra mã hóa với văn bản có độ dài lẻ với trường hợp fail.
        [TestMethod]
        public void TC11_Encrypt_Test_Odd_Length_Plaintext_Fail_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa dài "daihoccongnghethongtin".
            // Khóa này sẽ được xử lý để loại bỏ ký tự trùng lặp và tạo bảng mã hóa 5x5.
            playfair.InitMatrix_4_Hau("daihoccongnghethongtin");

            // Văn bản đầu vào: "xinchaocacban" (độ dài sau xử lý là lẻ).
            // Thuật toán sẽ xóa khoảng trắng, chuyển thành "XINCHAOCACBAN" (13 ký tự),
            // sau đó thêm 'X' thành "XINCHAOCACBANX" (14 ký tự) trước khi mã hóa.
            string input_4_hau = "xinchaocacban";

            // Kết quả mong đợi sau khi mã hóa: "IGGNOIDTDNFDGW".
            // Đã được tính toán thủ công dựa trên ma trận Playfair với khóa trên.
            string expected_4_hau = "IGGNOIDRDNFDGW";

            // Gọi phương thức Encrypt_4_Hau để mã hóa văn bản đầu vào.
            string actual_4_hau = playfair.Encrypt_4_Hau(input_4_hau);

            // So sánh kết quả thực tế với kết quả mong đợi.
            // Nếu không khớp, hiển thị thông báo lỗi để dễ debug.
            Assert.AreEqual(expected_4_hau, actual_4_hau, "Mã hóa thất bại với văn bản có độ dài lẻ.");
        }

        // TC12: Kiểm tra mã hóa với văn bản có khoảng trắng với trường hợp fail.
        [TestMethod]
        public void TC12_Encrypt_Test_Short_Plaintext_Fail_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "playfair".
            playfair.InitMatrix_4_Hau("dai hoc cong nghe thong tin");

            // Văn bản đầu vào: "HI" (ngắn, không cần thêm ký tự đệm).
            string input_4_hau = "xin chao cac ban";

            // Kết quả mong đợi sau mã hóa: "BM".
            // Đã được tính toán dựa trên ma trận với khóa "playfair".
            string expected_4_hau = "IGGNOIETDNFDGW";

            // Gọi phương thức Encrypt_4_Hau để mã hóa văn bản.
            string actual_4_hau = playfair.Encrypt_4_Hau(input_4_hau);

            // So sánh kết quả thực tế với mong đợi.
            Assert.AreEqual(expected_4_hau, actual_4_hau, "Mã hóa thất bại với văn bản ngắn.");
        }

        // TC13: Kiểm tra giải mã với văn bản mã hóa chứa cặp ký tự giống nhau với trường hợp fail
        [TestMethod]
        public void TC13_Decrypt_Test_Same_Letters_Ciphertext_Fail_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "keyword"
            playfair.InitMatrix_4_Hau("keyword");

            // Văn bản mã hóa đầu vào: "CBIZSCZWQU"
            // Đây là kết quả mã hóa của "BALLOON" (sau khi chèn "X" thành "BALXLOON")
            string input_4_hau = "CBIZSCRWQU";

            // Kết quả mong đợi sau giải mã: "BALXLOON"
            // Vì đây là văn bản đã được chuẩn hóa khi mã hóa (chèn "X" giữa "LL")
            string expected_4_hau = "BALXLOXONX";

            // Gọi phương thức Decrypt_4_Hau để giải mã văn bản đầu vào
            string actual_4_hau = playfair.Decrypt_4_Hau(input_4_hau);

            // So sánh kết quả thực tế với kết quả mong đợi
            Assert.AreEqual(expected_4_hau, actual_4_hau, "Giải mã thất bại với văn bản mã hóa chứa cặp ký tự giống nhau.");
        }

        // TC14: Kiểm tra giải mã với văn bản mã hóa chứa chữ thường/hoa với trường hợp fail
        [TestMethod]
        public void TC14_Decrypt_Test_Mixed_Case_Ciphertext_Fail_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "kEyWoRd"
            // Khóa này tạo ma trận 5x5 với các ký tự từ "kEyWoRd" (sẽ được chuẩn hóa thành "KEYWORD") và phần còn lại của bảng chữ cái
            playfair.InitMatrix_4_Hau("kEyWoRd");

            // Văn bản mã hóa đầu vào: "BaLlOoN" (hỗn hợp chữ thường và hoa)
            // Thuật toán sẽ chuyển tất cả thành chữ hoa trước khi giải mã, thành "BALLOON"
            string input_4_hau = "BaLlOoN";

            // Kết quả mong đợi sau giải mã: "ADIIWWQU"
            // Đã được tính toán thủ công dựa trên ma trận với khóa "kEyWoRd"
            string expected_4_hau = "ADIIWWFU";

            // Gọi phương thức Decrypt_4_Hau để giải mã văn bản đầu vào
            string actual_4_hau = playfair.Decrypt_4_Hau(input_4_hau);

            // So sánh kết quả thực tế với mong đợi, đảm bảo giải mã đúng
            Assert.AreEqual(expected_4_hau, actual_4_hau, "Giải mã thất bại với văn bản mã hóa chữ thường/hoa.");
        }


        // TC15: Kiểm tra cả mã hóa và giải mã với dữ liệu từ file CSV.
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            @".\Data_csv_4_Hau\Test_Data_4_Hau.csv",
            "Test_Data_4_Hau#csv",
            DataAccessMethod.Sequential)]
        public void TC15_TestPlayfairCipher_Encrypt_Decrypt_WithCSV_4_Hau()
        {
            // Lấy dữ liệu từ file CSV thông qua TestContext.DataRow:
            // - Cột 0: Văn bản đầu vào (plaintext hoặc ciphertext).
            // - Cột 1: Khóa để khởi tạo ma trận.
            // - Cột 2: Thao tác ("Encrypt" hoặc "Decrypt").
            // - Cột 3: Kết quả mong đợi.
            string input_4_Hau = TestContext.DataRow[0].ToString();
            string key_4_Hau = TestContext.DataRow[1].ToString();
            string operation_4_Hau = TestContext.DataRow[2].ToString();
            string expected_4_Hau = TestContext.DataRow[3].ToString();

            // Chuyển kết quả mong đợi thành chữ hoa để đồng nhất định dạng.
            string expectedUpper_4_Hau = expected_4_Hau.ToUpper();

            // Khởi tạo ma trận Playfair với khóa từ file CSV.
            playfair.InitMatrix_4_Hau(key_4_Hau);

            // Biến lưu kết quả thực tế sau khi mã hóa/giải mã.
            string actual_4_Hau;

            // Thực hiện thao tác dựa trên giá trị operation_4_Hau.
            if (operation_4_Hau == "Encrypt")
            {
                // Nếu thao tác là mã hóa, gọi Encrypt_4_Hau.
                actual_4_Hau = playfair.Encrypt_4_Hau(input_4_Hau);
            }
            else if (operation_4_Hau == "Decrypt")
            {
                // Nếu thao tác là giải mã, gọi Decrypt_4_Hau.
                actual_4_Hau = playfair.Decrypt_4_Hau(input_4_Hau);
            }
            else
            {
                // Nếu thao tác không hợp lệ, làm thất bại test case.
                Assert.Fail("Thao tác không hợp lệ: " + operation_4_Hau);
                return;
            }

            // So sánh kết quả thực tế với mong đợi, kèm thông tin chi tiết nếu thất bại.
            Assert.AreEqual(expectedUpper_4_Hau, actual_4_Hau,
                $"Đầu vào: {input_4_Hau}, Khóa: {key_4_Hau}, Thao tác: {operation_4_Hau}");
        }

        // Phương thức hỗ trợ đọc dữ liệu kiểm thử từ file Excel.
        private static IEnumerable<object[]> GetTestCases_4_Hau()
        {
            // Đường dẫn file Excel chứa dữ liệu kiểm thử.
            string filePath_4_Hau = @"Data_excel_4_Hau\Test_Data_4_Hau.xlsx";

            // Danh sách lưu các test case, mỗi test case là mảng object chứa 4 giá trị.
            var testCases_4_Hau = new List<object[]>();

            // Kiểm tra sự tồn tại của file Excel.
            if (!File.Exists(filePath_4_Hau))
            {
                throw new FileNotFoundException($"Không tìm thấy file Excel tại: {filePath_4_Hau}");
            }

            // Mở file Excel bằng ClosedXML trong khối using để tự động giải phóng tài nguyên.
            using (var workbook = new XLWorkbook(filePath_4_Hau))
            {
                // Lấy worksheet đầu tiên (sheet 1).
                var worksheet = workbook.Worksheet(1);

                // Kiểm tra worksheet có tồn tại không.
                if (worksheet == null)
                {
                    throw new Exception("Không tìm thấy worksheet trong file Excel.");
                }

                // Lấy tất cả dòng có dữ liệu và chuyển thành danh sách.
                var usedRows = worksheet.RowsUsed().ToList();

                // Đếm số dòng dữ liệu.
                int rowCount = usedRows.Count;

                // Kiểm tra xem file có dữ liệu không (phải có ít nhất 2 dòng: tiêu đề + dữ liệu).
                if (rowCount < 2)
                {
                    throw new Exception("File Excel không có dữ liệu (chỉ có tiêu đề).");
                }

                // Duyệt qua các dòng dữ liệu, bắt đầu từ dòng thứ 2 (bỏ qua tiêu đề).
                for (int i = 1; i < rowCount; i++)
                {
                    var row = usedRows[i];

                    // Đọc dữ liệu từ các cột:
                    // - Cột 1: Văn bản đầu vào.
                    // - Cột 2: Khóa.
                    // - Cột 3: Thao tác (Encrypt/Decrypt).
                    // - Cột 4: Kết quả mong đợi.
                    string input_4_Hau = row.Cell(1).GetString();
                    string key_4_Hau = row.Cell(2).GetString();
                    string operation_4_Hau = row.Cell(3).GetString();
                    string expected_4_Hau = row.Cell(4).GetString();

                    // Thêm test case vào danh sách.
                    testCases_4_Hau.Add(new object[] { input_4_Hau, key_4_Hau, operation_4_Hau, expected_4_Hau });
                }
            }

            // Trả về danh sách test case cho test case động.
            return testCases_4_Hau;
        }

        // TC16: Kiểm tra cả mã hóa và giải mã với dữ liệu từ file Excel.
        [DataTestMethod]
        [DynamicData(nameof(GetTestCases_4_Hau), DynamicDataSourceType.Method)]
        public void TC16_TestPlayfairCipher_Encrypt_Decrypt_WithExcel_4_Hau(
            string input_4_Hau, string key_4_Hau, string operation_4_Hau, string expected_4_Hau)
        {
            // Kiểm tra dữ liệu đầu vào có hợp lệ không (không rỗng hoặc null).
            if (string.IsNullOrEmpty(input_4_Hau) || string.IsNullOrEmpty(key_4_Hau) ||
                string.IsNullOrEmpty(operation_4_Hau) || string.IsNullOrEmpty(expected_4_Hau))
            {
                Assert.Fail($"Dữ liệu không hợp lệ: Đầu vào={input_4_Hau}, Khóa={key_4_Hau}, Thao tác={operation_4_Hau}, Mong đợi={expected_4_Hau}");
                return;
            }

            // Chuyển kết quả mong đợi thành chữ hoa để đồng nhất.
            string expectedUpper_4_Hau = expected_4_Hau.ToUpper();

            // Khởi tạo ma trận Playfair với khóa từ file Excel.
            try
            {
                playfair.InitMatrix_4_Hau(key_4_Hau);
            }
            catch (Exception ex)
            {
                // Nếu khởi tạo ma trận thất bại (khóa không hợp lệ), làm thất bại test case.
                Assert.Fail($"Lỗi khởi tạo ma trận: Khóa={key_4_Hau}, Lỗi={ex.Message}");
                return;
            }

            // Biến lưu kết quả thực tế.
            string actual_4_Hau = null;

            // Thực hiện thao tác mã hóa hoặc giải mã.
            try
            {
                if (operation_4_Hau.Equals("Encrypt", StringComparison.OrdinalIgnoreCase))
                {
                    // Nếu thao tác là mã hóa, gọi Encrypt_4_Hau.
                    actual_4_Hau = playfair.Encrypt_4_Hau(input_4_Hau);
                }
                else if (operation_4_Hau.Equals("Decrypt", StringComparison.OrdinalIgnoreCase))
                {
                    // Nếu thao tác là giải mã, gọi Decrypt_4_Hau.
                    actual_4_Hau = playfair.Decrypt_4_Hau(input_4_Hau);
                }
                else
                {
                    // Nếu thao tác không hợp lệ, làm thất bại test case.
                    Assert.Fail($"Thao tác không hợp lệ: {operation_4_Hau}");
                    return;
                }
            }
            catch (Exception ex)
            {
                // Nếu có lỗi khi mã hóa/giải mã, làm thất bại test case.
                Assert.Fail($"Lỗi khi thực hiện {operation_4_Hau}: Đầu vào={input_4_Hau}, Khóa={key_4_Hau}, Lỗi={ex.Message}");
                return;
            }

            // So sánh kết quả thực tế với mong đợi, kèm thông tin chi tiết nếu thất bại.
            Assert.AreEqual(expectedUpper_4_Hau, actual_4_Hau,
                $"Kết quả sai: Đầu vào={input_4_Hau}, Khóa={key_4_Hau}, Thao tác={operation_4_Hau}, Mong đợi={expectedUpper_4_Hau}, Thực tế={actual_4_Hau}");
        }
    }
}