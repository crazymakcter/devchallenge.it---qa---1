using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace devchallenge.it___qa___1
{
  public static class RequestExecutorOnServer
  {
    public static string Get = "GET";
    public static string Post = "POST";
    public static string Head = "HEAD";
    public static string Put = "PUT";
    public static string Delete = "DELETE";

    public enum Method
    {
      Get,
      Post,
      Head,
      Put,
      Delete
    };

    public static string MainUrl = "http://petstore.swagger.io/v2";
    public static string PetUrl = "http://petstore.swagger.io/v2/pet";


    public static string RequestExecutor(string Method, string url, string json = null)
    {
      var httpWebRequest = (HttpWebRequest)WebRequest.Create($"{url}");
      httpWebRequest.ContentType = "application/json";
      httpWebRequest.Method = Method;
      //httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.167 Safari/537.36";
      //httpWebRequest.Accept = "text/plain, */*; q=0.01";
      //httpWebRequest.Headers["Accept-Language"] = "en-US,en;q=0.5";
      //httpWebRequest.Headers["X-Requested-With"] = "XMLHttpRequest";
      //httpWebRequest.Headers["api_key"] = "";
      httpWebRequest.Referer = url;
      if (json != null)
      {
        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
          streamWriter.Write(json);
          streamWriter.Flush();
          streamWriter.Close();
        }
      }
      try
      {
        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var reader = new StreamReader(httpResponse.GetResponseStream()))
        {
          return reader.ReadToEnd();
        }
      }
      catch (WebException e)
      {
        using (WebResponse response = e.Response)
        {
          HttpWebResponse httpResponse = (HttpWebResponse)response;
          Console.WriteLine($"Error code: {httpResponse.StatusCode}");
          using (Stream data = response.GetResponseStream())
          using (var reader = new StreamReader(data))
          {
            return reader.ReadToEnd();
          }
        }
      }
    }
  }
}
