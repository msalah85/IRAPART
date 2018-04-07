using LZStringCSharp;
using Share.CMS.Business;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IRACMS.DA.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Select2Controller : ApiController
    {
        static readonly Regex trimmer = new Regex(@"/(^\s+|\s+$)/g");

        public string Get()
        {
            HttpContext Context = HttpContext.Current;

            string fnName = String.IsNullOrEmpty(Context.Request["fnName"]) ? "" : Context.Request["fnName"],
                searchTerm = String.IsNullOrEmpty(Context.Request["searchTerm"]) ? "" : Context.Request["searchTerm"],
                pageNum = String.IsNullOrEmpty(Context.Request["pageNum"]) ? "10" : Context.Request["pageNum"],
                pageSize = String.IsNullOrEmpty(Context.Request["pageSize"]) ? "0" : Context.Request["pageSize"],
                names = String.IsNullOrEmpty(Context.Request["names"]) ? "" : Context.Request["names"],
                values = String.IsNullOrEmpty(Context.Request["values"]) ? "" : Context.Request["values"]; // asc or desc

            // grid static parameters
            string[] defaultNames = { "pageNum", "pageSize", "key" },
                     defaultValues = { pageNum, pageSize, searchTerm },

            // get dynamic more parameters from user
            addtionNames = string.IsNullOrEmpty(names) ? new string[0] : names.Split('~'),
            addtionValues = string.IsNullOrEmpty(values) ? new string[0] : values.Split('~'),

            // merge all parameters (union)
            namesAll = defaultNames.Concat(addtionNames).ToArray(),
            valuesAll = defaultValues.Concat(addtionValues).ToArray();

            var _ds = new Select().SelectLists(fnName, namesAll, valuesAll);
            var pureXMLString = trimmer.Replace(_ds.GetXml(), "");

            var compressedXML = LZString.CompressToUTF16(pureXMLString);
            return trimmer.Replace(compressedXML, "");
        }
    }
}
