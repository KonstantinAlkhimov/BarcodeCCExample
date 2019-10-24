using System;
using System.Collections.Generic;
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
            string tmp = Path.GetTempFileName();
            using (Stream response = api.BarCodeGetBarCodeGenerate(new BarCodeGetBarCodeGenerateRequest("Sample text", "Code128", "jpg")))
            using (FileStream stream = System.IO.File.Create(tmp))
            {
                response.CopyTo(stream);
            }

            FileStream res = new FileStream(tmp, FileMode.Open);
            return File(res, "application/octet-stream", "barcode.jpg");
        }
    }
}