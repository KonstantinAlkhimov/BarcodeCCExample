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
        public FileStreamResult Generate()
        {
            BarCodeApi api = new BarCodeApi(AppKey, AppSid);
            Stream response = api.BarCodeGetBarCodeGenerate(new BarCodeGetBarCodeGenerateRequest("Sample text", "Code128", "png"));
            FileStream stream = System.IO.File.Create("barcodeCloud.png");
            response.CopyTo(stream);
            return new FileStreamResult(stream, "application/octet-stream");
        }
    }
}