using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Data;

namespace AutomatedMessagingServices.Classes.SendMessages
{
    public class ClsWhatsapp
    {
        private IWebDriver driver;

        public ClsWhatsapp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--remote-debugging-port=64625");
            options.AddArgument("--user-data-dir=C:/Users/mufad/AppData/Local/Google/Chrome/User Data");
            //options.AddArgument("--profile-directory=Default");
            driver = new ChromeDriver("C:\\Users\\mufad\\Downloads\\chromedriver-win64\\chromedriver-win64", options);
        }

        // Function 1: Loop through DataTable and send messages
        public void SendMessagesFromDataTable(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                string phoneNumber = row["PhoneNumber"].ToString();
                string message = row["message"].ToString();
                SendMessage(phoneNumber, message);
            }
        }

        // Function 2: Convert HTML message to API link format
        public string ConvertHtmlToApiLink(string htmlMessage)
        {
            string apiLink = "https://web.whatsapp.com/send?phone={0}&text=" + Uri.EscapeDataString(htmlMessage);
            return apiLink;
        }

        // Function 3: Send message directly to a phone number
        public void SendMessage(string phoneNumber, string message)
        {
            string url = $"https://web.whatsapp.com/send?phone={phoneNumber}&text={Uri.EscapeDataString(message)}";
            driver.Navigate().GoToUrl(url);
            System.Threading.Thread.Sleep(50000); // Wait for WhatsApp Web to load
            IWebElement sendButton = driver.FindElement(By.XPath("//button[@aria-label='Send']"));
            sendButton.Click();

        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
