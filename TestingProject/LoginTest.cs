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

            IWebElement logo = driver.FindElement(By.CssSelector(".login_logo"));
            bool verLogo = logo.Displayed;
            Assert.IsTrue(verLogo, "Logo is not displayed.");
            Thread.Sleep(1000);
        }

        [Test]
        public void TestCase1() //Login with valid credential
        {
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
        public void TestCase2() //Login with invalid credential
        {
            IWebElement email = driver.FindElement(By.CssSelector("#user-name"));
            IWebElement password = driver.FindElement(By.CssSelector("#password"));
            IWebElement submitBtn = driver.FindElement(By.CssSelector("#login-button"));

            email.SendKeys("standard_user");
            password.SendKeys("secretsup");
            submitBtn.Click();
            Thread.Sleep(2000);

            IWebElement errorMsg = driver.FindElement(By.CssSelector("h3[data-test='error']"));
            string verErrMsg = errorMsg.Text;
            Assert.IsTrue(verErrMsg.Contains("Username and password do not match any user in this service"), $"Expected 'Products', but found '{verErrMsg}'");
        }

        [Test]
        public void TestCase3() //Login with empty username
        {
            IWebElement email = driver.FindElement(By.CssSelector("#user-name"));
            IWebElement password = driver.FindElement(By.CssSelector("#password"));
            IWebElement submitBtn = driver.FindElement(By.CssSelector("#login-button"));

            email.SendKeys("");
            password.SendKeys("secretsup");
            submitBtn.Click();
            Thread.Sleep(1000);

            IWebElement errorMsg = driver.FindElement(By.CssSelector("h3[data-test='error']"));
            string verErrMsg = errorMsg.Text;
            Assert.IsTrue(verErrMsg.Contains("Username is required"), $"Expected 'Products', but found '{verErrMsg}'");

        }

        [Test]
        public void TestCase4() //Login with empty password
        {
            IWebElement email = driver.FindElement(By.CssSelector("#user-name"));
            IWebElement password = driver.FindElement(By.CssSelector("#password"));
            IWebElement submitBtn = driver.FindElement(By.CssSelector("#login-button"));

            email.SendKeys("standard_user");
            password.SendKeys("");
            submitBtn.Click();
            Thread.Sleep(1000);

            IWebElement errorMsg = driver.FindElement(By.CssSelector("h3[data-test='error']"));
            string verErrMsg = errorMsg.Text;
            Assert.IsTrue(verErrMsg.Contains("Password is required"), $"Expected 'Products', but found '{verErrMsg}'");

        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}
