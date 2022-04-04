using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Proxy;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Members.Controllers;


[ApiController]
[Route("api/dashboard/")]
[AllowAnonymous]
public class DashboardController : Controller
{

  [HttpGet]
  [Route("route/{*route}")]
  public async Task<IActionResult> Route(string? route = "")
  {
    var original = HttpContext.Request;

    var newRequest = new HttpRequestMessage(HttpMethod.Get,
        $"https://localhost:5601/app/dashboards#/view/{route}");

    newRequest.Content = original.ContentType;
    newRequest.Method = original.HttpMethod;
    newRequest.UserAgent = original.UserAgent;

    byte[] originalStream = ReadToByteArray(original.InputStream, 1024);

    Stream reqStream = newRequest.GetRequestStream();
    reqStream.Write(originalStream, 0, originalStream.Length);
    reqStream.Close();


    return newRequest.GetResponse();
  }

  private HttpClient Http { get; } = new HttpClient();
}
