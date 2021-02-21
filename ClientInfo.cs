using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
namespace DNWS
{
  class ClientInfo : IPlugin
  {
    protected static Dictionary<String, int> statDictionary = null;
    public ClientInfo()
    {
      if (statDictionary == null)
      {
        statDictionary = new Dictionary<String, int>();

      }
    }

    public void PreProcessing(HTTPRequest request)
    {
      if (statDictionary.ContainsKey(request.Url))
      {
        statDictionary[request.Url] = (int)statDictionary[request.Url] + 1;
      }
      else
      {
        statDictionary[request.Url] = 1;
      }
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
      string[] client = request.getPropertyByKey("RemoteEndPoint").Split(":");
      sb.Append("<html><body>");
      sb.Append("Client IP: "+client[0].ToString() + "<br /><br />");
      sb.Append("Client Port: "+client[1].ToString() + "<br /><br />");
      sb.Append("Browser Information: "+request.getPropertyByKey("user-agent").ToString()+"<br /><br />");
      sb.Append("Acept Language: "+request.getPropertyByKey("accept-language").ToString()+"<br /><br />");
      sb.Append("Acept Encoding: "+request.getPropertyByKey("accept-encoding").ToString()+"<br /><br />");
      sb.Append("</body></html>");
      response = new HTTPResponse(200);
      response.body = Encoding.UTF8.GetBytes(sb.ToString());
      return response;
    }

    public HTTPResponse PostProcessing(HTTPResponse response)
    {
      throw new NotImplementedException();
    }
  }
}