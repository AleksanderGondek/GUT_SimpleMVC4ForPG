using System;
using System.Web;

namespace SimpleMVC4.Helpers
{
    public static class FilesControllerHelper
    {
        public static byte[] ConvertUploadedFileToBytes(HttpPostedFileBase file)
        {
            try
            {
                var fileBytes = new byte[file.InputStream.Length];
                file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.InputStream.Length));

                return fileBytes;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}