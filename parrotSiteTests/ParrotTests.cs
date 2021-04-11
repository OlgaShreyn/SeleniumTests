using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace ParrotSiteTests
{
    public class ParrotTests
    {
        public ChromeDriver driver;

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);
        }

        private readonly By emailInputLocator = By.Name("email");
        private readonly By buttonLocator = By.Id("sendMe");
        private readonly By emailResultLocator = By.ClassName("your-email");
        private readonly By textResultLocator = By.ClassName("result-text");
        private readonly By linkLocator = By.Id("anotherEmail");
        private readonly By radioButtonMaleLocator = By.Id("boy");
        private readonly By radioButtonFemaleLocator = By.Id("girl");
        private readonly By resultTextBlockLocator = By.Id("resultTextBlock");
        private readonly By errorTextLocator = By.ClassName("form-error");
        private readonly String checkingSite = "https://qa-course.kontur.host/selenium-practice";


        [Test]
        public void ParrotSite_ChooseFillFormateWithEmail_Success()
        {
            var expectedEmail = "test@mail.ru";
            driver.Navigate().GoToUrl(checkingSite);
            driver.FindElement(emailInputLocator).SendKeys(expectedEmail);
            driver.FindElement(buttonLocator).Click();
            Assert.AreEqual(expectedEmail, driver.FindElement(emailResultLocator).Text, "Send to other emal");
        }

        [Test]
        public void ParrotSite_ChooseWoman_Success()
        {
            var expectedEmail = "test@mail.ru";
            driver.Navigate().GoToUrl(checkingSite);
            driver.FindElement(emailInputLocator).SendKeys(expectedEmail);
            driver.FindElement(radioButtonFemaleLocator).Click();
            driver.FindElement(buttonLocator).Click();
            Assert.IsTrue(driver.FindElement(textResultLocator).Text.Contains("девочки"), "Name for other sex");
        }

        [Test]
        public void ParrotSite_ChooseMale_Success()
        {
            var expectedEmail = "test@mail.ru";
            driver.Navigate().GoToUrl(checkingSite);
            driver.FindElement(emailInputLocator).SendKeys(expectedEmail);
            driver.FindElement(radioButtonMaleLocator).Click();
            driver.FindElement(buttonLocator).Click();
            Assert.IsTrue(driver.FindElement(textResultLocator).Text.Contains("мальчика"), "Name for other sex");
        }

        [Test]
        public void ParrotSite_ClickAnotherEmail_EmailInputIsEmpty()
        {
            var expectedEmail = "test@mail.ru";
            driver.Navigate().GoToUrl(checkingSite);
            driver.FindElement(emailInputLocator).SendKeys(expectedEmail);
            driver.FindElement(buttonLocator).Click();
            driver.FindElement(linkLocator).Click();
            Assert.AreEqual(string.Empty, driver.FindElement(emailInputLocator).Text, "Not empty field after click");
            Assert.IsFalse(driver.FindElement(linkLocator).Displayed, "Link is visible");
        }

        [Test]
        public void ParrotSite_EmptyEmail()
        {
            driver.Navigate().GoToUrl(checkingSite);
            driver.FindElement(buttonLocator).Click();
            Assert.IsTrue(driver.FindElements(resultTextBlockLocator).Count == 0, "Accept empty email");
            Assert.AreEqual("Введите email", driver.FindElement(errorTextLocator).Text);
        }

        [Test]
        public void ParrotSite_IncorrectEmail()
        {
            driver.Navigate().GoToUrl(checkingSite);
            var incorrectEmail = "testmail.ru";
            driver.FindElement(emailInputLocator).SendKeys(incorrectEmail);
            driver.FindElement(buttonLocator).Click();
            Assert.IsTrue(driver.FindElements(resultTextBlockLocator).Count == 0, "Accept incorrect email");
            Assert.AreEqual("Некорректный email", driver.FindElement(errorTextLocator).Text);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}

