// File: Playfair_Test_4_Hau.cs
// Mục đích: File này chứa các phương thức Test case (unit test) cho thuật toán mã hóa Playfair.
// Các test case kiểm tra các chức năng như mã hóa (encrypt), giải mã (decrypt), xử lý chuỗi đầu vào,
// và tích hợp dữ liệu từ file CSV/Excel để kiểm thử tự động.

using Microsoft.VisualStudio.TestTools.UnitTesting; // Thư viện dùng để viết và chạy unit test trong Visual Studio.
using System; // Thư viện cơ bản của .NET, cung cấp các lớp như String, Console, Exception.
using Playfair_Cipher_4_Hau_N1.src; // Namespace chứa lớp PlayFair_4_Hau và Coordinate_4_Hau, nơi triển khai thuật toán Playfair.
using System.Data; // Thư viện hỗ trợ làm việc với dữ liệu, dùng cho DataSource trong test case CSV.
using System.Linq; // Thư viện hỗ trợ các thao tác LINQ (dùng để xử lý danh sách trong Excel).
using ClosedXML.Excel; // Thư viện ClosedXML để đọc và ghi file Excel trong test case Excel.
using System.Collections.Generic; // Thư viện cung cấp các kiểu dữ liệu tập hợp như List, dùng để lưu test case từ Excel.
using System.IO; // Thư viện để làm việc với file (ví dụ: kiểm tra file tồn tại).

namespace Playfair_Cipher_Tester_4_Hau_N1 // Không gian tên (namespace) chứa lớp kiểm thử Playfair.
{
    [TestClass] // Đánh dấu lớp này là một lớp chứa các test case (kiểm thử) sẽ được chạy bởi MSTest.
    public class Playfair_Test_4_Hau
    {
        // Khai báo biến instance playfair kiểu PlayFair_4_Hau để sử dụng trong các test case.
        // Biến này đại diện cho đối tượng thực hiện thuật toán Playfair.
        private PlayFair_4_Hau playfair;

        // Thuộc tính TestContext để truy cập thông tin ngữ cảnh kiểm thử (dùng cho test case đọc từ CSV).
        // Được MSTest tự động gán giá trị khi chạy test.
        public TestContext TestContext { get; set; }

        // Phương thức khởi tạo chạy trước mỗi test case để thiết lập môi trường kiểm thử.
        [TestInitialize] // Đánh dấu phương thức này sẽ chạy trước mỗi test case trong lớp.
        public void Setup_4_Hau()
        {
            // Khởi tạo đối tượng PlayFair_4_Hau với kích thước ma trận 5x5 (chuẩn cho Playfair Cipher).
            // Ma trận này sẽ được sử dụng trong tất cả các test case.
            playfair = new PlayFair_4_Hau(5);
        }

        // Test case 1: Kiểm tra phương thức RemoveSpaceInString với chuỗi đầu vào hợp lệ.
        [TestMethod] // Đánh dấu đây là một test case sẽ được chạy bởi framework kiểm thử.
        public void TC1_RemoveSpaceInString_TestValid_Value_Return_4_Hau()
        {
            // Chuỗi đầu vào chứa khoảng trắng để kiểm tra việc xóa khoảng trắng.
            string input_4_hau = "Nguyen Trung Hau";

            // Kết quả mong đợi sau khi xóa khoảng trắng: các từ được nối liền thành một chuỗi.
            string expected_4_hau = "NguyenTrungHau";

            // Gọi phương thức RemoveSpaceInString_4_Hau để xóa khoảng trắng từ chuỗi đầu vào.
            string actual_4_hau = playfair.RemoveSpaceInString_4_Hau(input_4_hau);

            // So sánh kết quả thực tế với kết quả mong đợi.
            // Nếu không khớp, in thông báo lỗi "RemoveSpaceInString failed with valid input."
            Assert.AreEqual(expected_4_hau, actual_4_hau, "RemoveSpaceInString failed with valid input.");
        }

        // Test case 2: Kiểm tra phương thức HoanDoi với các cặp tọa độ hợp lệ trong chế độ mã hóa.
        [TestMethod]
        public void TC2_HoanDoi_Test_Valid_Value_Return_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "phi" để tạo bảng mã hóa.
            playfair.InitMatrix_4_Hau("phi");

            // Trường hợp 1: Hai ký tự ở khác hàng, khác cột.
            string input1_4_hau = "PM"; // Chuỗi đầu vào giả định để minh họa (không dùng trực tiếp trong hàm).
            string expected1_4_hau = "IK"; // Kết quả mong đợi khi mã hóa "PM" với khóa "phi".
            // Gọi phương thức HoanDoi_4_Hau với tọa độ của "P" (0,0) và "M" (2,2), chế độ mã hóa (true là mặc định).
            string actual1_4_hau = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 0, J = 0 }, new Coordinate_4_Hau { I = 2, J = 2 });
            Assert.AreEqual(expected1_4_hau, actual1_4_hau, "HoanDoi failed for different row and column.");

            // Trường hợp 2: Hai ký tự ở cùng cột.
            string input2_4_hau = "PV"; // Chuỗi đầu vào giả định để minh họa.
            string expected2_4_hau = "CP"; // Kết quả mong đợi khi mã hóa "PV".
            // Gọi phương thức HoanDoi_4_Hau với tọa độ của "P" (0,0) và "V" (4,0), chế độ mã hóa.
            string actual2_4_hau = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 0, J = 0 }, new Coordinate_4_Hau { I = 4, J = 0 });
            Assert.AreEqual(expected2_4_hau, actual2_4_hau, "HoanDoi failed for same column.");

            // Trường hợp 3: Hai ký tự ở cùng hàng.
            string input3_4_hau = "PB"; // Chuỗi đầu vào giả định để minh họa.
            string expected3_4_hau = "HP"; // Kết quả mong đợi khi mã hóa "PB".
            // Gọi phương thức HoanDoi_4_Hau với tọa độ của "P" (0,0) và "B" (0,4), chế độ mã hóa.
            string actual3_4_hau = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 0, J = 0 }, new Coordinate_4_Hau { I = 0, J = 4 });
            Assert.AreEqual(expected3_4_hau, actual3_4_hau, "HoanDoi failed for same row.");
        }

        // Test case 3: Kiểm tra phương thức HoanDoi với tọa độ không hợp lệ, mong đợi ngoại lệ.
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))] // Mong đợi ngoại lệ IndexOutOfRangeException khi tọa độ vượt quá ma trận.
        public void TC3_HoanDoi_Test_IndexOutOfRange_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "phi".
            playfair.InitMatrix_4_Hau("phi");

            // Chuỗi đầu vào giả định để minh họa (không dùng trực tiếp).
            string input_4_hau = "Invalid Coordinates";

            // Gọi phương thức HoanDoi_4_Hau với tọa độ không hợp lệ (J = 6 vượt quá kích thước ma trận 5x5).
            // Mong đợi ngoại lệ IndexOutOfRangeException được ném ra.
            string actual_4_hau = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 2, J = 6 }, new Coordinate_4_Hau { I = 2, J = 2 });
        }

        // Test case 4: Kiểm tra phương thức HoanDoi với các cặp tọa độ hợp lệ trong chế độ giải mã.
        [TestMethod]
        public void TC4_HoanDoi_Test_Decrypt_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "phi".
            playfair.InitMatrix_4_Hau("phi");

            // Trường hợp 1: Hai ký tự ở khác hàng, khác cột (giải mã).
            string input1_4_hau = "PM"; // Chuỗi đầu vào giả định để minh họa.
            string expected1_4_hau = "IK"; // Kết quả mong đợi khi giải mã.
            // Gọi phương thức HoanDoi_4_Hau với tọa độ của "P" (0,0) và "M" (2,2), chế độ giải mã (false).
            string actual1_4_hau = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 0, J = 0 }, new Coordinate_4_Hau { I = 2, J = 2 }, false);
            Assert.AreEqual(expected1_4_hau, actual1_4_hau, "HoanDoi decrypt failed for different row and column.");

            // Trường hợp 2: Hai ký tự ở cùng cột (giải mã).
            string input2_4_hau = "PV"; // Chuỗi đầu vào giả định để minh họa.
            string expected2_4_hau = "VQ"; // Kết quả mong đợi khi giải mã.
            // Gọi phương thức HoanDoi_4_Hau với tọa độ của "P" (0,0) và "V" (4,0), chế độ giải mã.
            string actual2_4_hau = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 0, J = 0 }, new Coordinate_4_Hau { I = 4, J = 0 }, false);
            Assert.AreEqual(expected2_4_hau, actual2_4_hau, "HoanDoi decrypt failed for same column.");

            // Trường hợp 3: Hai ký tự ở cùng hàng (giải mã).
            string input3_4_hau = "PB"; // Chuỗi đầu vào giả định để minh họa.
            string expected3_4_hau = "BA"; // Kết quả mong đợi khi giải mã.
            // Gọi phương thức HoanDoi_4_Hau với tọa độ của "P" (0,0) và "B" (0,4), chế độ giải mã.
            string actual3_4_hau = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 0, J = 0 }, new Coordinate_4_Hau { I = 0, J = 4 }, false);
            Assert.AreEqual(expected3_4_hau, actual3_4_hau, "HoanDoi decrypt failed for same row.");
        }

        // Test case 5: Kiểm tra phương thức EncryptTwoCharacter với cặp ký tự hợp lệ.
        [TestMethod]
        public void TC5_EncryptTwoCharacter_Test_valid_result_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "phi".
            playfair.InitMatrix_4_Hau("phi");

            // Trường hợp 1: Mã hóa cặp "hm".
            string input1_4_hau = "hm"; // Chuỗi đầu vào giả định để minh họa.
            string expected1_4_hau = "IL"; // Kết quả mong đợi khi mã hóa "hm".
            string actual1_4_hau = playfair.EncryptTwoCharacter_4_Hau('h', 'm'); // Gọi phương thức mã hóa hai ký tự.
            Assert.AreEqual(expected1_4_hau, actual1_4_hau, "EncryptTwoCharacter failed for 'hm'.");

            // Trường hợp 2: Mã hóa cặp "At".
            string input2_4_hau = "At"; // Chuỗi đầu vào giả định để minh họa.
            string expected2_4_hau = "FY"; // Kết quả mong đợi khi mã hóa "At".
            string actual2_4_hau = playfair.EncryptTwoCharacter_4_Hau('A', 't'); // Gọi phương thức mã hóa hai ký tự.
            Assert.AreEqual(expected2_4_hau, actual2_4_hau, "EncryptTwoCharacter failed for 'At'.");

            // Trường hợp 3: Mã hóa cặp "qR".
            string input3_4_hau = "qR"; // Chuỗi đầu vào giả định để minh họa.
            string expected3_4_hau = "RS"; // Kết quả mong đợi khi mã hóa "qR".
            string actual3_4_hau = playfair.EncryptTwoCharacter_4_Hau('q', 'R'); // Gọi phương thức mã hóa hai ký tự.
            Assert.AreEqual(expected3_4_hau, actual3_4_hau, "EncryptTwoCharacter failed for 'qR'.");
        }

        // Test case 6: Kiểm tra phương thức EncryptTwoCharacter với ký tự không hợp lệ (khoảng trắng).
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))] // Mong đợi ngoại lệ ArgumentException khi đầu vào không hợp lệ.
        public void TC6_EncryptTwoCharacter_Test_input_empty_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "hau".
            playfair.InitMatrix_4_Hau("hau");

            // Chuỗi đầu vào giả định để minh họa (chứa khoảng trắng).
            string input_4_hau = "A ";

            // Gọi phương thức EncryptTwoCharacter_4_Hau với ký tự không hợp lệ (khoảng trắng).
            // Mong đợi ngoại lệ ArgumentException được ném ra.
            string actual_4_hau = playfair.EncryptTwoCharacter_4_Hau('A', ' ');
        }

        // Test case 7: Kiểm tra phương thức Encrypt_4_Hau với văn bản có độ dài lẻ.
        [TestMethod]
        public void TC7_Encrypt_Test_plain_text_is_odd_character_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa dài "dai hoc cong nghe thong tin".
            playfair.InitMatrix_4_Hau("dai hoc cong nghe thong tin");

            // Chuỗi đầu vào có độ dài lẻ (16 ký tự kể cả khoảng trắng, sau khi xóa còn 13).
            string input_4_hau = "xin chao cac ban";

            // Kết quả mong đợi sau khi mã hóa (chuỗi lẻ sẽ được thêm "X" ở cuối trước khi mã hóa).
            string expected_4_hau = "IGGNOIDTDNFDGW";

            // Gọi phương thức Encrypt_4_Hau để mã hóa toàn bộ chuỗi.
            string actual_4_hau = playfair.Encrypt_4_Hau(input_4_hau);

            // So sánh kết quả thực tế với kết quả mong đợi.
            Assert.AreEqual(expected_4_hau, actual_4_hau, "Encrypt failed for odd-length plaintext.");
        }

        // Test case 8: Kiểm tra phương thức Encrypt_4_Hau với văn bản chứa ký tự không hợp lệ.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))] // Mong đợi ngoại lệ ArgumentException khi đầu vào không hợp lệ.
        public void TC8_Encrypt_Test_argument_invalid_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "dai hoc cong nghe thong tin".
            playfair.InitMatrix_4_Hau("dai hoc cong nghe thong tin");

            // Chuỗi đầu vào chứa ký tự đặc biệt không hợp lệ "!@#$".
            string input_4_hau = "!@#$AVSDD";

            // Gọi phương thức Encrypt_4_Hau, mong đợi ngoại lệ ArgumentException do ký tự không hợp lệ.
            string actual_4_hau = playfair.Encrypt_4_Hau(input_4_hau);
        }

        // Test case 9: Kiểm tra phương thức Encrypt_4_Hau với văn bản chỉ chứa khoảng trắng.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "plaintext is empty")] // Mong đợi ngoại lệ với thông báo cụ thể.
        public void TC9_Encrypt_Test_argument_is_space_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "dai hoc cong nghe thong tin".
            playfair.InitMatrix_4_Hau("dai hoc cong nghe thong tin");

            // Chuỗi đầu vào chỉ chứa khoảng trắng.
            string input_4_hau = " ";

            // Gọi phương thức Encrypt_4_Hau, mong đợi ngoại lệ ArgumentException vì chuỗi rỗng sau khi xóa khoảng trắng.
            string actual_4_hau = playfair.Encrypt_4_Hau(input_4_hau);
        }

        // Test case 10: Kiểm tra phương thức Encrypt_4_Hau với văn bản rỗng.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "plaintext is empty")] // Mong đợi ngoại lệ với thông báo cụ thể.
        public void TC10_Encrypt_Test_argument_is_empty_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "dai hoc cong nghe thong tin".
            playfair.InitMatrix_4_Hau("dai hoc cong nghe thong tin");

            // Chuỗi đầu vào rỗng.
            string input_4_hau = "";

            // Gọi phương thức Encrypt_4_Hau, mong đợi ngoại lệ ArgumentException vì chuỗi rỗng.
            string actual_4_hau = playfair.Encrypt_4_Hau(input_4_hau);
        }

        // Test case 11: Kiểm tra phương thức DecryptTwoCharacter với cặp ký tự hợp lệ.
        [TestMethod]
        public void TC11_DecryptTwoCharacter_Test_result_valid_with_plaintext_lower_character_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "phi".
            playfair.InitMatrix_4_Hau("phi");

            // Trường hợp 1: Giải mã cặp "af" (cùng cột).
            string input1_4_hau = "af"; // Chuỗi đầu vào giả định để minh họa.
            string expected1_4_hau = "YA"; // Kết quả mong đợi khi giải mã "af".
            string actual1_4_hau = playfair.DecryptTwoCharacter_4_Hau('a', 'f'); // Gọi phương thức giải mã hai ký tự.
            Assert.AreEqual(expected1_4_hau, actual1_4_hau, "DecryptTwoCharacter failed for 'af'.");

            // Trường hợp 2: Giải mã cặp "cE" (cùng hàng).
            string input2_4_hau = "cE"; // Chuỗi đầu vào giả định để minh họa.
            string expected2_4_hau = "GD"; // Kết quả mong đợi khi giải mã "cE".
            string actual2_4_hau = playfair.DecryptTwoCharacter_4_Hau('c', 'E'); // Gọi phương thức giải mã hai ký tự.
            Assert.AreEqual(expected2_4_hau, actual2_4_hau, "DecryptTwoCharacter failed for 'cE'.");

            // Trường hợp 3: Giải mã cặp "Kt" (khác hàng, khác cột).
            string input3_4_hau = "Kt"; // Chuỗi đầu vào giả định để minh họa.
            string expected3_4_hau = "NQ"; // Kết quả mong đợi khi giải mã "Kt".
            string actual3_4_hau = playfair.DecryptTwoCharacter_4_Hau('K', 't'); // Gọi phương thức giải mã hai ký tự.
            Assert.AreEqual(expected3_4_hau, actual3_4_hau, "DecryptTwoCharacter failed for 'Kt'.");
        }

        // Test case 12: Kiểm tra phương thức DecryptTwoCharacter với ký tự đặc biệt.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))] // Mong đợi ngoại lệ ArgumentException khi đầu vào không hợp lệ.
        public void TC12_DecryptTwoCharacter_Test_result_valid_with_plaintex_special_character_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "hau".
            playfair.InitMatrix_4_Hau("hau");

            // Chuỗi đầu vào giả định chứa ký tự đặc biệt "@".
            string input_4_hau = "a@";

            // Gọi phương thức DecryptTwoCharacter_4_Hau với ký tự không hợp lệ, mong đợi ngoại lệ.
            string actual_4_hau = playfair.DecryptTwoCharacter_4_Hau('a', '@');
        }

        // Test case 13: Kiểm tra phương thức DecryptTwoCharacter với ký tự khoảng trắng.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))] // Mong đợi ngoại lệ ArgumentException khi đầu vào không hợp lệ.
        public void TC13_DecryptTwoCharacter_Test_result_valid_with_plaintex_is_empty_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "hau".
            playfair.InitMatrix_4_Hau("hau");

            // Chuỗi đầu vào giả định chứa khoảng trắng.
            string input_4_hau = "a ";

            // Gọi phương thức DecryptTwoCharacter_4_Hau với ký tự không hợp lệ, mong đợi ngoại lệ.
            string actual_4_hau = playfair.DecryptTwoCharacter_4_Hau('a', ' ');
        }

        // Test case 14: Kiểm tra phương thức Decrypt_4_Hau với văn bản chứa chữ thường.
        [TestMethod]
        public void TC14_Decrypt_Test_plaintext_Lower_Character_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "phi".
            playfair.InitMatrix_4_Hau("phi");

            // Chuỗi đầu vào là bản mã (ciphertext) chứa chữ thường và chữ hoa.
            string input_4_hau = "iekfibmZ";

            // Kết quả mong đợi sau khi giải mã (plaintext in hoa).
            string expected_4_hau = "XINCHAOX";

            // Gọi phương thức Decrypt_4_Hau để giải mã toàn bộ chuỗi.
            string actual_4_hau = playfair.Decrypt_4_Hau(input_4_hau);

            // So sánh kết quả thực tế với kết quả mong đợi.
            Assert.AreEqual(expected_4_hau, actual_4_hau, "Decrypt failed for lowercase plaintext.");
        }

        // Test case 15: Kiểm tra phương thức Decrypt_4_Hau với văn bản chứa ký tự đặc biệt.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))] // Mong đợi ngoại lệ ArgumentException khi đầu vào không hợp lệ.
        public void TC15_Decrypt_Test_plaintext_contain_specical_Character_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "hau".
            playfair.InitMatrix_4_Hau("hau");

            // Chuỗi đầu vào chứa ký tự đặc biệt "@" và "$".
            string input_4_hau = "i@ekf$ibmZ";

            // Gọi phương thức Decrypt_4_Hau, mong đợi ngoại lệ ArgumentException do ký tự không hợp lệ.
            string actual_4_hau = playfair.Decrypt_4_Hau(input_4_hau);
        }

        // Test case 16: Kiểm tra phương thức Decrypt_4_Hau với văn bản chỉ chứa khoảng trắng.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))] // Mong đợi ngoại lệ ArgumentException khi đầu vào không hợp lệ.
        public void TC16_Decrypt_Test_plaintext_is_whitespace_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "hau".
            playfair.InitMatrix_4_Hau("hau");

            // Chuỗi đầu vào chỉ chứa khoảng trắng.
            string input_4_hau = " ";

            // Gọi phương thức Decrypt_4_Hau, mong đợi ngoại lệ ArgumentException vì chuỗi rỗng sau khi xử lý.
            string actual_4_hau = playfair.Decrypt_4_Hau(input_4_hau);
        }

        // Test case 17: Kiểm tra phương thức Decrypt_4_Hau với văn bản rỗng.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))] // Mong đợi ngoại lệ ArgumentException khi đầu vào không hợp lệ.
        public void TC17_Decrypt_Test_plaintext_is_empty_4_Hau()
        {
            // Khởi tạo ma trận Playfair với khóa "hau".
            playfair.InitMatrix_4_Hau("hau");

            // Chuỗi đầu vào rỗng.
            string input_4_hau = "";

            // Gọi phương thức Decrypt_4_Hau, mong đợi ngoại lệ ArgumentException vì chuỗi rỗng.
            string actual_4_hau = playfair.Decrypt_4_Hau(input_4_hau);
        }

        // Test case 18: Kiểm tra cả Encrypt_4_Hau và Decrypt_4_Hau với dữ liệu từ file CSV.
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", // Sử dụng nguồn dữ liệu từ file CSV.
            @".\Data_csv_4_Hau\Test_Data_4_Hau.csv", // Đường dẫn tương đối đến file CSV.
            "Test_Data_4_Hau#csv", // Tên file CSV (dùng trong DataSource).
            DataAccessMethod.Sequential)] // Đọc dữ liệu tuần tự từ file CSV.
        public void TC18_TestPlayfairCipher_Encrypt_Decrypt_WithCSV_4_Hau()
        {
            // Lấy dữ liệu từ từng dòng trong file CSV thông qua TestContext.DataRow.
            string input_4_Hau = TestContext.DataRow[0].ToString();    // Cột 0: Chuỗi đầu vào (plaintext hoặc ciphertext).
            string key_4_Hau = TestContext.DataRow[1].ToString();      // Cột 1: Khóa (key) để khởi tạo ma trận.
            string operation_4_Hau = TestContext.DataRow[2].ToString(); // Cột 2: Thao tác (Encrypt hoặc Decrypt).
            string expected_4_Hau = TestContext.DataRow[3].ToString();  // Cột 3: Kết quả mong đợi (expected).

            // Chuyển kết quả mong đợi thành chữ cái in hoa để đồng nhất định dạng.
            string expectedUpper_4_Hau = expected_4_Hau.ToUpper();

            // Khởi tạo ma trận Playfair với khóa từ file CSV.
            playfair.InitMatrix_4_Hau(key_4_Hau);

            // Biến lưu kết quả thực tế sau khi mã hóa hoặc giải mã.
            string actual_4_Hau;

            // Thực hiện thao tác dựa trên giá trị của operation_4_Hau.
            if (operation_4_Hau == "Encrypt")
            {
                // Nếu thao tác là "Encrypt", gọi phương thức mã hóa.
                actual_4_Hau = playfair.Encrypt_4_Hau(input_4_Hau);
            }
            else if (operation_4_Hau == "Decrypt")
            {
                // Nếu thao tác là "Decrypt", gọi phương thức giải mã.
                actual_4_Hau = playfair.Decrypt_4_Hau(input_4_Hau);
            }
            else
            {
                // Nếu operation không hợp lệ (không phải Encrypt hay Decrypt), thất bại test case.
                Assert.Fail("Operation không hợp lệ: " + operation_4_Hau);
                return;
            }

            // So sánh kết quả thực tế với kết quả mong đợi, kèm thông tin chi tiết nếu thất bại.
            Assert.AreEqual(expectedUpper_4_Hau, actual_4_Hau,
                $"Input: {input_4_Hau}, Key: {key_4_Hau}, Operation: {operation_4_Hau}");
        }

        // Phương thức GetTestCases_4_Hau: Đọc dữ liệu kiểm thử từ file Excel và trả về danh sách test case.
        private static IEnumerable<object[]> GetTestCases_4_Hau()
        {
            // Đường dẫn tương đối đến file Excel chứa dữ liệu kiểm thử.
            string filePath_4_Hau = @"Data_excel_4_Hau\Test_Data_4_Hau.xlsx";

            // Khởi tạo danh sách để lưu các test case, mỗi test case là một mảng object chứa 4 giá trị: input, key, operation, expected.
            var testCases_4_Hau = new List<object[]>();

            // Kiểm tra xem file Excel có tồn tại tại đường dẫn hay không.
            if (!File.Exists(filePath_4_Hau))
            {
                // Nếu file không tồn tại, ném ngoại lệ FileNotFoundException với thông báo chi tiết.
                throw new FileNotFoundException($"Không tìm thấy file Excel tại: {filePath_4_Hau}");
            }

            // Sử dụng thư viện ClosedXML để mở và đọc file Excel trong khối using (tự động giải phóng tài nguyên).
            using (var workbook = new XLWorkbook(filePath_4_Hau))
            {
                // Lấy worksheet đầu tiên (sheet 1) trong file Excel.
                var worksheet = workbook.Worksheet(1);

                // Kiểm tra xem worksheet có tồn tại không.
                if (worksheet == null)
                {
                    // Nếu không tìm thấy worksheet, ném ngoại lệ với thông báo chi tiết.
                    throw new Exception("Không tìm thấy worksheet trong file Excel.");
                }

                // Lấy tất cả các dòng đã sử dụng trong worksheet và chuyển thành danh sách để dễ xử lý.
                var usedRows = worksheet.RowsUsed().ToList();

                // Đếm số dòng có dữ liệu trong worksheet.
                int rowCount = usedRows.Count;

                // Kiểm tra xem file Excel có dữ liệu không (ít nhất phải có 2 dòng: tiêu đề + dữ liệu).
                if (rowCount < 2)
                {
                    // Nếu chỉ có dòng tiêu đề hoặc không có dữ liệu, ném ngoại lệ.
                    throw new Exception("File Excel không có dữ liệu (chỉ có tiêu đề).");
                }

                // In số dòng dữ liệu được đọc (trừ dòng tiêu đề) ra console để kiểm tra thủ công.
                Console.WriteLine($"Đọc {rowCount - 1} dòng dữ liệu từ Excel:");

                // Duyệt qua từng dòng dữ liệu trong worksheet, bắt đầu từ dòng thứ 2 (bỏ qua tiêu đề).
                for (int i = 1; i < rowCount; i++) // i = 1 tương ứng dòng thứ 2 (dòng đầu là header).
                {
                    var row = usedRows[i]; // Lấy dòng hiện tại từ danh sách các dòng đã sử dụng.

                    // Đọc dữ liệu từ các cột trong dòng hiện tại:
                    string input_4_Hau = row.Cell(1).GetString();    // Cột 1: Chuỗi đầu vào (input).
                    string key_4_Hau = row.Cell(2).GetString();      // Cột 2: Khóa (key).
                    string operation_4_Hau = row.Cell(3).GetString(); // Cột 3: Thao tác (Encrypt/Decrypt).
                    string expected_4_Hau = row.Cell(4).GetString();  // Cột 4: Kết quả mong đợi (expected).

                    // Thêm test case vào danh sách, mỗi test case là một mảng object chứa 4 giá trị trên.
                    testCases_4_Hau.Add(new object[] { input_4_Hau, key_4_Hau, operation_4_Hau, expected_4_Hau });
                }
            }

            // Trả về danh sách các test case dưới dạng IEnumerable<object[]> để sử dụng trong test case động.
            return testCases_4_Hau;
        }

        // Test case 19: Kiểm tra cả Encrypt_4_Hau và Decrypt_4_Hau với dữ liệu từ file Excel.
        [DataTestMethod] // Đánh dấu đây là test case sử dụng dữ liệu động (dynamic data).
        [DynamicData(nameof(GetTestCases_4_Hau), DynamicDataSourceType.Method)] // Lấy dữ liệu từ phương thức GetTestCases_4_Hau.
        public void TC19_TestPlayfairCipher_Encrypt_Decrypt_WithExcel_4_Hau(
            string input_4_Hau,    // Tham số 1: Chuỗi đầu vào (plaintext hoặc ciphertext).
            string key_4_Hau,      // Tham số 2: Khóa (key) để khởi tạo ma trận.
            string operation_4_Hau, // Tham số 3: Thao tác (Encrypt hoặc Decrypt).
            string expected_4_Hau)  // Tham số 4: Kết quả mong đợi (expected).
        {
            // Kiểm tra xem các tham số đầu vào có rỗng hoặc null không.
            if (string.IsNullOrEmpty(input_4_Hau) || string.IsNullOrEmpty(key_4_Hau) ||
                string.IsNullOrEmpty(operation_4_Hau) || string.IsNullOrEmpty(expected_4_Hau))
            {
                // Nếu bất kỳ tham số nào rỗng hoặc null, thất bại test case với thông báo chi tiết.
                Assert.Fail($"Dữ liệu không hợp lệ: Input={input_4_Hau}, Key={key_4_Hau}, Operation={operation_4_Hau}, Expected={expected_4_Hau}");
                return;
            }

            // Chuyển kết quả mong đợi thành chữ cái in hoa để đồng nhất định dạng so sánh.
            string expectedUpper_4_Hau = expected_4_Hau.ToUpper();

            // Khởi tạo ma trận Playfair với khóa từ file Excel.
            try
            {
                playfair.InitMatrix_4_Hau(key_4_Hau); // Gọi hàm khởi tạo ma trận từ lớp PlayFair_4_Hau.
            }
            catch (Exception ex)
            {
                // Nếu khởi tạo ma trận thất bại (ví dụ: khóa không hợp lệ), thất bại test case với thông báo chi tiết.
                Assert.Fail($"Lỗi khởi tạo ma trận: Key={key_4_Hau}, Error={ex.Message}");
                return;
            }

            // Biến lưu kết quả thực tế sau khi mã hóa hoặc giải mã.
            string actual_4_Hau = null;

            // Thực hiện thao tác mã hóa hoặc giải mã dựa trên giá trị của operation_4_Hau.
            try
            {
                if (operation_4_Hau.Equals("Encrypt", StringComparison.OrdinalIgnoreCase))
                {
                    // Nếu thao tác là "Encrypt" (không phân biệt hoa thường), gọi phương thức mã hóa.
                    actual_4_Hau = playfair.Encrypt_4_Hau(input_4_Hau);
                }
                else if (operation_4_Hau.Equals("Decrypt", StringComparison.OrdinalIgnoreCase))
                {
                    // Nếu thao tác là "Decrypt" (không phân biệt hoa thường), gọi phương thức giải mã.
                    actual_4_Hau = playfair.Decrypt_4_Hau(input_4_Hau);
                }
                else
                {
                    // Nếu operation không phải "Encrypt" hay "Decrypt", thất bại test case với thông báo chi tiết.
                    Assert.Fail($"Operation không hợp lệ: {operation_4_Hau}");
                    return;
                }
            }
            catch (Exception ex)
            {
                // Nếu có lỗi trong quá trình mã hóa/giải mã (ví dụ: đầu vào không hợp lệ), thất bại test case.
                Assert.Fail($"Lỗi khi thực hiện {operation_4_Hau}: Input={input_4_Hau}, Key={key_4_Hau}, Error={ex.Message}");
                return;
            }

            // So sánh kết quả thực tế với kết quả mong đợi, nếu không khớp thì báo lỗi kèm thông tin chi tiết.
            Assert.AreEqual(expectedUpper_4_Hau, actual_4_Hau,
                $"Sai kết quả: Input={input_4_Hau}, Key={key_4_Hau}, Operation={operation_4_Hau}, Expected={expectedUpper_4_Hau}, Actual={actual_4_Hau}");
        }
    }
}