namespace RAWI7AndFutureLabs.Services.Data
{
    public interface IDataService
    {
        int GetIntegerData();
        string GetTextData();
        byte[] GenerateExcelData();
    }
}
