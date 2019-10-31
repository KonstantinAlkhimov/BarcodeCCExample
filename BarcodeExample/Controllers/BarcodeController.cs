using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Aspose.BarCode.Cloud.Sdk;
using Aspose.BarCode.Cloud.Sdk.Model.Requests;

namespace BarcodeExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarcodeController : ControllerBase
    {
        private string _jwt_qa =
            "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE1NzI0OTQ5NzcsImV4cCI6MTU3MjU4MT" +
            "M3NywiaXNzIjoiaHR0cHM6Ly9hcGktcWEuYXNwb3NlLmNsb3VkIiwiYXVkIjpbImh0dHBzOi8vYXBpL" +
            "XFhLmFzcG9zZS5jbG91ZC9yZXNvdXJjZXMiLCJhcGkucGxhdGZvcm0iLCJhcGkucHJvZHVjdHMiXSwi" +
            "Y2xpZW50X2lkIjoiYzFmZDFhMTUtOGZhYi00Y2UyLTk0NzYtMmFiZWJmNTVhZGUzIiwiY2xpZW50X2l" +
            "kU3J2SWQiOiIiLCJzY29wZSI6WyJhcGkucGxhdGZvcm0iLCJhcGkucHJvZHVjdHMiXX0.TJFw-Lgp0J" +
            "CSFr_Vg_xC1qQIVmOloa_R5jqVf8VHiOWtSjCz7zC0lSwBlAKN8B-3vgHEW2yS-Z6YAmHMoiS55Rcho" +
            "wKf5_73PATY3Gl8N6TQw6SxL3mTZI69iRN5PuziGnbdX5MFconCs-K4gLC62jV0t2gJrq9qKYwC70-W" +
            "3NonSplFjb6M-vqq_Z_fYWZ8R8jnxWr5Wey3w0wWNISDlERL00VX3ebZKVdjZHHQSauXp-RrqqQYzPw" +
            "tbFs7zhxuilqIiZGGsQfsgPxkoQ99IH6QZEiWvVd5IPFtotZAFp__L9Dyj0nVzGHu9JcjbK-9IR1BIQ" +
            "YvlkVO4jdvCLu1BQ";

        [HttpGet]
        public IActionResult Generate()
        {
            string jwt = Utils.GetJwt(Request);

            string baseUrl = "https://api-qa.aspose.cloud";//v3.0";
            var conf = new Configuration();
            conf.ApiBaseUrl = baseUrl;
            conf.JwtToken = jwt;
            conf.ApiVersion = ApiVersion.V3;
            conf.AuthType = AuthType.ExternalAuth;
            BarCodeApi api = new BarCodeApi(conf);

            var srcBmp = new System.Drawing.Bitmap("Card.png");
            string tmp = Path.GetTempFileName();
            using (Stream response =
                api.BarCodeGetBarCodeGenerate(
                    new BarCodeGetBarCodeGenerateRequest("Hello from conholdate.cloud!", "QR", "png")))
            {
                var qr = new Bitmap(response);
                var bmp = new Bitmap(srcBmp.Width, srcBmp.Height);
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.DrawImage(srcBmp, 0, 0, srcBmp.Width, srcBmp.Height);
                    graphics.DrawImage(qr, srcBmp.Width - qr.Width - 10, 10);
                }

                using (FileStream stream = System.IO.File.Create(tmp))
                {
                    bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

                }
            }

            FileStream res = new FileStream(tmp, FileMode.Open);
            return File(res, "application/octet-stream", "CardWithQr.png");
        }
    }
}