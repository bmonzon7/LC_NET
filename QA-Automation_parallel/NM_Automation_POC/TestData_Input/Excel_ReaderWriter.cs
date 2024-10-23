using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text.RegularExpressions;
using Browserstack.Interfaces;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

///<summary>
///This reads an Excel workbook - single sheet now
///it uses the productId (i.e., Col1.Value as the Key and stores the Value as a kvp list)
/// DDDDD-----DDDDD
/// </summary>
/// 
namespace Browserstack
{

    public class Excel_ReaderWriter : IExcelReadWriter
    {
        protected string _fileName = string.Empty;

        public string fileName { get; set; }

        //Default constructror
        public Excel_ReaderWriter()
        {
        }

        public void ReadXLS(string fileName)
        {
            //var fileName = @"C:\Temp\TestDataInputs.xlsx";
            this._fileName = fileName;

            using SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(_fileName, false);
            WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;

            //Get sheet from excel
            var sheets = workbookPart.Workbook.Descendants<Sheet>();

            //First sheet from excel
            Sheet sheet = sheets.FirstOrDefault();

            var worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
            var rows = worksheetPart.Worksheet.Descendants<Row>().ToList();

            //Get all data rows from sheet
            Row headerRow = rows.First();
            var headerCells = headerRow.Elements<Cell>();
            int totalColumns = headerCells.Count();


            List<string> lstHeaders = new List<string>();
            foreach (var value in headerCells)
            {
                var stringId = Convert.ToInt32(value.InnerText);
                lstHeaders.Add(workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(stringId).InnerText);
            }
            // Remove the header row
            rows.RemoveAt(0);

            //Dictionary to map row data into key value pair
            Dictionary<string, List<KeyValuePair<string, string>>> dict = new Dictionary<string, List<KeyValuePair<string, string>>>();

            var testID = string.Empty;

            //Iterate through the rows
            foreach (Row r in rows)
            {

                List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>();

                //Iterate to through all cells in current row
                foreach (Cell c in r.Elements<Cell>())
                {
                    if (c.DataType != null && c.DataType == CellValues.SharedString)
                    {
                        var stringId = Convert.ToInt32(c.InnerText);
                        string val = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(stringId).InnerText;

                        //Find cell index and map to each cell also add in key value pair
                        switch (GetColumnIndex(c.CellReference))
                        {
                            case 1:
                                testID = val;
                                break;

                            case 2:
                                keyValuePairs.Add(new KeyValuePair<string, string>("TestName", val));
                                break;

                            case 3:
                                keyValuePairs.Add(new KeyValuePair<string, string>("Component", val));
                                break;

                            case 4:
                                keyValuePairs.Add(new KeyValuePair<string, string>("URL", val));
                                break;

                            case 5:
                                keyValuePairs.Add(new KeyValuePair<string, string>("ExpectedText", val));
                                break;

                            case 6:
                                keyValuePairs.Add(new KeyValuePair<string, string>("Status", val));
                                dict.Add(testID, keyValuePairs);
                                break;
                        }
                    }
                }
            }
            foreach (KeyValuePair<string, List<KeyValuePair<string, string>>> kvp in dict)
            {
                Console.WriteLine("TestId is: " /*Key*/ + kvp.Key.ToString());

                List<KeyValuePair<string, string>> valueList = new List<KeyValuePair<string, string>>();
                valueList = kvp.Value;
                for (int i = 0; i < kvp.Value.Count; i++)
                {

                    Console.WriteLine(valueList[i].Key.ToString());
                    Console.WriteLine(valueList[i].Value.ToString());
                }
            }
        }
        private static int? GetColumnIndex(string cellReference)
        {
            if (string.IsNullOrEmpty(cellReference))
            {
                return null;
            }

            string columnReference = Regex.Replace(cellReference.ToUpper(), @"[\d]", string.Empty);
            /*
             * //stackoverflow.com/questions/28875815/get-the-column-index-of-a-cell-in-excel-using-openxml-c-sharp
             * */

            int columnNumber = -1;
            int multiplier = 1;

            foreach (char c in columnReference.ToCharArray().Reverse())
            {
                columnNumber += multiplier * ((int)c - 64);
                multiplier = multiplier * 26;
            }
            return columnNumber + 1;
        }

        public void WriteXLS(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}