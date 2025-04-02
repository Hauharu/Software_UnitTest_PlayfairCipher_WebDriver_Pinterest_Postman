// File: Playfair_Test_4_Hau.cs
// Mục đích: File này chứa các phương thức kiểm thử (unit test) cho thuật mã Playfair,
// bao gồm kiểm tra các chức năng mã hóa, giải mã, và xử lý chuỗi của lớp PlayFair_4_Hau.

using Microsoft.VisualStudio.TestTools.UnitTesting; 
using System; 
using Playfair_Cipher_4_Hau_N1.src; // Namespace chứa các lớp của thuật mã Playfair (PlayFair_4_Hau, Coordinate_4_Hau, v.v.).
using System.Data; 
using System.Linq; 

namespace Playfair_Cipher_Tester_4_Hau_N1 
{
    [TestClass]
    public class Playfair_Test_4_Hau
    {
        
        public TestContext TestContext { get; set; }

        
        [TestInitialize]
        public void Setup_4_Hau()
        {
            
        }

        // Kiểm thử 1: Kiểm tra phương thức RemoveSpaceInString với chuỗi hợp lệ.
        [TestMethod]
        public void RemoveSpaceInString_TestValid_Value_Return_4_Hau()
        {
            // Bước 1: Khởi tạo đối tượng PlayFair_4_Hau với ma trận 5x5.
            PlayFair_4_Hau playfair = new PlayFair_4_Hau(5);

            // Bước 2: Gọi phương thức RemoveSpaceInString để loại bỏ khoảng trắng trong chuỗi.
            var result = playfair.RemoveSpaceInString_4_Hau("Nguyen Trung Hau");

            // Bước 3: So sánh kết quả với giá trị mong muốn.
            Assert.AreEqual("NguyenTrungHau", result); // Kết quả mong muốn là chuỗi không có khoảng trắng.
        }

        // Kiểm thử 2: Kiểm tra phương thức HoanDoi với các cặp tọa độ hợp lệ (chế độ mã hóa).
        [TestMethod]
        public void HoanDoi_Test_Valid_Value_Return_4_Hau()
        {
            // Bước 1: Khởi tạo đối tượng PlayFair_4_Hau với ma trận 5x5.
            PlayFair_4_Hau playfair = new PlayFair_4_Hau(5);

            // Bước 2: Khởi tạo ma trận Playfair với key "phi".
            playfair.InitMatrix_4_Hau("phi");

            // Bước 3: Gọi phương thức HoanDoi với 3 cặp tọa độ:
            // - (0,0) và (2,2): Khác hàng, khác cột (P và M).
            var result1 = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 0, J = 0 }, new Coordinate_4_Hau { I = 2, J = 2 });
            Assert.AreEqual("IK", result1); // Kết quả mong muốn: "IK".

            // - (0,0) và (4,0): Cùng cột (P và V).
            var result2 = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 0, J = 0 }, new Coordinate_4_Hau { I = 4, J = 0 });
            Assert.AreEqual("CP", result2); // Kết quả mong muốn: "CP".

            // - (0,0) và (0,4): Cùng hàng (P và B).
            var result3 = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 0, J = 0 }, new Coordinate_4_Hau { I = 0, J = 4 });
            Assert.AreEqual("HP", result3); // Kết quả mong muốn: "HP".
        }

        // Kiểm thử 3: Kiểm tra phương thức HoanDoi với tọa độ không hợp lệ (ném ngoại lệ).
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))] // Mong muốn ném ngoại lệ IndexOutOfRangeException.
        public void HoanDoi_Test_IndexOutOfRange_4_Hau()
        {
            // Bước 1: Khởi tạo đối tượng PlayFair_4_Hau với ma trận 5x5.
            PlayFair_4_Hau playfair = new PlayFair_4_Hau(5);

            // Bước 2: Khởi tạo ma trận Playfair với key "phi".
            playfair.InitMatrix_4_Hau("phi");

            // Bước 3: Gọi phương thức HoanDoi với các cặp tọa độ không hợp lệ (vượt giới hạn ma trận 5x5):
            var result = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 2, J = 6 }, new Coordinate_4_Hau { I = 2, J = 2 });
            result = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 5, J = 2 }, new Coordinate_4_Hau { I = 2, J = 2 });
            result = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 0, J = 4 }, new Coordinate_4_Hau { I = 2, J = 5 });
            result = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 0, J = 4 }, new Coordinate_4_Hau { I = 5, J = 2 });
            // Phương thức sẽ ném ngoại lệ IndexOutOfRangeException do tọa độ vượt quá giới hạn.
        }

        // Kiểm thử 4: Kiểm tra phương thức HoanDoi với các cặp tọa độ hợp lệ (chế độ giải mã).
        [TestMethod]
        public void HoanDoi_Test_Decrypt_4_Hau()
        {
            // Bước 1: Khởi tạo đối tượng PlayFair_4_Hau với ma trận 5x5.
            PlayFair_4_Hau playfair = new PlayFair_4_Hau(5);

            // Bước 2: Khởi tạo ma trận Playfair với key "phi".
            playfair.InitMatrix_4_Hau("phi");

            // Bước 3: Gọi phương thức HoanDoi với 3 cặp tọa độ (chế độ giải mã):
            // - (0,0) và (2,2): Khác hàng, khác cột (P và M).
            var result1 = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 0, J = 0 }, new Coordinate_4_Hau { I = 2, J = 2 }, false);
            Assert.AreEqual("IK", result1); // Kết quả mong muốn: "IK".

            // - (0,0) và (4,0): Cùng cột (P và V).
            var result2 = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 0, J = 0 }, new Coordinate_4_Hau { I = 4, J = 0 }, false);
            Assert.AreEqual("VQ", result2); // Kết quả mong muốn: "VQ".

            // - (0,0) và (0,4): Cùng hàng (P và B).
            var result3 = playfair.HoanDoi_4_Hau(new Coordinate_4_Hau { I = 0, J = 0 }, new Coordinate_4_Hau { I = 0, J = 4 }, false);
            Assert.AreEqual("BA", result3); // Kết quả mong muốn: "BA".
        }

        // Kiểm thử 5: Kiểm tra phương thức EncryptTwoCharacter với cặp ký tự hợp lệ.
        [TestMethod]
        public void EncryptTwoCharacter_Test_valid_result_4_Hau()
        {
            // Bước 1: Khởi tạo đối tượng PlayFair_4_Hau với ma trận 5x5.
            PlayFair_4_Hau playfair = new PlayFair_4_Hau(5);

            // Bước 2: Khởi tạo ma trận Playfair với key "phi".
            playfair.InitMatrix_4_Hau("phi");

            // Bước 3: Gọi phương thức EncryptTwoCharacter với 3 cặp ký tự:
            // - 'h' và 'm'.
            var result = playfair.EncryptTwoCharacter_4_Hau('h', 'm');
            Assert.AreEqual("IL", result); // Kết quả mong muốn: "IL".

            // - 'A' và 't'.
            var result2 = playfair.EncryptTwoCharacter_4_Hau('A', 't');
            Assert.AreEqual("FY", result2); // Kết quả mong muốn: "FY".

            // - 'q' và 'R'.
            var result3 = playfair.EncryptTwoCharacter_4_Hau('q', 'R');
            Assert.AreEqual("RS", result3); // Kết quả mong muốn: "RS".
        }

        // Kiểm thử 6: Kiểm tra phương thức EncryptTwoCharacter với ký tự không hợp lệ (khoảng trắng).
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))] // Mong muốn ném ngoại lệ ArgumentException.
        public void EncryptTwoCharacter_Test_input_empty_4_Hau()
        {
            // Bước 1: Khởi tạo đối tượng PlayFair_4_Hau với ma trận 5x5.
            PlayFair_4_Hau playfair = new PlayFair_4_Hau(5);

            // Bước 2: Khởi tạo ma trận Playfair với key "hau".
            playfair.InitMatrix_4_Hau("hau");

            // Bước 3: Gọi phương thức EncryptTwoCharacter với cặp ký tự không hợp lệ: 'A' và ' '.
            var result = playfair.EncryptTwoCharacter_4_Hau('A', ' ');
            // Phương thức sẽ ném ngoại lệ ArgumentException do ký tự khoảng trắng không hợp lệ.
        }

        // Kiểm thử 7: Kiểm tra phương thức Encrypt_4_Hau với văn bản có độ dài lẻ.
        [TestMethod]
        public void Encrypt_Test_plain_text_is_odd_character_4_Hau()
        {
            // Bước 1: Khởi tạo đối tượng PlayFair_4_Hau với ma trận 5x5.
            PlayFair_4_Hau playfair = new PlayFair_4_Hau(5);

            // Bước 2: Khởi tạo ma trận Playfair với key "dai hoc cong nghe thong tin".
            playfair.InitMatrix_4_Hau("dai hoc cong nghe thong tin");

            // Bước 3: Gọi phương thức Encrypt_4_Hau với văn bản: "xin chao cac ban".
            string result = playfair.Encrypt_4_Hau("xin chao cac ban");

            // Bước 4: So sánh kết quả với giá trị mong muốn.
            Assert.AreEqual("IGGNOIDTDNFDGW", result); // Kết quả mong muốn: "IGGNOIDTDNFDGW".
        }

        // Kiểm thử 8: Kiểm tra phương thức Encrypt_4_Hau với văn bản chứa ký tự không hợp lệ.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))] // Mong muốn ném ngoại lệ ArgumentException.
        public void Encrypt_Test_argument_invalid_4_Hau()
        {
            // Bước 1: Khởi tạo đối tượng PlayFair_4_Hau với ma trận 5x5.
            PlayFair_4_Hau playfair = new PlayFair_4_Hau(5);

            // Bước 2: Khởi tạo ma trận Playfair với key "dai hoc cong nghe thong tin".
            playfair.InitMatrix_4_Hau("dai hoc cong nghe thong tin");

            // Bước 3: Gọi phương thức Encrypt_4_Hau với văn bản không hợp lệ: "!@#$AVSDD".
            string result = playfair.Encrypt_4_Hau("!@#$AVSDD");
            // Phương thức sẽ ném ngoại lệ ArgumentException do văn bản chứa ký tự đặc biệt.
        }

        // Kiểm thử 9: Kiểm tra phương thức Encrypt_4_Hau với văn bản chỉ chứa khoảng trắng.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "plaintext is empty")] // Mong muốn ném ngoại lệ ArgumentException với thông báo "plaintext is empty".
        public void Encrypt_Test_argument_is_space_4_Hau()
        {
            // Bước 1: Khởi tạo đối tượng PlayFair_4_Hau với ma trận 5x5.
            PlayFair_4_Hau playfair = new PlayFair_4_Hau(5);

            // Bước 2: Khởi tạo ma trận Playfair với key "dai hoc cong nghe thong tin".
            playfair.InitMatrix_4_Hau("dai hoc cong nghe thong tin");

            // Bước 3: Gọi phương thức Encrypt_4_Hau với văn bản chỉ chứa khoảng trắng: " ".
            string result = playfair.Encrypt_4_Hau(" ");
            // Phương thức sẽ ném ngoại lệ ArgumentException do văn bản rỗng sau khi loại bỏ khoảng trắng.
        }

        // Kiểm thử 10: Kiểm tra phương thức Encrypt_4_Hau với văn bản rỗng.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "plaintext is empty")] // Mong muốn ném ngoại lệ ArgumentException với thông báo "plaintext is empty".
        public void Encrypt_Test_argument_is_empty_4_Hau()
        {
            // Bước 1: Khởi tạo đối tượng PlayFair_4_Hau với ma trận 5x5.
            PlayFair_4_Hau playfair = new PlayFair_4_Hau(5);

            // Bước 2: Khởi tạo ma trận Playfair với key "dai hoc cong nghe thong tin".
            playfair.InitMatrix_4_Hau("dai hoc cong nghe thong tin");

            // Bước 3: Gọi phương thức Encrypt_4_Hau với văn bản rỗng: "".
            string result = playfair.Encrypt_4_Hau("");
            // Phương thức sẽ ném ngoại lệ ArgumentException do văn bản rỗng.
        }

        // Kiểm thử 11: Kiểm tra phương thức DecryptTwoCharacter với cặp ký tự hợp lệ (chữ thường và chữ hoa).
        [TestMethod]
        public void DecryptTwoCharacter_Test_result_valid_with_plaintext_lower_character_4_Hau()
        {
            // Bước 1: Khởi tạo đối tượng PlayFair_4_Hau với ma trận 5x5.
            PlayFair_4_Hau playfair = new PlayFair_4_Hau(5);

            // Bước 2: Khởi tạo ma trận Playfair với key "phi".
            playfair.InitMatrix_4_Hau("phi");

            // Bước 3: Gọi phương thức DecryptTwoCharacter với 3 cặp ký tự:
            // - 'a' và 'f': Cùng cột.
            var result = playfair.DecryptTwoCharacter_4_Hau('a', 'f');
            Assert.AreEqual("YA", result); // Kết quả mong muốn: "YA".

            // - 'c' và 'E': Cùng hàng.
            result = playfair.DecryptTwoCharacter_4_Hau('c', 'E');
            Assert.AreEqual("GD", result); // Kết quả mong muốn: "GD".

            // - 'K' và 't': Khác hàng, khác cột.
            result = playfair.DecryptTwoCharacter_4_Hau('K', 't');
            Assert.AreEqual("NQ", result); // Kết quả mong muốn: "NQ".
        }

        // Kiểm thử 12: Kiểm tra phương thức DecryptTwoCharacter với ký tự đặc biệt.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))] // Mong muốn ném ngoại lệ ArgumentException.
        public void DecryptTwoCharacter_Test_result_valid_with_plaintex_special_character_4_Hau()
        {
            // Bước 1: Khởi tạo đối tượng PlayFair_4_Hau với ma trận 5x5.
            PlayFair_4_Hau playfair = new PlayFair_4_Hau(5);

            // Bước 2: Khởi tạo ma trận Playfair với key "hau".
            playfair.InitMatrix_4_Hau("hau");

            // Bước 3: Gọi phương thức DecryptTwoCharacter với cặp ký tự không hợp lệ: 'a' và '@'.
            var result = playfair.DecryptTwoCharacter_4_Hau('a', '@');
            // Phương thức sẽ ném ngoại lệ ArgumentException do ký tự đặc biệt không hợp lệ.
        }

        // Kiểm thử 13: Kiểm tra phương thức DecryptTwoCharacter với ký tự khoảng trắng.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))] // Mong muốn ném ngoại lệ ArgumentException.
        public void DecryptTwoCharacter_Test_result_valid_with_plaintex_is_empty_4_Hau()
        {
            // Bước 1: Khởi tạo đối tượng PlayFair_4_Hau với ma trận 5x5.
            PlayFair_4_Hau playfair = new PlayFair_4_Hau(5);

            // Bước 2: Khởi tạo ma trận Playfair với key "hau".
            playfair.InitMatrix_4_Hau("hau");

            // Bước 3: Gọi phương thức DecryptTwoCharacter với cặp ký tự không hợp lệ: 'a' và ' '.
            var result = playfair.DecryptTwoCharacter_4_Hau('a', ' ');
            // Phương thức sẽ ném ngoại lệ ArgumentException do ký tự khoảng trắng không hợp lệ.
        }

        // Kiểm thử 14: Kiểm tra phương thức Decrypt_4_Hau với văn bản chứa chữ thường.
        [TestMethod]
        public void Decrypt_Test_plaintext_Lower_Character_4_Hau()
        {
            // Bước 1: Khởi tạo đối tượng PlayFair_4_Hau với ma trận 5x5.
            PlayFair_4_Hau playfair = new PlayFair_4_Hau(5);

            // Bước 2: Khởi tạo ma trận Playfair với key "phi".
            playfair.InitMatrix_4_Hau("phi");

            // Bước 3: Gọi phương thức Decrypt_4_Hau với văn bản: "iekfibmZ".
            var result = playfair.Decrypt_4_Hau("iekfibmZ");

            // Bước 4: So sánh kết quả với giá trị mong muốn.
            Assert.AreEqual("XINCHAOX", result); // Kết quả mong muốn: "XINCHAOX".
        }

        // Kiểm thử 15: Kiểm tra phương thức Decrypt_4_Hau với văn bản chứa ký tự đặc biệt.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))] // Mong muốn ném ngoại lệ ArgumentException.
        public void Decrypt_Test_plaintext_contain_specical_Character_4_Hau()
        {
            // Bước 1: Khởi tạo đối tượng PlayFair_4_Hau với ma trận 5x5.
            PlayFair_4_Hau playfair = new PlayFair_4_Hau(5);

            // Bước 2: Khởi tạo ma trận Playfair với key "hau".
            playfair.InitMatrix_4_Hau("hau");

            // Bước 3: Gọi phương thức Decrypt_4_Hau với văn bản không hợp lệ: "i@ekf$ibmZ".
            var result = playfair.Decrypt_4_Hau("i@ekf$ibmZ");
            // Phương thức sẽ ném ngoại lệ ArgumentException do văn bản chứa ký tự đặc biệt.
        }

        // Kiểm thử 16: Kiểm tra phương thức Decrypt_4_Hau với văn bản chỉ chứa khoảng trắng.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))] // Mong muốn ném ngoại lệ ArgumentException.
        public void Decrypt_Test_plaintext_is_whitespace_4_Hau()
        {
            // Bước 1: Khởi tạo đối tượng PlayFair_4_Hau với ma trận 5x5.
            PlayFair_4_Hau playfair = new PlayFair_4_Hau(5);

            // Bước 2: Khởi tạo ma trận Playfair với key "hau".
            playfair.InitMatrix_4_Hau("hau");

            // Bước 3: Gọi phương thức Decrypt_4_Hau với văn bản chỉ chứa khoảng trắng: " ".
            playfair.Decrypt_4_Hau(" ");
            // Phương thức sẽ ném ngoại lệ ArgumentException do văn bản rỗng sau khi loại bỏ khoảng trắng.
        }

        // Kiểm thử 17: Kiểm tra phương thức Decrypt_4_Hau với văn bản rỗng.
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))] // Mong muốn ném ngoại lệ ArgumentException.
        public void Decrypt_Test_plaintext_is_empty_4_Hau()
        {
            // Bước 1: Khởi tạo đối tượng PlayFair_4_Hau với ma trận 5x5.
            PlayFair_4_Hau playfair = new PlayFair_4_Hau(5);

            // Bước 2: Khởi tạo ma trận Playfair với key "hau".
            playfair.InitMatrix_4_Hau("hau");

            // Bước 3: Gọi phương thức Decrypt_4_Hau với văn bản rỗng: "".
            playfair.Decrypt_4_Hau("");
            // Phương thức sẽ ném ngoại lệ ArgumentException do văn bản rỗng.
        }

        // Kiểm thử 18: Kiểm tra phương thức Encrypt_4_Hau với dữ liệu từ file CSV.
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @".\Data_4_Hau\Test_Data_4_Hau.csv", "Test_Data_4_Hau#csv", DataAccessMethod.Sequential)]
        // Sử dụng nguồn dữ liệu từ file CSV để kiểm thử.
        public void Encrypt_Test_plain_text_is_odd_character_csv_4_Hau()
        {
            // Bước 1: Lấy dữ liệu từ file CSV thông qua TestContext.
            string key = TestContext.DataRow[0].ToString(); // Cột 0: Key.
            string plainText = TestContext.DataRow[1].ToString(); // Cột 1: Plaintext.
            string expectedCipherText = TestContext.DataRow[2].ToString(); // Cột 2: Expected Ciphertext.

            // Bước 2: Khởi tạo đối tượng PlayFair_4_Hau với ma trận 5x5.
            PlayFair_4_Hau playfair = new PlayFair_4_Hau(5);

            // Bước 3: Khởi tạo ma trận Playfair với key từ file CSV.
            playfair.InitMatrix_4_Hau(key);

            // Bước 4: Gọi phương thức Encrypt_4_Hau với plaintext từ file CSV.
            string result = playfair.Encrypt_4_Hau(plainText);

            // Bước 5: So sánh kết quả với expectedCipherText từ file CSV.
            Assert.AreEqual(expectedCipherText, result);
        }
    }
}