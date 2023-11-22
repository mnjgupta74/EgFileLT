using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using System.Data;

public partial class UserControls_CustomTreasury : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DropDownListX dp = new DropDownListX();
        DataTable treasuryData = new DataTable();
        treasuryData = dp.FillTreasury();

        for (int i = 1; i < 41; i++)
        {
            var rows = treasuryData.AsEnumerable().Where(t => t.Field<int>("TGroupCode") == i);

            string group = rows.ElementAtOrDefault(0).Field<string>("TreasuryName"); // ElementAtOrDefault(0).treasuryName.ToString().Trim();
            ddllocation.AddItemGroup(group.Trim());
            foreach (var item in rows)
            {
                ListItem items = new ListItem(item.Field<string>("TreasuryName"), item.Field<string>("TreasuryCode"));
                ddllocation.Items.Add(items);

            }

        }
    }
}