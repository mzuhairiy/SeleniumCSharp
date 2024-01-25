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
    public class ProductTest
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
        public void TestCase1() //Verify all products attribute (name, image and price) are displayed
        {
            IWebElement email = driver.FindElement(By.CssSelector("#user-name"));
            IWebElement password = driver.FindElement(By.CssSelector("#password"));
            IWebElement submitBtn = driver.FindElement(By.CssSelector("#login-button"));

            email.SendKeys("standard_user");
            password.SendKeys("secret_sauce");
            submitBtn.Click();
            Thread.Sleep(2000);

            ReadOnlyCollection<IWebElement> productList = driver.FindElements(By.CssSelector("div.inventory_container div.inventory_list > div.inventory_item"));
            //List<string> productNames = new List<string>();
            Console.WriteLine($"Number of products found: {productList.Count}");

            foreach (IWebElement product in productList)
            {
                IWebElement productName = product.FindElement(By.CssSelector("div.inventory_item_name"));
                bool verPrdName = productName.Displayed;
                Assert.IsTrue(verPrdName, "Product name is not displayed");
                //string prdName = productName.Text;
                //productNames.Add(prdName);
                //Console.WriteLine($"Product Name: {prdName}");
                //Console.WriteLine($"Product Name for product {productList.ToList().IndexOf(product) + 1}: {prdName}");

                IWebElement productImage = product.FindElement(By.CssSelector("img.inventory_item_img"));
                bool verImg = productImage.Displayed;
                Assert.IsTrue(verImg, "Product image is not displayed");
                //string imgAlt = productImage.GetAttribute("alt");
                //Console.WriteLine($"Img ALt Text: {imgAlt}");

                IWebElement productPrice = product.FindElement(By.CssSelector("div.inventory_item_price"));
                bool verPrc = productPrice.Displayed;
                Assert.IsTrue(verPrc, "Product price is not displayed");
                //string price = productPrice.Text;
                //Console.WriteLine($"Price Tag for product {productList.ToList().IndexOf(product) + 1}: {price}");
            }
        }

        [Test]
        public void TestCase2() //add a product to cart
        {
            IWebElement email = driver.FindElement(By.CssSelector("#user-name"));
            IWebElement password = driver.FindElement(By.CssSelector("#password"));
            IWebElement submitBtn = driver.FindElement(By.CssSelector("#login-button"));

            email.SendKeys("standard_user");
            password.SendKeys("secret_sauce");
            submitBtn.Click();
            Thread.Sleep(2000);

            IWebElement productOne = driver.FindElement(By.CssSelector("#add-to-cart-sauce-labs-backpack"));
            productOne.Click();

            IWebElement cartBadge = driver.FindElement(By.CssSelector("span.shopping_cart_badge"));
            //string verBdg = cartBadge.Text;
            //Console.WriteLine($"Cart Badge : {verBdg}");
            string verBdgQty = cartBadge.Text;
            Assert.IsTrue(verBdgQty.Contains("1"), $"Expected '1', but found '{verBdgQty}'");

            IWebElement cart = driver.FindElement(By.CssSelector("a.shopping_cart_link"));
            cart.Click();

            IWebElement qty = driver.FindElement(By.CssSelector("div.cart_quantity"));
            string verQty = qty.Text;
            Assert.IsTrue(verQty.Contains("1"), $"Expected '1', but found '{verQty}'");
            //string verQty = qty.Text;
            //Console.WriteLine($"Total Qty : {verQty}");
        }

        [Test]
        public void TestCase3() //add multiple product to cart
        {
            IWebElement email = driver.FindElement(By.CssSelector("#user-name"));
            IWebElement password = driver.FindElement(By.CssSelector("#password"));
            IWebElement submitBtn = driver.FindElement(By.CssSelector("#login-button"));

            email.SendKeys("standard_user");
            password.SendKeys("secret_sauce");
            submitBtn.Click();
            Thread.Sleep(2000);

            IWebElement productOne = driver.FindElement(By.CssSelector("#add-to-cart-sauce-labs-backpack"));
            productOne.Click();

            IWebElement productTwo = driver.FindElement(By.CssSelector("#add-to-cart-sauce-labs-bike-light"));
            productTwo.Click();

            IWebElement productThree = driver.FindElement(By.CssSelector("#add-to-cart-sauce-labs-bolt-t-shirt"));
            productThree.Click();
            Thread.Sleep(1000);

            IWebElement cartBadge = driver.FindElement(By.CssSelector("span.shopping_cart_badge"));
            string verBdg = cartBadge.Text;
            //Console.WriteLine($"Cart Badge : {verBdg}");
            Assert.IsTrue(verBdg.Contains("3"), $"Expected '3', but found '{verBdg}'");

            IWebElement cart = driver.FindElement(By.CssSelector("a.shopping_cart_link"));
            cart.Click();
            Thread.Sleep(2000);

            ReadOnlyCollection<IWebElement> itemList = driver.FindElements(By.CssSelector("div.cart_list > div.cart_item > div.cart_quantity"));
            //Console.WriteLine($"Number of Item: {itemList.Count}");
            int totalList = itemList.Count;
            Assert.IsTrue(totalList.Equals(3), $"Expected '3', but found {totalList}");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}
