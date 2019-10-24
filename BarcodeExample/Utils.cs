using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
namespace BarcodeExample
{
    internal class Utils
    {
        internal static string GetJwt(HttpRequest rq)
        {
            string token = rq.Headers["Authorization"];
            string bearerToken = string.Empty;
            Regex regexp = new Regex(@"Bearer\s+(?<token>\S+)", RegexOptions.IgnoreCase);
            Match match = regexp.Match(token);
            if (match.Success)
                bearerToken = match.Groups["token"].Value;
            return bearerToken;
        }
    }
}
