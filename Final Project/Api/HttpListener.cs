using System;
using System.Net;
using System.Threading.Tasks;
public  class HttpListenerServer
{
    public  void CreateConnection()
    {
        using var listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:7072/");

        listener.Start();

        Console.WriteLine("http://localhost:7071/");

        while (true)
        {
            HttpListenerContext ctx = listener.GetContext();
            HttpListenerRequest request = ctx.Request;
            using HttpListenerResponse resp = ctx.Response;
            System.IO.Stream body = request.InputStream;
            System.Text.Encoding encoding = request.ContentEncoding;
            System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
            if (request.ContentType != null)
            {
                Console.WriteLine("Client data content type {0}", request.ContentType);
            }
            Console.WriteLine("Client data content length {0}", request.ContentLength64);

            Console.WriteLine("Start of client data:");
            // Convert the data to a string and display it on the console.
            string s = reader.ReadToEnd();
            Console.WriteLine(s);
            Console.WriteLine("End of client data:");
            body.Close();
            reader.Close();


            resp.StatusCode = (int)HttpStatusCode.OK;
            resp.StatusDescription = "Status OK";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes("hello");
            // Get a response stream and write the response to it.
            resp.ContentLength64 = buffer.Length;
            System.IO.Stream output = resp.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

    }



}
