using OpenQA.Selenium;
using System.Collections.Generic;

namespace Core
{
    public abstract class FormBase : Form
    {
        protected FormBase(By locator, string name) : base(locator, name)
        {
            Elements = new Dictionary<string, By>();
            ElementXPath = new Dictionary<string, string>();
        }

        public IDictionary<string, By> Elements { get; }
        public IDictionary<string, string> ElementXPath { get; protected set; }
    }
}