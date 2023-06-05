using ApiLogic.Logic;
using System.Net;
namespace HttpListenerApi
{
    public class Program
    {
        static void Main(string[] args)
        {
            HandleClientRequests();

        }
        public static async void HandleClientRequests()
        {
            using var listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:7071/");

            listener.Start();

            Console.WriteLine("http://localhost:7071/");

            while (true)
            {
                HttpListenerContext ctx = listener.GetContext();
                HttpListenerRequest request = ctx.Request;
                using HttpListenerResponse resp = ctx.Response;

                string HttpMethod = request.HttpMethod;
                string RawUrl = request.RawUrl;
                System.IO.Stream body = request.InputStream;
                System.Text.Encoding encoding = request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string stringbody = reader.ReadToEnd();
                Console.WriteLine(RawUrl);
                Console.WriteLine(HttpMethod);
                if (HttpMethod == "POST" && RawUrl == "/api/user/Mark/login")
                {
                    bool response = await UserLogic.Login(stringbody);
                    if (response)
                    {
                        resp.StatusCode = (int)HttpStatusCode.OK;
                        resp.StatusDescription = "Status OK";
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Welcome");
                        // Get a response stream and write the response to it.
                        resp.ContentLength64 = buffer.Length;
                        System.IO.Stream output = resp.OutputStream;
                        output.Write(buffer, 0, buffer.Length);
                        output.Close();
                    }
                    else
                    {
                        resp.StatusCode = (int)HttpStatusCode.Unauthorized ;
                        resp.StatusDescription = "Unauthorized";
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Incorrect Username or Password");
                        // Get a response stream and write the response to it.
                        resp.ContentLength64 = buffer.Length;
                        System.IO.Stream output = resp.OutputStream;
                        output.Write(buffer, 0, buffer.Length);
                        output.Close();
                    }
                    body.Close();
                    reader.Close();

                }
                
            }
        }
    }
}