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
    public class BarcodeCloudController : ControllerBase
    {
        private string AppSid = "0d146706-30af-4bbc-a0cd-9a2179a55fa1";
        private string AppKey = "ee155a541298b173c93d26497c70c0f8";

        [HttpGet]
        public async Task<IActionResult> Generate()
        {
            BarCodeApi api = new BarCodeApi(AppKey, AppSid);

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