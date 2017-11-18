using IRACMS.DA.Models;
using LZStringCSharp;
using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Share.CMS.Business;
using System.Linq;
using System.Net.Http;
using System.Net;

namespace IRACMS.DA.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {
        static readonly Regex trimmer = new Regex(@"/(^\s+|\s+$)/g");

        /// <summary>
        /// GET api/values
        /// </summary>
        public string Get()
        {
            HttpContext Context = HttpContext.Current;

            string sSearch = String.IsNullOrEmpty(Context.Request["sSearch"]) ? "" : Context.Request["sSearch"];
            string iDisplayStart = String.IsNullOrEmpty(Context.Request["iDisplayStart"]) ? "0" : Context.Request["iDisplayStart"];
            string iDisplayLength = String.IsNullOrEmpty(Context.Request["iDisplayLength"]) ? "0" : Context.Request["iDisplayLength"];
            string sortColumnIndex = String.IsNullOrEmpty(Context.Request["iSortCol_0"]) ? "" : Context.Request["iSortCol_0"];
            string sortDirection = String.IsNullOrEmpty(Context.Request["sSortDir_0"]) ? "" : Context.Request["sSortDir_0"]; // asc or desc

            // create filter parameters
            // grid static parameters
            string[] names = { "DisplayStart", "DisplayLength", "SortColumn", "SortDirection", "SearchParam" },
                     values = { iDisplayStart.ToString(), iDisplayLength.ToString(), sortColumnIndex.ToString(), sortDirection, sSearch },

                     // get dynamic more parameters from user
                     addtionNames = string.IsNullOrEmpty(Context.Request["names"]) ? new string[0] : Context.Request["names"].Split('~'),
                     addtionValues = string.IsNullOrEmpty(Context.Request["values"]) ? new string[0] : Context.Request["values"].Split('~'),

                     // merge all parameters (union)
                     namesAll = names.Concat(addtionNames).ToArray(),
                     valuesAll = values.Concat(addtionValues).ToArray();

            // get all of data.
            var _ds = new Select().SelectLists(Context.Request["f"], namesAll, valuesAll);
            var pureXMLString = trimmer.Replace(_ds.GetXml(), "");

            var compressedXML = LZString.CompressToUTF16(pureXMLString);
            return trimmer.Replace(compressedXML, "");
        }

        // GET api/values?id=5&name=
        public string GetDataByID(string name, string id)
        {
            // create filter parameters
            string[,] _params = { { "ID", id ?? "0" } };

            // get all of data.
            var _ds = new Select().SelectLists(name, _params);
            var pureXMLString = trimmer.Replace(_ds.GetXml(), "");

            return LZString.CompressToUTF16(pureXMLString);
        }

        public string GetData(string fnName)
        {
            var _ds = new Select().SelectLists(fnName); // get all of data.
            var compressedXML = LZString.CompressToUTF16(_ds.GetXml());
            return compressedXML; //.Replace(" ", "");
        }

        public HttpResponseMessage Post([FromBody]SaveDataModel model)
        {
            try
            {
                var saved = new Save().SaveRow(model.fun, model.names, model.values);

                object data = new { };
                if (saved != -1)
                {
                    data = new { ID = saved, Status = true };
                }
                else
                {
                    data = new { ID = 0, status = false };
                }

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Error!: {0}", ex.Message)),
                    ReasonPhrase = "Saving Error: " + ex.Message
                };
                throw new HttpResponseException(response);
            }
        }
    }
}
