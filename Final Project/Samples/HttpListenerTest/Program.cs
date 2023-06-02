using System.Net;

using var listener = new HttpListener();
listener.Prefixes.Add("http://localhost:7071/api/user/");

listener.Start();

Console.WriteLine("http://localhost:7071/api/user");

while (true)
{
    HttpListenerContext ctx = listener.GetContext();
    HttpListenerRequest request = ctx.Request;
    using HttpListenerResponse resp = ctx.Response;
    Console.WriteLine(request.RawUrl);

    resp.StatusCode = (int)HttpStatusCode.OK;
    resp.StatusDescription = "Status OK";
}
