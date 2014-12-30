using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using DataTablesServerSide.Models;

namespace DataTablesServerSide
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
     public string GetTableData()
     {
         var echo = int.Parse (HttpContext.Current.Request.Params["sEcho"]);
         var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
         var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);
         var sortOrder = HttpContext.Current.Request.Params["sSortDir_0"].ToString(CultureInfo.CurrentCulture);
         var roleId = HttpContext.Current.Request.Params["roleId"].ToString(CultureInfo.CurrentCulture);

         var records = GetRecordsFromDatabaseWithFilter().ToList();
         if (!records.Any())
         {
             return string.Empty;
         }

         var orderedResults = sortOrder == "asc"
                              ? records.OrderBy(o => o.UserId)
                              :  records.OrderByDescending(o => o.UserId);
            var itemsToSkip = displayStart == 0 
                              ? 0 
                              : displayStart + 1;
            var pagedResults = orderedResults.Skip(itemsToSkip).Take(displayLength).ToList();
            var hasMoreRecords = false;

            var sb = new StringBuilder();
            sb.Append(@"{" + "\"sEcho\": " + echo + ",");
            sb.Append("\"recordsTotal\": " + records.Count + ",");
            sb.Append("\"recordsFiltered\": " + records.Count + ",");
            sb.Append("\"iTotalRecords\": " + records.Count + ",");
            sb.Append("\"iTotalDisplayRecords\": " + records.Count + ",");
            sb.Append("\"aaData\": [");
            foreach (var result in pagedResults)
            {
                if (hasMoreRecords)
                {
                    sb.Append(",");
                }

                sb.Append("[");
                sb.Append("\"" + result.UserId + "\",");
                sb.Append("\"" + result.Name + "\",");
                sb.Append("\"" + result.Address + "\",");
                sb.Append("\"" + result.Age + "\",");
                sb.Append("\"<img class='image-details' src='content/details_open.png' runat='server' height='16' width='16' alt='View Details'/>\"");
                sb.Append("]");
                hasMoreRecords = true;
            }
            sb.Append("]}");
            return sb.ToString();
        }

        private static IEnumerable<CustomUser> GetRecordsFromDatabaseWithFilter()
        {
            // At this point you can call to your database to get the data
            // but I will just populate a sample collection in code
            return new List<CustomUser>
            {
                new CustomUser
                {
                    UserId = 1,
                    Address = "1 Newton Square, London",
                    Age=25,
                    Name="John Smith"
                },
                new CustomUser
                {
                    UserId = 2,
                    Address = "5 George Road, Manchester",
                    Age= 31,
                    Name = "Erica Keir"
                },
                new CustomUser
                {
                    UserId = 3,
                    Address = "32 Queen Mary St, Newcastle",
                    Age = 12,
                    Name = "Test McDermont"
                }
            };
        }
    }
}
