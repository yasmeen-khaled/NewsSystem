using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.DAL.Helpers
{
    public static class ImageHelpers
    {
        public static string ByteArrayToString(byte[] bytes)
        {
            string url = "";
            if (bytes != null && bytes.Length > 0)
            {
                string img = Convert.ToBase64String(bytes, 0, bytes.Length);
                url = "data:image/jpeg;base64," + img;
            }
            if (bytes == null)
            {
                url = "~/Images/RandomImage.png";
            }
            return url;
        }

        public static byte[]? ImageToByteArray(IFormFile image)
        {
            byte[]? byteArr = null;
            using (var memoryStream = new MemoryStream())
            {
                if (image != null)
                {
                    image.CopyTo(memoryStream);

                    if (memoryStream.Length < 2097152)//less than 2 MB
                    {
                        byteArr = memoryStream.ToArray();
                    }
                    else
                    {
                        throw new Exception("File is too large");
                    }
                }
                return byteArr;
            }
        }
    }
}
