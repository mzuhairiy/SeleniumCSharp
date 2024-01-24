using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TestingProject
{
    [TestFixture]
    public class LoginTest
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            driver.Manage().Window.Maximize();
            Thread.Sleep(2000);
        }

        [Test]
        public void TestCase1()
        {
            //Verify web is loaded.
            IWebElement logo = driver.FindElement(By.CssSelector(".login_logo"));
            bool verLogo = logo.Displayed;
            Assert.IsTrue(verLogo, "Logo is not displayed.");
            Thread.Sleep(1000);

            IWebElement email = driver.FindElement(By.CssSelector("#user-name"));
            IWebElement password = driver.FindElement(By.CssSelector("#password"));
            IWebElement submitBtn = driver.FindElement(By.CssSelector("#login-button"));

            email.SendKeys("standard_user");
            password.SendKeys("secret_sauce");
            submitBtn.Click();
            Thread.Sleep(2000);

            IWebElement homepageTitle = driver.FindElement(By.CssSelector(".title"));
            string titleText = homepageTitle.Text;
            Assert.IsTrue(titleText.Contains("Products"), $"Expected 'Products', but found '{titleText}'");
        }

        [Test]
        public void TestCase2()
        {
            IWebElement logo = driver.FindElement(By.CssSelector(".login_logo"));
            bool verLogo = logo.Displayed;
            Assert.IsTrue(verLogo, "Logo is not displayed.");
            Thread.Sleep(1000);

            IWebElement email = driver.FindElement(By.CssSelector("#user-name"));
            IWebElement password = driver.FindElement(By.CssSelector("#password"));
            IWebElement submitBtn = driver.FindElement(By.CssSelector("#login-button"));

            email.SendKeys("standard_user");
            password.SendKeys("secretsup");
            submitBtn.Click();
            Thread.Sleep(2000);

            IWebElement errorMsg = driver.FindElement(By.CssSelector("h3[data-test='error']"));
            bool verErrMsg = errorMsg.Displayed;
            Assert.IsTrue(verErrMsg, "Error is not displayed");
        }

        [Test]
        public void TestCase3()
        {
            IWebElement logo = driver.FindElement(By.CssSelector(".login_logo"));
            bool verLogo = logo.Displayed;
            Assert.IsTrue(verLogo, "Logo is not displayed.");
            Thread.Sleep(1000);

            IWebElement email = driver.FindElement(By.CssSelector("#user-name"));
            IWebElement password = driver.FindElement(By.CssSelector("#password"));
            IWebElement submitBtn = driver.FindElement(By.CssSelector("#login-button"));

            email.SendKeys("standard_user");
            password.SendKeys("secret_sauce");
            submitBtn.Click();
            Thread.Sleep(2000);

            ReadOnlyCollection<IWebElement> productList = driver.FindElements(By.CssSelector("div.inventory_container div.inventory_list > div.inventory_item"));
            List<string> productNames = new List<string>();
            //Console.WriteLine($"Number of products found: {productList.Count}");

            foreach (IWebElement product in productList)
            {
                IWebElement productName = product.FindElement(By.CssSelector("div.inventory_item_name"));
                bool verPrdName = productName.Displayed;
                Assert.IsTrue(verPrdName, "Product name is not displayed");
                //string prdName = productName.Text;
                //productNames.Add(prdName);
                //Console.WriteLine($"Product Name: {prdName}");
                //Console.WriteLine($"Product Name for product {productList.ToList().IndexOf(product) + 1}: {prdName}");

                //IWebElement productImage = product.FindElement(By.CssSelector("div.inventory_item_img"));
                //string imgAlt = productImage.GetAttribute("alt");
                //Console.WriteLine("Img ALt Text: " + imgAlt);
                
            }
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}
