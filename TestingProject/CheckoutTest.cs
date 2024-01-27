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
    public class C_CheckoutTest
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
        public void TestCase1() //checkout a product
        {
            //Login
            IWebElement email = driver.FindElement(By.CssSelector("#user-name"));
            IWebElement password = driver.FindElement(By.CssSelector("#password"));
            IWebElement submitBtn = driver.FindElement(By.CssSelector("#login-button"));
            email.SendKeys("standard_user");
            password.SendKeys("secret_sauce");
            submitBtn.Click();
            Thread.Sleep(2000);

            //Add a product
            IWebElement productOne = driver.FindElement(By.CssSelector("#add-to-cart-sauce-labs-backpack"));
            productOne.Click();
            IWebElement cartBadge = driver.FindElement(By.CssSelector("span.shopping_cart_badge"));
            //string verBdg = cartBadge.Text;
            //Console.WriteLine($"Cart Badge : {verBdg}");
            string verBdgQty = cartBadge.Text;
            Assert.IsTrue(verBdgQty.Contains("1"), $"Expected '1', but found '{verBdgQty}'");

            //Check cart page
            IWebElement cart = driver.FindElement(By.CssSelector("a.shopping_cart_link"));
            cart.Click();

            IWebElement items = driver.FindElement(By.CssSelector("div.cart_item_label"));
            string verItemsName = items.Text;
            Assert.IsTrue(verItemsName.Contains("Sauce Labs Backpack"), $"Expected '1', but found '{verItemsName}'");

            IWebElement qty = driver.FindElement(By.CssSelector("div.cart_quantity"));
            string verQty = qty.Text;
            Assert.IsTrue(verQty.Contains("1"), $"Expected '1', but found '{verQty}'");
            //string verQty = qty.Text;
            //Console.WriteLine($"Total Qty : {verQty}");

            IWebElement price = driver.FindElement(By.CssSelector("div.inventory_item_price"));
            string verPrice = price.Text;
            Assert.IsTrue(verPrice.Contains("29.99"), $"Expected '29.99', but found '{verPrice}'");

            //Checkout a product
            IWebElement checkout = driver.FindElement(By.CssSelector("#checkout"));
            checkout.Click();

            IWebElement firstName = driver.FindElement(By.CssSelector("#first-name"));
            IWebElement lastName = driver.FindElement(By.CssSelector("#last-name"));
            IWebElement postalCode = driver.FindElement(By.CssSelector("#postal-code"));
            IWebElement ctnuBtn = driver.FindElement(By.CssSelector("#continue"));
            firstName.SendKeys("Zuhair");
            lastName.SendKeys("Muhammad");
            postalCode.SendKeys("121272");
            ctnuBtn.Click();

            IWebElement coPageTitle = driver.FindElement(By.CssSelector("span.title"));
            string verTitle = coPageTitle.Text;
            Assert.IsTrue(verTitle.Contains("Checkout: Overview"), $"Expected 'Checkout: Overview', but found '{verTitle}' ");
            Assert.IsTrue(verItemsName.Contains("Sauce Labs Backpack"), $"Expected 'Sauce Labs Backpack', but found '{verItemsName}'");

            IWebElement finishBtn = driver.FindElement(By.CssSelector("#finish"));
            finishBtn.Click();

            IWebElement endPageTitle = driver.FindElement(By.CssSelector("span.title"));
            IWebElement successOrder = driver.FindElement(By.CssSelector("#checkout_complete_container"));
            bool successMsg = successOrder.Displayed;
            string verSuccessMsg = successOrder.Text;
            string verEndPageTitle = endPageTitle.Text;
            Assert.IsTrue(verEndPageTitle.Contains("Checkout: Complete!"), $"Expected 'Checkout: Complete', but found '{verEndPageTitle}' ");
            Assert.IsTrue(successMsg, "Success Message not displayed");
            Assert.IsTrue(verSuccessMsg.Contains("Thank you for your order!"), $"Expected '1', but found '{verSuccessMsg}'");

        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}
