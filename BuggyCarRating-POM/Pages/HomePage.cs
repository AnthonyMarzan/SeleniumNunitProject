using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BuggyCarRating_POM.Pages
{
    public class HomePage
    {
        private IWebDriver Driver;

        public HomePage(IWebDriver driver) => Driver = driver;


        //Element Locators
        IWebElement buggyRatingMenu => Driver.FindElement(By.XPath("//a[contains(text(), 'Buggy Rating')]"));
        IWebElement loginTextMenu => Driver.FindElement(By.Name("login"));
        IWebElement passwordTextMenu => Driver.FindElement(By.Name("password"));
        IWebElement loginButtonMenu => Driver.FindElement(By.XPath("//*[@class='form-inline']/button[@type='submit']"));
        IWebElement registerButtonMenu => Driver.FindElement(By.XPath("//form[@class='form-inline']/a[contains(text(), 'Register')]"));
        IWebElement displayNameMenu => Driver.FindElement(By.TagName("nav")).FindElement(By.XPath("//li[@class='nav-item']/span"));
        IWebElement logoutButtonMenu => Driver.FindElement(By.XPath("//a[contains(text(),'Logout')]"));
        IWebElement popularMakelink => Driver.FindElement(By.XPath("//h2[contains(text(), 'Popular Make')]//parent::div//a"));
        IWebElement overallRatingLink=> Driver.FindElement(By.XPath("//h2[contains(text(), 'Overall Rating')]//parent::div//a"));
        IWebElement popularMakeVotes => Driver.FindElement(By.XPath("//h2[contains(text(), 'Popular Make')]//parent::div/div/h3/small"));
        IWebElement popularModelCar => Driver.FindElement(By.XPath("//h2[contains(text(), 'Popular Model')]//parent::div/div/h3"));
        IWebElement popularModelVotes => Driver.FindElement(By.XPath("//h2[contains(text(), 'Popular Model')]//parent::div/div/h3/small"));


        //Click Methods
        public void ClickLoginMenu() => loginButtonMenu.Click();
        public void ClickLogoutMenu() => logoutButtonMenu.Click();
        public void ClickRegisterMenu() => registerButtonMenu.Click();
        public void ClickPopularMake() => popularMakelink.Click();
        public void ClickOverallRating() => overallRatingLink.Click();
        public void ClickBuggyRatingOnMenu() => buggyRatingMenu.Click();


        //Input Methods
        public void InputTextLoginMenu(string username) => loginTextMenu.SendKeys(username);
        public void InputTextPasswordMenu(string password) => passwordTextMenu.SendKeys(password);


        //Get Methods
        public string GetPopularMakeVoteCount() => popularMakeVotes.Text;
        public string GetPopularModelVoteCount() => popularModelVotes.Text;
        public string GetPopularModelCar() => popularModelCar.Text;


        //Validate Methods
        public void ValidateDisplayName(string firstName)
        {
            Assert.That(displayNameMenu.Text.Equals(firstName));
        }
        public void ValidateLogout() => Assert.IsTrue(loginButtonMenu.Displayed);


    }
}
