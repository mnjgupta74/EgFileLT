using System.Collections.Generic;
/// <summary>
/// Summary description for DeptIntegrationController
/// </summary>
[System.Serializable]
public class DeptIntegrationController
{
    public Dictionary<string, IntegrationProp> Data { get; set; }
    public int ManualBanksType { get; set; }
    public bool CheckData(IntegrationInterface objIntegrationInterface)
    {
        bool result = objIntegrationInterface.IntegrationInterface();
        Data = objIntegrationInterface.Data;
        ManualBanksType = objIntegrationInterface.ManualBanksType;
        return result;
    }
    public System.Int64 InsertChallan(IntegrationInterface objIntegrationInterface)
    {
        objIntegrationInterface.Data = Data;
        return objIntegrationInterface.InsertChallan();
    }
}