using System.Collections.Generic;

namespace Browserstack.Interfaces
{
    /// <summary>
    /// This interface will be used to retrieve expected results from db or excel
    /// </summary>
    public interface IPageExpedResults
    {
       string GetExpectedPageTitle();
       string GetExpectedUri();
       Dictionary<string, List<KeyValuePair<string, string>>> GetExpectedValuesXLS();
    }
}