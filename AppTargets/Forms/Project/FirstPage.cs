using OpenQA.Selenium;
using Core;

namespace AppTargets.Forms.Project
{
    public class FirstPage : FormBase
    {
        public FirstPage() : base(By.Id("First"), "First page")
        {
            /// Add Elements - Element Identifiers are LOWER case (apart from ID)
            ///
            Elements.Add("ID", By.XPath("//mat-card-title[contains(text(),'Maintain Non-Primary Charge Types')]"));
            Elements.Add("username", By.Id("username"));
            Elements.Add("password", By.Id("password"));
            Elements.Add("loginbutton", By.XPath("//button[contains(text(),'Login')]"));
            Elements.Add("errormessage", By.ClassName("error-message"));
            Elements.Add("dashboard", By.Id("dashboard-container"));
            
            // Also populate XPath dictionary if needed
            ElementXPath.Add("ID", "//mat-card-title[contains(text(),'Maintain Non-Primary Charge Types')]");
            ElementXPath.Add("username", "//input[@id='username']");
            ElementXPath.Add("password", "//input[@id='password']");
            ElementXPath.Add("loginbutton", "//button[contains(text(),'Login')]");
        }
    }
}