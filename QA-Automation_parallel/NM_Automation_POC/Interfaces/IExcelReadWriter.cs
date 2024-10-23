namespace Browserstack.Interfaces
{

    /// <summary>
    /// Provides 2 properties (fileName, connectionString)
    /// provides 2 methods for reading and writing Excel files
    /// Currently we have only implemented the fileName property and
    /// the ReadXLS() method 
    /// 
    /// ToDo: create property and methods to connect to SQL db
    ///         implement WriteXLS() method and Excel_ReaderWriter class
    /// </summary>
    public interface IExcelReadWriter
    {
        string fileName { get; set; }
        void ReadXLS(string fileName);
        void WriteXLS(string fileName);
    }
}