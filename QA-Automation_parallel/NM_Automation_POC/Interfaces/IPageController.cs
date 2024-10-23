using OpenQA.Selenium;

namespace Browserstack.Interfaces
{
    public interface IPageController
    {
        string GetActualPageTitle();
        string GetActualUri();

        

        //void HoverOverElement(IWebElement hoverElementName);
        //string GetElementCssStyleAfterHover(IWebElement elementToCheck, IWebElement element);      
          

    }
}