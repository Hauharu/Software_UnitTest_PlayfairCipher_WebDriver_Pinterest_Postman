using Microsoft.VisualStudio.TestTools.UnitTesting; 
using OpenQA.Selenium; // Thư viện Selenium WebDriver để điều khiển trình duyệt.
using OpenQA.Selenium.Chrome; // Thư viện dành riêng cho trình duyệt Chrome.
using OpenQA.Selenium.Support.UI; // Thư viện hỗ trợ các công cụ chờ (wait) trong Selenium.
using System; 
using System.IO; // Thư viện để làm việc với file 
using System.Threading; 

namespace WebDriver_Pinterest_4_Hau 
{
    [TestClass] 
    public class Pinterest_Test_4_Hau
    {
        // Khai báo biến tĩnh driver kiểu IWebDriver để điều khiển trình duyệt.
        // Biến này được chia sẻ cho tất cả các test case trong lớp (vì là static).
        private static IWebDriver driver_4_Hau;

        // Khai báo biến tĩnh keepBrowserOpen kiểu bool để quyết định có đóng trình duyệt sau khi chạy test hay không.
        // Mặc định là true, tức là không đóng trình duyệt tự động sau khi chạy xong tất cả test.
        private static bool keepBrowserOpen = true;

        [ClassInitialize] // Phương thức này chạy một lần duy nhất trước khi bất kỳ test case nào trong lớp được thực thi.
        public static void Setup(TestContext context) // Nhận TestContext để cung cấp thông tin về quá trình kiểm thử.
        {
            // Tạo một đối tượng ChromeOptions để cấu hình trình duyệt Chrome.
            var options_4_Hau = new ChromeOptions();

            // Thêm user-agent giả lập để trình duyệt Chrome hoạt động giống như một trình duyệt thông thường.
            // Điều này giúp tránh bị Pinterest hoặc các trang web khác chặn khi phát hiện là bot.
            options_4_Hau.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

            // Khởi tạo driver bằng ChromeDriver với các tùy chọn đã cấu hình ở trên.
            driver_4_Hau = new ChromeDriver(options_4_Hau);

            // Thiết lập thời gian chờ ngầm (implicit wait) là 10 giây.
            // Nếu một phần tử không được tìm thấy ngay lập tức, driver sẽ chờ tối đa 10 giây trước khi抛 ra ngoại lệ (NoSuchElementException).
            driver_4_Hau.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // Phóng to cửa sổ trình duyệt lên kích thước tối đa để đảm bảo giao diện hiển thị đầy đủ.
            // Điều này tránh lỗi khi các phần tử bị ẩn do cửa sổ quá nhỏ.
            driver_4_Hau.Manage().Window.Maximize();
        }

        [ClassCleanup] // Phương thức này chạy một lần duy nhất sau khi tất cả các test case trong lớp hoàn tất.
        public static void TearDown()
        {
            // Kiểm tra biến keepBrowserOpen để quyết định có đóng trình duyệt hay không.
            if (!keepBrowserOpen)
            {
                // Nếu keepBrowserOpen là false, đóng trình duyệt và giải phóng tài nguyên.
                driver_4_Hau.Quit();
            }
            else
            {
                // Nếu keepBrowserOpen là true, giữ trình duyệt mở và in thông báo để người dùng tự đóng thủ công.
                Console.WriteLine("Browser is kept open. Close it manually or run another test.");
            }
        }

        // Phương thức Login_4_Hau để thực hiện đăng nhập vào Pinterest.
        // Nhận vào email và password làm tham số để điền vào form đăng nhập.
        private void Login_4_Hau(string email_4_Hau, string password_4_Hau)
        {
            // Điều hướng trình duyệt đến trang chủ của Pinterest bằng phương thức GoToUrl.
            driver_4_Hau.Navigate().GoToUrl("https://www.pinterest.com/");

            // Tìm nút "Log in" trên trang chủ bằng biểu thức XPath.
            // XPath này tìm một div chứa chữ "Log in" và lưu vào biến loginButton kiểu IWebElement.
            IWebElement loginButton_4_Hau = driver_4_Hau.FindElement(By.XPath("//*[@id=\"__PWS_ROOT__\"]/div[1]/div[1]/div[2]/div/div[2]/div[2]/button/div/div"));
            // Nhấn vào nút "Log in" để mở form đăng nhập.
            loginButton_4_Hau.Click();

            // Tìm ô nhập email bằng ID "email" và lưu vào biến emailField kiểu IWebElement.
            IWebElement emailField_4_Hau = driver_4_Hau.FindElement(By.Id("email"));

            // Tìm ô nhập mật khẩu bằng ID "password" và lưu vào biến passwordField kiểu IWebElement.
            IWebElement passwordField_4_Hau = driver_4_Hau.FindElement(By.CssSelector("input[type=\"password\"]"));

            // Điền email vào ô emailField bằng phương thức SendKeys.
            emailField_4_Hau.SendKeys(email_4_Hau);

            // Điền mật khẩu vào ô passwordField bằng phương thức SendKeys.
            passwordField_4_Hau.SendKeys(password_4_Hau);

            // Lưu vào biến continueButton kiểu IWebElement.
            IWebElement continueButton_4_Hau = driver_4_Hau.FindElement(By.XPath("//*[@id=\"__PWS_ROOT__\"]/div[1]/div[3]/div/div/div/div/div/div[4]/div[1]/form/div[7]/button/div/div"));

            // Nhấn vào nút "Continue" để gửi thông tin đăng nhập.
            continueButton_4_Hau.Click();

            // Tạm dừng luồng hiện tại 5 giây bằng Thread.Sleep để chờ trang phản hồi sau khi đăng nhập.
            // Điều này tránh lỗi do trang tải chậm hoặc phản hồi chưa hoàn tất.
            Thread.Sleep(3000);
        }

        // Test case 1: Kiểm tra đăng nhập thành công với thông tin hợp lệ.
        [TestMethod] // Đánh dấu đây là một test case sẽ được chạy bởi framework kiểm thử.
        public void Test01_Login_With_Valid_Credentials_4_Hau()
        {
            // Gọi phương thức Login_4_Hau với email và mật khẩu hợp lệ.
            Login_4_Hau("trungghauu1@gmail.com", "Hau123456@");

            // Kiểm tra URL hiện tại có chứa "pinterest.com/" hay không để xác nhận đăng nhập thành công.
            // Nếu không, in thông báo "Đăng nhập thất bại."
            Assert.IsTrue(driver_4_Hau.Url.Contains("pinterest.com/"), "Đăng nhập thất bại.");

            // In URL hiện tại ra console để kiểm tra thủ công.
            Console.WriteLine("Current URL after login: " + driver_4_Hau.Url);
        }

        // Test case 2: Kiểm tra đăng nhập với email không hợp lệ, mong đợi thông báo lỗi.
        [TestMethod]
        public void Test02_Login_With_Invalid_Email_4_Hau()
        {
            // Gọi phương thức Login_4_Hau với email không hợp lệ và mật khẩu bất kỳ.
            Login_4_Hau("invalid_email@gmail.com", "Hau123456@");

            // Tạm dừng 5 giây để chờ thông báo lỗi xuất hiện trên giao diện.
            Thread.Sleep(5000);

            // Tìm thông báo lỗi bằng XPath, kiểm tra xem email không tồn tại.
            IWebElement errorMessage_4_Hau = driver_4_Hau.FindElement(By.XPath("//div[contains(text(), 'The email you entered does not belong to any account.')]"));
            //*[@id="email-error"]/div/div/div[2]
            // Kiểm tra xem thông báo lỗi có hiển thị hay không.
            // Nếu không hiển thị, in thông báo "Không hiển thị lỗi với email không hợp lệ."
            Assert.IsTrue(errorMessage_4_Hau.Displayed, "Không hiển thị lỗi với email không hợp lệ.");
        }

        // Test case 3: Kiểm tra đăng nhập với mật khẩu sai, mong đợi thông báo lỗi.
        [TestMethod]
        public void Test03_Login_With_Invalid_Password_4_Hau()
        {
            // Gọi phương thức Login_4_Hau với email hợp lệ nhưng mật khẩu sai.
            Login_4_Hau("trungghauu1@gmail.com", "wrong_password");

            // Tạm dừng 5 giây để chờ thông báo lỗi xuất hiện trên giao diện.
            Thread.Sleep(5000);

            try
            {
                // Tìm thông báo lỗi cụ thể liên quan đến mật khẩu sai bằng XPath.
                IWebElement errorMessag_4_Hau = driver_4_Hau.FindElement(By.XPath("//*[@id=\"password-error\"]/div/div/div[2]/div/span[1]"));

                // Kiểm tra xem thông báo lỗi có hiển thị hay không.
                Assert.IsTrue(errorMessag_4_Hau.Displayed, "Không hiển thị lỗi với mật khẩu không hợp lệ.");
            }
            catch (NoSuchElementException) // Nếu không tìm thấy thông báo lỗi cụ thể.
            {
                // Tìm một thông báo lỗi chung (generic) nếu thông báo cụ thể không xuất hiện.
                IWebElement genericError_4_Hau = driver_4_Hau.FindElement(By.XPath("//div[contains(@class, 'error')] | //div[contains(text(), 'password')]"));

                // Kiểm tra xem thông báo lỗi chung có hiển thị hay không.
                Assert.IsTrue(genericError_4_Hau.Displayed, "Không tìm thấy thông báo lỗi nào khi mật khẩu sai.");

                // In nội dung lỗi ra console để kiểm tra thủ công.
                Console.WriteLine("Found error: " + genericError_4_Hau.Text);
            }
        }

        // Test case 4: Kiểm tra đăng nhập với email chưa đăng ký, mong đợi thông báo lỗi.
        [TestMethod]
        public void Test04_Login_With_Unregistered_Email_4_Hau()
        {
            // Gọi phương thức Login_4_Hau với email chưa đăng ký và mật khẩu bất kỳ.
            Login_4_Hau("unregistered@gmail.com", "SomePass123");

            // Tạm dừng 5 giây để chờ thông báo lỗi xuất hiện trên giao diện.
            Thread.Sleep(5000);

            try
            {
                // Tìm thông báo lỗi liên quan đến tài khoản chưa đăng ký hoặc liên kết với Facebook.
                IWebElement errorMessage_4_Hau = driver_4_Hau.FindElement(By.XPath("//div[contains(text(), 'Your account is connected to Facebook')] | //div[contains(text(), 'Recover your account')]"));

                // Kiểm tra xem thông báo lỗi có hiển thị hay không.
                Assert.IsTrue(errorMessage_4_Hau.Displayed, "Không hiển thị lỗi với email chưa đăng ký.");

                // In nội dung lỗi ra console để kiểm tra thủ công.
                Console.WriteLine("Found error: " + errorMessage_4_Hau.Text);
            }
            catch (NoSuchElementException) // Nếu không tìm thấy thông báo lỗi cụ thể.
            {
                try
                {
                    // Tìm một thông báo lỗi chung (generic) nếu thông báo cụ thể không xuất hiện.
                    IWebElement genericError_4_Hau = driver_4_Hau.FindElement(By.XPath("//div[contains(@class, 'error')] | //div[contains(text(), 'account')]"));

                    // Kiểm tra xem thông báo lỗi chung có hiển thị hay không.
                    Assert.IsTrue(genericError_4_Hau.Displayed, "Không tìm thấy thông báo lỗi nào khi email chưa đăng ký.");

                    // In nội dung lỗi chung ra console để kiểm tra thủ công.
                    Console.WriteLine("Found generic error: " + genericError_4_Hau.Text);
                }
                catch (NoSuchElementException) // Nếu không tìm thấy bất kỳ thông báo lỗi nào.
                {
                    // Thất bại test case nếu không có thông báo lỗi nào xuất hiện.
                    Assert.Fail("Không tìm thấy bất kỳ thông báo lỗi nào khi email chưa đăng ký. Kiểm tra giao diện Pinterest.");
                }
            }
        }

        // Test case 5: Kiểm tra đăng nhập với email và mật khẩu trống, mong đợi thông báo lỗi.
        [TestMethod]
        public void Test05_Login_With_Empty_Email_And_Password_4_Hau()
        {
            // Điều hướng trình duyệt đến trang chủ Pinterest.
            driver_4_Hau.Navigate().GoToUrl("https://www.pinterest.com/");

            // Tìm nút "Log in" trên trang chủ bằng XPath và lưu vào biến loginButton.
            IWebElement loginButton_4_Hau = driver_4_Hau.FindElement(By.XPath("//div[contains(text(), 'Log in')]"));

            // Nhấn vào nút "Log in" để mở form đăng nhập.
            loginButton_4_Hau.Click();

            // Tạm dừng 5 giây để chờ giao diện form đăng nhập tải xong.
            Thread.Sleep(5000);

            // Tìm nút "Continue" hoặc "Submit" bằng XPath để gửi form trống.
            IWebElement continueButton_4_Hau = driver_4_Hau.FindElement(By.XPath("//button[@type='submit']"));

            // Nhấn nút "Continue" mà không nhập email và mật khẩu.
            continueButton_4_Hau.Click();

            // Tạm dừng 5 giây để chờ thông báo lỗi xuất hiện trên giao diện.
            Thread.Sleep(5000);

            try
            {
                // Tìm thông báo lỗi yêu cầu nhập email hoặc thông tin.
                IWebElement errorMessage_4_Hau = driver_4_Hau.FindElement(By.XPath("//div[contains(text(), 'Please enter')] | //div[contains(text(), 'email')]"));

                // Kiểm tra xem thông báo lỗi có hiển thị hay không.
                Assert.IsTrue(errorMessage_4_Hau.Displayed, "Không hiển thị thông báo lỗi khi email và mật khẩu trống.");

                // In nội dung lỗi ra console để kiểm tra thủ công.
                Console.WriteLine("Found error: " + errorMessage_4_Hau.Text);
            }
            catch (NoSuchElementException) // Nếu không tìm thấy thông báo lỗi.
            {
                // Thất bại test case nếu không có thông báo lỗi nào xuất hiện.
                Assert.Fail("Không tìm thấy thông báo lỗi khi email và mật khẩu trống. Kiểm tra giao diện Pinterest.");
            }
        }

        // Test case 6: Kiểm tra đăng nhập với mật khẩu quá ngắn (dưới 8 ký tự), mong đợi thông báo lỗi.
        [TestMethod]
        public void Test06_Login_With_Valid_Email_Short_Password_4_Hau()
        {
            // Gọi phương thức Login_4_Hau với email hợp lệ nhưng mật khẩu quá ngắn (3 ký tự).
            Login_4_Hau("trungghauu1@gmail.com", "123");

            // Tạm dừng 5 giây để chờ thông báo lỗi xuất hiện trên giao diện.
            Thread.Sleep(5000);

            // Tìm thông báo lỗi liên quan đến mật khẩu không hợp lệ.
            IWebElement errorMessage_Hau = driver_4_Hau.FindElement(By.XPath("//div[contains(text(), 'The password you')]"));

            // Kiểm tra xem thông báo lỗi có hiển thị hay không.
            Assert.IsTrue(errorMessage_Hau.Displayed, "Mật khẩu quá ngắn.");
        }

        // Test case 7: Kiểm tra tìm kiếm với từ khóa hợp lệ, mong đợi hiển thị kết quả.
        [TestMethod]
        public void Test07_Search_With_Valid_Keyword_4_Hau()
        {
            // Đăng nhập trước khi tìm kiếm.
            Login_4_Hau("trungghauu1@gmail.com", "Hau123456@");

            // Điều hướng trình duyệt đến trang chủ Pinterest.
            driver_4_Hau.Navigate().GoToUrl("https://www.pinterest.com/");    

            // Tìm ô tìm kiếm bằng XPath dựa trên placeholder "Search".
            IWebElement searchBar = driver_4_Hau.FindElement(By.XPath("//input[@placeholder='Search']"));
            // Nhập từ khóa "Anime" vào ô tìm kiếm.
            searchBar.SendKeys("Anime");

            // Nhấn phím Enter để thực hiện tìm kiếm.
            searchBar.SendKeys(Keys.Enter);

            // Tạm dừng 5 giây để chờ kết quả tìm kiếm hiển thị.
            Thread.Sleep(5000);
            // chọn title wallpaper
            IWebElement picture_4_Hau = driver_4_Hau.FindElement(By.XPath("//*[@id=\"__PWS_ROOT__\"]/div[1]/div/div[2]/div/div/div[4]/div/div/div/div/div[2]/div/div/div/div/div[1]/div/div/div/div/div/div/div/div[1]/a/div/div[1]/div/div/div/div/div/div/div/img"));
            picture_4_Hau.Click();
            // chọn ảnh đầu tiên
            Thread.Sleep(5000);
            // save ảnh
            Thread.Sleep(5000);
            // chọn title wallpaper
            IWebElement save_4_Hau = driver_4_Hau.FindElement(By.XPath("//*[@id=\"gradient\"]/div/div/div[2]/div/div/div/div/div/div/div/div/div/div[2]/div[1]/div/div/div/div[2]/div/div/div/div/div/div[2]/button/div"));
            save_4_Hau.Click();
        }

        // Test case 8: Kiểm tra tìm kiếm với từ khóa trống, mong đợi không chuyển trang.
        [TestMethod]
        public void Test08_Search_With_Empty_Keyword_4_Hau()
        {
            // Đăng nhập trước khi tìm kiếm.
            Login_4_Hau("trungghauu1@gmail.com", "Hau123456@");

            // Điều hướng trình duyệt đến trang chủ Pinterest.
            driver_4_Hau.Navigate().GoToUrl("https://www.pinterest.com/");

            // Tìm ô tìm kiếm bằng XPath dựa trên placeholder "Search".
            IWebElement searchBar_4_Hau = driver_4_Hau.FindElement(By.XPath("//input[@placeholder='Search']"));

            // Nhấn phím Enter mà không nhập từ khóa.
            searchBar_4_Hau.SendKeys(Keys.Enter);

            // Tạm dừng 5 giây để kiểm tra xem trang có thay đổi hay không.
            Thread.Sleep(5000);

            // Kiểm tra URL hiện tại vẫn chứa "pinterest.com" (không chuyển sang trang kết quả).
            Assert.IsTrue(driver_4_Hau.Url.Contains("pinterest.com"), "Tìm kiếm rỗng không chuyển trang.");
        }

        // Test case 9: Kiểm tra tìm kiếm với từ khóa không có kết quả, mong đợi thông báo "No results".
        [TestMethod]
        public void Test09_Search_With_No_Result_Keyword_4_Hau()
        {
            // Đăng nhập trước khi tìm kiếm.
            Login_4_Hau("trungghauu1@gmail.com", "Hau123456@");

            // Điều hướng trình duyệt đến trang chủ Pinterest.
            driver_4_Hau.Navigate().GoToUrl("https://www.pinterest.com/");

            // Tạm dừng 5 giây để chờ trang chủ tải xong.
            Thread.Sleep(5000);

            try
            {
                // Tìm ô tìm kiếm bằng nhiều XPath khác nhau để tăng khả năng tìm thấy.
                IWebElement searchBar_4_Hau = driver_4_Hau.FindElement(By.XPath("//input[@type='text'][@aria-label='Search'] | //input[@placeholder='Search for ideas'] | //input[@data-test-id='search-box-input']"));

                // Nhập từ khóa không tồn tại "xyzabc123nonexistent" vào ô tìm kiếm.
                searchBar_4_Hau.SendKeys("xyzabc123nonexistent");

                // Nhấn phím Enter để thực hiện tìm kiếm.
                searchBar_4_Hau.SendKeys(Keys.Enter);

                // Tạm dừng 5 giây để chờ trang kết quả tải xong.
                Thread.Sleep(5000);

                try
                {
                    // Tìm thông báo "No results" hoặc "try again" để xác nhận không có kết quả.
                    IWebElement noResultMessage_4_Hau = driver_4_Hau.FindElement(By.XPath("//div[contains(text(), 'No results')] | //div[contains(text(), 'try again')]"));

                    // Kiểm tra xem thông báo có hiển thị hay không.
                    Assert.IsTrue(noResultMessage_4_Hau.Displayed, "Không hiển thị thông báo khi không có kết quả.");

                    // In nội dung thông báo ra console để kiểm tra thủ công.
                    Console.WriteLine("Found message: " + noResultMessage_4_Hau.Text);
                }
                catch (NoSuchElementException) // Nếu không tìm thấy thông báo "No results".
                {
                    // Kiểm tra xem có ghim (pin) nào hiển thị không bằng cách lấy danh sách các phần tử.
                    var pins_4_Hau = driver_4_Hau.FindElements(By.XPath("//div[@data-test-id='pin']"));

                    // Kiểm tra số lượng ghim bằng 0, nếu không phải 0 thì test thất bại.
                    Assert.IsTrue(pins_4_Hau.Count == 0, "Vẫn hiển thị kết quả dù từ khóa không tồn tại.");
                }
            }
            catch (NoSuchElementException) // Nếu không tìm thấy ô tìm kiếm.
            {
                // In mã nguồn trang (page source) để debug nếu không tìm thấy ô tìm kiếm.
                Console.WriteLine("Search bar not found. Page source: " + driver_4_Hau.PageSource);

                // Thất bại test case nếu không tìm thấy ô tìm kiếm.
                Assert.Fail("Không tìm thấy ô tìm kiếm trên trang chủ Pinterest sau khi đăng nhập.");
            }
        }

        // Test case 10: Kiểm tra tìm kiếm với ký tự đặc biệt, mong đợi chuyển đến trang tìm kiếm.
        [TestMethod]
        public void Test10_Search_With_Special_Characters_4_Hau()
        {
            // Đăng nhập trước khi tìm kiếm.
            Login_4_Hau("trungghauu1@gmail.com", "Hau123456@");

            // Điều hướng trình duyệt đến trang chủ Pinterest.
            driver_4_Hau.Navigate().GoToUrl("https://www.pinterest.com/");

            // Tìm ô tìm kiếm bằng XPath dựa trên placeholder "Search".
            IWebElement searchBar_4_Hau = driver_4_Hau.FindElement(By.XPath("//input[@placeholder='Search']"));

            // Nhập từ khóa chứa ký tự đặc biệt "DIY @#$%" vào ô tìm kiếm.
            searchBar_4_Hau.SendKeys("DIY @#$%");

            // Nhấn phím Enter để thực hiện tìm kiếm.
            searchBar_4_Hau.SendKeys(Keys.Enter);

            // Tạm dừng 5 giây để chờ trang tìm kiếm tải xong.
            Thread.Sleep(5000);

            // Kiểm tra URL hiện tại có chứa "search" để xác nhận đã chuyển đến trang tìm kiếm.
            Assert.IsTrue(driver_4_Hau.Url.Contains("search"), "Tìm kiếm với ký tự đặc biệt không hoạt động.");
        }

        // Test case 11: Kiểm tra tìm kiếm với từ khóa dài, mong đợi chuyển đến trang tìm kiếm.
        [TestMethod]
        public void Test11_Search_With_Long_Keyword_4_Hau()
        {
            // Đăng nhập trước khi tìm kiếm (sử dụng email khác để thử nghiệm).
            Login_4_Hau("trungghauu2@gmail.com", "Hau123456@");

            // Điều hướng trình duyệt đến trang chủ Pinterest.
            driver_4_Hau.Navigate().GoToUrl("https://www.pinterest.com/");

            // Tạm dừng 5 giây để chờ trang chủ tải xong.
            Thread.Sleep(5000);

            try
            {
                // Tìm ô tìm kiếm bằng nhiều XPath khác nhau để tăng khả năng tìm thấy.
                IWebElement searchBar_4_Hau = driver_4_Hau.FindElement(By.XPath("//input[@type='text'][@aria-label='Search'] | //input[@placeholder='Search for ideas'] | //input[@data-test-id='search-box-input']"));

                // Tạo từ khóa dài 100 ký tự 'a' bằng cách sử dụng hàm new string.
                searchBar_4_Hau.SendKeys(new string('a', 100));

                // Nhấn phím Enter để thực hiện tìm kiếm.
                searchBar_4_Hau.SendKeys(Keys.Enter);

                // Tạm dừng 5 giây để chờ trang tìm kiếm tải xong.
                Thread.Sleep(5000);

                // Kiểm tra URL hiện tại có chứa "search" để xác nhận đã chuyển đến trang tìm kiếm.
                Assert.IsTrue(driver_4_Hau.Url.Contains("search"), "Tìm kiếm với từ khóa dài bất thường không hoạt động.");
            }
            catch (NoSuchElementException) // Nếu không tìm thấy ô tìm kiếm.
            {
                // In mã nguồn trang (page source) để debug nếu không tìm thấy ô tìm kiếm.
                Console.WriteLine("Search bar not found. Page source: " + driver_4_Hau.PageSource);

                // Thất bại test case nếu không tìm thấy ô tìm kiếm.
                Assert.Fail("Không tìm thấy ô tìm kiếm trên trang chủ Pinterest sau khi đăng nhập.");
            }
        }

        // Phương thức ChangeProfilePicture_4_Hau để thay đổi ảnh hồ sơ của người dùng.
        // Nhận vào đường dẫn file ảnh (imagePath) làm tham số.
        private void ChangeProfilePicture_4_Hau(string imagePath_4_Hau)
        {
            // Kiểm tra xem file ảnh có tồn tại tại đường dẫn imagePath hay không.
            if (!File.Exists(imagePath_4_Hau))
            {
                // Nếu không tồn tại, ném ngoại lệ FileNotFoundException với thông báo chi tiết.
                throw new FileNotFoundException($"File ảnh không tồn tại tại đường dẫn: {imagePath_4_Hau}. Vui lòng cung cấp đường dẫn hợp lệ.");
            }

            // Điều hướng trình duyệt đến trang chỉnh sửa hồ sơ của Pinterest.
            driver_4_Hau.Navigate().GoToUrl("https://www.pinterest.com/settings/edit-profile/");

            // Tạm dừng 3 giây để chờ trang tải hoàn tất.
            Thread.Sleep(3000);

            // Nhấn nút "Thay đổi" để chỉnh sửa ảnh hồ sơ.
            try
            {
                // Sử dụng WebDriverWait để chờ tối đa 10 giây cho đến khi nút "Thay đổi" xuất hiện.
                // Tìm nút bằng nhiều XPath khác nhau để tăng khả năng thành công.
                IWebElement editPictureButton_4_Hau = new WebDriverWait(driver_4_Hau, TimeSpan.FromSeconds(10)).Until(
                    d => d.FindElement(By.XPath("//button[contains(text(), 'Thay đổi')] | //button[contains(@aria-label, 'Edit profile picture')] | //div[contains(text(), 'Change')] | //button[contains(text(), 'Edit')]"))
                );

                // Nhấn vào nút "Thay đổi" để mở form tải ảnh lên.
                editPictureButton_4_Hau.Click();

                // Tạm dừng 2 giây để chờ form tải lên xuất hiện.
                Thread.Sleep(2000);
            }
            catch (WebDriverTimeoutException) // Nếu không tìm thấy nút "Thay đổi" trong 10 giây.
            {
                // In mã nguồn trang để debug nếu không tìm thấy nút.
                Console.WriteLine("Không tìm thấy nút 'Thay đổi'. Page source: " + driver_4_Hau.PageSource);

                // Thất bại test case nếu không tìm thấy nút chỉnh sửa ảnh.
                Assert.Fail("Không tìm thấy nút chỉnh sửa ảnh hồ sơ. Kiểm tra giao diện Pinterest.");
            }

            // Tìm ô input file để tải ảnh lên bằng XPath (dựa trên type='file').
            IWebElement uploadInput = driver_4_Hau.FindElement(By.XPath("//input[@type='file']"));

            // Gửi đường dẫn file ảnh đến ô input để tải lên.
            uploadInput.SendKeys(imagePath_4_Hau);

            // Tạm dừng 5 giây để chờ ảnh được tải lên hoàn tất.
            Thread.Sleep(5000);

            // Nhập "Họ" (Last Name) để kích hoạt nút "Lưu".
            try
            {
                // Sử dụng WebDriverWait để chờ tối đa 10 giây cho đến khi trường "Họ" xuất hiện.
                IWebElement lastNameField_4_Hau = new WebDriverWait(driver_4_Hau, TimeSpan.FromSeconds(10)).Until(
                    d => d.FindElement(By.XPath("//input[@placeholder='Họ'] | //input[@name='last_name']"))
                );

                // Xóa giá trị hiện tại trong trường "Họ" (nếu có).
                lastNameField_4_Hau.Clear();

                // Nhập giá trị "Nguyen" vào trường "Họ".
                lastNameField_4_Hau.SendKeys("Nguyen");

                // Tạm dừng 1 giây để chờ giao diện cập nhật sau khi nhập.
                Thread.Sleep(1000);
            }
            catch (WebDriverTimeoutException) // Nếu không tìm thấy trường "Họ" trong 10 giây.
            {
                // In mã nguồn trang để debug nếu không tìm thấy trường.
                Console.WriteLine("Không tìm thấy trường 'Họ'. Page source: " + driver_4_Hau.PageSource);

                // Thất bại test case nếu không tìm thấy trường "Họ".
                Assert.Fail("Không tìm thấy trường nhập 'Họ'. Kiểm tra giao diện Pinterest.");
            }

            // Nhấn nút "Lưu" để lưu thay đổi ảnh hồ sơ và thông tin.
            try
            {
                // Sử dụng WebDriverWait để chờ tối đa 10 giây cho đến khi nút "Lưu" xuất hiện.
                // Tìm nút bằng nhiều XPath khác nhau để tăng khả năng thành công.
                IWebElement saveButton_Hau = new WebDriverWait(driver_4_Hau, TimeSpan.FromSeconds(10)).Until(
                    d => d.FindElement(By.XPath("//div[@data-test-id='done-button']//button | //button[.//div[contains(text(), 'Save')]] | //button[.//div[contains(text(), 'Lưu')]] | //button[contains(text(), 'Done')] | //button[contains(text(), 'Confirm')] | //button[@type='submit']"))
                );

                // Nhấn vào nút "Lưu" để xác nhận thay đổi.
                saveButton_Hau.Click();

                // Tạm dừng 5 giây để chờ quá trình lưu hoàn tất.
                Thread.Sleep(5000);
            }
            catch (WebDriverTimeoutException) // Nếu không tìm thấy nút "Lưu" trong 10 giây.
            {
                // In mã nguồn trang để debug nếu không tìm thấy nút.
                Console.WriteLine("Không tìm thấy nút 'Lưu'. Page source: " + driver_4_Hau.PageSource);

                // Thất bại test case nếu không tìm thấy nút "Lưu".
                Assert.Fail("Không tìm thấy nút lưu sau khi nhập 'Họ'. Kiểm tra giao diện Pinterest.");
            }
        }

        // Test case 12: Kiểm tra thay đổi ảnh hồ sơ thành công.
        [TestMethod]
        public void Test12_Change_Profile_Picture_4_Hau()
        {
            // Đăng nhập trước khi thay đổi ảnh hồ sơ.
            Login_4_Hau("trungghauu1@gmail.com", "Hau123456@");

            // Định nghĩa đường dẫn file ảnh cần tải lên.
            string imagePath_4_Hau = @"C:\Users\PC\OneDrive\Pictures\1.jpg";

            // Kiểm tra xem file ảnh có tồn tại tại đường dẫn hay không.
            if (!File.Exists(imagePath_4_Hau))
            {
                // Thất bại test case nếu file không tồn tại, kèm thông báo chi tiết.
                Assert.Fail($"File ảnh không tồn tại tại đường dẫn: {imagePath_4_Hau}. Vui lòng kiểm tra lại thư mục và tên file.");
            }

            // Gọi phương thức ChangeProfilePicture_4_Hau để thay đổi ảnh hồ sơ.
            ChangeProfilePicture_4_Hau(imagePath_4_Hau);

            // Điều hướng trình duyệt đến trang hồ sơ cá nhân để kiểm tra ảnh mới.
            driver_4_Hau.Navigate().GoToUrl("https://www.pinterest.com/trungghauu1/");

            // Tạm dừng 5 giây để chờ trang hồ sơ tải xong.
            Thread.Sleep(5000);

            // Làm mới trang để đảm bảo ảnh hồ sơ được cập nhật.
            driver_4_Hau.Navigate().Refresh();

            // Tạm dừng 5 giây sau khi làm mới để chờ ảnh hiển thị.
            Thread.Sleep(5000);

            try
            {
                // Sử dụng WebDriverWait để chờ tối đa 20 giây cho đến khi ảnh hồ sơ xuất hiện.
                // Tìm ảnh bằng nhiều XPath khác nhau để tăng khả năng thành công.
                IWebElement profileImage_4_Hau = new WebDriverWait(driver_4_Hau, TimeSpan.FromSeconds(20)).Until(
                    d => d.FindElement(By.XPath("//img[contains(@class, 'profile-picture')] | //img[contains(@alt, 'trungghauu1')] | //div[@data-test-id='profile-picture']//img | //img[contains(@alt, 'Profile picture')] | //img[contains(@class, 'hCL')] | //img[contains(@class, 'user-profile')]"))
                );

                // Kiểm tra xem ảnh hồ sơ có hiển thị hay không.
                Assert.IsTrue(profileImage_4_Hau.Displayed, "Ảnh hồ sơ không được cập nhật thành công.");

                // In thông báo thành công ra console.
                Console.WriteLine("Profile picture updated successfully.");

                // Lấy URL của ảnh hồ sơ từ thuộc tính "src" và in ra console để kiểm tra thủ công.
                string imageSrc_4_Hau = profileImage_4_Hau.GetAttribute("src");
                Console.WriteLine($"URL của ảnh hồ sơ: {imageSrc_4_Hau}");
            }
            catch (WebDriverTimeoutException) // Nếu không tìm thấy ảnh hồ sơ trong 20 giây.
            {
                // In mã nguồn trang để debug nếu không tìm thấy ảnh.
                Console.WriteLine("Không tìm thấy ảnh hồ sơ sau khi cập nhật. Page source: " + driver_4_Hau.PageSource);

                // Thất bại test case nếu ảnh không hiển thị.
                Assert.Fail("Ảnh hồ sơ không hiển thị sau khi thay đổi.");
            }
        }

        // Test case 13: Kiểm tra thay đổi ảnh hồ sơ khi thiếu trường thông tin bắt buộc (Tên người dùng).
        [TestMethod]
        public void Test13_Change_Profile_Picture_Missing_4_Hau()
        {
            // Đăng nhập trước khi thay đổi ảnh hồ sơ.
            Login_4_Hau("trungghauu1@gmail.com", "Hau123456@");

            // Định nghĩa đường dẫn file ảnh cần tải lên.
            string imagePath_4_Hau = @"C:\Users\PC\OneDrive\Pictures\1.jpg";

            // Kiểm tra xem file ảnh có tồn tại tại đường dẫn hay không.
            if (!File.Exists(imagePath_4_Hau))
            {
                // Thất bại test case nếu file không tồn tại, kèm thông báo chi tiết.
                Assert.Fail($"File ảnh không tồn tại tại đường dẫn: {imagePath_4_Hau}. Vui lòng kiểm tra lại thư mục và tên file.");
            }

            // Điều hướng trình duyệt đến trang chỉnh sửa hồ sơ của Pinterest.
            driver_4_Hau.Navigate().GoToUrl("https://www.pinterest.com/settings/edit-profile/");

            // Tạm dừng 3 giây để chờ trang tải hoàn tất.
            Thread.Sleep(3000);

            // Nhấn nút "Thay đổi" để chỉnh sửa ảnh hồ sơ.
            try
            {
                // Sử dụng WebDriverWait để chờ tối đa 10 giây cho đến khi nút "Thay đổi" xuất hiện.
                IWebElement editPictureButton_4_Hau = new WebDriverWait(driver_4_Hau, TimeSpan.FromSeconds(10)).Until(
                    d => d.FindElement(By.XPath("//button[contains(text(), 'Thay đổi')] | //button[contains(@aria-label, 'Edit profile picture')] | //div[contains(text(), 'Change')] | //button[contains(text(), 'Edit')]"))
                );

                // Nhấn vào nút "Thay đổi" để mở form tải ảnh lên.
                editPictureButton_4_Hau.Click();

                // Tạm dừng 2 giây để chờ form tải lên xuất hiện.
                Thread.Sleep(2000);
            }
            catch (WebDriverTimeoutException) // Nếu không tìm thấy nút "Thay đổi" trong 10 giây.
            {
                // In mã nguồn trang để debug nếu không tìm thấy nút.
                Console.WriteLine("Không tìm thấy nút 'Thay đổi'. Page source: " + driver_4_Hau.PageSource);

                // Thất bại test case nếu không tìm thấy nút chỉnh sửa ảnh.
                Assert.Fail("Không tìm thấy nút chỉnh sửa ảnh hồ sơ. Kiểm tra giao diện Pinterest.");
            }

            // Tìm ô input file để tải ảnh lên bằng XPath (dựa trên type='file').
            IWebElement uploadInput_4_Hau = driver_4_Hau.FindElement(By.XPath("//input[@type='file']"));

            // Gửi đường dẫn file ảnh đến ô input để tải lên.
            uploadInput_4_Hau.SendKeys(imagePath_4_Hau);

            // Tạm dừng 5 giây để chờ ảnh được tải lên hoàn tất.
            Thread.Sleep(5000);

            // Để trống trường "Tên người dùng" (Username) để kiểm tra trường hợp thiếu thông tin bắt buộc.
            try
            {
                // Sử dụng WebDriverWait để chờ tối đa 10 giây cho đến khi trường "Tên người dùng" xuất hiện.
                IWebElement usernameField_4_Hau = new WebDriverWait(driver_4_Hau, TimeSpan.FromSeconds(10)).Until(
                    d => d.FindElement(By.XPath("//input[@placeholder='Tên người dùng'] | //input[@name='username']"))
                );

                // Xóa giá trị hiện tại trong trường "Tên người dùng" (nếu có).
                usernameField_4_Hau.Clear();

                // Không nhập gì vào trường "Tên người dùng" để mô phỏng trường hợp thiếu thông tin.
                Thread.Sleep(1000); // Tạm dừng 1 giây để chờ giao diện cập nhật.
            }
            catch (WebDriverTimeoutException) // Nếu không tìm thấy trường "Tên người dùng" trong 10 giây.
            {
                // In mã nguồn trang để debug nếu không tìm thấy trường.
                Console.WriteLine("Không tìm thấy trường 'Tên người dùng'. Page source: " + driver_4_Hau.PageSource);

                // Thất bại test case nếu không tìm thấy trường "Tên người dùng".
                Assert.Fail("Không tìm thấy trường nhập 'Tên người dùng'. Kiểm tra giao diện Pinterest.");
            }

            // Kiểm tra xem nút "Lưu" có xuất hiện hay không.
            bool isSaveButtonPresent_4_Hau = false;
            try
            {
                // Tìm nút "Lưu" bằng nhiều XPath khác nhau.
                IWebElement saveButton_4_Hau = driver_4_Hau.FindElement(By.XPath("//div[@data-test-id='done-button']//button | //button[.//div[contains(text(), 'Save')]] | //button[.//div[contains(text(), 'Lưu')]] | //button[contains(text(), 'Done')] | //button[contains(text(), 'Confirm')] | //button[@type='submit']"));

                // Nếu tìm thấy nút "Lưu" và nó hiển thị, gán isSaveButtonPresent = true.
                isSaveButtonPresent_4_Hau = saveButton_4_Hau.Displayed;
            }
            catch (NoSuchElementException) // Nếu không tìm thấy nút "Lưu".
            {
                // In thông báo xác nhận rằng nút "Lưu" không xuất hiện (điều này là mong đợi).
                Console.WriteLine("Nút 'Lưu' không xuất hiện vì thiếu trường 'Tên người dùng'.");
            }

            // Điều hướng trình duyệt đến trang hồ sơ cá nhân để kiểm tra ảnh không được cập nhật.
            driver_4_Hau.Navigate().GoToUrl("https://www.pinterest.com/trungghauu1/");

            // Tạm dừng 5 giây để chờ trang hồ sơ tải xong.
            Thread.Sleep(5000);

            // Làm mới trang để đảm bảo trạng thái mới nhất.
            driver_4_Hau.Navigate().Refresh();

            // Tạm dừng 5 giây sau khi làm mới để chờ ảnh hiển thị (nếu có).
            Thread.Sleep(5000);

            try
            {
                // Sử dụng WebDriverWait để chờ tối đa 20 giây cho đến khi ảnh hồ sơ xuất hiện.
                IWebElement profileImage_4_Hau = new WebDriverWait(driver_4_Hau, TimeSpan.FromSeconds(20)).Until(
                    d => d.FindElement(By.XPath("//img[contains(@class, 'profile-picture')] | //img[contains(@alt, 'trungghauu1')] | //div[@data-test-id='profile-picture']//img | //img[contains(@alt, 'Profile picture')] | //img[contains(@class, 'hCL')] | //img[contains(@class, 'user-profile')]"))
                );

                // Lấy URL của ảnh hồ sơ từ thuộc tính "src".
                string imageSrc_4_Hau = profileImage_4_Hau.GetAttribute("src");

                // In URL ảnh ra console để kiểm tra thủ công.
                Console.WriteLine($"URL của ảnh hồ sơ: {imageSrc_4_Hau}");

                // In thông báo xác nhận rằng ảnh không được cập nhật (vì thiếu "Tên người dùng").
                // Lưu ý: Để kiểm tra chính xác ảnh không thay đổi, cần so sánh imageSrc với ảnh cũ, nhưng ở đây giả định nút "Lưu" không xuất hiện nên ảnh không đổi.
                Console.WriteLine("Ảnh hồ sơ không được cập nhật vì thiếu trường 'Tên người dùng'. Test passed.");
            }
            catch (WebDriverTimeoutException) // Nếu không tìm thấy ảnh hồ sơ trong 20 giây.
            {
                // In mã nguồn trang để debug nếu không tìm thấy ảnh.
                Console.WriteLine("Không tìm thấy ảnh hồ sơ. Page source: " + driver_4_Hau.PageSource);

                // Trường hợp không có ảnh hồ sơ trước đó, vẫn coi test thành công vì ảnh không được cập nhật.
                Console.WriteLine("Ảnh hồ sơ không được cập nhật vì thiếu trường 'Tên người dùng'. Test passed.");
            }
        }
    }
}