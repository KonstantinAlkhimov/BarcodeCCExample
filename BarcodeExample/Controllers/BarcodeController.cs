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
        public async Task<IActionResult> Generate()
        {
            string jwt = Utils.GetJwt(Request);
            string basePath = "https://api-qa.aspose.cloud";
            //string basePath = "https://api-qa.aspose.cloud/v3.0";
            var conf = new Configuration();
            conf.ApiBaseUrl = basePath;
            conf.JwtToken = jwt;
            conf.ApiVersion = ApiVersion.V3;
            conf.AuthType = AuthType.ExternalAuth;
            BarCodeApi api = new BarCodeApi(conf);

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