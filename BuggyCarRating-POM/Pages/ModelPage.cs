using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuggyCarRating_POM.Pages
{
    public class ModelPage
    {
        private IWebDriver Driver;

        public ModelPage(IWebDriver driver) => Driver = driver;


        //Element Locators
        IWebElement voteButton => Driver.FindElement(By.XPath("//button[contains(text(), 'Vote!')]"));
        IWebElement commentTextBox => Driver.FindElement(By.XPath("//textarea[@id='comment']"));
        IWebElement voteCount => Driver.FindElement(By.XPath("//h4[contains(text(), 'Votes: ')]/strong"));
        IWebElement voteThanksMessage => Driver.FindElement(By.XPath("//p[contains(@class, 'card-text') and text()='Thank you for your vote!']"));
        IWebElement commentTable => Driver.FindElement(By.TagName("table"));


        //Click Methods
        public void ClickVotePage() => voteButton.Click();


        //Input Methods
        public void InputTextCommentPage(string comment) => commentTextBox.SendKeys(comment);
        

        //Get Methods
        public string GetVoteCount()
        {
            return voteCount.Text;
        }


        //Validate Methods
        public bool ValidateVoteThanksMessage() => voteThanksMessage.Displayed;
        public bool ValidateCommentDisplayed(string comment)
        {
            var commentTableRecords = commentTable.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
            //Goes through each comment, scrolls through for only comments made today, if comment found, return true
            foreach (IWebElement element in commentTableRecords)
            {
                //Make sure to only check todays comments
                if (element.Text.Contains(DateTime.Now.ToString("MMM dd, yyyy")))
                {
                    if (element.Text.Contains(comment))
                    {
                        return true;
                    }
                }
                else return false;
            }
            return false;
        }
    }
}
