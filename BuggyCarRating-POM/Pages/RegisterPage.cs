using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuggyCarRating_POM.Pages
{
    public class RegisterPage
    {
        private IWebDriver Driver;

        public RegisterPage(IWebDriver driver) => Driver = driver;


        //Element Locators
        IWebElement loginFieldPage => Driver.FindElement(By.Id("username"));
        IWebElement firstNameField => Driver.FindElement(By.Id("firstName"));
        IWebElement lastNameField => Driver.FindElement(By.Id("lastName"));
        IWebElement passwordField => Driver.FindElement(By.Id("password"));
        IWebElement confirmPasswordField => Driver.FindElement(By.Id("confirmPassword"));
        IWebElement registerFieldPage => Driver.FindElement(By.XPath("//button[contains(@type, 'submit') and text()='Register']"));


        //Click Methods
        public void ClickRegisterOnPage() => registerFieldPage.Click();


        //Input Methods
        public void InputTextLoginPage(string login) => loginFieldPage.SendKeys(login);
        public void InputTextFirstNamePage(string firstName) => firstNameField.SendKeys(firstName);
        public void InputTextLastNamePage(string lastName) => lastNameField.SendKeys(lastName);
        public void InputTextPasswordPage(string password) => passwordField.SendKeys(password);
        public void InputTextConfirmPasswordPage(string confirmPassword) => confirmPasswordField.SendKeys(confirmPassword);
        public void InputMatchingPasswords(string password)
        {
            passwordField.SendKeys(password);
            confirmPasswordField.SendKeys(password);
        }


        //Clear Methods
        public void ClearTextLoginPage() => loginFieldPage.Clear();
        public void ClearTextFirstNamePage() => firstNameField.Clear();
        public void ClearTextLastNamePage() => lastNameField.Clear();
        public void ClearTextPasswordPage() => passwordField.Clear();
        public void ClearTextConfirmPasswordPage() => confirmPasswordField.Clear();
        public void ClearMatchingPasswords()
        {
            passwordField.Clear();
            confirmPasswordField.Clear();
        }


        //Get Methods
        public bool GetFirstNameTooLongError()
        {
            return Driver.FindElement(By.Id("username")).FindElement(By.XPath("//parent::div/div[contains(text(), 'Login cannot be more than 50 characters long')]")).Displayed;
        }
        public bool GetPasswordTooShortError()
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0.3);
            if(Driver.FindElement(By.TagName("Form")).FindElements(By.XPath("//div[contains(text(), 'Password not long enough') or contains(text(), 'minimum field size of 6, SignUpInput.Password.')]")).Count != 0)
            {
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                return true;
            }
            return false;
        }
        public bool GetPasswordUppercaseError()
        {
            return Driver.FindElement(By.TagName("Form")).FindElements(By.XPath("//div[contains(text(), 'Password must have uppercase characters')]")).Count != 0;
        }
        public bool GetPasswordLowercaseError()
        {
            return Driver.FindElement(By.TagName("Form")).FindElements(By.XPath("//div[contains(text(), 'Password must have lowercase characters')]")).Count != 0;
        }
        public bool GetPasswordNumericError()
        {
            return Driver.FindElement(By.TagName("Form")).FindElements(By.XPath("//div[contains(text(), 'Password must have numeric characters')]")).Count != 0;
        }
        public bool GetPasswordSymbolError()
        {
            return Driver.FindElement(By.TagName("Form")).FindElements(By.XPath("//div[contains(text(), 'Password must have symbol characters')]")).Count != 0;
        }
        public bool GetUserExistsError()
        {
            return Driver.FindElement(By.TagName("Form")).FindElements(By.XPath("//div[contains(text(), 'User already exists')]")).Count != 0;
        }
        public string GetUsername() => loginFieldPage.GetAttribute("value");
        public bool GetRegistrationSuccessfulMessage()
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0.3);
            if (Driver.FindElement(By.TagName("Form")).FindElements(By.XPath("//div[contains(text(), 'Registration is successful')]")).Count != 0)
            {
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                return true;
            }
            return false;
        }
    }
}
