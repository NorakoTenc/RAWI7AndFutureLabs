using ClosedXML.Excel;

namespace RAWI7AndFutureLabs.Services.Data
{
    public class DataService : IDataService
    {
        public int GetIntegerData()
        {
            return 2147483647;
        }

        public string GetTextData()
        {
            return "Ora Ora Ora Ora Ora Ora Ora Ora Ora Ora Ora Ora Ora Ora Ora Ora Ora Ora Ora Ora";
        }

        public byte[] GenerateExcelData()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Data");
                worksheet.Cell("A1").Value = "Kono";
                worksheet.Cell("A2").Value = "Giorno";
                worksheet.Cell("A3").Value = "Giovanna";
                worksheet.Cell("A4").Value = "Yume";
                worksheet.Cell("A5").Value = "Ga";
                worksheet.Cell("A6").Value = "Aru";

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}