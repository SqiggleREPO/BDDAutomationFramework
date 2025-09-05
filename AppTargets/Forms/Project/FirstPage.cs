using OpenQA.Selenium;
using Core;

namespace AppTargets.Forms
{
    public class FirstPage : FormBase
    {
        public FirstPage() : base(By.Id("First"), "First page")
        {

            Elements.Add("username", By.XPath("/html/body/div/div/div[2]/div[1]/div/div/form/div[1]/input"));
            Elements.Add("password", By.XPath("//input[@type='password']"));
            Elements.Add("swaglabs", By.XPath("//div[@class='Swag Labs']"));
            Elements.Add("login", By.XPath("//input[@id='login-button']"));
            Elements.Add("dashboard", By.Id("dashboard-container"));
            
            // Also populate XPath dictionary if needed
            ElementXPath.Add("ID", "//mat-card-title[contains(text(),'Maintain Non-Primary Charge Types')]");
            ElementXPath.Add("username", "//input[@id='username']");
            ElementXPath.Add("password", "//input[@id='password']");
            ElementXPath.Add("loginbutton", "//button[contains(text(),'Login')]");
        }
    }
}