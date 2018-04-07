using IRACMS.Web.Host.Models;
using LZStringCSharp;
using System;
using System.Text.RegularExpressions;
using Share.CMS.Business;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net;

namespace IRACMS.Web.Host.Controllers
{
    //[EnableCors("*", "*", "*")]
    [Route("api/[values]")]
    public class ValuesController : Controller
    {
        static readonly Regex trimmer = new Regex(@"/(^\s+|\s+$)/g");

        /// <summary>
        /// GET api/values
        /// </summary>
        public string Get()
        {
            var req = HttpContext.Request;

            string sSearch = String.IsNullOrEmpty(req.Query["sSearch"].ToString()) ? "" : req.Query["sSearch"].ToString();
            string iDisplayStart = String.IsNullOrEmpty(req.Query["iDisplayStart"].ToString()) ? "0" : req.Query["iDisplayStart"].ToString();
            string iDisplayLength = String.IsNullOrEmpty(req.Query["iDisplayLength"].ToString()) ? "0" : req.Query["iDisplayLength"].ToString();
            string sortColumnIndex = String.IsNullOrEmpty(req.Query["iSortCol_0"].ToString()) ? "" : req.Query["iSortCol_0"].ToString();
            string sortDirection = String.IsNullOrEmpty(req.Query["sSortDir_0"].ToString()) ? "" : req.Query["sSortDir_0"].ToString(); // asc or desc

            // create filter parameters
            // grid static parameters
            string[] names = { "DisplayStart", "DisplayLength", "SortColumn", "SortDirection", "SearchParam" },
                     values = { iDisplayStart.ToString(), iDisplayLength.ToString(), sortColumnIndex.ToString(), sortDirection, sSearch },

                     // get dynamic more parameters from user
                     addtionNames = string.IsNullOrEmpty(req.Query["names"].ToString()) ? new string[0] : req.Query["names"].ToString().Split('~'),
                     addtionValues = string.IsNullOrEmpty(req.Query["values"].ToString()) ? new string[0] : req.Query["values"].ToString().Split('~'),

                     // merge all parameters (union)
                     namesAll = names.Concat(addtionNames).ToArray(),
                     valuesAll = values.Concat(addtionValues).ToArray();

            // get all of data.
            var _ds = new Select().SelectLists(req.Query["f"].ToString(), namesAll, valuesAll);
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

        public IActionResult Post([FromBody]SaveDataModel model)
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

                return Ok(data);
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Error!: {0}", ex.Message)),
                    ReasonPhrase = "Saving Error: " + ex.Message
                };

                Response.StatusCode = 500; // error                
                return new ObjectResult(response);
            }
        }
    }
}