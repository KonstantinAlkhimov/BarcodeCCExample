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
    public class BarcodeController : ControllerBase
    {
        [HttpGet]
        public FileStreamResult Generate()
        {
            string jwt = Utils.GetJwt(Request);
            string basePath = "https://api-qa.aspose.cloud";
            //string basePath = "https://api-qa.aspose.cloud/v3.0";
            var conf = new Configuration();
            conf.ApiBaseUrl = basePath;
            conf.JwtToken = jwt;
            BarCodeApi api = new BarCodeApi(conf);

            using (Stream response = api.BarCodeGetBarCodeGenerate(new BarCodeGetBarCodeGenerateRequest("Sample text", "Code128", "png")))
            {
                using (FileStream stream = System.IO.File.Create("barcode.png"))
                {
                    response.CopyTo(stream);
                    return new FileStreamResult(stream, "application/octet-stream");
                }
            }
        }
    }
}