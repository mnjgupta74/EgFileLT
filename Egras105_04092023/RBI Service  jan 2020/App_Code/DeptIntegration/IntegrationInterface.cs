using System.Collections.Generic;
/// <summary>
/// Summary description for IntegrationInterface
/// </summary>
public interface IntegrationInterface
{
    Dictionary<string, IntegrationProp> Data { get; set; }
    int ManualBanksType { get; set; }
    bool IntegrationInterface();
    System.Int64 InsertChallan();

}