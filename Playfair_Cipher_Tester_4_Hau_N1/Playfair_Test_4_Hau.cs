// File: Playfair_Test_4_Hau.cs
// Mục đích: File này chứa các phương thức Test case (unit test) cho thuật mã Playfair,
// bao gồm kiểm tra các chức năng mã hóa, giải mã, và xử lý chuỗi 

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Playfair_Cipher_4_Hau_N1.src;
using System.Data;
using System.Linq;
using ClosedXML.Excel;// Thư viện ClosedXML
using System.Collections.Generic;
using System.IO;

namespace Playfair_Cipher_Tester_4_Hau_N1
{
    [TestClass]
    public class Playfair_Test_4_Hau
    {
        private PlayFair_4_Hau playfair; // Khai báo biến instance cho đối tượng Playfair

        public TestContext TestContext { get; set; }

        // Khởi tạo ma trận 5x5 một lần duy nhất
        [TestInitialize]
        public void Setup_4_Hau()
        {
            playfair = new PlayFair_4_Hau(5); // Khởi tạo đối tượng Playfair với ma trận 5x5
        }

        // Test case 1: Kiểm tra phương thức RemoveSpaceInString với chuỗi hợp lệ
        [TestMethod]
        public void RemoveSpaceInString_TestValid_Value_Return_4_Hau()
        {
            string input_4_hau = "Nguyen Trung Hau";
            string expected_4_hau = "NguyenTrungHau";
            string actual_4_hau = playfair.RemoveSpaceInString_4_Hau(input_4_hau);
            Assert.AreEqual(expected_4_hau, actual_4_hau, "RemoveSpaceInString failed with valid input.");
        }

        // Test case 2: Kiểm tra phương thức HoanDoi với các cặp tọa độ hợp lệ (chế độ mã hóa)
        [TestMethod]
        public void HoanDoi_Test_Valid_Value_Return_4_Hau()
        {
            playfair.InitMatrix_4_Hau("phi");

            // Test case 1: Khác hàng, khác cột
            string input1_4_hau = "PM";
            string expected1_4_hau = "IK";
            string actual1_4_hau = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 0, J = 0 }, new Coordinate_4_Hau { I = 2, J = 2 });
            Assert.AreEqual(expected1_4_hau, actual1_4_hau, "HoanDoi failed for different row and column.");

            // Test case 2: Cùng cột
            string input2_4_hau = "PV";
            string expected2_4_hau = "CP";
            string actual2_4_hau = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 0, J = 0 }, new Coordinate_4_Hau { I = 4, J = 0 });
            Assert.AreEqual(expected2_4_hau, actual2_4_hau, "HoanDoi failed for same column.");

            // Test case 3: Cùng hàng
            string input3_4_hau = "PB";
            string expected3_4_hau = "HP";
            string actual3_4_hau = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 0, J = 0 }, new Coordinate_4_Hau { I = 0, J = 4 });
            Assert.AreEqual(expected3_4_hau, actual3_4_hau, "HoanDoi failed for same row.");
        }

        // Test case 3: Kiểm tra phương thức HoanDoi với tọa độ không hợp lệ
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void HoanDoi_Test_IndexOutOfRange_4_Hau()
        {
            playfair.InitMatrix_4_Hau("phi");
            string input_4_hau = "Invalid Coordinates";
            string actual_4_hau = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 2, J = 6 }, new Coordinate_4_Hau { I = 2, J = 2 });
            // Không cần expected vì mong đợi ngoại lệ
        }

        // Test case 4: Kiểm tra phương thức HoanDoi với các cặp tọa độ hợp lệ (chế độ giải mã)
        [TestMethod]
        public void HoanDoi_Test_Decrypt_4_Hau()
        {
            playfair.InitMatrix_4_Hau("phi");

            // Test case 1: Khác hàng, khác cột
            string input1_4_hau = "PM";
            string expected1_4_hau = "IK";
            string actual1_4_hau = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 0, J = 0 }, new Coordinate_4_Hau { I = 2, J = 2 }, false);
            Assert.AreEqual(expected1_4_hau, actual1_4_hau, "HoanDoi decrypt failed for different row and column.");

            // Test case 2: Cùng cột
            string input2_4_hau = "PV";
            string expected2_4_hau = "VQ";
            string actual2_4_hau = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 0, J = 0 }, new Coordinate_4_Hau { I = 4, J = 0 }, false);
            Assert.AreEqual(expected2_4_hau, actual2_4_hau, "HoanDoi decrypt failed for same column.");

            // Test case 3: Cùng hàng
            string input3_4_hau = "PB";
            string expected3_4_hau = "BA";
            string actual3_4_hau = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 0, J = 0 }, new Coordinate_4_Hau { I = 0, J = 4 }, false);
            Assert.AreEqual(expected3_4_hau, actual3_4_hau, "HoanDoi decrypt failed for same row.");
        }

        // Test case 5: Kiểm tra phương thức EncryptTwoCharacter với cặp ký tự hợp lệ
        [TestMethod]
        public void EncryptTwoCharacter_Test_valid_result_4_Hau()
        {
            playfair.InitMatrix_4_Hau("phi");

            // Test case 1
            string input1_4_hau = "hm";
            string expected1_4_hau = "IL";
            string actual1_4_hau = playfair.EncryptTwoCharacter_4_Hau('h', 'm');
            Assert.AreEqual(expected1_4_hau, actual1_4_hau, "EncryptTwoCharacter failed for 'hm'.");

            // Test case 2
            string input2_4_hau = "At";
            string expected2_4_hau = "FY";
            string actual2_4_hau = playfair.EncryptTwoCharacter_4_Hau('A', 't');
            Assert.AreEqual(expected2_4_hau, actual2_4_hau, "EncryptTwoCharacter failed for 'At'.");

            // Test case 3
            string input3_4_hau = "qR";
            string expected3_4_hau = "RS";
            string actual3_4_hau = playfair.EncryptTwoCharacter_4_Hau('q', 'R');
            Assert.AreEqual(expected3_4_hau, actual3_4_hau, "EncryptTwoCharacter failed for 'qR'.");
        }

        // Test case 6: Kiểm tra phương thức EncryptTwoCharacter với ký tự không hợp lệ
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EncryptTwoCharacter_Test_input_empty_4_Hau()
        {
            playfair.InitMatrix_4_Hau("hau");
            string input_4_hau = "A ";
            string actual_4_hau = playfair.EncryptTwoCharacter_4_Hau('A', ' ');
            // Không cần expected vì mong đợi ngoại lệ
        }

        // Test case 7: Kiểm tra phương thức Encrypt_4_Hau với văn bản có độ dài lẻ
        [TestMethod]
        public void Encrypt_Test_plain_text_is_odd_character_4_Hau()
        {
            playfair.InitMatrix_4_Hau("dai hoc cong nghe thong tin");
            string input_4_hau = "xin chao cac ban";
            string expected_4_hau = "IGGNOIDTDNFDGW";
            string actual_4_hau = playfair.Encrypt_4_Hau(input_4_hau);
            Assert.AreEqual(expected_4_hau, actual_4_hau, "Encrypt failed for odd-length plaintext.");
        }

        // Test case 8: Kiểm tra phương thức Encrypt_4_Hau với văn bản chứa ký tự không hợp lệ
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Encrypt_Test_argument_invalid_4_Hau()
        {
            playfair.InitMatrix_4_Hau("dai hoc cong nghe thong tin");
            string input_4_hau = "!@#$AVSDD";
            string actual_4_hau = playfair.Encrypt_4_Hau(input_4_hau);
            // Không cần expected vì mong đợi ngoại lệ
        }

        // Test case 9: Kiểm tra phương thức Encrypt_4_Hau với văn bản chỉ chứa khoảng trắng
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "plaintext is empty")]
        public void Encrypt_Test_argument_is_space_4_Hau()
        {
            playfair.InitMatrix_4_Hau("dai hoc cong nghe thong tin");
            string input_4_hau = " ";
            string actual_4_hau = playfair.Encrypt_4_Hau(input_4_hau);
            // Không cần expected vì mong đợi ngoại lệ
        }

        // Test case 10: Kiểm tra phương thức Encrypt_4_Hau với văn bản rỗng
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "plaintext is empty")]
        public void Encrypt_Test_argument_is_empty_4_Hau()
        {
            playfair.InitMatrix_4_Hau("dai hoc cong nghe thong tin");
            string input_4_hau = "";
            string actual_4_hau = playfair.Encrypt_4_Hau(input_4_hau);
            // Không cần expected vì mong đợi ngoại lệ
        }

        // Test case 11: Kiểm tra phương thức DecryptTwoCharacter với cặp ký tự hợp lệ
        [TestMethod]
        public void DecryptTwoCharacter_Test_result_valid_with_plaintext_lower_character_4_Hau()
        {
            playfair.InitMatrix_4_Hau("phi");

            // Test case 1: Cùng cột
            string input1_4_hau = "af";
            string expected1_4_hau = "YA";
            string actual1_4_hau = playfair.DecryptTwoCharacter_4_Hau('a', 'f');
            Assert.AreEqual(expected1_4_hau, actual1_4_hau, "DecryptTwoCharacter failed for 'af'.");

            // Test case 2: Cùng hàng
            string input2_4_hau = "cE";
            string expected2_4_hau = "GD";
            string actual2_4_hau = playfair.DecryptTwoCharacter_4_Hau('c', 'E');
            Assert.AreEqual(expected2_4_hau, actual2_4_hau, "DecryptTwoCharacter failed for 'cE'.");

            // Test case 3: Khác hàng, khác cột
            string input3_4_hau = "Kt";
            string expected3_4_hau = "NQ";
            string actual3_4_hau = playfair.DecryptTwoCharacter_4_Hau('K', 't');
            Assert.AreEqual(expected3_4_hau, actual3_4_hau, "DecryptTwoCharacter failed for 'Kt'.");
        }

        // Test case 12: Kiểm tra phương thức DecryptTwoCharacter với ký tự đặc biệt
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DecryptTwoCharacter_Test_result_valid_with_plaintex_special_character_4_Hau()
        {
            playfair.InitMatrix_4_Hau("hau");
            string input_4_hau = "a@";
            string actual_4_hau = playfair.DecryptTwoCharacter_4_Hau('a', '@');
            // Không cần expected vì mong đợi ngoại lệ
        }

        // Test case 13: Kiểm tra phương thức DecryptTwoCharacter với ký tự khoảng trắng
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DecryptTwoCharacter_Test_result_valid_with_plaintex_is_empty_4_Hau()
        {
            playfair.InitMatrix_4_Hau("hau");
            string input_4_hau = "a ";
            string actual_4_hau = playfair.DecryptTwoCharacter_4_Hau('a', ' ');
            // Không cần expected vì mong đợi ngoại lệ
        }

        // Test case 14: Kiểm tra phương thức Decrypt_4_Hau với văn bản chứa chữ thường
        [TestMethod]
        public void Decrypt_Test_plaintext_Lower_Character_4_Hau()
        {
            playfair.InitMatrix_4_Hau("phi");
            string input_4_hau = "iekfibmZ";
            string expected_4_hau = "XINCHAOX";
            string actual_4_hau = playfair.Decrypt_4_Hau(input_4_hau);
            Assert.AreEqual(expected_4_hau, actual_4_hau, "Decrypt failed for lowercase plaintext.");
        }

        // Test case 15: Kiểm tra phương thức Decrypt_4_Hau với văn bản chứa ký tự đặc biệt
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Decrypt_Test_plaintext_contain_specical_Character_4_Hau()
        {
            playfair.InitMatrix_4_Hau("hau");
            string input_4_hau = "i@ekf$ibmZ";
            string actual_4_hau = playfair.Decrypt_4_Hau(input_4_hau);
            // Không cần expected vì mong đợi ngoại lệ
        }

        // Test case 16: Kiểm tra phương thức Decrypt_4_Hau với văn bản chỉ chứa khoảng trắng
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Decrypt_Test_plaintext_is_whitespace_4_Hau()
        {
            playfair.InitMatrix_4_Hau("hau");
            string input_4_hau = " ";
            string actual_4_hau = playfair.Decrypt_4_Hau(input_4_hau);
            // Không cần expected vì mong đợi ngoại lệ
        }

        // Test case 17: Kiểm tra phương thức Decrypt_4_Hau với văn bản rỗng
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Decrypt_Test_plaintext_is_empty_4_Hau()
        {
            playfair.InitMatrix_4_Hau("hau");
            string input_4_hau = "";
            string actual_4_hau = playfair.Decrypt_4_Hau(input_4_hau);
            // Không cần expected vì mong đợi ngoại lệ
        }

        // Test case 18: Kiểm tra cả Encrypt_4_Hau và Decrypt_4_Hau với dữ liệu từ file CSV
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @".\Data_csv_4_Hau\Test_Data_4_Hau.csv", "Test_Data_4_Hau#csv", DataAccessMethod.Sequential)]
        public void TestPlayfairCipher_Encrypt_Decrypt_WithCSV_4_Hau()
        {
            // Lấy dữ liệu từ CSV
            string input_4_Hau = TestContext.DataRow[0].ToString();    // Cột 0: input_4_Hau
            string key_4_Hau = TestContext.DataRow[1].ToString();      // Cột 1: key_4_Hau
            string operation_4_Hau = TestContext.DataRow[2].ToString(); // Cột 2: operation_4_Hau
            string expected_4_Hau = TestContext.DataRow[3].ToString();  // Cột 3: Expected_4_Hau (ký tự thường)

            // Chuyển expected_4_Hau thành ký tự in hoa
            string expectedUpper_4_Hau = expected_4_Hau.ToUpper();

            // Khởi tạo ma trận với key
            playfair.InitMatrix_4_Hau(key_4_Hau);

            // Thực hiện hành động
            string actual_4_Hau;
            if (operation_4_Hau == "Encrypt")
            {
                actual_4_Hau = playfair.Encrypt_4_Hau(input_4_Hau);
            }
            else if (operation_4_Hau == "Decrypt")
            {
                actual_4_Hau = playfair.Decrypt_4_Hau(input_4_Hau);
            }
            else
            {
                Assert.Fail("Operation không hợp lệ: " + operation_4_Hau);
                return;
            }

            Assert.AreEqual(expectedUpper_4_Hau, actual_4_Hau, $"Input: {input_4_Hau}, Key: {key_4_Hau}, Operation: {operation_4_Hau}");
        }

        // Hàm này đọc dữ liệu từ file Excel 
        private static IEnumerable<object[]> GetTestCases_4_Hau()
        {
            // Đường dẫn tới file Excel chứa dữ liệu kiểm thử.
            string filePath_4_Hau = @"Data_excel_4_Hau\Test_Data_4_Hau.xlsx";

            // Khởi tạo danh sách để lưu các test case, mỗi test case là một mảng object chứa 4 giá trị: input, key, operation, expected.
            var testCases_4_Hau = new List<object[]>();

            // Kiểm tra xem file Excel có tồn tại không, nếu không thì ném ngoại lệ.
            if (!File.Exists(filePath_4_Hau))
            {
                throw new FileNotFoundException($"Không tìm thấy file Excel tại: {filePath_4_Hau}");
            }

            // Sử dụng thư viện ClosedXML để mở và đọc file Excel.
            using (var workbook = new XLWorkbook(filePath_4_Hau))
            {
                // Lấy worksheet đầu tiên trong file Excel.
                var worksheet = workbook.Worksheet(1);

                // Kiểm tra xem worksheet có tồn tại không, nếu không thì ném ngoại lệ.
                if (worksheet == null)
                {
                    throw new Exception("Không tìm thấy worksheet trong file Excel.");
                }

                // Lấy tất cả các dòng đã sử dụng trong worksheet và chuyển thành danh sách.
                var usedRows = worksheet.RowsUsed().ToList();
                int rowCount = usedRows.Count; // Đếm số dòng có dữ liệu.

                // Kiểm tra xem file Excel có dữ liệu không (ít nhất phải có 2 dòng: tiêu đề + dữ liệu).
                if (rowCount < 2)
                {
                    throw new Exception("File Excel không có dữ liệu (chỉ có tiêu đề).");
                }

                // In thông báo số dòng dữ liệu được đọc (trừ dòng tiêu đề).
                Console.WriteLine($"Đọc {rowCount - 1} dòng dữ liệu từ Excel:");

                // Duyệt qua từng dòng dữ liệu (bỏ qua dòng tiêu đề, bắt đầu từ dòng thứ 2).
                for (int i = 1; i < rowCount; i++) // i = 1 tương ứng dòng thứ 2 (dòng đầu là header)
                {
                    var row = usedRows[i]; // Lấy dòng hiện tại.

                    // Đọc dữ liệu từ các cột trong dòng:
                    string input_4_Hau = row.Cell(1).GetString();    // Cột 1: Chuỗi đầu vào (input).
                    string key_4_Hau = row.Cell(2).GetString();      // Cột 2: Khóa (key).
                    string operation_4_Hau = row.Cell(3).GetString(); // Cột 3: Thao tác (Encrypt/Decrypt).
                    string expected_4_Hau = row.Cell(4).GetString();  // Cột 4: Kết quả mong đợi (expected).

                    // Thêm test case vào danh sách, mỗi test case là một mảng object chứa 4 giá trị trên.
                    testCases_4_Hau.Add(new object[] { input_4_Hau, key_4_Hau, operation_4_Hau, expected_4_Hau });
                }
            }

            // Trả về danh sách các test case để sử dụng trong kiểm thử.
            return testCases_4_Hau;
        }

        // Test case 19: Kiểm tra cả Encrypt_4_Hau và Decrypt_4_Hau với dữ liệu từ file excel
        [DataTestMethod]
        [DynamicData(nameof(GetTestCases_4_Hau), DynamicDataSourceType.Method)]
        public void TestPlayfairCipher_Encrypt_Decrypt_WithExcel_4_Hau(
            string input_4_Hau,    
            string key_4_Hau,      
            string operation_4_Hau, 
            string expected_4_Hau)  
        {
            // Kiểm tra xem các tham số đầu vào có rỗng hoặc null không, nếu có thì báo lỗi.
            if (string.IsNullOrEmpty(input_4_Hau) || string.IsNullOrEmpty(key_4_Hau) ||
                string.IsNullOrEmpty(operation_4_Hau) || string.IsNullOrEmpty(expected_4_Hau))
            {
                Assert.Fail($"Dữ liệu không hợp lệ: Input={input_4_Hau}, Key={key_4_Hau}, Operation={operation_4_Hau}, Expected={expected_4_Hau}");
                return;
            }

            // Chuyển kết quả mong đợi thành chữ cái in hoa để so sánh đồng nhất.
            string expectedUpper_4_Hau = expected_4_Hau.ToUpper();

            // Khởi tạo ma trận Playfair với khóa, nếu lỗi thì báo thất bại.
            try
            {
                playfair.InitMatrix_4_Hau(key_4_Hau); // Gọi hàm khởi tạo ma trận từ lớp Playfair.
            }
            catch (Exception ex)
            {
                Assert.Fail($"Lỗi khởi tạo ma trận: Key={key_4_Hau}, Error={ex.Message}");
                return;
            }

            // Biến lưu kết quả thực tế sau khi mã hóa/giải mã.
            string actual_4_Hau = null;

            // Thực hiện thao tác mã hóa hoặc giải mã dựa trên giá trị của operation_4_Hau.
            try
            {
                if (operation_4_Hau.Equals("Encrypt", StringComparison.OrdinalIgnoreCase))
                {
                    actual_4_Hau = playfair.Encrypt_4_Hau(input_4_Hau); // Mã hóa chuỗi đầu vào.
                }
                else if (operation_4_Hau.Equals("Decrypt", StringComparison.OrdinalIgnoreCase))
                {
                    actual_4_Hau = playfair.Decrypt_4_Hau(input_4_Hau); // Giải mã chuỗi đầu vào.
                }
                else
                {
                    // Nếu operation không phải "Encrypt" hay "Decrypt" thì báo lỗi.
                    Assert.Fail($"Operation không hợp lệ: {operation_4_Hau}");
                    return;
                }
            }
            catch (Exception ex)
            {
                // Nếu có lỗi trong quá trình mã hóa/giải mã, báo thất bại kèm thông tin chi tiết.
                Assert.Fail($"Lỗi khi thực hiện {operation_4_Hau}: Input={input_4_Hau}, Key={key_4_Hau}, Error={ex.Message}");
                return;
            }

            // So sánh kết quả thực tế với kết quả mong đợi, nếu không khớp thì báo lỗi.
            Assert.AreEqual(expectedUpper_4_Hau, actual_4_Hau,
                $"Sai kết quả: Input={input_4_Hau}, Key={key_4_Hau}, Operation={operation_4_Hau}, Expected={expectedUpper_4_Hau}, Actual={actual_4_Hau}");
        }
    }
}