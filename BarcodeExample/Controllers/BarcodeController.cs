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
        private string _jwt = 
            "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE1NzE5OTU3MjcsImV4cCI" +
            "6MTU3MjA4MjEyNywiaXNzIjoiaHR0cHM6Ly9hcGkuYXNwb3NlLmNsb3VkIiwiYXVkIjp" +
            "bImh0dHBzOi8vYXBpLmFzcG9zZS5jbG91ZC9yZXNvdXJjZXMiLCJhcGkucGxhdGZvcm0" +
            "iLCJhcGkucHJvZHVjdHMiXSwiY2xpZW50X2lkIjoiMGQxNDY3MDYtMzBhZi00YmJjLWE" +
            "wY2QtOWEyMTc5YTU1ZmExIiwiY2xpZW50X2lkU3J2SWQiOiIiLCJzY29wZSI6WyJhcGk" +
            "ucGxhdGZvcm0iLCJhcGkucHJvZHVjdHMiXX0.pt-qb1Ilscd9Pp5v7djHBlm1f9LvSag" +
            "xONhJCj2NlJI0kkF3HOl-0pWrC-J0sUqVGBa56mJqjgLaTmRAIC2ru4Xr9uQyGEpgyuV" +
            "lUKrRk4ECyb1Z0MIuDLqUFXJp7G6mN5bDqKk6knsf9TpS5h5aRI9HFhGSA6YLTRlw-9F" +
            "IBUj-0lD3vllkBsjtIrbLWBkZmobZAUgHylN9PkfKrkNlrUA-WT5g4mHNFJkJD5wYOrE" +
            "M_f-DnXL99WUz9O1l60vNL_UR8VO4Qdl_wLixxTX1MI9wdMfCos_WayiYcPIIue98K7j" +
            "j-m4z-lWvh0gCw7EzaGQfzOQGtVwvnoLmEHqkoQ";

        private string _jwt_qa =
            "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE1NzE5OTU1NzcsImV4cCI" +
            "6MTU3MjA4MTk3NywiaXNzIjoiaHR0cHM6Ly9hcGktcWEuYXNwb3NlLmNsb3VkIiwiYXV" +
            "kIjpbImh0dHBzOi8vYXBpLXFhLmFzcG9zZS5jbG91ZC9yZXNvdXJjZXMiLCJhcGkucGx" +
            "hdGZvcm0iLCJhcGkucHJvZHVjdHMiXSwiY2xpZW50X2lkIjoiYzFmZDFhMTUtOGZhYi0" +
            "0Y2UyLTk0NzYtMmFiZWJmNTVhZGUzIiwiY2xpZW50X2lkU3J2SWQiOiIiLCJzY29wZSI" +
            "6WyJhcGkucGxhdGZvcm0iLCJhcGkucHJvZHVjdHMiXX0.INqnxKtVBi2Wi8nZX9NTkho" +
            "UFKC_VrAYO0k1ZxCBUp7EUVB7FHE_UO85UjndeW8tU4TaanFv7D19e99Zq56m1DmNOPt" +
            "2qFAwOMmHp7cttK75b_7q2Eab3-roC6jSaPB4Y6sA2i_tjV_wBTm_saqWRgo3B0It764" +
            "4kOUZUUVpzsPa2CVb6nM9_5iL2UmtC079rEHTrJ2XeVLQ3AxDaMyiWcE-2c7jQXmdhil" +
            "XsVpe78Y47VR09Dt3YFr9PG2tut_BSAp4xsYAXibba5HQmt7UO0uBEITzVfVnjxdWR7N" +
            "R1zjwsKAiyuIuABUW7AZcZqvGC8xOv0vtn-t1P3BG1Ia66Q";

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