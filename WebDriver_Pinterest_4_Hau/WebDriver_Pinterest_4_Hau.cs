using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebDriver_Pinterest_4_Hau
{
    public partial class form1 : Form
    {
        public form1()
        {
            InitializeComponent();
        }
        string email_4_Hau = "@gmail.com";
        string password_4_Hau = "@";

        private void btn_DangNhap_4_Hau_Click(object sender, EventArgs e)
        {
            // Đóng màn hình đen 
            ChromeDriverService chrome_4_Hau = ChromeDriverService.CreateDefaultService();
            chrome_4_Hau.HideCommandPromptWindow = true;

            // Điều hướng trình duyệt
            IWebDriver driver_4_Hau = new ChromeDriver(chrome_4_Hau);
            driver_4_Hau.Navigate().GoToUrl("https://www.pinterest.com/");

            // Ngủ 1,5s
            Thread.Sleep(1500);

            // Click vào button Login để mở Form
            driver_4_Hau.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div/div/div/div[1]/div/div[2]/div[2]/button")).Click();

            // TC1_DangNhap_4_Hau: Không điền thông tin Email và Password
            Thread.Sleep(3000); // Ngủ 3s
            driver_4_Hau.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div[2]/div/div/div/div/div/div[4]/form/div[7]/button")).Click();
            Thread.Sleep(2000); // Ngủ 2s
            if (driver_4_Hau.FindElements(By.XPath("/html/body/div[1]/div/div[1]/div[2]/div/div/div/div/div/div[4]/form/div[2]/fieldset/span/div[2]/div/span/div/div/div[2]"))
                .Count > 0)
            {
                Console.WriteLine("TC1_DangNhap_4_Hau: Đăng nhập không thành công.");
            }
            else
            {
                Console.WriteLine("TC1_DangNhap_4_Hau: Đăng nhập thành công.");
                return;
            }

            // TC2_DangNhap_4_Hau: Chỉ điền Email
            Thread.Sleep(5000); // Ngủ 5s
            driver_4_Hau.FindElement(By.Id("email")).SendKeys(email_4_Hau);
            driver_4_Hau.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div[2]/div/div/div/div/div/div[4]/form/div[7]/button")).Click();
            Thread.Sleep(2000);// Ngủ 2s
            if (driver_4_Hau.FindElements(By.XPath("/html/body/div[1]/div/div[1]/div[2]/div/div/div/div/div/div[4]/form/div[4]/fieldset/span/div[2]/div/span/div"))
                .Count > 0)
            {
                Console.WriteLine("TC2_DangNhap_4_Hau: Đăng nhập không thành công.");
                IWebElement emailField_4_Hau = driver_4_Hau.FindElement(By.Id("email"));
                emailField_4_Hau.Click(); // Đảm bảo trường email được focus
                emailField_4_Hau.SendKeys(OpenQA.Selenium.Keys.Control + "a"); // Chọn tất cả nội dung
                emailField_4_Hau.SendKeys(OpenQA.Selenium.Keys.Delete); // Xóa nội dung đã chọn
            }
            else
            {
                Console.WriteLine("TC2_DangNhap_4_Hau: Đăng nhập thành công.");
                return;
            }

            // TC3_DangNhap_4_Hau: Chỉ điền Password
            Thread.Sleep(5000); // Ngủ 5s
            driver_4_Hau.FindElement(By.Name("password")).SendKeys(password_4_Hau);
            driver_4_Hau.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div[2]/div/div/div/div/div/div[4]/form/div[7]/button")).Click();
            Thread.Sleep(2000); // Ngủ 2s
            if (driver_4_Hau.FindElements(
                By.XPath("/html/body/div[1]/div/div[1]/div[2]/div/div/div/div/div/div[4]/form/div[2]/fieldset/span/div[2]/div/span/div/div/div[2]"))
                .Count > 0)
            {
                Console.WriteLine("TC3_DangNhap_4_Hau: Đăng nhập không thành công.");
                IWebElement passField_4_Hau = driver_4_Hau.FindElement(By.Name("password"));
                passField_4_Hau.Click(); // Đảm bảo trường email được focus
                passField_4_Hau.SendKeys(OpenQA.Selenium.Keys.Control + "a"); // Chọn tất cả nội dung
                passField_4_Hau.SendKeys(OpenQA.Selenium.Keys.Delete); // Xóa nội dung đã chọn
            }
            else
            {
                Console.WriteLine("TC3_DangNhap_4_Hau: Đăng nhập thành công.");
                return;
            }

            // TC4_DangNhap_4_Hau: Điền thông tin Email và Password
            Thread.Sleep(5000); // Ngủ 5s
            driver_4_Hau.FindElement(By.Id("email")).SendKeys(email_4_Hau);
            driver_4_Hau.FindElement(By.Name("password")).SendKeys(password_4_Hau);
            driver_4_Hau.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div[2]/div/div/div/div/div/div[4]/form/div[7]/button")).Click();
            Thread.Sleep(2000);
            if (driver_4_Hau.FindElements(By.XPath("/html/body/div[1]/div/div[1]/div[2]/div/div/div/div/div/div[4]/form/div[4]/fieldset/span/div[2]"))
                .Count <= 0
                && driver_4_Hau.FindElements(By.XPath("/html/body/div[1]/div/div[1]/div[2]/div/div/div/div/div/div[4]/form/div[2]/fieldset/span/div[2]/div/span/div/div/div[2]"))
                .Count <= 0)
            {
                Console.WriteLine("TC4_DangNhap_4_Hau: Đăng nhập thành công!");
            }
            else
            {
                Console.WriteLine("TC4_DangNhap_4_Hau: Đăng nhập thất bại...!");
            }
        }

        private void btn_NhanTin_4_Hau_Click(object sender, EventArgs e)
        {
            // Đóng màn hình đen 
            ChromeDriverService chrome_4_Hau = ChromeDriverService.CreateDefaultService();
            chrome_4_Hau.HideCommandPromptWindow = true;

            // Điều hướng trình duyệt
            IWebDriver driver_4_Hau = new ChromeDriver(chrome_4_Hau);
            driver_4_Hau.Navigate().GoToUrl("https://www.pinterest.com/");

            // Ngủ 1,5s
            Thread.Sleep(1500);

            try
            {
                // Click vào button Login để mở Form
                driver_4_Hau.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div/div/div/div[1]/div/div[2]/div[2]/button")).Click();

                Thread.Sleep(5000); // Ngủ 5s
                driver_4_Hau.FindElement(By.Id("email")).SendKeys(email_4_Hau);
                driver_4_Hau.FindElement(By.Name("password")).SendKeys(password_4_Hau);
                driver_4_Hau.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div[2]/div/div/div/div/div/div[4]/form/div[7]/button")).Click();
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            // Ngủ 5s
            Thread.Sleep(5000);

            // Click vào button mở hộp thoại trò chuyện
            driver_4_Hau.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div[1]/div[2]/div/div/div[2]/div/div/div/div[5]/div[3]/div/div/div/div/div"))
                .Click();

            // Sleep 1s
            Thread.Sleep(1000);

            // Vào box chat
            driver_4_Hau.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div[2]/div[2]/div/div/div/div[2]/div[5]/div/div/ul/div"))
                .Click();

            Thread.Sleep(3000);

            // TC1_NhanTin_4_Hau: Không soạn tin nhắn và gửi
            string content1_4_Hau = "";
            driver_4_Hau.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div[2]/div[2]/div/div/div[3]/div/div/div/div[1]/div/div/textarea"))
                .SendKeys(content1_4_Hau);
            Thread.Sleep(500);
            IWebElement check_button1_4_Hau = driver_4_Hau.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div[2]/div[2]/div/div/div[3]/div/div/div/div[2]/button"));
            // Lấy giá trị của 'aria-label'
            string ariaLabel1_4_Hau = check_button1_4_Hau.GetAttribute("aria-label");
            if (ariaLabel1_4_Hau != "Gửi tin nhắn cho cuộc trò chuyện")
            {
                check_button1_4_Hau.Click();
                Console.WriteLine("TC1_NhanTin_4_Hau: Không nhập text chỉ gửi icon - FAIL");
            }
            else
            {
                Console.WriteLine("Không nhập nội dung tin nhắn!!!");
            }

            Thread.Sleep(6000);

            // TC2_NhanTin_4_Hau: Soạn tin nhắn và gửi
            string content_4_Hau = "Hello: Test Case 2 có nhập nội dung";
            driver_4_Hau.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div[2]/div[2]/div/div/div[3]/div/div/div/div[1]/div/div/textarea"))
                .SendKeys(content_4_Hau);
            Thread.Sleep(500);
            IWebElement check_button_4_Hau = driver_4_Hau.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div[2]/div[2]/div/div/div[3]/div/div/div/div[2]/button"));
            // Lấy giá trị của 'aria-label'
            string ariaLabel_4_Hau = check_button_4_Hau.GetAttribute("aria-label");
            if (ariaLabel_4_Hau == "Gửi tin nhắn cho cuộc trò chuyện")
            {
                check_button_4_Hau.Click();
                Console.WriteLine("TC2_NhanTin_4_Hau: Gửi nội dung text mong muốn thành công - PASS");
            }
            else
            {
                Console.WriteLine("Không nhập nội dung tin nhắn!!!");
            }
        }
    }
}

