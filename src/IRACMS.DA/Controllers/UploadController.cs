using Share.CMS.Business;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Hosting;
using System.Web.Http;
using System.Xml;

namespace IRACMS.DA.Controllers
{
    public class UploadController : ApiController
    {
        [HttpGet]
        public string Del(string id)
        {
            string path = HostingEnvironment.MapPath(string.Format("~/Public/parts/")),
                f = Path.Combine(path, id),
                fThumb = string.Format("{0}_thumb\\{1}", path, id);

            try
            {
                if (File.Exists(f))
                {
                    File.Delete(f); // Delete main image
                }

                if (File.Exists(fThumb))
                {
                    File.Delete(fThumb); // Delete thumb image
                }
            }
            catch { }


            // delete from db
            string[] names = { "ID" }, values = { id };
            var deleted = new Save().SaveRow("Images_Delete", names, values);

            return deleted.ToString();
        }

        [ActionName("Main")]
        public string GetMainImage(string id)
        {
            // delete from db
            string[] names = { "ID" }, values = { id };
            var result = new Save().SaveRow("Images_Main", names, values);
            return result.ToString();
        }

        // upload image to server.
        public void Post([FromBody]uploadModel value)
        {
            // add this picture to list to save into DB.
            var xmldoc = new XmlDocument();
            XmlElement doc = xmldoc.CreateElement("doc");
            
            for (int i = 0; i < value.Name.Length; i++)
            {
                var media = value.Name[i];
                if (string.IsNullOrEmpty(media))
                    break;
                byte[] imageBytes = Convert.FromBase64String(media);
                var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                
                string newFile = string.Format("{0}.jpg", Guid.NewGuid()),
                       path = HostingEnvironment.MapPath("~/Public/parts/"),
                       filePath = Path.Combine(path, newFile);

                // xml document that will has all picture to save to DB.
                XmlElement xmlelement = xmldoc.CreateElement("Pictures");
                xmlelement.SetAttribute("URL", newFile);
                xmlelement.SetAttribute("ID", value.ID);
                xmlelement.SetAttribute("Index", string.Format("{0}", i + 1));
                doc.AppendChild(xmlelement);

                try
                {
                    // Convert byte[] to Image
                    ms.Write(imageBytes, 0, imageBytes.Length);
                    var image = Image.FromStream(ms, true);

                    // save a full image
                    CreateFolderIfNotExist(path);
                    image.Save(filePath, ImageFormat.Jpeg);

                    // Save Thumb image //////////////////////////////////                   
                    // prepaire thumb folder
                    string pPath = Path.Combine(path, "_thumb\\");
                    CreateFolderIfNotExist(pPath);

                    // save image in thumb folder
                    // Set image height and width to be loaded on web page
                    byte[] buffer = getResizedImage(filePath, 150, 150);
                    File.WriteAllBytes(pPath + newFile, buffer);
                    // end ///////////////////////////////////////////////
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            } // end for

            xmldoc.AppendChild(doc);
            // start save all into db.
            SaveDB(xmldoc.OuterXml);
        }

        private void SaveDB(string xml)
        {
            string[] names = { "doc" }, values = { xml };
            var saved = new Save().SaveRow("Images_Save", names, values);
        }

        private void CreateFolderIfNotExist(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        byte[] getResizedImage(String path, int width, int height)
        {
            var imgIn = new Bitmap(path);
            double y = imgIn.Height;
            double x = imgIn.Width;

            double factor = 1;
            if (width > 0)
            {
                factor = width / x;
            }
            else if (height > 0)
            {
                factor = height / y;
            }

            var outStream = new MemoryStream();
            var imgOut = new Bitmap((int)(x * factor), (int)(y * factor));

            // Set DPI of image (xDpi, yDpi)
            imgOut.SetResolution(96, 96); //72, 72);

            Graphics g = Graphics.FromImage(imgOut);
            g.Clear(Color.White);
            g.DrawImage(imgIn, new Rectangle(0, 0, (int)(factor * x), (int)(factor * y)),
              new Rectangle(0, 0, (int)x, (int)y), GraphicsUnit.Pixel);

            imgOut.Save(outStream, getImageFormat(path));
            outStream.Flush();
            outStream.Close();
            outStream.Dispose();
            return outStream.ToArray();
        }

        ImageFormat getImageFormat(String path)
        {
            switch (Path.GetExtension(path))
            {
                case ".bmp": return ImageFormat.Bmp;
                case ".gif": return ImageFormat.Gif;
                case ".jpg":
                case ".jpeg": return ImageFormat.Jpeg;
                case ".png": return ImageFormat.Png;
                default: break;
            }
            return ImageFormat.Jpeg;
        }
    }

    public class uploadModel
    {
        public string[] Name { get; set; }
        public string ID { get; set; } = "0";
    }
}
