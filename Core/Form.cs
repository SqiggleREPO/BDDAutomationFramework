using OpenQA.Selenium;

namespace Core
{
    public abstract class Form
    {
        public By Locator { get; }
        public string Name { get; }

        protected Form(By locator, string name)
        {
            Locator = locator;
            Name = name;
        }
    }
}