using System;
using System.Drawing;
using System.IO;
using System.Web.Hosting;

namespace PersonSearch.Services
{
    public class ImageUploader
    {
        /// <summary>
        /// Upload an image from a stream in the designated upload location
        /// </summary>
        /// <param name="stream">Stream of image</param>
        /// <returns>Generated file name (not fullpath)</returns>
        public string Upload(Stream stream)
        {
            var image = Image.FromStream(stream);
            var dirPath = HostingEnvironment.MapPath("~/Content/Images");
            var fileName = $"{Guid.NewGuid()}.jpg";
            var fullPath = Path.Combine(dirPath, fileName);
            image.Save(fullPath);
            return fileName;
        }
    }
}