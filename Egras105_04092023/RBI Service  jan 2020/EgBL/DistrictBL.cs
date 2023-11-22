using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EgDAL;
using System.Data;
using System.Web.Caching;
using System.Runtime.Caching;
using System.Web;

namespace EgBL
{
    public class DistrictBL
    {

        GenralFunction gf = new GenralFunction();
        public DataTable FillLocation(string DeptCode)
        {
            DataTable dt = new DataTable();
            gf.Filldatatablevalue(null, "EgFillLocation", dt, null);
            DataTable district = dt;
            var cache = MemoryCache.Default;


            switch (DeptCode)
            {
                case "105":
                    //Craete Date    //16/12/2021  District  Prority Wise


                    //900 BIKANER
                    //2600    JODHPUR(CITY)
                    //800 BHILWARA
                    //100 TREASURY OFFICE, AJMER
                    //1000    BUNDI
                    //3000    NAGAUR
                    //500 BARMER
                    //1800    JAIPUR(CITY)
                    //2900    KOTA
                    //2000    JAIPUR(RURAL)
                    //3300    RAJSAMAND
                    //1100    CHITTORGARH
                    //3800    Udaipur City

                    if (cache["T105"] == null)
                    {

                        Dictionary<string, string> districtList = new Dictionary<string, string>();
                        districtList.Add("BIKANER", "09");
                        districtList.Add("JODHPUR(CITY)", "28");
                        districtList.Add("BHILWARA", "08");
                        districtList.Add("BUNDI", "10");
                        districtList.Add("NAGAUR", "30");

                        for (int i = 0; i < districtList.Count; i++)
                        {
                            string tCode = districtList.ElementAt(i).Value;

                            // DataRow[] dr = district.Select("TreasuryCode ='" + tCode + "'");
                            var filterArray = districtList.ElementAt(i).Value.Split(',').Select(s => s.Trim());
                            DataRow[] dr = dt.AsEnumerable()
                                .Where(row => filterArray.All(f => row.Field<string>("TreasuryCode")
                                                               .Split(',')
                                                               .Any(v => v.Trim() == f)))
                                .ToArray();
                            DataRow newRow = district.NewRow();
                            // We "clone" the row
                            newRow.ItemArray = dr[0].ItemArray;
                            // We remove the old and insert the new
                            district.Rows.Remove(dr[0]);
                            district.Rows.InsertAt(newRow, i);
                            var policy = new CacheItemPolicy { SlidingExpiration = new TimeSpan(1, 2, 10) };
                            cache.Add("T105", district, policy);
                        }
                    }
                    else
                    {
                        district = cache["T105"] as DataTable;
                    }

                    return district;

                case "31":
                    //Craete Date    //16/12/2021  District  Prority Wise


                    //1800    JAIPUR(CITY)
                    //3500    SIKAR
                    //200 ALWAR
                    //3000    NAGAUR
                    //1700    HANUMANGARH


                    if (cache["T31"] == null)
                    {

                        Dictionary<string, string> districtList = new Dictionary<string, string>();
                        districtList.Add("BIKANER", "18");
                        districtList.Add("SIKAR", "35");
                        districtList.Add("ALWAR", "02");
                        districtList.Add("NAGAUR", "30");
                        districtList.Add("HANUMANGARH", "17");

                        for (int i = 0; i < districtList.Count; i++)
                        {
                            string tCode = districtList.ElementAt(i).Value;

                            //DataRow[] dr = district.Select("TreasuryCode ='" + tCode + "'");

                            var filterArray = districtList.ElementAt(i).Value.Split(',').Select(s => s.Trim());
                            DataRow[] dr = dt.AsEnumerable()
                                .Where(row => filterArray.All(f => row.Field<string>("TreasuryCode")
                                                               .Split(',')
                                                               .Any(v => v.Trim() == f)))
                                .ToArray();
                            DataRow newRow = district.NewRow();
                            // We "clone" the row
                            newRow.ItemArray = dr[0].ItemArray;
                            // We remove the old and insert the new
                            district.Rows.Remove(dr[0]);
                            district.Rows.InsertAt(newRow, i);
                            var policy = new CacheItemPolicy { SlidingExpiration = new TimeSpan(1, 2, 10) };
                            cache.Add("T31", district, policy);
                        }
                    }
                    else
                    {
                        district = cache["T31"] as DataTable;
                    }

                    return district;
                case "88":
                    //Craete Date    //16/12/2021  District  Prority Wise


                    //                    1800 JAIPUR(CITY)
                    //2600    JODHPUR(CITY)
                    //800 BHILWARA
                    //3500    SIKAR
                    //3000    NAGAUR


                    if (cache["T88"] == null)
                    {

                        Dictionary<string, string> districtList = new Dictionary<string, string>();
                        districtList.Add("BIKANER", "09");
                        districtList.Add("JODHPUR(CITY)", "26");
                        districtList.Add("BHILWARA", "08");
                        districtList.Add("SIKAR", "35");
                        districtList.Add("NAGAUR", "30");

                        for (int i = 0; i < districtList.Count; i++)
                        {
                            string tCode = districtList.ElementAt(i).Value;

                            //DataRow[] dr = district.Select("TreasuryCode ='" + tCode + "'");

                            var filterArray = districtList.ElementAt(i).Value.Split(',').Select(s => s.Trim());
                            DataRow[] dr = dt.AsEnumerable()
                                .Where(row => filterArray.All(f => row.Field<string>("TreasuryCode")
                                                               .Split(',')
                                                               .Any(v => v.Trim() == f)))
                                .ToArray();


                            DataRow newRow = district.NewRow();
                            // We "clone" the row
                            newRow.ItemArray = dr[0].ItemArray;
                            // We remove the old and insert the new
                            district.Rows.Remove(dr[0]);
                            district.Rows.InsertAt(newRow, i);
                            var policy = new CacheItemPolicy { SlidingExpiration = new TimeSpan(1, 2, 10) };
                            cache.Add("T88", district, policy);
                        }
                    }
                    else
                    {
                        district = cache["T88"] as DataTable;
                    }

                    return district;

                case "64":
                    //Craete Date    //16/12/2021  District  Prority Wise


                //                    1800 JAIPUR(CITY)
                //2600    JODHPUR(CITY)
                //900 BIKANER
                //100 TREASURY OFFICE, AJMER
                //2900    KOTA

                    if (cache["T64"] == null)
                    {

                        Dictionary<string, string> districtList = new Dictionary<string, string>();
                       // districtList.Add("JAIPUR", "18");
                        districtList.Add("Barmer", "05");
                        districtList.Add("AJMER", "01");
                        districtList.Add("KOTA", "29");
                     

                        for (int i = 0; i < districtList.Count; i++)
                        {
                            string tCode = districtList.ElementAt(i).Value;

                            // DataRow[] dr = district.Select("TreasuryCode ='" + tCode + "'");
                            var filterArray = districtList.ElementAt(i).Value.Split(',').Select(s => s.Trim());
                            DataRow[] dr = dt.AsEnumerable()
                                .Where(row => filterArray.All(f => row.Field<string>("TreasuryCode")
                                                               .Split(',')
                                                               .Any(v => v.Trim() == f)))
                                .ToArray();
                            DataRow newRow = district.NewRow();
                            // We "clone" the row
                            newRow.ItemArray = dr[0].ItemArray;
                            // We remove the old and insert the new
                            district.Rows.Remove(dr[0]);
                            district.Rows.InsertAt(newRow, i);
                            var policy = new CacheItemPolicy { SlidingExpiration = new TimeSpan(1, 2, 10) };
                            cache.Add("T64", district, policy);
                        }
                    }
                    else
                    {
                        district = cache["T64"] as DataTable;
                    }

                    return district;

                case "60":


                        //                    800 BHILWARA
                        //2100    JAIPUR(SECTT.)
                        //3800    Udaipur City
                        //3300    RAJSAMAND
                        //2600    JODHPUR(CITY)


                    if (cache["T60"] == null)
                    {

                        Dictionary<string, string> districtList = new Dictionary<string, string>();
                        districtList.Add("BHILWARA", "08");
                        districtList.Add("JAIPUR(SECTT.)", "21");
                        districtList.Add("Udaipur", "38");
                        districtList.Add("RAJSAMAND", "33");
                        districtList.Add("JODHPUR", "26");

                        for (int i = 0; i < districtList.Count; i++)
                        {
                            string tCode = districtList.ElementAt(i).Value;

                            //DataRow[] dr = district.Select("TreasuryCode ='" + tCode + "'");

                            var filterArray = districtList.ElementAt(i).Value.Split(',').Select(s => s.Trim());
                            DataRow[] dr = dt.AsEnumerable()
                                .Where(row => filterArray.All(f => row.Field<string>("TreasuryCode")
                                                               .Split(',')
                                                               .Any(v => v.Trim() == f)))
                                .ToArray();
                            DataRow newRow = district.NewRow();
                            // We "clone" the row
                            newRow.ItemArray = dr[0].ItemArray;
                            // We remove the old and insert the new
                            district.Rows.Remove(dr[0]);
                            district.Rows.InsertAt(newRow, i);
                            var policy = new CacheItemPolicy { SlidingExpiration = new TimeSpan(1, 2, 10) };
                            cache.Add("T60", district, policy);
                        }
                    }
                    else
                    {
                        district = cache["T60"] as DataTable;
                    }
            

                    return district;
             
                default:
                   return district;
        }

        }


    }
        
}
